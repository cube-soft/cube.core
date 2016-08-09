namespace Cube.Forms
{
    partial class VersionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VersionForm));
            this.LayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.VersionControl = new Cube.Forms.VersionControl();
            this.ExitButton = new System.Windows.Forms.Button();
            this.LayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LayoutPanel
            // 
            this.LayoutPanel.ColumnCount = 1;
            this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutPanel.Controls.Add(this.VersionControl, 0, 0);
            this.LayoutPanel.Controls.Add(this.ExitButton, 0, 1);
            this.LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.LayoutPanel.Name = "LayoutPanel";
            this.LayoutPanel.RowCount = 2;
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.LayoutPanel.Size = new System.Drawing.Size(434, 271);
            this.LayoutPanel.TabIndex = 0;
            // 
            // VersionControl
            // 
            this.VersionControl.BackColor = System.Drawing.SystemColors.Window;
            this.VersionControl.Description = "このソフトウェアは Apache 2.0 でライセンスされています。";
            this.VersionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VersionControl.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.VersionControl.Location = new System.Drawing.Point(0, 0);
            this.VersionControl.Logo = ((System.Drawing.Image)(resources.GetObject("VersionControl.Logo")));
            this.VersionControl.Margin = new System.Windows.Forms.Padding(0);
            this.VersionControl.Name = "VersionControl";
            this.VersionControl.Padding = new System.Windows.Forms.Padding(30);
            this.VersionControl.Size = new System.Drawing.Size(434, 211);
            this.VersionControl.TabIndex = 0;
            this.VersionControl.Url = "http://www.cube-soft.jp/";
            // 
            // ExitButton
            // 
            this.ExitButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ExitButton.Location = new System.Drawing.Point(157, 226);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(120, 30);
            this.ExitButton.TabIndex = 1;
            this.ExitButton.Text = "OK";
            this.ExitButton.UseVisualStyleBackColor = true;
            // 
            // VersionForm
            // 
            this.ClientSize = new System.Drawing.Size(434, 271);
            this.Controls.Add(this.LayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VersionForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "VersionForm";
            this.LayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel LayoutPanel;
        private VersionControl VersionControl;
        private System.Windows.Forms.Button ExitButton;
    }
}