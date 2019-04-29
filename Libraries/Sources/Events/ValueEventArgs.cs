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
    /// Provides a value of type T to use for events.
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
        /// Initializes a new instance of the ValueEventArgs class with
        /// the specified value.
        /// </summary>
        ///
        /// <param name="value">Value to use for the event.</param>
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
        /// Gets a value to use for the event.
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
    /// Provides data for a cancelable event with a value of type T.
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
        /// Initializes a new instance of the ValueCancelEventArgs class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="value">Value to use for the event.</param>
        ///
        /* ----------------------------------------------------------------- */
        public ValueCancelEventArgs(T value) : this(value, false) { }

        /* ----------------------------------------------------------------- */
        ///
        /// ValueCancelEventArgs
        ///
        /// <summary>
        /// Initializes a new instance of the ValueCancelEventArgs class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="value">Value to use for the event.</param>
        /// <param name="cancel">
        /// true to cancel the event; otherwise, false.
        /// </param>
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
    /// Provides values that represent before and after changing for user
    /// events.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ValueChangedEventArgs<T> : EventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ValueChangedEventArgs(T)
        ///
        /// <summary>
        /// Initializes a new instance of the ValueChangedEventArgs class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="oldvalue">Value before changed.</param>
        /// <param name="newvalue">Value after changed.</param>
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
        /// Gets the value before changed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public T OldValue { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// NewValue
        ///
        /// <summary>
        /// Gets the value after changed.
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
    /// Represents the method to invoke an event.
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
    /// Represents the method to invoke an event.
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
    /// Represents the method to invoke an event.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public delegate void ValueChangedEventHandler<T>(object sender, ValueChangedEventArgs<T> e);

    #endregion
}
