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
using System.Drawing;
using System.Windows.Forms;

namespace Cube.Forms.Models
{
    /* --------------------------------------------------------------------- */
    ///
    /// ControlInfo
    /// 
    /// <summary>
    /// Control オブジェクトの各種プロパティの値を記憶するための
    /// クラスです。
    /// </summary>
    /// 
    /// <remarks>
    /// DPI に応じて大きさを変更する際に、基準となる値を記憶するために
    /// 使用されます。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    internal class ControlInfo
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Size
        /// 
        /// <summary>
        /// コントロールの大きさを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Size Size { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Margin
        /// 
        /// <summary>
        /// コントロールの外余白 (Margin）を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Padding Margin { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Padding
        /// 
        /// <summary>
        /// コントロールの内余白 (Padding) を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Padding Padding { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// FontSize
        /// 
        /// <summary>
        /// コントロールのフォントサイズを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public double FontSize { get; set; }

        #endregion
    }
}
