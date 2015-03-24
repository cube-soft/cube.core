/* ------------------------------------------------------------------------- */
///
/// DemoNotify.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
///
/* ------------------------------------------------------------------------- */
using System;

namespace Cube.Forms.Demo
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.Demo.DemoNotify
    /// 
    /// <summary>
    /// 通知フォームのデモ用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class DemoNotify : FormBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// DemoNotify
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DemoNotify()
        {
            InitializeComponent();

            LevelComboBox.Items.Add(NotifyLevel.None);
            LevelComboBox.Items.Add(NotifyLevel.Information);
            LevelComboBox.Items.Add(NotifyLevel.Recommended);
            LevelComboBox.Items.Add(NotifyLevel.Important);
            LevelComboBox.Items.Add(NotifyLevel.Warning);
            LevelComboBox.Items.Add(NotifyLevel.Error);
            LevelComboBox.SelectedItem = NotifyLevel.Information;

            _component.Showing += (s, ev) => Log("Showing");
            _component.Shown += (s, ev) => Log("Shown");
            _component.TitleClick += (s, ev) => Log("TitleClick");
            _component.ImageClick += (s, ev) => Log("ImageClick");
            _component.Hiding += (s, ev) => Log("Hiding");
            _component.Hidden += (s, ev) => Log("Hidden");

            FormClosing += (s, ev) => { _component.Close(); };
        }

        #endregion

        #region Event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// ShowButton_Click
        ///
        /// <summary>
        /// Show ボタンが押下された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ShowButton_Click(object sender, EventArgs e)
        {
            Log("ShowButton.Click");
            if (!string.IsNullOrEmpty(ImageTextBox.Text) &&
                System.IO.File.Exists(ImageTextBox.Text))
            {
                _component.Image = System.Drawing.Bitmap.FromFile(ImageTextBox.Text);
            }
            _component.Level = (NotifyLevel)LevelComboBox.SelectedItem;
            _component.Title = TitleTextBox.Text;
            _component.InitialDelay = (int)DelayMilliseconds.Value;
            _component.Show((int)DisplayMilliseconds.Value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ImageBrowseButton_Click
        ///
        /// <summary>
        /// 画像ファイルを選択させるためのブラウザを表示します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ImageBrowseButton_Click(object sender, EventArgs e)
        {
            var dialog = new System.Windows.Forms.OpenFileDialog();
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            ImageTextBox.Text = dialog.FileName;
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Log
        ///
        /// <summary>
        /// ログ用のテキストボックスにメッセージを追加します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Log(string message)
        {
            var builder = new System.Text.StringBuilder();
            var newline = string.Format("{0} {1}", DateTime.Now, message);
            builder.Append(LogTextBox.Text);
            if (!string.IsNullOrEmpty(LogTextBox.Text)) builder.AppendLine();
            builder.Append(newline);
            LogTextBox.Text = builder.ToString();
        }

        #endregion

        #region Fields
        private NotifyForm _component = new NotifyForm();
        #endregion
    }
}
