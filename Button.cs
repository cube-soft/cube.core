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

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.Button
    /// 
    /// <summary>
    /// ボタンを作成するためのクラスです。
    /// </summary>
    /// 
    /// <remarks>
    /// Button クラスは、System.Windows.Forms.Button クラスにおける
    /// いくつかの表示上の問題を解決するために定義されたクラスです。
    /// さらに柔軟な外観を定義する場合は、FlatButton クラスを利用して下さい。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class Button : System.Windows.Forms.Button
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Button
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Button() : base() { }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// ShowFocusCues
        ///
        /// <summary>
        /// フォーカス時に枠線を表示するかどうかを示す値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override bool ShowFocusCues
        {
            get { return false; }
        }

        #endregion
    }
}
