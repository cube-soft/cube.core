/* ------------------------------------------------------------------------- */
///
/// DemoNotify.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///  http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
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

            LevelComboBox.Items.Add(Cube.NotifyLevel.None);
            LevelComboBox.Items.Add(Cube.NotifyLevel.Information);
            LevelComboBox.Items.Add(Cube.NotifyLevel.Recommended);
            LevelComboBox.Items.Add(Cube.NotifyLevel.Important);
            LevelComboBox.Items.Add(Cube.NotifyLevel.Warning);
            LevelComboBox.Items.Add(Cube.NotifyLevel.Error);
            LevelComboBox.SelectedItem = Cube.NotifyLevel.Information;

            _notify.View.Showing    += (s, e) => Log("Showing");
            _notify.View.TextClick += (s, e) => Log("TextClick");
            _notify.View.ImageClick += (s, e) => Log("ImageClick");
            _notify.View.Hidden     += View_Hidden;

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
            item.Description = DescriptionTextBox.Text;
            item.DisplayTime = TimeSpan.FromSeconds((double)DisplaySeconds.Value);
            item.InitialDelay = TimeSpan.FromSeconds((double)DelaySeconds.Value);

            Log(string.Format("Enqueue: Level = {0}", item.Level));
            _notify.Model.Enqueue(item);
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
            _notify.Model.Clear();
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

        /* ----------------------------------------------------------------- */
        ///
        /// View_Hidden
        ///
        /// <summary>
        /// 通知フォームが非表示になった直後に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void View_Hidden(object sender, EventArgs e)
        {
            if (!_notify.View.IsBusy && _notify.Model.Count == 0) Log("Hidden: Queue is empty");
            else Log("Hidden");
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
