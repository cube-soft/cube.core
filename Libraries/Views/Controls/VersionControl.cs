/* ------------------------------------------------------------------------- */
//
// Copyright (c) 2010 CubeSoft, Inc.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
/* ------------------------------------------------------------------------- */
using System;
using System.ComponentModel;
using System.Drawing;
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
    public class VersionControl : ControlBase
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
        /// <param name="assembly">アセンブリ情報</param>
        ///
        /* ----------------------------------------------------------------- */
        public VersionControl(Assembly assembly) : base()
        {
            Size = new Size(340, 120);
            InitializeLayout();
            Update(assembly);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// VersionControl
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public VersionControl()
            : this(Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly())
        { }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Image
        ///
        /// <summary>
        /// バージョン画面に表示するイメージオブジェクトを取得
        /// または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public Image Image
        {
            get { return _image.Image; }
            set
            {
                if (_image.Image == value) return;
                _panel.SplitterDistance = value?.Width ?? 1;
                _image.Image = value;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Product
        ///
        /// <summary>
        /// 製品名を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public string Product
        {
            get { return _product.Text; }
            set
            {
                if (_product.Text == value) return;
                _product.Text = value;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Version
        ///
        /// <summary>
        /// バージョンを表す文字列を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public string Version
        {
            get { return _version.Text; }
            set
            {
                if (_version.Text == value) return;
                _version.Text = value;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Description
        ///
        /// <summary>
        /// バージョン画面に表示するその他の情報を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public string Description
        {
            get { return _others.Text; }
            set
            {
                if (_others.Text == value) return;
                _others.Text = value;
                _others.Visible = !string.IsNullOrEmpty(value);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Copyright
        ///
        /// <summary>
        /// コピーライト表記を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public string Copyright
        {
            get { return _copyright.Text; }
            set
            {
                if (_copyright.Text == value) return;
                _copyright.Text = value;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Uri
        ///
        /// <summary>
        /// コピーライト表記をクリックした時に表示する Web ページの
        /// URL を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public Uri Uri { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Platform
        ///
        /// <summary>
        /// プラットフォームのバージョンを表す文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        public string Platform => string.Format("{0}{1}Microsoft .NET Framework {2}{3}",
            Environment.OSVersion,
            Environment.NewLine,
            Environment.Version,
            Environment.NewLine
        );

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Update
        ///
        /// <summary>
        /// アセンブリ情報を基に表示内容を更新します。
        /// </summary>
        /// 
        /// <param name="assembly">アセンブリ情報</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Update(Assembly assembly)
        {
            if (assembly == null) return;
            var reader = new AssemblyReader(assembly);

            Product   = reader.Product;
            Version   = $"Version {reader.Version.ToString()}";
            Copyright = reader.Copyright;
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// InitializeLayout
        ///
        /// <summary>
        /// レイアウトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void InitializeLayout()
        {
            SuspendLayout();

            _panel = new System.Windows.Forms.SplitContainer();
            _panel.Dock = System.Windows.Forms.DockStyle.Fill;
            _panel.Margin = new System.Windows.Forms.Padding(0);
            _panel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            _panel.IsSplitterFixed = true;
            _panel.Panel1MinSize = 0;
            _panel.Panel2MinSize = 0;
            _panel.SplitterDistance = 48;
            _panel.SplitterWidth = 8;
            _panel.Size = Size;
            _panel.SuspendLayout();

            _image = new PictureBox();
            _image.Dock = System.Windows.Forms.DockStyle.Fill;
            _image.Image = Properties.Resources.LogoLarge;
            _image.Margin = new System.Windows.Forms.Padding(0);

            _contents = new FlowLayoutPanel();
            _contents.Dock = System.Windows.Forms.DockStyle.Fill;
            _contents.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            _contents.Margin = new System.Windows.Forms.Padding(0);
            _contents.SuspendLayout();

            _product = new System.Windows.Forms.Label();
            _product.AutoEllipsis = true;
            _product.AutoSize = true;
            _product.Margin = new System.Windows.Forms.Padding(0);
            _product.TabIndex = 0;

            _version = new System.Windows.Forms.Label();
            _version.AutoEllipsis = true;
            _version.AutoSize = true;
            _version.Margin = new System.Windows.Forms.Padding(0);
            _version.TabIndex = 1;

            _platform = new System.Windows.Forms.Label();
            _platform.AutoEllipsis = true;
            _platform.AutoSize = true;
            _platform.ForeColor = SystemColors.GrayText;
            _platform.Margin = new System.Windows.Forms.Padding(0, _margin, 0, 0);
            _platform.TabIndex = 2;
            _platform.Text = Platform;

            _others = new System.Windows.Forms.Label();
            _others.AutoEllipsis = true;
            _others.AutoSize = true;
            _others.ForeColor = SystemColors.GrayText;
            _others.Margin = new System.Windows.Forms.Padding(0, _margin, 0, 0);
            _others.TabIndex = 3;
            _others.Text = string.Empty;
            _others.Visible = false;

            _copyright = new System.Windows.Forms.LinkLabel();
            _copyright.AutoSize = true;
            _copyright.Margin = new System.Windows.Forms.Padding(0, _margin, 0, 0);
            _copyright.TabIndex = 4;
            _copyright.LinkClicked += (s, e) =>
            {
                if (Uri == null) return;
                try { System.Diagnostics.Process.Start(Uri.ToString()); }
                catch (Exception err) { this.LogWarn(err.ToString()); }
            };

            _contents.Controls.Add(_product);
            _contents.Controls.Add(_version);
            _contents.Controls.Add(_platform);
            _contents.Controls.Add(_others);
            _contents.Controls.Add(_copyright);

            _panel.Panel1.Controls.Add(_image);
            _panel.Panel2.Controls.Add(_contents);

            Controls.Add(_panel);
            SetStyle(System.Windows.Forms.ControlStyles.Selectable, false);

            _contents.ResumeLayout(false);
            _panel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #region Fields
        private System.Windows.Forms.SplitContainer _panel;
        private System.Windows.Forms.FlowLayoutPanel _contents;
        private System.Windows.Forms.PictureBox _image;
        private System.Windows.Forms.Label _product;
        private System.Windows.Forms.Label _version;
        private System.Windows.Forms.Label _platform;
        private System.Windows.Forms.Label _others;
        private System.Windows.Forms.LinkLabel _copyright;
        private readonly int _margin = 16;
        #endregion

        #endregion
    }
}
