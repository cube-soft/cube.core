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
using System.ComponentModel;

namespace Cube
{
    #region QueryEventArgs

    /* --------------------------------------------------------------------- */
    ///
    /// QueryEventArgs
    ///
    /// <summary>
    /// provides methods to create a new instance of the QueryEventArgs(T)
    /// class.
    /// </summary>
    ///
    /// <remarks>
    /// QueryEventArgs(T, U) には対応していません。
    /// QueryEventArgs(T, U) オブジェクトを生成する場合、new 演算子を
    /// 用いて生成して下さい。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public static class QueryEventArgs
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// Initializes a new instance of the QueryEventArgs(T) class with
        /// the specified query.
        /// </summary>
        ///
        /// <param name="query">Query to use for event.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static QueryEventArgs<T> Create<T>(T query) =>
            new QueryEventArgs<T>(query);

        #endregion
    }

    #endregion

    #region QueryEventArgs<T>

    /* --------------------------------------------------------------------- */
    ///
    /// QueryEventArgs(T)
    ///
    /// <summary>
    /// Provides a query and result to use for events.
    /// </summary>
    ///
    /// <remarks>
    /// Query および Result が同じ型を示します。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class QueryEventArgs<T> : QueryEventArgs<T, T>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// QueryEventArgs
        ///
        /// <summary>
        /// Initializes a new instance of the QueryEventArgs class with
        /// the specified query. The Cancel property is set to false.
        /// </summary>
        ///
        /// <param name="query">Query to use for the event.</param>
        ///
        /* ----------------------------------------------------------------- */
        public QueryEventArgs(T query) : this(query, false) { }

        /* ----------------------------------------------------------------- */
        ///
        /// QueryEventArgs
        ///
        /// <summary>
        /// Initializes a new instance of the QueryEventArgs class with
        /// the specified arguments.
        /// </summary>
        ///
        /// <param name="query">Query to use for the event.</param>
        /// <param name="cancel">
        /// true to cancel the event; otherwise, false.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public QueryEventArgs(T query, bool cancel) : this(query, default(T), cancel) { }

        /* ----------------------------------------------------------------- */
        ///
        /// QueryEventArgs
        ///
        /// <summary>
        /// Initializes a new instance of the QueryEventArgs class with
        /// the specified arguments.
        /// </summary>
        ///
        /// <param name="query">Query to use for the event.</param>
        /// <param name="result">Initial value of the result.</param>
        /// <param name="cancel">
        /// true to cancel the event; otherwise, false.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public QueryEventArgs(T query, T result, bool cancel) : base(query, result, cancel) { }

        #endregion
    }

    #endregion

    #region QueryEventArgs<T, U>

    /* --------------------------------------------------------------------- */
    ///
    /// QueryEventArgs(T, U)
    ///
    /// <summary>
    /// Provides a query and result to use for events.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class QueryEventArgs<T, U> : CancelEventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// QueryEventArgs
        ///
        /// <summary>
        /// Initializes a new instance of the QueryEventArgs class with
        /// the specified query. The Cancel property is set to false.
        /// </summary>
        ///
        /// <param name="query">Query to use for the event.</param>
        ///
        /* ----------------------------------------------------------------- */
        public QueryEventArgs(T query) : this(query, false) { }

        /* ----------------------------------------------------------------- */
        ///
        /// QueryEventArgs
        ///
        /// <summary>
        /// Initializes a new instance of the QueryEventArgs class with
        /// the specified arguments.
        /// </summary>
        ///
        /// <param name="query">Query to use for the event.</param>
        /// <param name="cancel">
        /// true to cancel the event; otherwise, false.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public QueryEventArgs(T query, bool cancel) : this(query, default(U), cancel) { }

        /* ----------------------------------------------------------------- */
        ///
        /// QueryEventArgs
        ///
        /// <summary>
        /// Initializes a new instance of the QueryEventArgs class with
        /// the specified arguments.
        /// </summary>
        ///
        /// <param name="query">Query to use for the event.</param>
        /// <param name="result">Initial value of the result.</param>
        /// <param name="cancel">
        /// true to cancel the event; otherwise, false.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public QueryEventArgs(T query, U result, bool cancel) : base(cancel)
        {
            Query  = query;
            Result = result;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Query
        ///
        /// <summary>
        /// Gets the query to use for the event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public T Query { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Result
        ///
        /// <summary>
        /// Gets the result of the event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public U Result { get; set; }

        #endregion
    }

    #endregion

    #region QueryEventHandlers

    /* --------------------------------------------------------------------- */
    ///
    /// QueryEventHandler(T, U)
    ///
    /// <summary>
    /// Represents the method to invoke an event.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public delegate void QueryEventHandler<T, U>(object sender, QueryEventArgs<T, U> e);

    /* --------------------------------------------------------------------- */
    ///
    /// QueryEventHandler(T)
    ///
    /// <summary>
    /// Represents the method to invoke an event.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public delegate void QueryEventHandler<T>(object sender, QueryEventArgs<T> e);

    #endregion
}
