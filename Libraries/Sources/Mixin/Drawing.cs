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
using System.IO;
using System.Windows.Media.Imaging;

namespace Cube.Mixin.Drawing
{
    /* --------------------------------------------------------------------- */
    ///
    /// XuiExtension
    ///
    /// <summary>
    /// Provides the extended method of the Image class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class XuiExtension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// ToBitmapImage
        ///
        /// <summary>
        /// Converts to the BitmapImage object.
        /// </summary>
        ///
        /// <param name="src">Image object.</param>
        ///
        /// <returns>BitmapImage object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static BitmapImage ToBitmapImage(this System.Drawing.Image src) =>
            src.ToBitmapImage(false);

        /* ----------------------------------------------------------------- */
        ///
        /// ToBitmapImage
        ///
        /// <summary>
        /// Converts to the BitmapImage object.
        /// </summary>
        ///
        /// <param name="src">Image object.</param>
        /// <param name="dispose">Whether disposing the source.</param>
        ///
        /// <returns>BitmapImage object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static BitmapImage ToBitmapImage(this System.Drawing.Image src, bool dispose)
        {
            if (src == null) return default;

            using (var ss = new StreamProxy(new MemoryStream()))
            {
                src.Save(ss, System.Drawing.Imaging.ImageFormat.Png);
                var dest = new BitmapImage();
                dest.BeginInit();
                dest.CacheOption = BitmapCacheOption.OnLoad;
                dest.CreateOptions = BitmapCreateOptions.None;
                dest.StreamSource = ss;
                dest.EndInit();
                if (dest.CanFreeze) dest.Freeze();
                if (dispose) src.Dispose();
                return dest;
            }
        }

        #endregion
    }
}
