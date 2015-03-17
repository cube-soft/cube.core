/* ------------------------------------------------------------------------- */
///
/// WebForm.cs
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
    /// Cube.Forms.Demo.WebForm
    /// 
    /// <summary>
    /// Web ブラウザのデモ用フォームです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class WebForm : FormBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// WebForm
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public WebForm()
        {
            InitializeComponent();
            var url = WebBrowser.Url;
            if (url != null) UrlTextBox.Text = url.ToString();
        }

        #endregion

        #region Event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateButton_Click
        ///
        /// <summary>
        /// テキストボックスに入力された URL を設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(UrlTextBox.Text)) return;
                WebBrowser.Url = new Uri(UrlTextBox.Text);
            }
            catch (Exception err) { ShowError(err); }
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// ShowError
        ///
        /// <summary>
        /// 例外の内容をメッセージボックスに表示します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ShowError(Exception err)
        {
            System.Windows.Forms.MessageBox.Show(err.ToString(), "Cube.Forms.Demo",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Error
            );
        }

        #endregion
    }
}
