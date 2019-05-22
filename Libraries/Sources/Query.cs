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
        /// <typeparam name="T">type of Query and Value.</typeparam>
        ///
        /// <param name="query">Query of the message.</param>
        ///
        /// <returns>QueryMessage(T, T) object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static QueryMessage<T, T> NewMessage<T>(T query) =>
            NewMessage<T, T>(query);

        /* ----------------------------------------------------------------- */
        ///
        /// NewMessage
        ///
        /// <summary>
        /// Creates a new instance of the QueryMessage(T, U) class with
        /// the specified query.
        /// </summary>
        ///
        /// <typeparam name="T">type of Query.</typeparam>
        /// <typeparam name="U">type of Value.</typeparam>
        ///
        /// <param name="query">Query of the message.</param>
        ///
        /// <returns>QueryMessage(T, U) object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static QueryMessage<T, U> NewMessage<T, U>(T query) =>
            new QueryMessage<T, U> { Query = query };

        #endregion

        #region Wrap

        /* ----------------------------------------------------------------- */
        ///
        /// Wrap
        ///
        /// <summary>
        /// Wraps the specified value as an IQuery(T, T) interface.
        /// </summary>
        ///
        /// <typeparam name="T">type of Query.</typeparam>
        ///
        /// <param name="value">
        /// Value that is always used as the requested result.
        /// </param>
        ///
        /// <returns>IQuery(T, T) object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IQuery<T, T> Wrap<T>(T value) => Wrap<T, T>(value);

        /* ----------------------------------------------------------------- */
        ///
        /// Wrap
        ///
        /// <summary>
        /// Wraps the specified value as an IQuery(T, T) interface.
        /// </summary>
        ///
        /// <typeparam name="T">type of Query.</typeparam>
        ///
        /// <param name="value">
        /// Value that is always used as the requested result.
        /// </param>
        ///
        /// <param name="once">
        /// Value indicating whether to allow only once.
        /// </param>
        ///
        /// <returns>IQuery(T, T) object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IQuery<T, T> Wrap<T>(T value, bool once) => Wrap<T, T>(value, once);

        /* ----------------------------------------------------------------- */
        ///
        /// Wrap
        ///
        /// <summary>
        /// Wraps the specified value as an IQuery(T, U) interface.
        /// </summary>
        ///
        /// <typeparam name="T">type of Query.</typeparam>
        /// <typeparam name="U">type of Value.</typeparam>
        ///
        /// <param name="value">
        /// Value that is always used as the requested result.
        /// </param>
        ///
        /// <returns>IQuery(T, U) object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IQuery<T, U> Wrap<T, U>(U value) => Wrap<T, U>(value, false);

        /* ----------------------------------------------------------------- */
        ///
        /// Wrap
        ///
        /// <summary>
        /// Wraps the specified value as an IQuery(T, U) interface.
        /// </summary>
        ///
        /// <typeparam name="T">type of Query.</typeparam>
        /// <typeparam name="U">type of Value.</typeparam>
        ///
        /// <param name="value">
        /// Value that is always used as the requested result.
        /// </param>
        ///
        /// <param name="once">
        /// Value indicating whether to allow only once.
        /// </param>
        ///
        /// <returns>IQuery(T, U) object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IQuery<T, U> Wrap<T, U>(U value, bool once) =>
            once ?
            new OnceQuery<T, U>(e => e.Value = value) as IQuery<T, U> :
            new Query<T, U>(e => e.Value = value) as IQuery<T, U>;

        #endregion
    }
}
