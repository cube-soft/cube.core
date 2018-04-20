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
using System;

namespace Cube.Conversions
{
    /* --------------------------------------------------------------------- */
    ///
    /// ByteFormat
    ///
    /// <summary>
    /// バイトサイズの書式に変換する拡張メソッド用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class ByteFormat
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// ToPrettyBytes
        ///
        /// <summary>
        /// バイトサイズを読みやすい文字列に変換します。
        /// </summary>
        ///
        /// <param name="bytes">バイト数</param>
        ///
        /// <returns>バイト数を表す文字列</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string ToPrettyBytes(this long bytes)
        {
            var units = new[] { "Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
            var value = (double)bytes;
            var index = 0;

            while (value > 1000.0)
            {
                value /= 1024.0;
                ++index;
                if (index >= units.Length - 1) break;
            }

            return $"{value:G3} {units[index]}";
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToRoughBytes
        ///
        /// <summary>
        /// バイトサイズの概算値を文字列に変換します。
        /// </summary>
        ///
        /// <param name="bytes">バイト数</param>
        ///
        /// <returns>バイト数を表す文字列</returns>
        ///
        /// <remarks>
        /// Windows の Explorer 等のように 1024 バイト未満の値を "1 KB" と
        /// 出力します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static string ToRoughBytes(this long bytes) =>
            ToPrettyBytes(bytes > 0 ? Math.Max(bytes, 1024) : 0);

        #endregion
    }
}
