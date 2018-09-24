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
using System.Collections.Generic;

namespace Cube.Collections
{
    /* --------------------------------------------------------------------- */
    ///
    /// KeyValuePair
    ///
    /// <summary>
    /// KeyValuePair(T, U) の拡張用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class KeyValuePair
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// KeyValuePair(T, U) オブジェクトを生成します。
        /// </summary>
        ///
        /// <param name="key">キー</param>
        /// <param name="value">値</param>
        ///
        /// <returns>KeyValuePair(T, U) オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static KeyValuePair<T, U> Create<T, U>(T key, U value) =>
            new KeyValuePair<T, U>(key, value);

        #endregion
    }
}
