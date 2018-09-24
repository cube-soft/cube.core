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
    /* --------------------------------------------------------------------- */
    ///
    /// OnceQuery(T)
    ///
    /// <summary>
    /// 1 度だけ Query(T) を実行するクラスです。
    /// 2 回目以降の実行時には TwiceException が送出されます。
    /// </summary>
    ///
    /// <remarks>
    /// 予め決まっている結果を一度だけ返す時などに利用します。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class OnceQuery<T> : IQuery<T>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// OnceQuery
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="result">Request 時に設定される結果</param>
        ///
        /* ----------------------------------------------------------------- */
        public OnceQuery(T result) : this(e =>
        {
            e.Result = result;
            e.Cancel = false;
        }) { }

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
        public OnceQuery(Action<QueryEventArgs<T>> callback)
        {
            _query = new Query<T>(callback);
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Request
        ///
        /// <summary>
        /// 問い合わせを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Request(QueryEventArgs<T> value)
        {
            var dest = Interlocked.Exchange(ref _query, null);
            if (dest != null) dest.Request(value);
            else throw new TwiceException();
        }

        #endregion

        #region Fields
        private Query<T> _query;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// OnceQuery(T, U)
    ///
    /// <summary>
    /// 1 度だけ Query(T, U) を実行するクラスです。
    /// 2 回目以降の実行時には TwiceException が送出されます。
    /// </summary>
    ///
    /// <remarks>
    /// 予め決まっている結果を一度だけ返す時などに利用します。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class OnceQuery<T, U> : IQuery<T, U>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// OnceQuery
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="result">Request 時に設定される結果</param>
        ///
        /* ----------------------------------------------------------------- */
        public OnceQuery(U result) : this(e =>
        {
            e.Result = result;
            e.Cancel = false;
        }) { }

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
        public OnceQuery(Action<QueryEventArgs<T, U>> callback)
        {
            _query = new Query<T, U>(callback);
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Request
        ///
        /// <summary>
        /// 問い合わせを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Request(QueryEventArgs<T, U> value)
        {
            var dest = Interlocked.Exchange(ref _query, null);
            if (dest != null) dest.Request(value);
            else throw new TwiceException();
        }

        #endregion

        #region Fields
        private Query<T, U> _query;
        #endregion
    }
}
