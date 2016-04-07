/* ------------------------------------------------------------------------- */
///
/// Images.cs
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
using System.Drawing;

namespace Cube.Forms.Images
{
    /* --------------------------------------------------------------------- */
    ///
    /// Images.Operations
    /// 
    /// <summary>
    /// System.Drawing.Image の拡張メソッドを定義したクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Operations
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Reduce
        /// 
        /// <summary>
        /// リサイズします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static Image Reduce(this Image src, Size newsize)
        {
            var scale = src.Width > src.Height ?
                        Math.Min(newsize.Width / (double)src.Width, 1.0) :
                        Math.Min(newsize.Height / (double)src.Height, 1.0);

            var width  = (int)(src.Width * scale);
            var height = (int)(src.Height * scale);

            var x = (newsize.Width - width) / 2;
            var y = (newsize.Height - height) / 2;

            var dest = new Bitmap(newsize.Width, newsize.Height);
            using (var gs = Graphics.FromImage(dest)) gs.DrawImage(src, x, y, width, height);
            return dest;
        }
    }
}
