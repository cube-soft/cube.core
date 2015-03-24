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

            _notify.View.Showing    += (s, e) => Log("Showing");
            _notify.View.TitleClick += (s, e) => Log("TitleClick");
            _notify.View.ImageClick += (s, e) => Log("ImageClick");
            _notify.View.Hidden     += (s, e) => Log("Hidden");

            FormClosing += (s, ev) => { _notify.View.Close(); };
        }

        #endregion

        #region Event handlers for buttons

        /* ----------------------------------------------------------------- */
        ///
        /// EnqueueButton_Click
        ///
        /// <summary>
        /// Enqueue ボタンが押下された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void EnqueueButton_Click(object sender, EventArgs e)
        {
            var item = new NotifyItem();
            if (!string.IsNullOrEmpty(ImageTextBox.Text) &&
                System.IO.File.Exists(ImageTextBox.Text))
            {
                item.Image = System.Drawing.Bitmap.FromFile(ImageTextBox.Text);
            }
            item.Level = (NotifyLevel)LevelComboBox.SelectedItem;
            item.Title = TitleTextBox.Text;
            item.DisplayTime = TimeSpan.FromSeconds((double)DisplaySeconds.Value);
            item.InitialDelay = TimeSpan.FromSeconds((double)DelaySeconds.Value);

            Log(string.Format("Enqueue: Level = {0}", item.Level));
            _notify.Queue.Enqueue(item);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ClearButton_Click
        ///
        /// <summary>
        /// Clear ボタンが押下された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ClearButton_Click(object sender, EventArgs e)
        {
            Log("ClearButton.Click");
            _notify.Queue.Clear();
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
            LogTextBox.SelectionStart = LogTextBox.TextLength;
            LogTextBox.Focus();
            LogTextBox.ScrollToCaret();
        }

        #endregion

        #region Fields
        private NotifyPresenter _notify = new NotifyPresenter();
        #endregion
    }
}
