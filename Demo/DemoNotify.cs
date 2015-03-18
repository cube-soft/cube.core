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
            var dialog = new Cube.Forms.NotifyForm();
            dialog.Title = TitleTextBox.Text;
            dialog.Image = System.Drawing.Bitmap.FromFile(ImageTextBox.Text);
            dialog.InitialDelay = (int)DelayMilliseconds.Value;
            dialog.Shown += (s, ev) => Log("Shown");
            dialog.TitleClick += (s, ev) => Log("TitleClick");
            dialog.ImageClick += (s, ev) => Log("ImageClick");
            dialog.FormClosed += (s, ev) => Log("FormClosed");
            dialog.Show((int)DisplayMilliseconds.Value);
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
            builder.AppendLine(LogTextBox.Text);
            builder.Append(newline);
            LogTextBox.Text = builder.ToString();
        }

        #endregion
    }
}
