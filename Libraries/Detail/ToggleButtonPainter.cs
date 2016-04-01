/* ------------------------------------------------------------------------- */
///
/// ToggleButtonPainter.cs
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

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// ToggleButtonPainter
    /// 
    /// <summary>
    /// トグルボタンの外観を描画するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal class ToggleButtonPainter : FlatButtonPainter
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ToggleButtonPainter
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ToggleButtonPainter(System.Windows.Forms.CheckBox view)
            : base(view)
        {
            view.CheckedChanged += (s, e) => OnCheckedChanged(e);
            InitializeSurface();
        }

        #endregion

        #region Virtual methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnCheckedChanged
        /// 
        /// <summary>
        /// 描画対象となるボタンの CheckedChanged イベントを捕捉する
        /// ハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnCheckedChanged(EventArgs e)
        {
            var control = View as System.Windows.Forms.CheckBox;
            if (control == null) return;
            IsChecked = control.Checked;
        }

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// InitializeSurface
        /// 
        /// <summary>
        /// 外観の描画に関して CheckBox オブジェクトと競合するプロパティを
        /// 無効にします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void InitializeSurface()
        {
            var radio = View as System.Windows.Forms.CheckBox;
            if (radio != null) radio.Appearance = System.Windows.Forms.Appearance.Button;
        }

        #endregion
    }
}
