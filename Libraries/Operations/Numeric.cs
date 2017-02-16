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
using System;

namespace Cube.Numeric
{
    /* --------------------------------------------------------------------- */
    ///
    /// Numeric.Operations
    /// 
    /// <summary>
    /// 数値に対する汎用的な操作を定義するための拡張メソッド用クラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public static class Operations
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Times
        ///
        /// <summary>
        /// 指定回数だけ同じ操作を繰り返します。
        /// </summary>
        /// 
        /// <param name="n">繰り返し回数</param>
        /// <param name="action">操作内容</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Times(this int n, Action action)
        {
            for (var i = 0; i < n; ++i) action();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Times
        ///
        /// <summary>
        /// 指定回数だけ同じ操作を繰り返します。
        /// </summary>
        /// 
        /// <param name="n">繰り返し回数</param>
        /// <param name="action">操作内容</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Times(this int n, Action<int> action)
        {
            for (var i = 0; i < n; ++i) action(i);
        }
    }
}
