/* ------------------------------------------------------------------------- */
///
/// Button.cs
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
using System.Runtime.InteropServices;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// ListView
    /// 
    /// <summary>
    /// リストビューを表示するクラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public class ListView : System.Windows.Forms.ListView
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Theme
        ///
        /// <summary>
        /// 表示用のテーマを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(WindowTheme.Normal)]
        public WindowTheme Theme
        {
            get { return _theme; }
            set
            {
                if (_theme != value)
                {
                    _theme = value;
                    UpdateTheme(_theme);
                }
            }
        }

        #endregion

        #region Override methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnCreateControl
        ///
        /// <summary>
        /// コントロールの生成時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnCreateControl()
        {
            UpdateTheme(_theme);
            base.OnCreateControl();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnSelectedIndexChanged
        ///
        /// <summary>
        /// 選択項目が変更された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            if (Theme == WindowTheme.Normal) return;
            User32.SendMessage(Handle, 0x127, 0x10001, 0);
        }
        /* ----------------------------------------------------------------- */
        ///
        /// OnEnter
        ///
        /// <summary>
        /// カーソルが領域内に侵入した時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            if (Theme == WindowTheme.Normal) return;
            User32.SendMessage(Handle, 0x127, 0x10001, 0);
        }

        #endregion

        #region Other private methods

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateTheme
        ///
        /// <summary>
        /// 表示用のテーマを更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateTheme(WindowTheme theme)
        {
            if (theme == WindowTheme.Normal) UxTheme.SetWindowTheme(Handle, null, null);
            else UxTheme.SetWindowTheme(Handle, theme.ToString(), null);
        }

        #endregion

        #region Fields
        private WindowTheme _theme = WindowTheme.Normal;
        #endregion
    }
}
