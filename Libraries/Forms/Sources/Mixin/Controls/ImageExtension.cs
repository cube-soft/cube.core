﻿/* ------------------------------------------------------------------------- */
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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Cube.Mixin.Forms.Controls
{
    /* --------------------------------------------------------------------- */
    ///
    /// ImageExtension
    ///
    /// <summary>
    /// Provides extended methods of the Image class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class ImageExtension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// ToImageList
        ///
        /// <summary>
        /// Converts the specified Image collection to an ImageList object.
        /// </summary>
        ///
        /// <param name="src">Image collection.</param>
        ///
        /// <returns>ImageList object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static ImageList ToImageList(this IEnumerable<Image> src) =>
            ToImageList(src, new(16, 16));

        /* ----------------------------------------------------------------- */
        ///
        /// ToImageList
        ///
        /// <summary>
        /// Converts the specified Image collection to an ImageList object.
        /// </summary>
        ///
        /// <param name="src">Image collection.</param>
        /// <param name="size">Image size.</param>
        ///
        /// <returns>ImageList object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static ImageList ToImageList(this IEnumerable<Image> src, Size size) =>
            ToImageList(src, size, ColorDepth.Depth32Bit);

        /* ----------------------------------------------------------------- */
        ///
        /// ToImageList
        ///
        /// <summary>
        /// Converts the specified Image collection to an ImageList object.
        /// </summary>
        ///
        /// <param name="src">Image collection.</param>
        /// <param name="size">Image size.</param>
        /// <param name="depth">Bit depth.</param>
        ///
        /// <returns>ImageList object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static ImageList ToImageList(this IEnumerable<Image> src, Size size, ColorDepth depth)
        {
            var dest = new ImageList
            {
                ImageSize  = size,
                ColorDepth = depth,
            };
            foreach (var image in src) dest.Images.Add(image);
            return dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToImageList
        ///
        /// <summary>
        /// Converts the specified Icon collection to an ImageList object.
        /// </summary>
        ///
        /// <param name="src">Icon collection.</param>
        ///
        /// <returns>ImageList object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static ImageList ToImageList(this IEnumerable<Icon> src) =>
            ToImageList(src, new(16, 16));

        /* ----------------------------------------------------------------- */
        ///
        /// ToImageList
        ///
        /// <summary>
        /// Converts the specified Icon collection to an ImageList object.
        /// </summary>
        ///
        /// <param name="src">Icon collection.</param>
        /// <param name="size">Image size.</param>
        ///
        /// <returns>ImageList object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static ImageList ToImageList(this IEnumerable<Icon> src, Size size) =>
            ToImageList(src, size, ColorDepth.Depth32Bit);

        /* ----------------------------------------------------------------- */
        ///
        /// ToImageList
        ///
        /// <summary>
        /// Converts the specified Icon collection to an ImageList object.
        /// </summary>
        ///
        /// <param name="src">Icon collection.</param>
        /// <param name="size">Image size.</param>
        /// <param name="depth">Bit depth.</param>
        ///
        /// <returns>ImageList object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static ImageList ToImageList(this IEnumerable<Icon> src, Size size, ColorDepth depth)
        {
            var dest = new ImageList
            {
                ImageSize  = size,
                ColorDepth = depth,
            };
            foreach (var image in src) dest.Images.Add(image);
            return dest;
        }

        #endregion
    }
}
