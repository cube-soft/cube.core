
namespace Cube.Forms.Demo
{
    partial class FileWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileWindow));
            this.RootPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ButtonsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.OpenFileButton = new System.Windows.Forms.Button();
            this.SaveFileButton = new System.Windows.Forms.Button();
            this.OpenDirectoryButton = new System.Windows.Forms.Button();
            this.PathTextBox = new System.Windows.Forms.TextBox();
            this.RootPanel.SuspendLayout();
            this.ButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            //
            // RootPanel
            //
            this.RootPanel.ColumnCount = 3;
            this.RootPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.RootPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.RootPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.RootPanel.Controls.Add(this.ButtonsPanel, 0, 3);
            this.RootPanel.Controls.Add(this.PathTextBox, 1, 1);
            this.RootPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RootPanel.Location = new System.Drawing.Point(0, 0);
            this.RootPanel.Name = "RootPanel";
            this.RootPanel.RowCount = 4;
            this.RootPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.RootPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.RootPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.RootPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.RootPanel.Size = new System.Drawing.Size(534, 121);
            this.RootPanel.TabIndex = 0;
            //
            // ButtonsPanel
            //
            this.ButtonsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonsPanel.ColumnCount = 5;
            this.RootPanel.SetColumnSpan(this.ButtonsPanel, 3);
            this.ButtonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ButtonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.ButtonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.ButtonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.ButtonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.ButtonsPanel.Controls.Add(this.OpenFileButton, 1, 1);
            this.ButtonsPanel.Controls.Add(this.SaveFileButton, 2, 1);
            this.ButtonsPanel.Controls.Add(this.OpenDirectoryButton, 3, 1);
            this.ButtonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonsPanel.Location = new System.Drawing.Point(0, 67);
            this.ButtonsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonsPanel.Name = "ButtonsPanel";
            this.ButtonsPanel.RowCount = 3;
            this.ButtonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.ButtonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ButtonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.ButtonsPanel.Size = new System.Drawing.Size(534, 54);
            this.ButtonsPanel.TabIndex = 0;
            //
            // OpenFileButton
            //
            this.OpenFileButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OpenFileButton.Location = new System.Drawing.Point(78, 12);
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(144, 30);
            this.OpenFileButton.TabIndex = 0;
            this.OpenFileButton.Text = "OpenFileDialog";
            this.OpenFileButton.UseVisualStyleBackColor = true;
            //
            // SaveFileButton
            //
            this.SaveFileButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SaveFileButton.Location = new System.Drawing.Point(228, 12);
            this.SaveFileButton.Name = "SaveFileButton";
            this.SaveFileButton.Size = new System.Drawing.Size(144, 30);
            this.SaveFileButton.TabIndex = 1;
            this.SaveFileButton.Text = "SaveFileDialog";
            this.SaveFileButton.UseVisualStyleBackColor = true;
            //
            // OpenDirectoryButton
            //
            this.OpenDirectoryButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OpenDirectoryButton.Location = new System.Drawing.Point(378, 12);
            this.OpenDirectoryButton.Name = "OpenDirectoryButton";
            this.OpenDirectoryButton.Size = new System.Drawing.Size(144, 30);
            this.OpenDirectoryButton.TabIndex = 2;
            this.OpenDirectoryButton.Text = "OpenDirectoryDialog";
            this.OpenDirectoryButton.UseVisualStyleBackColor = true;
            //
            // PathTextBox
            //
            this.PathTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PathTextBox.Location = new System.Drawing.Point(23, 23);
            this.PathTextBox.Name = "PathTextBox";
            this.PathTextBox.Size = new System.Drawing.Size(488, 23);
            this.PathTextBox.TabIndex = 0;
            //
            // FileWindow
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(534, 121);
            this.Controls.Add(this.RootPanel);
            this.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(500, 160);
            this.Name = "FileWindow";
            this.Text = "FileDialogs";
            this.RootPanel.ResumeLayout(false);
            this.RootPanel.PerformLayout();
            this.ButtonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel RootPanel;
        private System.Windows.Forms.TableLayoutPanel ButtonsPanel;
        private System.Windows.Forms.TextBox PathTextBox;
        private System.Windows.Forms.Button OpenFileButton;
        private System.Windows.Forms.Button SaveFileButton;
        private System.Windows.Forms.Button OpenDirectoryButton;
    }
}