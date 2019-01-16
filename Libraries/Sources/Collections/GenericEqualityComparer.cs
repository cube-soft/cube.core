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
    /// GenericEqualityComparer(T)
    ///
    /// <summary>
    /// Provides functionality to convert from the Func(T, T, bool) to
    /// the instance of EqualityComparer(T) inherited class.
    /// </summary>
    ///
    /// <typeparam name="T">The type of objects to compare.</typeparam>
    ///
    /* --------------------------------------------------------------------- */
    public class GenericEqualityComparer<T> : EqualityComparer<T>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// GenericEqualityComparer(T)
        ///
        /// <summary>
        /// Initializes a new instance of the GenericEqualityComparer(T)
        /// class with the specified comparer.
        /// </summary>
        ///
        /// <param name="src">Function to compare.</param>
        ///
        /* ----------------------------------------------------------------- */
        public GenericEqualityComparer(Func<T, T, bool> src) : this(src, e => 0) { }

        /* ----------------------------------------------------------------- */
        ///
        /// GenericEqualityComparer(T)
        ///
        /// <summary>
        /// Initializes a new instance of the GenericEqualityComparer(T)
        /// class with the specified arguments.
        /// </summary>
        ///
        /// <param name="src">Function to compare.</param>
        /// <param name="hash">Function to convert to the hash code.</param>
        ///
        /* ----------------------------------------------------------------- */
        public GenericEqualityComparer(Func<T, T, bool> src, Func<T, int> hash)
        {
            _comparer = src;
            _hash     = hash;
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Equals
        ///
        /// <summary>
        /// 2 つのオブジェクトが等しいかどうかを判別します。
        /// </summary>
        ///
        /// <param name="x">比較する最初のオブジェクト</param>
        /// <param name="y">比較する 2 番目のオブジェクト</param>
        ///
        /// <returns>比較結果</returns>
        ///
        /* ----------------------------------------------------------------- */
        public override bool Equals(T x, T y) => _comparer(x, y);

        /* ----------------------------------------------------------------- */
        ///
        /// GetHashCode
        ///
        /// <summary>
        /// GenericEqualityComparer(T) は GetHashcode(T) を必要とする
        /// 操作を許可しません。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public override int GetHashCode(T obj) => _hash(obj);

        #endregion

        #region Fields
        private readonly Func<T, T, bool> _comparer;
        private readonly Func<T, int> _hash;
        #endregion
    }
}
