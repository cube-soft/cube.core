/* ------------------------------------------------------------------------- */
///
/// DemoWeb.cs
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
    /// Cube.Forms.Demo.DemoWeb
    /// 
    /// <summary>
    /// Web ブラウザのデモ用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class DemoWeb : FormBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// DemoWeb
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DemoWeb()
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
