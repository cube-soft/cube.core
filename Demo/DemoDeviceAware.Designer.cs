namespace Cube.Forms.Demo
{
    partial class DemoDeviceAware
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoDeviceAware));
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // LogTextBox
            // 
            this.LogTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.LogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogTextBox.Location = new System.Drawing.Point(0, 0);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ReadOnly = true;
            this.LogTextBox.Size = new System.Drawing.Size(384, 262);
            this.LogTextBox.TabIndex = 0;
            this.LogTextBox.Text = "デバイスを挿入または取り外して下さい...";
            // 
            // DemoDeviceAware
            // 
            this.ClientSize = new System.Drawing.Size(384, 262);
            this.Controls.Add(this.LogTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DemoDeviceAware";
            this.ShowInTaskbar = false;
            this.Text = "DeviceAwareForm Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LogTextBox;
    }
}