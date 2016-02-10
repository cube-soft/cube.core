/* ------------------------------------------------------------------------- */
///
/// MainForm.cs
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
    /// MainForm
    /// 
    /// <summary>
    /// デモ用プロジェクトのメインフォームです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class MainForm : FormBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// MainForm
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// ButtonsButton_Click
        ///
        /// <summary>
        /// 各種ボタンのデモ用フォームを表示します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ButtonsButton_Click(object sender, EventArgs e)
        {
            var dialog = new DemoButtons();
            dialog.ShowDialog();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WebBrowserButton_Click
        ///
        /// <summary>
        /// Web ブラウザのデモ用フォームを表示します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WebBrowserButton_Click(object sender, EventArgs e)
        {
            var dialog = new DemoWeb();
            dialog.ShowDialog();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// NotifyFormButton_Click
        ///
        /// <summary>
        /// 通知フォームのデモ用フォームを表示します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void NotifyFormButton_Click(object sender, EventArgs e)
        {
            var dialog = new DemoNotify();
            dialog.ShowDialog();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// StockIconsButton_Click
        ///
        /// <summary>
        /// システムアイコン一覧を表示するためのデモ用フォームを表示します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void StockIconsButton_Click(object sender, EventArgs e)
        {
            var dialog = new DemoStockIcons();
            dialog.ShowDialog();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DeviceAwareButton_Click
        ///
        /// <summary>
        /// デバイスの追加・削除に反応するフォームのためのデモ用フォームを
        /// 表示します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void DeviceAwareButton_Click(object sender, EventArgs e)
        {
            var dialog = new DemoDeviceAware();
            dialog.ShowDialog();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// VersionButton_Click
        ///
        /// <summary>
        /// バージョン情報を確認するためのデモ用フォームを表示します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void VersionButton_Click(object sender, EventArgs e)
        {
            var dialog = new VersionForm(System.Reflection.Assembly.GetExecutingAssembly());
            dialog.Height = 250;
            dialog.Description = string.Empty;
            dialog.Url = "http://www.cube-soft.jp/";
            dialog.ShowDialog();
        }

        #endregion
    }
}
