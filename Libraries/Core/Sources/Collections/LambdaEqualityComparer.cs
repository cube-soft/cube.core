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
namespace Cube.Collections;

using System;
using System.Collections.Generic;

/* ------------------------------------------------------------------------- */
///
/// LambdaEqualityComparer(T)
///
/// <summary>
/// Provides functionality to convert from the Func(T, T, bool) to
/// the instance of EqualityComparer(T) inherited class.
/// </summary>
///
/// <typeparam name="T">The type of objects to compare.</typeparam>
///
/* ------------------------------------------------------------------------- */
public class LambdaEqualityComparer<T> : EqualityComparer<T>
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// LambdaEqualityComparer(T)
    ///
    /// <summary>
    /// Initializes a new instance of the LambdaEqualityComparer(T)
    /// class with the specified comparer.
    /// </summary>
    ///
    /// <param name="src">Function to compare.</param>
    ///
    /* --------------------------------------------------------------------- */
    public LambdaEqualityComparer(Func<T, T, bool> src) : this(src, _ => 0) { }

    /* --------------------------------------------------------------------- */
    ///
    /// LambdaEqualityComparer(T)
    ///
    /// <summary>
    /// Initializes a new instance of the LambdaEqualityComparer(T)
    /// class with the specified arguments.
    /// </summary>
    ///
    /// <param name="src">Function to compare.</param>
    /// <param name="hash">Function to convert to the hash code.</param>
    ///
    /* --------------------------------------------------------------------- */
    public LambdaEqualityComparer(Func<T, T, bool> src, Func<T, int> hash)
    {
        _comparer = src;
        _hash     = hash;
    }

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Equals
    ///
    /// <summary>
    /// Determines whether two objects of type T are equal.
    /// </summary>
    ///
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    ///
    /// <returns>
    /// true if the specified objects are equal; otherwise, false.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public override bool Equals(T x, T y) => _comparer(x, y);

    /* --------------------------------------------------------------------- */
    ///
    /// GetHashCode
    ///
    /// <summary>
    /// Serves as a hash function for the specified object for hashing
    /// algorithms and data structures, such as a hash table.
    /// </summary>
    ///
    /// <param name="obj">
    /// The object for which to get a hash code.
    /// </param>
    ///
    /// <returns>Hash code for the specified object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public override int GetHashCode(T obj) => _hash(obj);

    #endregion

    #region Fields
    private readonly Func<T, T, bool> _comparer;
    private readonly Func<T, int> _hash;
    #endregion
}
