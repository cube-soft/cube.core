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
using System.ComponentModel;
using System.Drawing;

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
        /// OnPropertyChanged
        /// 
        /// <summary>
        /// プロパティの内容が変化した時に実行されます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.PropertyName == nameof(Active)) UpdateLayout();
        }

        /* --------------------------------------------------------------------- */
        ///
        /// UpdateLayout
        /// 
        /// <summary>
        /// 外観を更新します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void UpdateLayout()
        {
            if (Active)
            {
                BackColor = _backColor;
                MinimizeButton.Styles.NormalStyle.Image = Properties.Resources.Minimize;
                MaximizeButton.Styles.NormalStyle.Image = Properties.Resources.Maximize;
                ExitButton.Styles.NormalStyle.Image     = Properties.Resources.Close;
            }
            else
            {
                BackColor = Color.White;
                MinimizeButton.Styles.NormalStyle.Image = Properties.Resources.MinimizeGray;
                MaximizeButton.Styles.NormalStyle.Image = Properties.Resources.MaximizeGray;
                ExitButton.Styles.NormalStyle.Image     = Properties.Resources.CloseGray;
            }
        }

        #region Fields
        private Color _backColor = Color.Empty;
        #endregion

        #endregion
    }
}
