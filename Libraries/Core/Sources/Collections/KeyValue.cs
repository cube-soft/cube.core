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
    /// KeyValue
    ///
    /// <summary>
    /// Provides factory methods of the KeyValuePair(T, U) class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class KeyValue
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Make
        ///
        /// <summary>
        /// Creates a new instance of the KeyValuePair(T, U) class with the
        /// specified key and value.
        /// </summary>
        ///
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        ///
        /// <returns>KeyValuePair(T, U) object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static KeyValuePair<T, U> Make<T, U>(T key, U value) => new(key, value);

        #endregion
    }
}
