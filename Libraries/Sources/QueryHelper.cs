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
namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// Query
    ///
    /// <summary>
    /// Provides functionality to create a new instance of the Query(T, U)
    /// or related classes.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Query
    {
        #region NewMessage

        /* ----------------------------------------------------------------- */
        ///
        /// NewMessage
        ///
        /// <summary>
        /// Creates a new instance of the QueryMessage(T, T) class with
        /// the specified query.
        /// </summary>
        ///
        /// <typeparam name="T">type of source and result.</typeparam>
        ///
        /// <param name="src">Source information.</param>
        ///
        /// <returns>QueryMessage(T, T) object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static QueryMessage<T, T> NewMessage<T>(T src) =>
            NewMessage<T, T>(src);

        /* ----------------------------------------------------------------- */
        ///
        /// NewMessage
        ///
        /// <summary>
        /// Creates a new instance of the QueryMessage(T, U) class with
        /// the specified query.
        /// </summary>
        ///
        /// <typeparam name="T">type of query source.</typeparam>
        /// <typeparam name="U">type of result value.</typeparam>
        ///
        /// <param name="src">Source information.</param>
        ///
        /// <returns>QueryMessage(T, U) object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static QueryMessage<T, U> NewMessage<T, U>(T src) =>
            NewMessage(src, default(U));

        /* ----------------------------------------------------------------- */
        ///
        /// NewMessage
        ///
        /// <summary>
        /// Creates a new instance of the QueryMessage(T, U) class with
        /// the specified query and default value.
        /// </summary>
        ///
        /// <typeparam name="T">type of query source.</typeparam>
        /// <typeparam name="U">type of result value.</typeparam>
        ///
        /// <param name="src">Source information.</param>
        /// <param name="value">Default value of the message.</param>
        ///
        /// <returns>QueryMessage(T, U) object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static QueryMessage<T, U> NewMessage<T, U>(T src, U value) => new QueryMessage<T, U>
        {
            Source = src,
            Value  = value,
        };

        #endregion
    }
}
