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
using System.Threading;

#region Dispatcher

/* ------------------------------------------------------------------------- */
///
/// Dispatcher
///
/// <summary>
/// Provides functionality to invoke the provided action.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public abstract class Dispatcher
{
    /* --------------------------------------------------------------------- */
    ///
    /// Vanilla
    ///
    /// <summary>
    /// Gets the dispatcher that invokes the provided action directly.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static Dispatcher Vanilla { get; } = new VanillaDispatcher();

    /* --------------------------------------------------------------------- */
    ///
    /// Invoke
    ///
    /// <summary>
    /// Invokes the specified action.
    /// </summary>
    ///
    /// <param name="action">Invoked action.</param>
    ///
    /* --------------------------------------------------------------------- */
    public abstract void Invoke(Action action);
}

#endregion

#region ContextDispatcher

/* ------------------------------------------------------------------------- */
///
/// ContextDispatcher
///
/// <summary>
/// Provides functionality to invoke the provided action with a
/// SynchronizationContext object.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class ContextDispatcher : Dispatcher
{
    /* --------------------------------------------------------------------- */
    ///
    /// ContextDispatcher
    ///
    /// <summary>
    /// Initializes a new instance of the ContextDispatcher class with
    /// the specified arguments.
    /// </summary>
    ///
    /// <param name="synchronous">
    /// Value indicating to invoke the provided action with the
    /// synchronous method.
    /// </param>
    ///
    /// <exception cref="ArgumentNullException">
    /// SynchronizationContext.Current is null.
    /// </exception>
    ///
    /* --------------------------------------------------------------------- */
    public ContextDispatcher(bool synchronous) : this(SynchronizationContext.Current, synchronous) { }

    /* --------------------------------------------------------------------- */
    ///
    /// ContextDispatcher
    ///
    /// <summary>
    /// Initializes a new instance of the ContextDispatcher class with
    /// the specified arguments.
    /// </summary>
    ///
    /// <param name="context">Synchronization context.</param>
    /// <param name="synchronous">
    /// Value indicating to invoke the provided action with the
    /// synchronous method.
    /// </param>
    ///
    /// <exception cref="ArgumentNullException">
    /// Specified SynchronizationContext object is null.
    /// </exception>
    ///
    /* --------------------------------------------------------------------- */
    public ContextDispatcher(SynchronizationContext context, bool synchronous)
    {
        Synchronous = synchronous;
        Context     = context ?? throw new ArgumentNullException(nameof(context));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Synchronous
    ///
    /// <summary>
    /// Gets or sets the value indicating whether the event is fired
    /// as synchronously.
    /// </summary>
    ///
    /// <remarks>
    /// Uses the Send method if the property is set to true;
    /// otherwise uses the Post method.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public bool Synchronous { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// Context
    ///
    /// <summary>
    /// Gets the synchronization context.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected SynchronizationContext Context { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// Invoke
    ///
    /// <summary>
    /// Invokes the specified action with the Synchronization context.
    /// </summary>
    ///
    /// <param name="action">Invoked action.</param>
    ///
    /* --------------------------------------------------------------------- */
    public override void Invoke(Action action)
    {
        if (Synchronous) Context.Send(_ => action(), null);
        else Context.Post(_ => action(), null);
    }
}

#endregion

#region VanillaDispatcher

/* ------------------------------------------------------------------------- */
///
/// VanillaDispatcher
///
/// <summary>
/// Provides functionality to invoke the provided action directly.
/// </summary>
///
/* ------------------------------------------------------------------------- */
internal sealed class VanillaDispatcher : Dispatcher
{
    /* --------------------------------------------------------------------- */
    ///
    /// Invoke
    ///
    /// <summary>
    /// Invokes the specified action.
    /// </summary>
    ///
    /// <param name="action">Invoked action.</param>
    ///
    /* --------------------------------------------------------------------- */
    public override void Invoke(Action action) => action();
}

#endregion
