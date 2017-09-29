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
using System.Collections.Generic;
using System.ComponentModel;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// EnumerableEventArgs(TValue)
    ///
    /// <summary>
    /// イベントハンドラに特定の型のコレクションを伝搬させるための
    /// クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class EnumerableEventArgs<TValue> : EventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// EnumerableEventArgs
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        /// 
        /// <param name="value">設定値</param>
        ///
        /* ----------------------------------------------------------------- */
        public EnumerableEventArgs(IEnumerable<TValue> value)
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
        public IEnumerable<TValue> Value { get; }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// EnumerableCancelEventArgs(TValue)
    ///
    /// <summary>
    /// イベントハンドラに特定の型のコレクションを伝搬させるための
    /// クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class EnumerableCancelEventArgs<TValue> : CancelEventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// EnumerableCancelEventArgs
        /// 
        /// <summary>
        /// Cancel の値を false に設定してオブジェクトを初期化します。
        /// </summary>
        /// 
        /// <param name="value">設定値</param>
        ///
        /* ----------------------------------------------------------------- */
        public EnumerableCancelEventArgs(IEnumerable<TValue> value)
            : this(value, false) { }

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
        public EnumerableCancelEventArgs(IEnumerable<TValue> value, bool cancel)
            : base(cancel)
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
        public IEnumerable<TValue> Value { get; }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// EnumerableEventArgs
    ///
    /// <summary>
    /// EnumerableEventArgs(T), EnumerableEventArgs(T) オブジェクトを
    /// 生成するための補助クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class EnumerableEventArgs
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Create
        /// 
        /// <summary>
        /// EnumerableEventArgs(T) オブジェクトを生成します。
        /// </summary>
        /// 
        /// <param name="value">設定値</param>
        ///
        /* ----------------------------------------------------------------- */
        public static EnumerableEventArgs<T> Create<T>(IEnumerable<T> value)
            => new EnumerableEventArgs<T>(value);

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        /// 
        /// <summary>
        /// EnumerableCancelEventArgs(T) オブジェクトを生成します。
        /// </summary>
        /// 
        /// <param name="value">設定値</param>
        /// <param name="cancel">キャンセルするかどうか</param>
        ///
        /* ----------------------------------------------------------------- */
        public static EnumerableCancelEventArgs<T> Create<T>(IEnumerable<T> value, bool cancel)
            => new EnumerableCancelEventArgs<T>(value, cancel);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// EnumerableEventHandler(TValue)
    /// 
    /// <summary>
    /// イベントを処理するメソッドを表します。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public delegate void EnumerableEventHandler<TValue>(object sender, EnumerableEventArgs<TValue> e);

    /* --------------------------------------------------------------------- */
    ///
    /// EnumerableCancelEventHandler(TValue)
    /// 
    /// <summary>
    /// イベントを処理するメソッドを表します。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public delegate void EnumerableCancelEventHandler<TValue>(object sender, EnumerableCancelEventArgs<TValue> e);
}
