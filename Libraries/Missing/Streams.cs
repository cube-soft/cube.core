/* ------------------------------------------------------------------------- */
///
/// Streams.cs
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
using IoEx = System.IO;

namespace Cube.Streams
{
    /* --------------------------------------------------------------------- */
    ///
    /// Streams.Operations
    /// 
    /// <summary>
    /// IO.Stream クラスの拡張メソッド用クラスです。
    /// </summary>
    /// 
    /// <remarks>
    /// .NET Framework 4 以降に追加されたメソッドを拡張メソッドとして補完します。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public static class Operations
    {
        /* ----------------------------------------------------------------- */
        ///
        /// CopyTo
        /// 
        /// <summary>
        /// 別のストリームへ内容をコピーします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static void CopyTo(this IoEx.Stream src, IoEx.Stream dest)
        {
            var buffer = new byte[16 * 1024];
            var result = 0;

            while ((result = src.Read(buffer, 0, buffer.Length)) > 0)
            {
                dest.Write(buffer, 0, result);
            }
        }
    }
}
