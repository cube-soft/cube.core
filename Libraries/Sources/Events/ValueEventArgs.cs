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
    #region ValueEventArgs

    /* --------------------------------------------------------------------- */
    ///
    /// ValueEventArgs
    ///
    /// <summary>
    /// ValueEventArgs(T), ValueCancelEventArgs(T), ValueChangedEventArgs(T)
    /// オブジェクトを生成するための補助クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class ValueEventArgs
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// ValueEventArgs(T) オブジェクトを生成します。
        /// </summary>
        ///
        /// <param name="value">設定値</param>
        ///
        /* ----------------------------------------------------------------- */
        public static ValueEventArgs<T> Create<T>(T value) =>
            new ValueEventArgs<T>(value);

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// ValueCancelEventArgs(T) オブジェクトを生成します。
        /// </summary>
        ///
        /// <param name="value">設定値</param>
        /// <param name="cancel">キャンセルするかどうか</param>
        ///
        /* ----------------------------------------------------------------- */
        public static ValueCancelEventArgs<T> Create<T>(T value, bool cancel) =>
            new ValueCancelEventArgs<T>(value, cancel);

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// ValueChangedEventArgs(T) オブジェクトを生成します。
        /// </summary>
        ///
        /// <param name="oldvalue">変更前の値</param>
        /// <param name="newvalue">変更後の値</param>
        /// 
        /// <remarks>
        /// bool 型の値を指定すると ValueCancelEventArgs(T) のオブジェクトが
        /// 生成される場合があります。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static ValueChangedEventArgs<T> Create<T>(T oldvalue, T newvalue) =>
            new ValueChangedEventArgs<T>(oldvalue, newvalue);

        #endregion
    }

    #endregion

    #region ValueEventArgs<T>

    /* --------------------------------------------------------------------- */
    ///
    /// ValueEventArgs(T)
    ///
    /// <summary>
    /// イベントハンドラに特定の型の値を渡すためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ValueEventArgs<T> : EventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ValueEventArgs
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="value">設定値</param>
        ///
        /* ----------------------------------------------------------------- */
        public ValueEventArgs(T value)
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
        public T Value { get; }

        #endregion
    }

    #endregion

    #region ValueCancelEventArgs<T>

    /* --------------------------------------------------------------------- */
    ///
    /// ValueCancelEventArgs(T)
    ///
    /// <summary>
    /// イベントハンドラに特定の型の値を渡すためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ValueCancelEventArgs<T> : CancelEventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ValueCancelEventArgs
        ///
        /// <summary>
        /// Cancel の値を false に設定してオブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="value">設定値</param>
        ///
        /* ----------------------------------------------------------------- */
        public ValueCancelEventArgs(T value) : this(value, false) { }

        /* ----------------------------------------------------------------- */
        ///
        /// ValueCancelEventArgs
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="value">設定値</param>
        /// <param name="cancel">キャンセルするかどうか</param>
        ///
        /* ----------------------------------------------------------------- */
        public ValueCancelEventArgs(T value, bool cancel) : base(cancel)
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
        public T Value { get; }

        #endregion
    }

    #endregion

    #region ValueChangedEventArgs<T>

    /* --------------------------------------------------------------------- */
    ///
    /// ValueChangedEventArgs(T)
    ///
    /// <summary>
    /// 値の変更に関連するイベントに使用するクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ValueChangedEventArgs<T> : EventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ValueChangedEventArgs
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="oldvalue">変更前の値</param>
        /// <param name="newvalue">変更後の値</param>
        ///
        /* ----------------------------------------------------------------- */
        public ValueChangedEventArgs(T oldvalue, T newvalue)
        {
            OldValue = oldvalue;
            NewValue = newvalue;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// OldValue
        ///
        /// <summary>
        /// 変更前のオブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public T OldValue { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// NewValue
        ///
        /// <summary>
        /// 変更後のオブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public T NewValue { get; }

        #endregion
    }

    #endregion

    #region ValueEventHandlers

    /* --------------------------------------------------------------------- */
    ///
    /// ValueEventHandler(T)
    ///
    /// <summary>
    /// イベントを処理するメソッドを表します。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public delegate void ValueEventHandler<T>(object sender, ValueEventArgs<T> e);

    /* --------------------------------------------------------------------- */
    ///
    /// ValueCancelEventHandler(T)
    ///
    /// <summary>
    /// イベントを処理するメソッドを表します。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public delegate void ValueCancelEventHandler<T>(object sender, ValueCancelEventArgs<T> e);

    /* --------------------------------------------------------------------- */
    ///
    /// ValueChangedEventHandler(T)
    ///
    /// <summary>
    /// イベントを処理するメソッドを表します。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public delegate void ValueChangedEventHandler<T>(object sender, ValueChangedEventArgs<T> e);

    #endregion
}
