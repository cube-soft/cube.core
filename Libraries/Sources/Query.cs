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
using System.Threading;

namespace Cube
{
    #region Query(T)

    /* --------------------------------------------------------------------- */
    ///
    /// IQuery(T)
    ///
    /// <summary>
    /// Represents the query provider.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public interface IQuery<T>
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Request
        ///
        /// <summary>
        /// Invokes the request with the specified arguments.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        void Request(QueryEventArgs<T> value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Query(T)
    ///
    /// <summary>
    /// Represents the IQuery(T) implementation.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Query<T> : IQuery<T>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Query
        ///
        /// <summary>
        /// Initializes a new instance of the Query class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Query()
        {
            _context = SynchronizationContext.Current;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Query
        ///
        /// <summary>
        /// Initializes a new instance of the Query class with the
        /// specified callback.
        /// </summary>
        ///
        /// <param name="callback">Callback function.</param>
        ///
        /* ----------------------------------------------------------------- */
        public Query(Action<QueryEventArgs<T>> callback) : this()
        {
            Requested += (s, e) => callback(e);
        }

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// Requested
        ///
        /// <summary>
        /// Occurs when the user request is received.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event QueryEventHandler<T> Requested;

        /* ----------------------------------------------------------------- */
        ///
        /// OnRequested
        ///
        /// <summary>
        /// Raises the Requested event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public virtual void OnRequested(QueryEventArgs<T> e)
        {
            if (Requested != null)
            {
                if (_context != null) _context.Send(_ => Requested(this, e), null);
                else Requested(this, e);
            }
            else e.Cancel = true;
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Request
        ///
        /// <summary>
        /// Invokes the request with the specified arguments.
        /// </summary>
        ///
        /// <remarks>
        /// 問い合わせの結果が無効な場合、Cancel プロパティが true に
        /// 設定されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public void Request(QueryEventArgs<T> value) => OnRequested(value);

        #endregion

        #region Fields
        private readonly SynchronizationContext _context;
        #endregion
    }

    #endregion

    #region Query(T, U)

    /* --------------------------------------------------------------------- */
    ///
    /// IQuery(T, U)
    ///
    /// <summary>
    /// Represents the query provider.
    /// </summary>
    ///
    /// <remarks>
    /// Query と Result の型が同じ場合 IQuery(T, U) の代わりに IQuery(T) を
    /// 実装する事を検討して下さい。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public interface IQuery<T, U>
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Request
        ///
        /// <summary>
        /// Invokes the request with the specified arguments.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        void Request(QueryEventArgs<T, U> value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Query(T, U)
    ///
    /// <summary>
    /// Represents the IQuery(T, U) implementation.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Query<T, U> : IQuery<T, U>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Query
        ///
        /// <summary>
        /// Initializes a new instance of the Query class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Query()
        {
            _context = SynchronizationContext.Current;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Query
        ///
        /// <summary>
        /// Initializes a new instance of the Query class with the
        /// specified callback.
        /// </summary>
        ///
        /// <param name="callback">Callback function.</param>
        ///
        /* ----------------------------------------------------------------- */
        public Query(Action<QueryEventArgs<T, U>> callback) : this()
        {
            Requested += (s, e) => callback(e);
        }

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// Requested
        ///
        /// <summary>
        /// Occurs when the user request is received.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event QueryEventHandler<T, U> Requested;

        /* ----------------------------------------------------------------- */
        ///
        /// OnRequested
        ///
        /// <summary>
        /// Raises the Requested event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public virtual void OnRequested(QueryEventArgs<T, U> e)
        {
            if (Requested != null)
            {
                if (_context != null) _context.Send(_ => Requested(this, e), null);
                else Requested(this, e);
            }
            else e.Cancel = true;
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Request
        ///
        /// <summary>
        /// Invokes the request with the specified arguments.
        /// </summary>
        ///
        /// <remarks>
        /// 問い合わせの結果が無効な場合、Cancel プロパティが true に
        /// 設定されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public void Request(QueryEventArgs<T, U> value) => OnRequested(value);

        #endregion

        #region Fields
        private readonly SynchronizationContext _context;
        #endregion
    }

    #endregion
}
