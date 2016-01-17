/* ------------------------------------------------------------------------- */
///
/// VersionForm.cs
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
    /// バージョン情報を表示するためのフォームクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class VersionForm : Form
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
        public VersionForm() : this(null) { }

        /* ----------------------------------------------------------------- */
        ///
        /// VersionForm
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public VersionForm(Assembly assembly)
        {
            InitializeComponent();
            VersionControl.Assembly = assembly;
            ExitButton.Click += (s, e) => Close();
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
            get { return VersionControl.Assembly; }
            set { VersionControl.Assembly = value; }
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
            get { return VersionControl.Description; }
            set { VersionControl.Description = value; }
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
            get { return VersionControl.Logo; }
            set { VersionControl.Logo = value; }
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
            get { return VersionControl.Url; }
            set { VersionControl.Url = value; }
        }

        #endregion

        #region Override methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnLoad
        ///
        /// <summary>
        /// フォームのロード時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnLoad(EventArgs e)
        {
            var reader = new AssemblyReader(Assembly);
            Text = string.Format("{0} について", reader.Product);
            Icon = reader.Icon;
            base.OnLoad(e);
        }

        #endregion
    }
}
