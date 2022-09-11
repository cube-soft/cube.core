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
/// Methods
///
/// <summary>
/// Provides extended methods of collection classes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class Methods
{
    #region IEnumerable<T>

    /* --------------------------------------------------------------------- */
    ///
    /// Concat
    ///
    /// <summary>
    /// Combines the specified items to the end of the specified source
    /// sequence.
    /// </summary>
    ///
    /// <param name="src">Source sequence.</param>
    /// <param name="items">Items to be combined.</param>
    ///
    /// <returns>Combined sequence.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static IEnumerable<T> Concat<T>(this IEnumerable<T> src, params T[] items) =>
        src.Concat(items.AsEnumerable());

    /* --------------------------------------------------------------------- */
    ///
    /// Join
    ///
    /// <summary>
    /// Combines the specified sequence with the specified separator.
    /// </summary>
    ///
    /// <param name="src">Source sequence.</param>
    /// <param name="separator">Concat separator.</param>
    /// <param name="formatter">Function to convert to string.</param>
    ///
    /// <returns>Combined string.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static string Join<T>(this IEnumerable<T> src, string separator, Func<T, string> formatter) =>
        src.Select(e => formatter(e)).Join(separator);

    /* --------------------------------------------------------------------- */
    ///
    /// Join
    ///
    /// <summary>
    /// Combines the specified string sequence with the specified
    /// separator.
    /// </summary>
    ///
    /// <param name="src">Source sequence.</param>
    /// <param name="separator">Concat separator.</param>
    ///
    /// <returns>Combined string.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static string Join(this IEnumerable<string> src, string separator) =>
        src.Any() ? src.Aggregate((x, y) => x + separator + y) : string.Empty;

    #endregion

    #region IDictionary<T, U>

    /* --------------------------------------------------------------------- */
    ///
    /// AddOrSet(T, U)
    ///
    /// <summary>
    /// Adds or sets the specified key-value pair.
    /// </summary>
    ///
    /// <param name="src">Dictionary collection.</param>
    /// <param name="key">Key element to be set.</param>
    /// <param name="value">Value element to be set.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void AddOrSet<T, U>(this IDictionary<T, U> src, T key, U value) =>
        src.AddOrSet(key, value, (x, y) => y);

    /* --------------------------------------------------------------------- */
    ///
    /// AddOrSet(T, U)
    ///
    /// <summary>
    /// Adds or sets the specified key-value pair.
    /// </summary>
    ///
    /// <param name="src">Dictionary collection.</param>
    /// <param name="key">Key element to be set.</param>
    /// <param name="value">Value element to be set.</param>
    /// <param name="selector">
    /// Function object to select which value is set.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    public static void AddOrSet<T, U>(this IDictionary<T, U> src, T key, U value, Func<U, U, U> selector)
    {
        if (src.TryGetValue(key, out var current)) src[key] = selector(current, value);
        else src.Add(key, value);
    }

    #endregion
}
