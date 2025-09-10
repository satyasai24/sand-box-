param([string]$AppToLaunch)

# --- Enable Auditing ---
# 1. Enable the "Audit Process Creation" policy
auditpol /set /subcategory:"Process Creation" /success:enable

# 2. Set the registry key to include command lines in the audit events
$regPath = "HKLM:\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\Audit"
if (-not (Test-Path $regPath)) {
    New-Item -Path $regPath -Force | Out-Null
}
Set-ItemProperty -Path $regPath -Name "ProcessCreationIncludeCmdLine_Enabled" -Value 1 -Type DWord -Force

# --- Launch the main application ---
if ($AppToLaunch -and (Test-Path $AppToLaunch)) {
    try
    {
        # Use -PassThru to get the process object, although we are not using it here.
        # It's good practice in case we want to wait for the process to exit in the future.
        Start-Process -FilePath $AppToLaunch -PassThru
    }
    catch
    {
        # Log the error to the shared logs folder
        "Error launching application '($AppToLaunch)': $_" | Add-Content -Path "C:\\Logs\\launch_errors.log"
    }
}
else
{
    "Error: Application path '($AppToLaunch)' not found or not provided." | Add-Content -Path "C:\\Logs\\launch_errors.log"
}
