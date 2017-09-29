/* ------------------------------------------------------------------------- */
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
using System.Reflection;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// VersionForm
    /// 
    /// <summary>
    /// バージョン情報を表示するためのフォームです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class VersionForm : StandardForm
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// VersionForm
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public VersionForm()
            : this(Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly())
        { }

        /* ----------------------------------------------------------------- */
        ///
        /// VersionForm
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public VersionForm(Assembly assembly) : base()
        {
            InitializeLayout(assembly);
        }

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
            get { return _control.Image; }
            set { _control.Image = value; }
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
            get { return _control.Product; }
            set { _control.Product = value; }
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
            get { return _control.Version; }
            set { _control.Version = value; }
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
            get { return _control.Description; }
            set { _control.Description = value; }
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
            get { return _control.Copyright; }
            set { _control.Copyright = value; }
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
        public Uri Uri
        {
            get { return _control.Uri; }
            set { _control.Uri = value; }
        }

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
            => _control.Update(assembly);

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
        private void InitializeLayout(Assembly assembly)
        {
            Size = new Size(400, 270);
            SuspendLayout();

            _panel = new TableLayoutPanel();
            _panel.Dock = System.Windows.Forms.DockStyle.Fill;
            _panel.Margin = new System.Windows.Forms.Padding(0);
            _panel.ColumnCount = 1;
            _panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            _panel.RowCount = 2;
            _panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            _panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            _panel.SuspendLayout();

            _control = new VersionControl(assembly);
            _control.BackColor = SystemColors.Window;
            _control.Dock = System.Windows.Forms.DockStyle.Fill;
            _control.Margin = new System.Windows.Forms.Padding(0);
            _control.Padding = new System.Windows.Forms.Padding(20, 20, 0, 0);

            _button = new Button();
            _button.Anchor = System.Windows.Forms.AnchorStyles.None;
            _button.Size = new Size(100, 25);
            _button.Text = "OK";
            _button.Click += (s, e) => Close();

            _panel.Controls.Add(_control, 0, 0);
            _panel.Controls.Add(_button, 0, 1);

            Font = FontFactory.Create(9, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;

            Controls.Add(_panel);
            _panel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #region Fields
        private VersionControl _control;
        private TableLayoutPanel _panel;
        private Button _button;
        #endregion

        #endregion
    }
}
