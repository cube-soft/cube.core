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
using System.Collections.Generic;

#region CollectionEventArgs

/* ------------------------------------------------------------------------- */
///
/// CollectionEventArgs
///
/// <summary>
/// Provides methods to create an instance of the CollectionEventArgs(T)
/// or CollectionEventArgs(T) classes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class CollectionEventArgs
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Create
    ///
    /// <summary>
    /// Creates a new instance of the CollectionEventArgs(T) class
    /// with the specified value.
    /// </summary>
    ///
    /// <param name="value">Value to use for the event.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static CollectionEventArgs<T> Create<T>(IEnumerable<T> value) => new(value);

    /* --------------------------------------------------------------------- */
    ///
    /// Create
    ///
    /// <summary>
    /// Creates a new instance of the CollectionEventArgs(T) class
    /// with the specified arguments.
    /// </summary>
    ///
    /// <param name="value">Value to use for the event.</param>
    /// <param name="cancel">
    /// true to cancel the event; otherwise, false.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    public static CollectionCancelEventArgs<T> Create<T>(IEnumerable<T> value, bool cancel) =>
        new(value, cancel);

    #endregion
}

#endregion

#region CollectionEventArgs<T>

/* ------------------------------------------------------------------------- */
///
/// CollectionEventArgs(T)
///
/// <summary>
/// Provides a value of type IEnumerable(T) to use for events.
/// </summary>
///
/// <param name="value">Value to use for the event.</param>
///
/* ------------------------------------------------------------------------- */
public class CollectionEventArgs<T>(IEnumerable<T> value) : ValueEventArgs<IEnumerable<T>>(value);

#endregion

#region CollectionCancelEventArgs<T>

/* ------------------------------------------------------------------------- */
///
/// CollectionCancelEventArgs(T)
///
/// <summary>
/// Provides data for a cancelable event with a value of type
/// IEnumerable(T).
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class CollectionCancelEventArgs<T> : ValueCancelEventArgs<IEnumerable<T>>
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// CollectionCancelEventArgs
    ///
    /// <summary>
    /// Initializes a new instance of the CollectionCancelEventArgs
    /// class with the specified value. The Cancel property is set
    /// to false.
    /// </summary>
    ///
    /// <param name="value">Value to use for the event.</param>
    ///
    /* --------------------------------------------------------------------- */
    public CollectionCancelEventArgs(IEnumerable<T> value) : this(value, false) { }

    /* --------------------------------------------------------------------- */
    ///
    /// CollectionCancelEventArgs
    ///
    /// <summary>
    /// Initializes a new instance of the CollectionCancelEventArgs
    /// class with the specified arguments.
    /// </summary>
    ///
    /// <param name="value">Value to use for the event.</param>
    /// <param name="cancel">
    /// true to cancel the event; otherwise, false.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    public CollectionCancelEventArgs(IEnumerable<T> value, bool cancel) : base(value, cancel) { }

    #endregion
}

#endregion

#region CollectionEventHandlers

/* ------------------------------------------------------------------------- */
///
/// CollectionEventHandler(T)
///
/// <summary>
/// Represents the method to invoke an event.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[Serializable]
public delegate void CollectionEventHandler<T>(object sender, CollectionEventArgs<T> e);

/* ------------------------------------------------------------------------- */
///
/// CollectionCancelEventHandler(T)
///
/// <summary>
/// Represents the method to invoke an event.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[Serializable]
public delegate void CollectionCancelEventHandler<T>(object sender, CollectionCancelEventArgs<T> e);

#endregion
