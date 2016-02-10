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
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// VersionControl
    /// 
    /// <summary>
    /// バージョン情報を表示するためのユーザコントロールです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class VersionControl : UserControl
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
                if (_reader != null && _reader.Assembly == value) return;
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
        /// VersionDigit
        ///
        /// <summary>
        /// 表示するバージョンの桁数を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(4)]
        public int VersionDigit
        {
            get { return _versionDigit; }
            set
            {
                if (_versionDigit == value) return;
                _versionDigit = Math.Min(Math.Max(value, 1), 4);
                UpdateInformation();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// VersionSuffix
        ///
        /// <summary>
        /// バージョンのサフィックスを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public string VersionSuffix
        {
            get { return _versionSuffix; }
            set
            {
                if (_versionSuffix == value) return;
                _versionSuffix = value;
                UpdateInformation();
            }
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
            set
            {
                if (DescriptionLabel.Text == value) return;
                DescriptionLabel.Text = value;
                UpdateInformation();
            }
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
                    if (LogoPanel.Image == value) return;
                    LogoPanel.Image = value;
                    UpdateInformation();
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
            set
            {
                if (WebLinkLabel.Text == value) return;
                WebLinkLabel.Text = value;
                UpdateInformation();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Spacing
        ///
        /// <summary>
        /// 項目間のスペースを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(16)]
        public int Spacing
        {
            get { return _spacing; }
            set
            {
                if (_spacing == value) return;
                _spacing = value;
                UpdateInformation();
            }
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
            var version = Create(_reader.Version, VersionDigit);
            ss.AppendFormat("{0} {1}{2} ({3})", _reader.Product, version, VersionSuffix, Architecture);
            ss.AppendLine();
            ss.AppendFormat("{0}", Environment.OSVersion.ToString());
            ss.AppendLine();
            ss.AppendFormat("Microsoft .NET Framework {0}", Environment.Version.ToString());

            VersionLabel.Text = ss.ToString();

            if (LogoPanel.Image != null)
            {
                LayoutPanel.ColumnStyles[0].Width = LogoPanel.Image.Width;
                var diff = Math.Max(LogoPanel.Image.Height - VersionLabel.Height, 0);
                VersionLabel.Margin = new Padding(0, diff / 2, 0, diff / 2);
            }

            var space = string.IsNullOrEmpty(Description) ? 0 : Spacing;
            DescriptionLabel.Margin = new Padding(0, space, 0, 0);

            CopyrightLabel.Text = _reader.Copyright;
            CopyrightLabel.Margin = new Padding(0, Spacing, 0, 0);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToString
        ///
        /// <summary>
        /// バージョンを表す文字列を生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private string Create(Version src, int digit)
        {
            var ss = new System.Text.StringBuilder();

            ss.Append(src.Major);
            if (digit <= 1) return ss.ToString();

            ss.AppendFormat(".{0}", src.Minor);
            if (digit <= 2) return ss.ToString();

            ss.AppendFormat(".{0}", src.Build);
            if (digit <= 3) return ss.ToString();

            ss.AppendFormat(".{0}", src.Revision);
            return ss.ToString();
        }

        #endregion

        #region Fields
        private AssemblyReader _reader;
        private int _versionDigit = 4;
        private string _versionSuffix = string.Empty;
        private int _spacing = 16;
        #endregion
    }
}
