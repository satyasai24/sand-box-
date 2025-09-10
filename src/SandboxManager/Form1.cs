using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SandboxManager
{
    public partial class Form1 : Form
    {
        private List<SandboxedApplication> applications;
        private FileSystemWatcher logWatcher;

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            applications = ConfigurationService.LoadApplications();

            appComboBox.DataSource = applications;
            appComboBox.DisplayMember = "Name";

            SetupLogWatcher();
            UpdateLogView();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigurationService.SaveApplications(applications);
        }

        private void launchButton_Click(object sender, EventArgs e)
        {
            if (appComboBox.SelectedItem is SandboxedApplication selectedApp)
            {
                LoggingService.Log($"Launching application: {selectedApp.Name}");
                var config = selectedApp.GetConfiguration();
                var launcher = new SandboxLauncher();
                try
                {
                    launcher.Launch(config);
                }
                catch (Exception ex)
                {
                    LoggingService.Log($"Error launching sandbox: {ex.Message}");
                    MessageBox.Show($"Could not launch the sandbox. Please ensure the Windows Sandbox feature is enabled on your system.\n\nError: {ex.Message}", "Sandbox Launch Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void viewLogsButton_Click(object sender, EventArgs e)
        {
            try
            {
                var process = new System.Diagnostics.Process
                {
                    StartInfo = new System.Diagnostics.ProcessStartInfo(LoggingService.GetLogFilePath())
                    {
                        UseShellExecute = true
                    }
                };
                process.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open log file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            using (var form = new AppEditorForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    applications.Add(form.Application);
                    RefreshAppList();
                }
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (appComboBox.SelectedItem is SandboxedApplication selectedApp)
            {
                using (var form = new AppEditorForm(selectedApp))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        RefreshAppList();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an application to edit.", "No Application Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (appComboBox.SelectedItem is SandboxedApplication selectedApp)
            {
                if (MessageBox.Show($"Are you sure you want to remove {selectedApp.Name}?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    applications.Remove(selectedApp);
                    RefreshAppList();
                }
            }
            else
            {
                MessageBox.Show("Please select an application to remove.", "No Application Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RefreshAppList()
        {
            appComboBox.DataSource = null;
            appComboBox.DataSource = applications;
            appComboBox.DisplayMember = "Name";
        }

        private void SetupLogWatcher()
        {
            logWatcher = new FileSystemWatcher
            {
                Path = LoggingService.GetLogDirectory(),
                Filter = Path.GetFileName(LoggingService.GetLogFilePath()),
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size
            };
            logWatcher.Changed += OnLogFileChanged;
            logWatcher.EnableRaisingEvents = true;
        }

        private void OnLogFileChanged(object sender, FileSystemEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(UpdateLogView));
            }
            else
            {
                UpdateLogView();
            }
        }

        private void UpdateLogView()
        {
            try
            {
                logTextBox.Text = File.ReadAllText(LoggingService.GetLogFilePath());
                logTextBox.SelectionStart = logTextBox.Text.Length;
                logTextBox.ScrollToCaret();
            }
            catch (IOException)
            {
                // Ignore IO exceptions which can happen if the file is locked
            }
        }
    }
}
