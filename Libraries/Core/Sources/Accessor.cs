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

/* ------------------------------------------------------------------------- */
///
/// Getter
///
/// <summary>
/// Represents the delegation to get a value of type T.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public delegate T Getter<out T>();

/* ------------------------------------------------------------------------- */
///
/// Setter
///
/// <summary>
/// Represents the delegation to set a value of type T.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public delegate void Setter<in T>(T value);

/* ------------------------------------------------------------------------- */
///
/// Accessor
///
/// <summary>
/// Provides functionality to get and set a value of type T.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class Accessor<T>
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// Accessor
    ///
    /// <summary>
    /// Initializes a new instance of the Accessor class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public Accessor() : this(default(T)) { }

    /* --------------------------------------------------------------------- */
    ///
    /// Accessor
    ///
    /// <summary>
    /// Initializes a new instance of the Accessor class with the
    /// specified value.
    /// </summary>
    ///
    /// <param name="value">Initial value.</param>
    ///
    /* --------------------------------------------------------------------- */
    public Accessor(T value) : this(value, EqualityComparer<T>.Default) { }

    /* --------------------------------------------------------------------- */
    ///
    /// Accessor
    ///
    /// <summary>
    /// Initializes a new instance of the Accessor class with the
    /// specified delegations.
    /// </summary>
    ///
    /// <param name="value">Initial value.</param>
    /// <param name="comparer">Object to compare two values.</param>
    ///
    /* --------------------------------------------------------------------- */
    public Accessor(T value, IEqualityComparer<T> comparer)
    {
        _comparer = comparer;

        var field = value;
        _getter = () => field;
        _setter = e  => field = e;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Accessor
    ///
    /// <summary>
    /// Initializes a new instance of the Accessor class with the
    /// specified delegation.
    /// </summary>
    ///
    /// <param name="getter">Function to get a value.</param>
    ///
    /// <remarks>
    /// Throws InvalidOperationException when the Set method is invoked.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public Accessor(Getter<T> getter) :
        this(getter, _ => throw new InvalidOperationException(nameof(Set))) { }

    /* --------------------------------------------------------------------- */
    ///
    /// Accessor
    ///
    /// <summary>
    /// Initializes a new instance of the Accessor class with the
    /// specified delegations.
    /// </summary>
    ///
    /// <param name="getter">Function to get a value.</param>
    /// <param name="setter">Function to set a value.</param>
    ///
    /* --------------------------------------------------------------------- */
    public Accessor(Getter<T> getter, Setter<T> setter) :
        this(getter, setter, EqualityComparer<T>.Default) { }

    /* --------------------------------------------------------------------- */
    ///
    /// Accessor
    ///
    /// <summary>
    /// Initializes a new instance of the Accessor class with the
    /// specified delegations.
    /// </summary>
    ///
    /// <param name="getter">Function to get a value.</param>
    /// <param name="setter">Function to set a value.</param>
    /// <param name="comparer">Object to compare two values.</param>
    ///
    /* --------------------------------------------------------------------- */
    public Accessor(Getter<T> getter, Setter<T> setter, IEqualityComparer<T> comparer)
    {
        _comparer = comparer;
        _getter   = getter;
        _setter   = setter;
    }

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Get
    ///
    /// <summary>
    /// Get a value.
    /// </summary>
    ///
    /// <returns>Result of the provided Getter(T) delegation.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public T Get() => _getter();

    /* --------------------------------------------------------------------- */
    ///
    /// Get
    ///
    /// <summary>
    /// Set a new value.
    /// </summary>
    ///
    /// <param name="value">Value to be set.</param>
    ///
    /// <returns>
    /// Value indicating whether to be executed the provided Setter(T)
    /// delegation.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public bool Set(T value)
    {
        if (_comparer.Equals(Get(), value)) return false;
        _setter(value);
        return true;
    }

    #endregion

    #region Fields
    private readonly Getter<T> _getter;
    private readonly Setter<T> _setter;
    private readonly IEqualityComparer<T> _comparer;
    #endregion
}
