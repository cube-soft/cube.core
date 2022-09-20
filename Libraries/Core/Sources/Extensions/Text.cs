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
namespace Cube.Text.Extensions;

using System;

/* ------------------------------------------------------------------------- */
///
/// Methods
///
/// <summary>
/// Provides extended methods for the string class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class Methods
{
    /* --------------------------------------------------------------------- */
    ///
    /// HasValue
    ///
    /// <summary>
    /// Gets a value indicating whether the specified string contains
    /// one or more character.
    /// </summary>
    ///
    /// <param name="src">Source string.</param>
    ///
    /// <returns>true for containing one or more character.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static bool HasValue(this string src) => !string.IsNullOrEmpty(src);

    /* --------------------------------------------------------------------- */
    ///
    /// Quote
    ///
    /// <summary>
    /// Quotes the specified string.
    /// </summary>
    ///
    /// <param name="src">Source string.</param>
    ///
    /// <returns>Quoted string.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static string Quote(this string src) => $"\"{src}\"";

    /* --------------------------------------------------------------------- */
    ///
    /// FuzzyEquals
    ///
    /// <summary>
    /// Compares the specified string objects in ignoring case.
    /// </summary>
    ///
    /// <param name="src">Source string.</param>
    /// <param name="cmp">String to compare.</param>
    ///
    /// <returns>true for equal; otherwise false.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static bool FuzzyEquals(this string src, string cmp) =>
        src.Equals(cmp, StringComparison.OrdinalIgnoreCase);

    /* --------------------------------------------------------------------- */
    ///
    /// FuzzyStartsWith
    ///
    /// <summary>
    /// Determines whether the beginning of this string instance
    /// matches the specified string in ignoring case.
    /// </summary>
    ///
    /// <param name="src">Source string.</param>
    /// <param name="cmp">String to compare.</param>
    ///
    /// <returns>true for match; otherwise, false.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static bool FuzzyStartsWith(this string src, string cmp) =>
        src.StartsWith(cmp, StringComparison.OrdinalIgnoreCase);

    /* --------------------------------------------------------------------- */
    ///
    /// FuzzyStartsWith
    ///
    /// <summary>
    /// Determines whether the end of this string instance matches the
    /// specified string in ignoring case.
    /// </summary>
    ///
    /// <param name="src">Source string.</param>
    /// <param name="cmp">String to compare.</param>
    ///
    /// <returns>true for match; otherwise, false.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static bool FuzzyEndsWith(this string src, string cmp) =>
        src.EndsWith(cmp, StringComparison.OrdinalIgnoreCase);
}
