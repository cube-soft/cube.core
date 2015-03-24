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

            _model.Queued += Model_Queued;

            _view.Shown      += (s, ev) => Log("Shown");
            _view.TitleClick += (s, ev) => Log("TitleClick");
            _view.ImageClick += (s, ev) => Log("ImageClick");
            _view.Hidden     += View_Hidden;

            FormClosing += (s, ev) => { _view.Close(); };
        }

        #endregion

        #region Event handlers for NotifyQueue/NotifyForm

        /* ----------------------------------------------------------------- */
        ///
        /// Model_Queued
        ///
        /// <summary>
        /// Queue にデータが追加された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Model_Queued(object sender, EventArgs e)
        {
            Log(string.Format("Queued: Count = {0}", _model.Count));
            if (_model.Count <= 0 || _view.IsBusy) return;
            Notify(_model.Dequeue());
        }

        /* ----------------------------------------------------------------- */
        ///
        /// View_Hidden
        ///
        /// <summary>
        /// 通知フォームが非表示になった時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void View_Hidden(object sender, EventArgs e)
        {
            Log(string.Format("Hidden: Rest = {0}", _model.Count));
            if (_model.Count <= 0) return;
            Notify(_model.Dequeue());
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
            item.DisplayTime = TimeSpan.FromMilliseconds((double)DisplayMilliseconds.Value);
            item.InitialDelay = TimeSpan.FromMilliseconds((double)DelayMilliseconds.Value);

            _model.Enqueue(item);
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
            _model.Clear();
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
        /// Notify
        ///
        /// <summary>
        /// 通知フォームを表示します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Notify(NotifyItem item)
        {
            Log(string.Format("Notify: Level = {0} ({1}s -> {2}s)",
                item.Level, item.InitialDelay.TotalSeconds, item.DisplayTime.TotalSeconds));
            if (item.Image != null) _view.Image = item.Image;
            _view.Level = item.Level;
            _view.Title = item.Title;
            _view.InitialDelay = (int)item.InitialDelay.TotalMilliseconds;
            _view.Show((int)item.DisplayTime.TotalMilliseconds);
        }

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
        private NotifyQueue _model = new NotifyQueue();
        private NotifyForm _view = new NotifyForm();
        #endregion
    }
}
