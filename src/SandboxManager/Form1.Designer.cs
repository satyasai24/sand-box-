namespace SandboxManager
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private System.Windows.Forms.ComboBox appComboBox;
        private System.Windows.Forms.Button launchButton;
        private System.Windows.Forms.Button viewLogsButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button removeButton;

        private void InitializeComponent()
        {
            this.appComboBox = new System.Windows.Forms.ComboBox();
            this.launchButton = new System.Windows.Forms.Button();
            this.viewLogsButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // appComboBox
            //
            this.appComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.appComboBox.FormattingEnabled = true;
            this.appComboBox.Location = new System.Drawing.Point(12, 12);
            this.appComboBox.Name = "appComboBox";
            this.appComboBox.Size = new System.Drawing.Size(188, 21);
            this.appComboBox.TabIndex = 0;
            //
            // launchButton
            //
            this.launchButton.Location = new System.Drawing.Point(206, 12);
            this.launchButton.Name = "launchButton";
            this.launchButton.Size = new System.Drawing.Size(75, 23);
            this.launchButton.TabIndex = 1;
            this.launchButton.Text = "Launch";
            this.launchButton.UseVisualStyleBackColor = true;
            this.launchButton.Click += new System.EventHandler(this.launchButton_Click);
            //
            // viewLogsButton
            //
            this.viewLogsButton.Location = new System.Drawing.Point(287, 12);
            this.viewLogsButton.Name = "viewLogsButton";
            this.viewLogsButton.Size = new System.Drawing.Size(75, 23);
            this.viewLogsButton.TabIndex = 2;
            this.viewLogsButton.Text = "View Logs";
            this.viewLogsButton.UseVisualStyleBackColor = true;
            this.viewLogsButton.Click += new System.EventHandler(this.viewLogsButton_Click);
            //
            // addButton
            //
            this.addButton.Location = new System.Drawing.Point(12, 39);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 3;
            this.addButton.Text = "Add...";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            //
            // editButton
            //
            this.editButton.Location = new System.Drawing.Point(93, 39);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(75, 23);
            this.editButton.TabIndex = 4;
            this.editButton.Text = "Edit...";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            //
            // removeButton
            //
            this.removeButton.Location = new System.Drawing.Point(174, 39);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 5;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            //
            // logTextBox
            //
            this.logTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logTextBox.Location = new System.Drawing.Point(12, 68);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTextBox.Size = new System.Drawing.Size(776, 370);
            this.logTextBox.TabIndex = 6;
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.viewLogsButton);
            this.Controls.Add(this.launchButton);
            this.Controls.Add(this.appComboBox);
            this.Name = "Form1";
            this.Text = "Sandbox Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TextBox logTextBox;

        #endregion
    }
}
