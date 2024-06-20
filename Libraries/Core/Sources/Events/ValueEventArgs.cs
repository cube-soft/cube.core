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
namespace Cube;

using System;
using System.ComponentModel;

#region ValueEventArgs

/* ------------------------------------------------------------------------- */
///
/// ValueEventArgs
///
/// <summary>
/// Provides methods to create a new instance of the ValueEventArgs(T),
/// ValueCancelEventArgs(T), or ValueChangedEventArgs(T) classes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class ValueEventArgs
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Create
    ///
    /// <summary>
    /// Creates a new instance of the ValueEventArgs(T) class with the
    /// specified value.
    /// </summary>
    ///
    /// <param name="value">Value to use for the event.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static ValueEventArgs<T> Create<T>(T value) => new(value);

    /* --------------------------------------------------------------------- */
    ///
    /// Create
    ///
    /// <summary>
    /// Creates a new instance of the ValueCancelEventArgs(T) class
    /// with the specified arguments.
    /// </summary>
    ///
    /// <param name="value">Value to use for the event.</param>
    /// <param name="cancel">
    /// true to cancel the event; otherwise, false.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    public static ValueCancelEventArgs<T> Create<T>(T value, bool cancel) => new(value, cancel);

    /* --------------------------------------------------------------------- */
    ///
    /// Create
    ///
    /// <summary>
    /// Creates a new instance of the ValueChangedEventArgs(T) class
    /// with the specified arguments.
    /// </summary>
    ///
    /// <param name="older">Value before changed.</param>
    /// <param name="newer">Value after changed.</param>
    ///
    /// <remarks>
    /// A ValueCancelEventArgs(T) object may be created if a value of type
    /// bool is specified.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public static ValueChangedEventArgs<T> Create<T>(T older, T newer) => new(older, newer);

    #endregion
}

#endregion

#region ValueEventArgs<T>

/* ------------------------------------------------------------------------- */
///
/// ValueEventArgs(T)
///
/// <summary>
/// Provides a value of type T to use for events.
/// </summary>
///
/// <param name="value">Value to use for the event.</param>
///
/* ------------------------------------------------------------------------- */
public class ValueEventArgs<T>(T value) : EventArgs
{
    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Value
    ///
    /// <summary>
    /// Gets a value to use for the event.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public T Value { get; } = value;

    #endregion
}

#endregion

#region ValueCancelEventArgs<T>

/* ------------------------------------------------------------------------- */
///
/// ValueCancelEventArgs(T)
///
/// <summary>
/// Provides data for a cancelable event with a value of type T.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class ValueCancelEventArgs<T> : CancelEventArgs
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// ValueCancelEventArgs
    ///
    /// <summary>
    /// Initializes a new instance of the ValueCancelEventArgs class
    /// with the specified value. The Cancel property is set to
    /// false.
    /// </summary>
    ///
    /// <param name="value">Value to use for the event.</param>
    ///
    /* --------------------------------------------------------------------- */
    public ValueCancelEventArgs(T value) : this(value, false) { }

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public ValueCancelEventArgs(T value, bool cancel) : base(cancel) => Value = value;

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Value
    ///
    /// <summary>
    /// Gets a value to use for the event.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public T Value { get; }

    #endregion
}

#endregion

#region ValueChangedEventArgs<T>

/* ------------------------------------------------------------------------- */
///
/// ValueChangedEventArgs(T)
///
/// <summary>
/// Provides values that represent before and after changing for user
/// events.
/// </summary>
///
/// <param name="older">Value before changed.</param>
/// <param name="newer">Value after changed.</param>
///
/* ------------------------------------------------------------------------- */
public class ValueChangedEventArgs<T>(T older, T newer) : EventArgs
{
    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// OldValue
    ///
    /// <summary>
    /// Gets a value before changed by the event.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public T OldValue { get; } = older;

    /* --------------------------------------------------------------------- */
    ///
    /// NewValue
    ///
    /// <summary>
    /// Gets a value after changed by the event.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public T NewValue { get; } = newer;

    #endregion
}

#endregion

#region ValueEventHandlers

/* ------------------------------------------------------------------------- */
///
/// ValueEventHandler(T)
///
/// <summary>
/// Represents the method to invoke an event.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[Serializable]
public delegate void ValueEventHandler<T>(object sender, ValueEventArgs<T> e);

/* ------------------------------------------------------------------------- */
///
/// ValueCancelEventHandler(T)
///
/// <summary>
/// Represents the method to invoke an event.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[Serializable]
public delegate void ValueCancelEventHandler<T>(object sender, ValueCancelEventArgs<T> e);

/* ------------------------------------------------------------------------- */
///
/// ValueChangedEventHandler(T)
///
/// <summary>
/// Represents the method to invoke an event.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[Serializable]
public delegate void ValueChangedEventHandler<T>(object sender, ValueChangedEventArgs<T> e);

#endregion
