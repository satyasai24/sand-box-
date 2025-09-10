using System;
using System.Windows.Forms;

namespace SandboxManager
{
    public partial class AppEditorForm : Form
    {
        public SandboxedApplication Application { get; private set; }

        public AppEditorForm(SandboxedApplication app = null)
        {
            InitializeComponent();

            if (app != null)
            {
                Application = app;
                nameTextBox.Text = app.Name;
                pathTextBox.Text = app.HostPath;
            }
            else
            {
                Application = new SandboxedApplication();
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Executable Files (*.exe)|*.exe|All files (*.*)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    pathTextBox.Text = dialog.FileName;
                    if (string.IsNullOrWhiteSpace(nameTextBox.Text))
                    {
                        nameTextBox.Text = System.IO.Path.GetFileNameWithoutExtension(dialog.FileName);
                    }
                }
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text) || string.IsNullOrWhiteSpace(pathTextBox.Text))
            {
                MessageBox.Show("Please enter a name and path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.Name = nameTextBox.Text;
            Application.HostPath = pathTextBox.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
