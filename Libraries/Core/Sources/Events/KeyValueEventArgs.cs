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

#region KeyValueEventArgs

/* ------------------------------------------------------------------------- */
///
/// KeyValueEventArgs
///
/// <summary>
/// Provides methods to create a new instance of the
/// KeyValueEventArgs(TKey, TValue) or KeyValueCancelEventArgs(TKey, TValue)
/// classes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class KeyValueEventArgs
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Create
    ///
    /// <summary>
    /// Creates a new instance of the KeyValueEventArgs(T, U) class
    /// with the specified key and value.
    /// </summary>
    ///
    /// <param name="key">Key to use for the event.</param>
    /// <param name="value">Value to use for the event.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static KeyValueEventArgs<TKey, TValue> Create<TKey, TValue>(TKey key, TValue value) => new(key, value);

    /* --------------------------------------------------------------------- */
    ///
    /// Create
    ///
    /// <summary>
    /// Creates a new instance of the KeyValueCancelEventArgs(TKey, TValue)
    /// class with the specified arguments.
    /// </summary>
    ///
    /// <param name="key">Key to use for the event.</param>
    /// <param name="value">Value to use for the event.</param>
    /// <param name="cancel">
    /// true to cancel the event; otherwise, false.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    public static KeyValueCancelEventArgs<TKey, TValue> Create<TKey, TValue>(TKey key, TValue value, bool cancel) =>
        new(key, value, cancel);

    #endregion
}

#endregion

#region KeyValueEventArgs<TKey, TValue>

/* ------------------------------------------------------------------------- */
///
/// KeyValueEventArgs
///
/// <summary>
/// Provides Key-Value data to use for events.
/// </summary>
///
/// <param name="key">Key to use for the event.</param>
/// <param name="value">Value to use for the event.</param>
///
/* ------------------------------------------------------------------------- */
public class KeyValueEventArgs<TKey, TValue>(TKey key, TValue value) : ValueEventArgs<TValue>(value)
{
    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Key
    ///
    /// <summary>
    /// Gets a key to use for the event.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public TKey Key { get; } = key;

    #endregion
}

#endregion

#region KeyValueCancelEventArgs<TKey, TValue>

/* ------------------------------------------------------------------------- */
///
/// KeyValueCancelEventArgs
///
/// <summary>
/// Provides data for a cancelable event with Key-Value data.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class KeyValueCancelEventArgs<TKey, TValue> : ValueCancelEventArgs<TValue>
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// KeyValueCancelEventArgs
    ///
    /// <summary>
    /// Initializes a new instance of the KeyValueCancelEventArgs class
    /// with the specified key and value. The Cancel property is set
    /// to false.
    /// </summary>
    ///
    /// <param name="key">Key to use for the event.</param>
    /// <param name="value">Value to use for the event.</param>
    ///
    /* --------------------------------------------------------------------- */
    public KeyValueCancelEventArgs(TKey key, TValue value) : this(key, value, false) { }

    /* --------------------------------------------------------------------- */
    ///
    /// KeyValueCancelEventArgs
    ///
    /// <summary>
    /// Initializes a new instance of the KeyValueCancelEventArgs class
    /// with the specified arguments.
    /// </summary>
    ///
    /// <param name="key">Key to use for the event.</param>
    /// <param name="value">Value to use for the event.</param>
    /// <param name="cancel">
    /// true to cancel the event; otherwise, false.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    public KeyValueCancelEventArgs(TKey key, TValue value, bool cancel) : base(value, cancel) => Key = key;

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Key
    ///
    /// <summary>
    /// Gets a key to use for the event.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public TKey Key { get; }

    #endregion
}

#endregion

#region KeyValueEventHandlers

/* ------------------------------------------------------------------------- */
///
/// KeyValueEventHandler
///
/// <summary>
/// Represents the method to invoke an event.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[Serializable]
public delegate void KeyValueEventHandler<TKey, TValue>(object sender, KeyValueEventArgs<TKey, TValue> e);

/* ------------------------------------------------------------------------- */
///
/// KeyValueCancelEventHandler
///
/// <summary>
/// Represents the method to invoke an event.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[Serializable]
public delegate void KeyValueCancelEventHandler<TKey, TValue>(object sender, KeyValueCancelEventArgs<TKey, TValue> e);

#endregion
