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
    /// KeyValueEventArgs(T, U)
    ///
    /// <summary>
    /// イベントハンドラに特定の型の Key-Value ペアを渡すためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class KeyValueEventArgs<T, U> : EventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// KeyValueEventArgs
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="key">設定するキー</param>
        /// <param name="value">設定値</param>
        ///
        /* ----------------------------------------------------------------- */
        public KeyValueEventArgs(T key, U value) : base()
        {
            Key   = key;
            Value = value;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Key
        ///
        /// <summary>
        /// キーを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public T Key { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Value
        ///
        /// <summary>
        /// 値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public U Value { get; }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// KeyValueCancelEventArgs(T, U)
    ///
    /// <summary>
    /// イベントハンドラに特定の型の Key-Value ペアを渡すためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class KeyValueCancelEventArgs<T, U> : CancelEventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// KeyValueCancelEventArgs
        ///
        /// <summary>
        /// Cancel の値を false に設定してオブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="key">設定するキー</param>
        /// <param name="value">設定値</param>
        ///
        /* ----------------------------------------------------------------- */
        public KeyValueCancelEventArgs(T key, U value) :
            this(key, value, false) { }

        /* ----------------------------------------------------------------- */
        ///
        /// KeyValueCancelEventArgs
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="key">設定するキー</param>
        /// <param name="value">設定値</param>
        /// <param name="cancel">キャンセルするかどうか</param>
        ///
        /* ----------------------------------------------------------------- */
        public KeyValueCancelEventArgs(T key, U value, bool cancel) : base(cancel)
        {
            Key   = key;
            Value = value;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Key
        ///
        /// <summary>
        /// キーを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public T Key { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Value
        ///
        /// <summary>
        /// 値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public U Value { get; }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// KeyValueEventArgs
    ///
    /// <summary>
    /// KeyValueEventArgs(T, U), KeyValueCancelEventArgs(T, U)
    /// オブジェクトを生成するための補助クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class KeyValueEventArgs
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// KeyValueEventArgs(T, U) オブジェクトを生成します。
        /// </summary>
        ///
        /// <param name="key">設定するキー</param>
        /// <param name="value">設定値</param>
        ///
        /* ----------------------------------------------------------------- */
        public static KeyValueEventArgs<T, U> Create<T, U>(T key, U value) =>
            new KeyValueEventArgs<T, U>(key, value);

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// KeyValueCancelEventArgs(T, U) オブジェクトを生成します。
        /// </summary>
        ///
        /// <param name="key">設定するキー</param>
        /// <param name="value">設定値</param>
        /// <param name="cancel">キャンセルするかどうか</param>
        ///
        /* ----------------------------------------------------------------- */
        public static KeyValueCancelEventArgs<T, U> Create<T, U>(T key, U value, bool cancel) =>
            new KeyValueCancelEventArgs<T, U>(key, value, cancel);

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// KeyValueEventHandler(T, U)
    ///
    /// <summary>
    /// イベントを処理するメソッドを表します。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public delegate void KeyValueEventHandler<T, U>(object sender, KeyValueEventArgs<T, U> e);

    /* --------------------------------------------------------------------- */
    ///
    /// KeyValueCanelEventHandler(T, U)
    ///
    /// <summary>
    /// イベントを処理するメソッドを表します。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public delegate void KeyValueCanelEventHandler<T, U>(object sender, KeyValueCancelEventArgs<T, U> e);
}
