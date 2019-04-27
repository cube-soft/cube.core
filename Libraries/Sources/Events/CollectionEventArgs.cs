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
using System.Collections.Generic;
using System.ComponentModel;

namespace Cube
{
    #region CollectionEventArgs

    /* --------------------------------------------------------------------- */
    ///
    /// CollectionEventArgs
    ///
    /// <summary>
    /// CollectionEventArgs(T), CollectionEventArgs(T) オブジェクトを
    /// 生成するための補助クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class CollectionEventArgs
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// CollectionEventArgs(T) オブジェクトを生成します。
        /// </summary>
        ///
        /// <param name="value">設定値</param>
        ///
        /* ----------------------------------------------------------------- */
        public static CollectionEventArgs<T> Create<T>(IEnumerable<T> value) =>
            new CollectionEventArgs<T>(value);

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// CollectionEventArgs(T) オブジェクトを生成します。
        /// </summary>
        ///
        /// <param name="value">設定値</param>
        /// <param name="cancel">キャンセルするかどうか</param>
        ///
        /* ----------------------------------------------------------------- */
        public static CollectionCancelEventArgs<T> Create<T>(IEnumerable<T> value, bool cancel) =>
            new CollectionCancelEventArgs<T>(value, cancel);

        #endregion
    }

    #endregion

    #region CollectionEventArgs<T>

    /* --------------------------------------------------------------------- */
    ///
    /// CollectionEventArgs(T)
    ///
    /// <summary>
    /// イベントハンドラに特定の型のコレクションを伝搬させるための
    /// クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class CollectionEventArgs<T> : EventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// CollectionEventArgs(T)
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="value">設定値</param>
        ///
        /* ----------------------------------------------------------------- */
        public CollectionEventArgs(IEnumerable<T> value)
        {
            Value = value;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Value
        ///
        /// <summary>
        /// 値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IEnumerable<T> Value { get; }

        #endregion
    }

    #endregion

    #region CollectionCancelEventArgs<T>

    /* --------------------------------------------------------------------- */
    ///
    /// CollectionCancelEventArgs(T)
    ///
    /// <summary>
    /// イベントハンドラに特定の型のコレクションを伝搬させるための
    /// クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class CollectionCancelEventArgs<T> : CancelEventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// EnumerableCancelEventArgs(T)
        ///
        /// <summary>
        /// Cancel の値を false に設定してオブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="value">設定値</param>
        ///
        /* ----------------------------------------------------------------- */
        public CollectionCancelEventArgs(IEnumerable<T> value) :
            this(value, false) { }

        /* ----------------------------------------------------------------- */
        ///
        /// EnumerableCancelEventArgs
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="value">設定値</param>
        /// <param name="cancel">キャンセルするかどうか</param>
        ///
        /* ----------------------------------------------------------------- */
        public CollectionCancelEventArgs(IEnumerable<T> value, bool cancel) :
            base(cancel)
        {
            Value = value;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Value
        ///
        /// <summary>
        /// 値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IEnumerable<T> Value { get; }

        #endregion
    }

    #endregion

    #region CollectionEventHandlers

    /* --------------------------------------------------------------------- */
    ///
    /// CollectionEventHandler(T)
    ///
    /// <summary>
    /// イベントを処理するメソッドを表します。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public delegate void CollectionEventHandler<T>(object sender, CollectionEventArgs<T> e);

    /* --------------------------------------------------------------------- */
    ///
    /// CollectionCancelEventHandler(T)
    ///
    /// <summary>
    /// イベントを処理するメソッドを表します。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public delegate void CollectionCancelEventHandler<T>(object sender, CollectionCancelEventArgs<T> e);

    #endregion
}
