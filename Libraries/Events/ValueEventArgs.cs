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
    /// ValueEventArgs(TValue)
    ///
    /// <summary>
    /// イベントハンドラに特定の型の値を渡すためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ValueEventArgs<TValue> : EventArgs
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
        /* ----------------------------------------------------------------- */
        public ValueEventArgs(TValue value)
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
        public TValue Value { get; }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// ValueChangedEventArgs(TValue)
    /// 
    /// <summary>
    /// 値の変更に関連するイベントに使用するクラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public class ValueChangedEventArgs<TValue> : EventArgs
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
        /* ----------------------------------------------------------------- */
        public ValueChangedEventArgs(TValue oldvalue, TValue newvalue)
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
        public TValue OldValue { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// NewValue
        /// 
        /// <summary>
        /// 変更後のオブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TValue NewValue { get; }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// ValueCancelEventArgs(TValue)
    ///
    /// <summary>
    /// イベントハンドラに特定の型の値を渡すためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ValueCancelEventArgs<TValue> : CancelEventArgs
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
        /* ----------------------------------------------------------------- */
        public ValueCancelEventArgs(TValue value) : this(value, false) { }

        /* ----------------------------------------------------------------- */
        ///
        /// ValueCancelEventArgs
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ValueCancelEventArgs(TValue value, bool cancel) : base(cancel)
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
        public TValue Value { get; }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// ValueEventArgs
    ///
    /// <summary>
    /// ValueEventArgs(T), ValueCancelEventArgs(T) オブジェクトを生成する
    /// ための補助クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class ValueEventArgs
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Create
        /// 
        /// <summary>
        /// ValueEventArgs(T) オブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static ValueEventArgs<T> Create<T>(T value)
            => new ValueEventArgs<T>(value);

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        /// 
        /// <summary>
        /// ValueChangedEventArgs(T) オブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static ValueChangedEventArgs<T> Create<T>(T oldvalue, T newvalue)
            => new ValueChangedEventArgs<T>(oldvalue, newvalue);

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        /// 
        /// <summary>
        /// ValueCancelEventArgs(T) オブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static ValueCancelEventArgs<T> Create<T>(T value, bool cancel)
            => new ValueCancelEventArgs<T>(value, cancel);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// ValueEventHandler(TValue)
    /// 
    /// <summary>
    /// イベントを処理するメソッドを表します。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public delegate void ValueEventHandler<TValue>(object sender, ValueEventArgs<TValue> e);

    /* --------------------------------------------------------------------- */
    ///
    /// ValueChangedEventHandler(TValue)
    /// 
    /// <summary>
    /// イベントを処理するメソッドを表します。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public delegate void ValueChangedEventHandler<TValue>(object sender, ValueChangedEventArgs<TValue> e);

    /* --------------------------------------------------------------------- */
    ///
    /// ValueCancelEventHandler(TValue)
    /// 
    /// <summary>
    /// イベントを処理するメソッドを表します。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public delegate void ValueCancelEventHandler<TValue>(object sender, ValueCancelEventArgs<TValue> e);
}
