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
using Cube.Log;

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
        public VersionControl() : this(Assembly.GetExecutingAssembly()) { }

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
            CopyrightLinkLabel.LinkClicked += LinkLabel_LinkClicked;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Version
        ///
        /// <summary>
        /// バージョン情報を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        public SoftwareVersion Version { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Assembly
        ///
        /// <summary>
        /// バージョン情報等を保持する Assembly オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Assembly Assembly
        {
            get { return _reader.Assembly; }
            set
            {
                if (_reader != null && _reader.Assembly == value) return;
                _reader = new AssemblyReader(value);
                UpdateVersion(value);
                Refresh();
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
                Refresh();
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
                    Refresh();
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
        public string Url { get; set; }

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
                Refresh();
            }
        }

        #endregion

        #region Override methods

        /* ----------------------------------------------------------------- */
        ///
        /// Refresh
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public override void Refresh()
        {
            try
            {
                SuspendLayout();

                ProductLabel.Text = _reader.Product;
                VersionLabel.Text = $"Version {Version}";

                PlatformLabel.Text = GetFrameworkVersion();
                PlatformLabel.Margin = PaddingTop();

                var visible = !string.IsNullOrEmpty(Description);
                DescriptionLabel.Margin = PaddingTop(visible ? Spacing : 0);
                DescriptionLabel.Visible = visible;

                CopyrightLinkLabel.Text = _reader.Copyright;
                CopyrightLinkLabel.Margin = PaddingTop();

                LayoutPanel.ColumnStyles[0].Width = LogoPanel?.Image?.Width ?? 1;
            }
            finally
            {
                ResumeLayout();
                base.Refresh();
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
        private void LinkLabel_LinkClicked(object sender,
            System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
            => this.LogException(() => Process.Start(Url));

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateVersion
        ///
        /// <summary>
        /// Assembly の内容にしたがって Version プロパティを更新します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void UpdateVersion(Assembly assembly)
        {
            var previous = Version;
            Version = new SoftwareVersion(assembly);
            if (previous == null) return;

            Version.Digit  = previous.Digit;
            Version.Prefix = previous.Prefix;
            Version.Suffix = previous.Suffix;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetFrameworkVersion
        ///
        /// <summary>
        /// 使用するフレームワークのバージョンを表す文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private string GetFrameworkVersion()
        {
            var ss = new System.Text.StringBuilder();
            ss.Append(Environment.OSVersion.ToString());
            ss.AppendLine();
            ss.Append($"Microsoft .NET Framework {Environment.Version}");
            return ss.ToString();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PaddingTop
        ///
        /// <summary>
        /// Top の値を設定した Padding オブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private System.Windows.Forms.Padding PaddingTop() => PaddingTop(Spacing);
        private System.Windows.Forms.Padding PaddingTop(int value)
            => new System.Windows.Forms.Padding(0, value, 0, 0);

        #endregion

        #region Fields
        private AssemblyReader _reader;
        private int _spacing = 16;
        #endregion
    }
}
