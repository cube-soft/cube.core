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

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// QueryValue(T)
    ///
    /// <summary>
    /// Provides functionality to wrap the specified value as the
    /// IQuery(T) interface.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class QueryValue<T> : IQuery<T>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// QueryValue
        ///
        /// <summary>
        /// Initializes a new instance of the QueryValue class.
        /// </summary>
        ///
        /// <param name="value">
        /// Result when the Request method is invoked.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public QueryValue(T value)
        {
            Value  = value;
            _query = new Query<T>(e =>
            {
                e.Result = value;
                e.Cancel = false;
            });
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Value
        ///
        /// <summary>
        /// Gets the value when the Request method is invoked.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public T Value { get; }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Request
        ///
        /// <summary>
        /// Requests with the specified query.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Request(QueryEventArgs<T> value) => _query.Request(value);

        #endregion

        #region Fields
        private Query<T> _query;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// QueryValue(T, U)
    ///
    /// <summary>
    /// Provides functionality to wrap the specified value as the
    /// IQuery(T, U) interface.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class QueryValue<T, U> : IQuery<T, U>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// QueryValue
        ///
        /// <summary>
        /// Initializes a new instance of the QueryValue class.
        /// </summary>
        ///
        /// <param name="value">
        /// Result when the Request method is invoked.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public QueryValue(U value)
        {
            Value  = value;
            _query = new Query<T, U>(e =>
            {
                e.Result = Value;
                e.Cancel = false;
            });
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnceQuery
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="callback">Request 時に実行される内容</param>
        ///
        /* ----------------------------------------------------------------- */
        public QueryValue(Action<QueryEventArgs<T, U>> callback)
        {
            _query = new Query<T, U>(callback);
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Value
        ///
        /// <summary>
        /// Gets the value when the Request method is invoked.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public U Value { get; }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Request
        ///
        /// <summary>
        /// Requests with the specified query.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Request(QueryEventArgs<T, U> value) => _query.Request(value);

        #endregion

        #region Fields
        private Query<T, U> _query;
        #endregion
    }
}
