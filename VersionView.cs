/* ------------------------------------------------------------------------- */
///
/// VersionView.cs
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

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.VersionView
    /// 
    /// <summary>
    /// バージョン情報を表示するための View を作成するクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class VersionView : NtsUserControl
    {
        #region Cosntructors

        /* ----------------------------------------------------------------- */
        ///
        /// VersionView
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public VersionView()
        {
            InitializeComponent();
            InitializeVersion();
            ResetPosition();
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Name
        ///
        /// <summary>
        /// アプリケーション名を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public string ProductTitle
        {
            get { return ProductLabel.Text; }
            set
            {
                if (ProductLabel.Text != value)
                {
                    ProductLabel.Text = value;
                    ResetPosition();
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Version
        ///
        /// <summary>
        /// バージョン番号を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public string Version
        {
            get { return _version; }
            set
            {
                if (_version != value)
                {
                    _version = value;
                    VersionLabel.Text = string.Format("Version {0} ({1})", _version, Edition);
                    ResetPosition();
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Publisher
        ///
        /// <summary>
        /// アプリケーションの発行者を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public string Publisher
        {
            get { return _publisher; }
            set
            {
                if (_publisher != value)
                {
                    _publisher = value;
                    PublisherLabel.Text = string.Format("Copyright (c) {0} {1}.", _year, _publisher);
                    ResetPosition();
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Year
        ///
        /// <summary>
        /// アプリケーションの発行年を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public int Year
        {
            get { return _year; }
            set
            {
                if (_year != value)
                {
                    _year = value;
                    PublisherLabel.Text = string.Format("Copyright (c) {0} {1}.", _year, _publisher);
                    ResetPosition();
                }
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
            get { return LogoPictureBox.Image; }
            set
            {
                if (LogoPictureBox.Image != value)
                {
                    LogoPictureBox.Image = value;
                    ResetPosition();
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Edition
        ///
        /// <summary>
        /// アプリケーションのアーキテクチャを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Edition
        {
            get { return (IntPtr.Size == 4) ? "x86" : "x64"; }
        }

        #endregion

        #region Override methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnResize
        ///
        /// <summary>
        /// サイズが変更された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ResetPosition();
        }

        #endregion

        #region Event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// WebLinkLabel_LinkClicked
        ///
        /// <summary>
        /// リンクがクリックされた時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WebLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var control = sender as LinkLabel;
            if (control == null) return;

            try { System.Diagnostics.Process.Start(control.Text); }
            catch (Exception /* err */) { /* ignore */ }
        }

        #endregion

        #region Other private methods

        /* ----------------------------------------------------------------- */
        ///
        /// InitializeVersion
        ///
        /// <summary>
        /// 各種コンポーネントを再配置します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void InitializeVersion()
        {
            VersionLabel.Text = string.Format("Version {0} ({1})", Version, Edition);
            OSVersionLabel.Text = Environment.OSVersion.ToString();
            DotNetVersionLabel.Text = string.Format(".NET Framework {0}", Environment.Version.ToString());
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ResetPosition
        ///
        /// <summary>
        /// 各種コンポーネントを再配置します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ResetPosition()
        {
            var header = Math.Max((HeaderPanel.Height - _FixedHeaderHeight) / 2, 0);
            var footer = Math.Max((FooterPanel.Height - _FixedFooterHeight) / 2, 0);
            LayoutContainer.Panel1.Padding = new Padding(0, header, 0, 0);
            LayoutContainer.Panel2.Padding = new Padding(0, footer, 0, 0);

            var size = TextRenderer.MeasureText(ProductLabel.Text, ProductLabel.Font);
            var offset = (ProductLabel.Width - size.Width) / 2;
            var x = Math.Max(offset - LogoPictureBox.Width - 20, 8);
            var y = LogoPictureBox.Location.Y;
            LogoPictureBox.Location = new System.Drawing.Point(x, y);
        }

        #endregion

        #region Fields
        private string _version = "1.0.0";
        private string _publisher = "CubeSoft, Inc";
        private int _year = DateTime.Today.Year;
        #endregion

        #region Static fields
        private int _FixedHeaderHeight = 135;
        private int _FixedFooterHeight = 50;
        #endregion
    }
}
