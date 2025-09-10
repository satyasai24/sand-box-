# Sandbox Manager

Sandbox Manager is a Windows desktop application that allows you to run applications in a secure, isolated sandbox environment using the Windows Sandbox feature. It is designed to protect the host system from potentially malicious applications and websites.

## Architecture

The solution consists of the following components:

*   **SandboxManager.exe**: A C# Windows Forms application that provides the main user interface.
*   **ConfigurationService**: A static class that handles loading and saving the application list to `applications.json` in the user's AppData folder.
*   **.wsb Files**: The application dynamically generates Windows Sandbox configuration files (`.wsb`) to configure the sandbox environment.
*   **boot.ps1**: A PowerShell script that runs on sandbox startup to enable enhanced security auditing.
*   **LoggingService**: A static class that provides logging for the host application's events.
*   **WiX Installer Script**: A `SandboxManager.wxs` file is provided to build an MSI installer for the application.
*   **Unit Test Project**: A separate project containing unit tests for the core application logic.

## Features

### Application Management
*   **Add, Edit, and Remove** applications from the launch list.
*   **Configuration Persistence**: The list of applications is saved and reloaded between sessions.

### Security
*   **Sandbox Isolation**: Each application runs in a disposable Windows Sandbox, isolating it from the host system.
*   **Enhanced Security Auditing**: On sandbox startup, a script runs to enable process creation auditing (Event ID 4688), including full command-line arguments. This provides a detailed audit trail of all activity within the sandbox, which can be viewed in the sandbox's Windows Event Viewer (under Security logs).
*   **Host-side Logging**: The main application logs its own events (like launching an app) to a file in `%APPDATA%\SandboxManager\Logs`, which is also visible in the UI.

## Threat Model

Sandbox Manager is designed to mitigate the following threats:

*   **Malware**: By running applications in an isolated sandbox, malware that infects the sandboxed application cannot escape to the host system.
*   **Phishing and Malicious Websites**: If a user visits a malicious website in a sandboxed browser, any downloaded malware or scripts will be contained within the sandbox.
*   **Data Leakage**: The sandbox can be configured to restrict access to the host file system, preventing data from being exfiltrated from the host.
*   **Ransomware**: If ransomware is executed within the sandbox, it will only be able to encrypt files within the sandbox, leaving the host system unaffected.

The sandbox is disposable. After each session, the sandbox can be destroyed, and a new, clean one can be created.

## Usage

1.  **Launch the Application**: Run `SandboxManager.exe`.
2.  **Select an Application**: Choose an application from the dropdown list.
3.  **Launch the Sandbox**: Click the "Launch" button to start the application in a new sandbox.
4.  **Manage Applications**: Use the "Add", "Edit", and "Remove" buttons to manage the list of available applications.
5.  **View Logs**: The main window displays a real-time view of the event log. You can also click the "View Logs" button to open the full log file in a text editor.

## Building from Source

To build the application and the installer from source, you will need the following prerequisites:

*   **.NET 8 SDK** (or newer)
*   **WiX Toolset v3** (or newer)

### Creating the Solution File (Optional)

If you want to open the project in Visual Studio, you will need to create a solution file (`.sln`). You can do this from the command line:

```bash
# Create a new solution file
dotnet new sln -n SandboxManager

# Add the main project to the solution
dotnet sln add src/SandboxManager/SandboxManager.csproj

# Add the test project to the solution
dotnet sln add tests/SandboxManager.Tests/SandboxManager.Tests.csproj
```

### Building the Application

You can build the C# project using the `dotnet build` command. If you have created a solution file, you can build the entire solution at once.

```bash
# Navigate to the project directory
cd src/SandboxManager

# Build the project
dotnet build --configuration Release
```

The output will be in `src/SandboxManager/bin/Release/net8.0-windows/`. Make sure to copy the `scripts` folder from the project root to this output directory so the application can find it.

## Testing

The solution includes a unit test project. You can run the tests from the command line:

```bash
# Navigate to the test project directory
cd tests/SandboxManager.Tests

# Run the tests
dotnet test
```

## Building the Installer

The WiX installer can be built from the command line using the `candle.exe` (compiler) and `light.exe` (linker) tools from the WiX Toolset. The installer is configured to bundle the application executable and the `scripts` folder.

1.  **Compile the WiX source file**:
    ```bash
    candle.exe -dSandboxManager.TargetPath=src/SandboxManager/bin/Release/net8.0-windows/SandboxManager.exe src/SandboxManager/SandboxManager.wxs
    ```

2.  **Link the installer**:
    ```bash
    light.exe -out SandboxManager.msi SandboxManager.wixobj
    ```

This will produce `SandboxManager.msi`, which can be used to install the application.
