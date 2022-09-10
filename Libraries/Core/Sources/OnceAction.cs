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

#region OnceAction

/* ------------------------------------------------------------------------- */
///
/// OnceAction
///
/// <summary>
/// Provides functionality to invoke the specified action once.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public sealed class OnceAction : Once<Action>
{
    /* --------------------------------------------------------------------- */
    ///
    /// OnceAction
    ///
    /// <summary>
    /// Initializes a new instance of the OnceAction class with the
    /// specified action.
    /// </summary>
    ///
    /// <param name="action">Action that is invoked once.</param>
    ///
    /* --------------------------------------------------------------------- */
    public OnceAction(Action action) : base(action) { }

    /* --------------------------------------------------------------------- */
    ///
    /// Invoke
    ///
    /// <summary>
    /// Invokes the specified action.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public void Invoke() => Invoke(e => e());
}

#endregion

#region OnceAction<T>

/* ------------------------------------------------------------------------- */
///
/// OnceAction(T)
///
/// <summary>
/// Initializes a new instance of the OnceAction class with the
/// specified action.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public sealed class OnceAction<T> : Once<Action<T>>
{
    /* --------------------------------------------------------------------- */
    ///
    /// OnceAction
    ///
    /// <summary>
    /// Initializes a new instance of the OnceAction class with the
    /// specified action.
    /// </summary>
    ///
    /// <param name="action">Action that is invoked once.</param>
    ///
    /* --------------------------------------------------------------------- */
    public OnceAction(Action<T> action) : base(action) { }

    /* --------------------------------------------------------------------- */
    ///
    /// Invoke
    ///
    /// <summary>
    /// Invokes the specified action with the specified arguments.
    /// </summary>
    ///
    /// <param name="obj">Arguments of the action.</param>
    ///
    /* --------------------------------------------------------------------- */
    public void Invoke(T obj) => Invoke(e => e(obj));
}

#endregion

#region Once<T>

/* ------------------------------------------------------------------------- */
///
/// Once(T)
///
/// <summary>
/// Represents the base class of OnceAction classes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public abstract class Once<T> where T : class
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// Once
    ///
    /// <summary>
    /// Initializes a new instance of the Once class with the specified
    /// value.
    /// </summary>
    ///
    /// <param name="value">Value to be invoked once.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected Once(T value)
    {
        _value = value ?? throw new ArgumentNullException(nameof(value));
    }

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// IgnoreTwice
    ///
    /// <summary>
    /// Gets or sets a value indicating whether to ignore the second
    /// or later call. If set to false, TwiceException will be thrown
    /// on the second or later.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public bool IgnoreTwice { get; set; } = true;

    /* --------------------------------------------------------------------- */
    ///
    /// Invoked
    ///
    /// <summary>
    /// Gets a value indicating whether the provided action has been
    /// already invoked.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public bool Invoked => _value is null;

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Invoke
    ///
    /// <summary>
    /// Gets the provided value and invokes the specified callback.
    /// </summary>
    ///
    /// <param name="action">
    /// Action to be invoked only in the first call.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    protected void Invoke(Action<T> action)
    {
        if (!Invoked)
        {
            var obj = Interlocked.Exchange(ref _value, null);
            if (obj is not null) { action(obj); return; }
        }

        if (!IgnoreTwice) throw new TwiceException();
    }

    #endregion

    #region Fields
    private T _value;
    #endregion
}

#endregion
