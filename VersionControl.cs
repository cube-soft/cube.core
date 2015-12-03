/* ------------------------------------------------------------------------- */
///
/// VersionControl.cs
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
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.Reflection;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.VersionControl
    /// 
    /// <summary>
    /// バージョン情報を表示するためのユーザコントロールクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class VersionControl : NtsUserControl
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// VersionControl
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public VersionControl() : this(null) { }

        /* ----------------------------------------------------------------- */
        ///
        /// VersionControl
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public VersionControl(Assembly assembly)
        {
            InitializeComponent();
            Assembly = assembly;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Assembly
        ///
        /// <summary>
        /// バージョン情報等を保持する Assembly オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Assembly Assembly
        {
            get { return _reader.Assembly; }
            set
            {
                _reader = new AssemblyReader(value);
                UpdateInformation();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Architecture
        ///
        /// <summary>
        /// アプリケーションのアーキテクチャを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Architecture
        {
            get { return (IntPtr.Size == 4) ? "x86" : "x64"; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Description
        ///
        /// <summary>
        /// バージョン情報画面で表示する情報を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public string Description
        {
            get { return DescriptionLabel.Text; }
            set { DescriptionLabel.Text = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Logo
        ///
        /// <summary>
        /// ロゴ画像を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public Image Logo
        {
            get { return LogoPanel.Image; }
            set
            {
                if (LogoPanel.Image != value)
                {
                    LogoPanel.Image = value;
                    LayoutPanel.RowStyles[0].Height = LogoPanel.Image.Height;
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Url
        ///
        /// <summary>
        /// Web ページの URL を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public string Url
        {
            get { return WebLinkLabel.Text; }
            set { WebLinkLabel.Text = value; }
        }

        #endregion

        #region Event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// LinkLabel_LinkClicked
        ///
        /// <summary>
        /// リンクがクリックされた時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void LinkLabel_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            var control = sender as System.Windows.Forms.LinkLabel;
            if (control == null) return;

            try { Process.Start(control.Text); }
            catch (Exception /* err */) { /* ignore */ }
        }

        #endregion

        #region Other private methods

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateInformation
        ///
        /// <summary>
        /// 各種情報を更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateInformation()
        {
            var ss = new System.Text.StringBuilder();
            ss.AppendFormat("{0} {1} ({2})", _reader.Product, _reader.Version, Architecture);
            ss.AppendLine();
            ss.AppendFormat("{0}", Environment.OSVersion.ToString());
            ss.AppendLine();
            ss.AppendFormat("Microsoft .NET Framework {0}", Environment.Version.ToString());

            VersionLabel.Text = ss.ToString(); ;
            CopyrightLabel.Text = _reader.Copyright;
        }

        #endregion

        #region Fields
        private AssemblyReader _reader;
        #endregion
    }
}
