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
    /* --------------------------------------------------------------------- */
    ///
    /// QueryEventArgs(T, U)
    ///
    /// <summary>
    /// クエリーデータを受け渡すためのクラスです。
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
        /// Cancel の値を false に設定してオブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="query">クエリーデータ</param>
        ///
        /* ----------------------------------------------------------------- */
        public QueryEventArgs(T query) : this(query, false) { }

        /* ----------------------------------------------------------------- */
        ///
        /// QueryEventArgs
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="query">クエリーデータ</param>
        /// <param name="cancel">キャンセルするかどうか</param>
        ///
        /* ----------------------------------------------------------------- */
        public QueryEventArgs(T query, bool cancel) : this(query, default(U), cancel) { }

        /* ----------------------------------------------------------------- */
        ///
        /// QueryEventArgs
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="query">クエリーデータ</param>
        /// <param name="result">結果の初期値</param>
        /// <param name="cancel">キャンセルするかどうか</param>
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
        /// イベント発生元から受け取ったクエリーデータを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public T Query { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Result
        ///
        /// <summary>
        /// クエリーデータに対する結果を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public U Result { get; set; }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// QueryEventArgs(T)
    ///
    /// <summary>
    /// クエリーデータを受け渡すためのクラスです。
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
        /// Cancel の値を false に設定してオブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="query">クエリーデータ</param>
        ///
        /* ----------------------------------------------------------------- */
        public QueryEventArgs(T query) : this(query, false) { }

        /* ----------------------------------------------------------------- */
        ///
        /// QueryEventArgs
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="query">クエリーデータ</param>
        /// <param name="cancel">キャンセルするかどうか</param>
        ///
        /* ----------------------------------------------------------------- */
        public QueryEventArgs(T query, bool cancel) : this(query, default(T), cancel) { }

        /* ----------------------------------------------------------------- */
        ///
        /// QueryEventArgs
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="query">クエリーデータ</param>
        /// <param name="result">結果の初期値</param>
        /// <param name="cancel">キャンセルするかどうか</param>
        ///
        /* ----------------------------------------------------------------- */
        public QueryEventArgs(T query, T result, bool cancel) : base(query, result, cancel) { }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// QueryEventHandler(T, U)
    ///
    /// <summary>
    /// イベントを処理するメソッドを表します。
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
    /// イベントを処理するメソッドを表します。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public delegate void QueryEventHandler<T>(object sender, QueryEventArgs<T> e);
}
