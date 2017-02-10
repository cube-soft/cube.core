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
            InitializeEvents();
        }

        #endregion

        #region Initialize methods

        /* --------------------------------------------------------------------- */
        ///
        /// InitializeEvents
        /// 
        /// <summary>
        /// 各種イベントを初期化します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void InitializeEvents()
        {
            MinimizeButton.Click += (s, e) => OnMinimize(e);
            MaximizeButton.Click += (s, e) => OnMaximize(e);
            ExitButton.Click     += (s, e) => OnClose(e);
        }

        #endregion

        #region Override methods

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

            switch (e.PropertyName)
            {
                case nameof(MaximizeBox):
                    MaximizeButton.Visible = MaximizeBox;
                    break;
                case nameof(MinimizeBox):
                    MinimizeButton.Visible = MinimizeBox;
                    break;
                case nameof(CloseBox):
                    ExitButton.Visible = CloseBox;
                    break;
                case nameof(Active):
                    UpdateLayout();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Others

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
                MinimizeButton.Styles.NormalStyle.Image = Properties.Resources.MinimizeGrey;
                MaximizeButton.Styles.NormalStyle.Image = Properties.Resources.MaximizeGrey;
                ExitButton.Styles.NormalStyle.Image     = Properties.Resources.CloseGrey;
            }
        }

        #endregion

        #region Fields
        private Color _backColor = Color.Empty;
        #endregion
    }
}
