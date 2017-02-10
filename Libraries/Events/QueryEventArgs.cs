/* ------------------------------------------------------------------------- */
///
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///  http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
///
/* ------------------------------------------------------------------------- */
using System;
using System.ComponentModel;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// QueryEventArgs(TQuery, TResult)
    /// 
    /// <summary>
    /// クエリーデータを受け渡すためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class QueryEventArgs<TQuery, TResult> : CancelEventArgs
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
        /* ----------------------------------------------------------------- */
        public QueryEventArgs(TQuery query) : this(query, false) { }
        
        /* ----------------------------------------------------------------- */
        ///
        /// QueryEventArgs
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public QueryEventArgs(TQuery query, bool cancel)
            : this(query, default(TResult), cancel)
        { }

        /* ----------------------------------------------------------------- */
        ///
        /// QueryEventArgs
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public QueryEventArgs(TQuery query, TResult result, bool cancel) : base(cancel)
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
        public TQuery Query { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Result
        /// 
        /// <summary>
        /// クエリーデータに対する結果を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TResult Result { get; set; }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// QueryEventHandler(TValue)
    /// 
    /// <summary>
    /// イベントを処理するメソッドを表します。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public delegate void QueryEventHandler<TQuery, TResult>(object sender, QueryEventArgs<TQuery, TResult> e);
}
