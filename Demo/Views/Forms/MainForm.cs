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
using System.Drawing;
using System.Windows.Forms;

namespace Cube.Forms.Demo
{
    /* --------------------------------------------------------------------- */
    ///
    /// MainForm
    /// 
    /// <summary>
    /// メイン画面を表すクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class MainForm : BorderlessForm
    {
        #region Constructors

        /* --------------------------------------------------------------------- */
        ///
        /// MainForm
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public MainForm()
        {
            InitializeComponent();

            ContentsControl.Resize += ContentsControl_Resize;
            DemoButton1.Click += (s, e) => new VersionForm().ShowDialog();

            Caption = HeaderCaptionControl;
        }

        #endregion

        #region Override methods

        /* --------------------------------------------------------------------- */
        ///
        /// OnLoad
        /// 
        /// <summary>
        /// ロード時に実行されます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var area = Screen.FromControl(this).WorkingArea;
            var x = (int)(area.Left + area.Width * 0.1);
            var y = (int)(area.Top + area.Height * 0.1);
            Location = new Point(x, y);
        }

        #endregion

        #region Event handlers

        /* --------------------------------------------------------------------- */
        ///
        /// ContentsControl_Resize
        /// 
        /// <summary>
        /// リサイズ時に実行されるハンドラです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void ContentsControl_Resize(object sender, EventArgs e)
        {
            var control = sender as Control;
            if (control == null) return;

            var width = control.ClientSize.Width;
            var left  = control.Padding.Left;
            var right = control.Padding.Right;

            DemoButton1.Width = width - left - right;
        }

        #endregion
    }
}
