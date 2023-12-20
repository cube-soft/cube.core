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
namespace Cube.Collections.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;

/* ------------------------------------------------------------------------- */
///
/// FlattenMethods
///
/// <summary>
/// Provides extended methods to convert a tree structure to a
/// one-dimensional sequence.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class FlattenMethods
{
    /* --------------------------------------------------------------------- */
    ///
    /// Flatten
    ///
    /// <summary>
    /// Convert a tree structure to a one-dimensional sequence with
    /// breadth first search.
    /// </summary>
    ///
    /// <param name="src">Source sequence.</param>
    /// <param name="func">Conversion function.</param>
    ///
    /// <returns>Converted sequence.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static IEnumerable<T> Flatten<T>(this IEnumerable<T> src, Func<T, IEnumerable<T>> func) =>
        src.Flatten((e, _) => func(e));

    /* --------------------------------------------------------------------- */
    ///
    /// Flatten
    ///
    /// <summary>
    /// Convert a tree structure to a one-dimensional sequence with
    /// breadth first search.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static IEnumerable<T> Flatten<T>(this IEnumerable<T> src, Func<T, IEnumerable<T>, IEnumerable<T>> func) =>
        src.Concat(
            src.Where(e => func(e, src) is not null)
               .SelectMany(e => func(e, src).Flatten(func))
        );
}
