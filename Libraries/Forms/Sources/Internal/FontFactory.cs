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
    /// Provides functionality to create a default Font object.
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
        /// Creates a new instance of the Font class with the specified
        /// arguments.
        /// </summary>
        ///
        /// <param name="hint">Hint object.</param>
        ///
        /// <returns>Font object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static Font Create(Font hint) => Create(hint.Size, hint.Style, hint.Unit);

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// Creates a new instance of the Font class with the specified
        /// arguments.
        /// </summary>
        ///
        /// <param name="size">Font size.</param>
        /// <param name="style">Font style.</param>
        /// <param name="unit">Font size unit.</param>
        ///
        /// <returns>Font object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static Font Create(float size, FontStyle style, GraphicsUnit unit)
        {
            var f0 = "Meiryo UI";
            var f1 = SystemFonts.DefaultFont.FontFamily.Name;

            var dest = new Font(f0, size, style, unit);
            return dest.Name == f0 ? dest : new(f1, size, style, unit);
        }

        #endregion
    }
}
