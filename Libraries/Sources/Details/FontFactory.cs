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
using System.Drawing;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// FontFactory
    ///
    /// <summary>
    /// Cube.Forms で使用する既定のフォントオブジェクトを生成するための
    /// クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal static class FontFactory
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// フォントオブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static Font Create(Font hint) => Create(hint.Size, hint.Style, hint.Unit);

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// フォントオブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static Font Create(float size, FontStyle style, GraphicsUnit unit)
        {
            var primary   = "Meiryo UI";
            var secondary = SystemFonts.DefaultFont.FontFamily.Name;

            var dest = new Font(primary, size, style, unit);
            return dest.Name == primary ?
                   dest :
                   new Font(secondary, size, style, unit);
        }

        #endregion
    }
}
