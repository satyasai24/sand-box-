# Sandbox Manager

Sandbox Manager is a Windows desktop application that allows you to run applications in a secure, isolated sandbox environment using the Windows Sandbox feature. It is designed to protect the host system from potentially malicious applications and websites.

## Architecture

The solution consists of the following components:

*   **SandboxManager.exe**: A C# Windows Forms application that provides the main user interface for managing and launching sandboxed applications.
*   **.wsb Files**: The application dynamically generates Windows Sandbox configuration files (`.wsb`) to configure the sandbox environment for each application. These files define mapped folders, logon commands, and other sandbox settings.
*   **LoggingService**: A static class that provides logging functionality. It logs events to a file in `%APPDATA%\SandboxManager\Logs`. This log folder is also mapped into the sandbox to allow sandboxed applications to write to the logs.
*   **WiX Installer Script**: A `SandboxManager.wxs` file is provided to build an MSI installer for the application using the WiX Toolset.

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

### Building the Application

You can build the C# project using the `dotnet build` command or by opening the solution in Visual Studio.

```bash
# Navigate to the project directory
cd src/SandboxManager

# Build the project
dotnet build --configuration Release
```

The output will be in `src/SandboxManager/bin/Release/net8.0-windows/`.

### Building the Installer

The WiX installer can be built from the command line using the `candle.exe` (compiler) and `light.exe` (linker) tools from the WiX Toolset.

1.  **Compile the WiX source file**:
    ```bash
    candle.exe -dSandboxManager.TargetPath=src/SandboxManager/bin/Release/net8.0-windows/SandboxManager.exe src/SandboxManager/SandboxManager.wxs
    ```

2.  **Link the installer**:
    ```bash
    light.exe -out SandboxManager.msi SandboxManager.wixobj
    ```

This will produce `SandboxManager.msi`, which can be used to install the application.
