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
/// LambdaComparer(T)
///
/// <summary>
/// Provides functionality to convert from the Func(T, T, bool) to
/// the instance of Comparer(T) inherited class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class LambdaComparer<T> : Comparer<T>
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// LambdaComparer(T)
    ///
    /// <summary>
    /// Initializes a new instance of the LambdaComparer(T) with the
    /// specified function.
    /// </summary>
    ///
    /// <param name="src">Function to compare.</param>
    ///
    /* --------------------------------------------------------------------- */
    public LambdaComparer(Func<T, T, int> src) => _comparer = src;

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Compare
    ///
    /// <summary>
    /// Compares two objects of type T and returns an indication of
    /// their relative sort order.
    /// </summary>
    ///
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    ///
    /// <returns>
    /// Zero if the specified objects are equal;
    /// Less than zero if x is less than y;
    /// otherwise, Greater than zero.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public override int Compare(T x, T y) => _comparer(x, y);

    #endregion

    #region Fields
    private readonly Func<T, T, int> _comparer;
    #endregion
}
