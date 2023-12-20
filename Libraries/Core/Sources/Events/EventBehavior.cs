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
using System.Reflection;

/* ------------------------------------------------------------------------- */
///
/// EventBehavior
///
/// <summary>
/// Provides functionality to wrap the specified event as
/// Subscribe/Dispose pattern.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class EventBehavior : DisposableBase
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// EventBehavior
    ///
    /// <summary>
    /// Creates a new instance of the EventBehavior class
    /// with the specified arguments.
    /// </summary>
    ///
    /// <param name="src">Source object.</param>
    /// <param name="name">Event name to wrap.</param>
    /// <param name="action">
    /// Action when the specified event is fired.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    public EventBehavior(object src, string name, Action action) :
        this(src, name, new EventHandler((_, _) => action())) { }

    /* --------------------------------------------------------------------- */
    ///
    /// EventBehavior
    ///
    /// <summary>
    /// Creates a new instance of the EventBehavior class
    /// with the specified arguments.
    /// </summary>
    ///
    /// <param name="src">Source object.</param>
    /// <param name="name">Event name to wrap.</param>
    /// <param name="handler">
    /// Handler when the specified event is fired.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    public EventBehavior(object src, string name, Delegate handler)
    {
        _source  = src ?? throw new ArgumentNullException(nameof(src));
        _event   = src.GetType().GetEvent(name) ?? throw new ArgumentNullException(name);
        _handler = handler ?? throw new ArgumentNullException(nameof(handler));

        _event.AddEventHandler(_source, _handler);
    }

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Dispose
    ///
    /// <summary>
    /// Releases the unmanaged resources used by the object and
    /// optionally releases the managed resources.
    /// </summary>
    ///
    /// <param name="disposing">
    /// true to release both managed and unmanaged resources;
    /// false to release only unmanaged resources.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    protected override void Dispose(bool disposing)
    {
        if (disposing) _event.RemoveEventHandler(_source, _handler);
    }

    #endregion

    #region Fields
    private readonly object _source;
    private readonly EventInfo _event;
    private readonly Delegate _handler;
    #endregion
}
