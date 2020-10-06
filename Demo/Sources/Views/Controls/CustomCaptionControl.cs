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
using System.Drawing;
using Cube.Forms.Controls;

namespace Cube.Forms.Demo
{
    /* --------------------------------------------------------------------- */
    ///
    /// CustomCaptionControl
    ///
    /// <summary>
    /// 画面上部のキャプション（タイトルバー）を表すコントロールです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class CustomCaptionControl : CaptionControl
    {
        #region Constructors

        /* --------------------------------------------------------------------- */
        ///
        /// CustomCaptionControl
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public CustomCaptionControl()
        {
            InitializeComponent();

            MaximizeControl = MaximizeButton;
            MinimizeControl = MinimizeButton;
            CloseControl    = ExitButton;
        }

        #endregion

        #region Implementations

        /* --------------------------------------------------------------------- */
        ///
        /// OnControlAdded
        ///
        /// <summary>
        /// このコントロールが何らかのコンテナに追加された時に実行されます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected override void OnControlAdded(System.Windows.Forms.ControlEventArgs e)
        {
            base.OnControlAdded(e);
            _backColor = BackColor;
        }

        /* --------------------------------------------------------------------- */
        ///
        /// OnActivated
        ///
        /// <summary>
        /// アクティブ化された時に実行されます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected override void OnActivated(EventArgs e)
        {
            BackColor = _backColor;
            MinimizeButton.Styles.Default.Image = Properties.Resources.Minimize;
            MaximizeButton.Styles.Default.Image = Properties.Resources.Maximize;
            ExitButton.Styles.Default.Image = Properties.Resources.Close;
            base.OnActivated(e);
        }

        /* --------------------------------------------------------------------- */
        ///
        /// OnDeactivate
        ///
        /// <summary>
        /// 非アクティブ化された時に実行されるハンドラです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected override void OnDeactivate(EventArgs e)
        {
            BackColor = Color.White;
            MinimizeButton.Styles.Default.Image = Properties.Resources.MinimizeGray;
            MaximizeButton.Styles.Default.Image = Properties.Resources.MaximizeGray;
            ExitButton.Styles.Default.Image = Properties.Resources.CloseGray;
            base.OnDeactivate(e);
        }

        #endregion

        #region Fields
        private Color _backColor = Color.Empty;
        #endregion
    }
}
