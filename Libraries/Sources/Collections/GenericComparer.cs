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
using System.Collections.Generic;

namespace Cube.Collections
{
    /* --------------------------------------------------------------------- */
    ///
    /// GenericComparer(T)
    ///
    /// <summary>
    /// Func(T, T, int) を Comparer(T) に変換するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class GenericComparer<T> : Comparer<T>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// GenericComparer(T)
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="src">関数オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public GenericComparer(Func<T, T, int> src)
        {
            _comparer = src;
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Compare
        ///
        /// <summary>
        /// 2 つのオブジェクトを比較します。
        /// </summary>
        ///
        /// <param name="x">比較する最初のオブジェクト</param>
        /// <param name="y">比較する 2 番目のオブジェクト</param>
        ///
        /// <returns>比較結果</returns>
        ///
        /* ----------------------------------------------------------------- */
        public override int Compare(T x, T y) => _comparer(x, y);

        #endregion

        #region Fields
        private readonly Func<T, T, int> _comparer;
        #endregion
    }
}
