/* ------------------------------------------------------------------------- */
///
/// FormatExtensions.cs
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

namespace Cube.Extensions
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Extensions.ByteFormatExtensions
    /// 
    /// <summary>
    /// バイトサイズの書式に関する拡張メソッドを定義したクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class ByteFormatExtensions
    {
        /* ----------------------------------------------------------------- */
        ///
        /// ToPrettyBytes
        /// 
        /// <summary>
        /// バイトサイズを読みやすい文字列に変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string ToPrettyBytes(this UInt64 bytes)
        {
            var units = new string[] { "Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

            var value = (double)bytes;
            var n = 0;

            while (value > 1000.0)
            {
                value /= 1024.0;
                ++n;
                if (n >= units.Length - 1) break;
            }

            return string.Format("{0:G3} {1}", value, units[n]);
        }
    }
}
