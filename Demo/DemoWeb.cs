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
using System.Windows.Forms;

namespace Cube.Forms.Demo
{
    /* --------------------------------------------------------------------- */
    ///
    /// DemoWeb
    /// 
    /// <summary>
    /// Web ブラウザのデモ用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class DemoWeb : WidgetForm
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

            CloseButton.Click += (s, e) => Close();
            UpdateButton.Click += (s, e) => UpdateBrowser();

            var url = WebBrowser.Url;
            if (url != null) UrlTextBox.Text = url.ToString();

            Caption = TitleBar;
        }

        #endregion

        #region Override methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnKeyDown
        ///
        /// <summary>
        /// キーボードのキーが押下された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnKeyDown(KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                    case Keys.F5:
                        UpdateBrowser();
                        break;
                    default:
                        break;
                }
            }
            finally { base.OnKeyDown(e); }
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateBrowser
        ///
        /// <summary>
        /// Web ブラウザを更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateBrowser()
        {
            try
            {
                if (string.IsNullOrEmpty(UrlTextBox.Text)) return;
                WebBrowser.Url = new Uri(UrlTextBox.Text);
            }
            catch (Exception err) { ShowError(err); }
        }

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
            MessageBox.Show(err.ToString(), "Cube.Forms.Demo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        #endregion
    }
}
