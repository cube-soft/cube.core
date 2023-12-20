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
using System.Collections;
using System.Collections.Generic;

/* ------------------------------------------------------------------------- */
///
/// SoftwareVersionComparer
///
/// <summary>
/// Provides functionality to compare SoftwareVersion values.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class SoftwareVersionComparer :
    IComparer<SoftwareVersion>,
    IEqualityComparer<SoftwareVersion>,
    IComparer, IEqualityComparer
{
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
    public int Compare(SoftwareVersion x, SoftwareVersion y)
    {
        if (x is null && y is null) return 0;
        if (x is null) return -1;
        if (y is null) return 1;

        var e0 = CompareValue(x.Prefix, y.Prefix);
        if (e0 != 0) return e0;

        var e1 = CompareValue(Zero(x.Number.Major), Zero(y.Number.Major));
        if (e1 != 0) return e1;

        var e2 = CompareValue(Zero(x.Number.Minor), Zero(y.Number.Minor));
        if (e2 != 0) return e2;

        var e3 = CompareValue(Zero(x.Number.Build), Zero(y.Number.Build));
        if (e3 != 0) return e3;

        var e4 = CompareValue(Zero(x.Number.Revision), Zero(y.Number.Revision));
        if (e4 != 0) return e4;

        return CompareValue(x.Suffix, y.Suffix);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Compare
    ///
    /// <summary>
    /// Compares two objects and returns an indication of their
    /// relative sort order.
    /// </summary>
    ///
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    ///
    /// <returns>
    /// Zero if the specified objects are equal;
    /// Less than zero if x is less than y or x is null;
    /// otherwise, Greater than zero.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public int Compare(object x, object y)
    {
        if (x == y) return 0;
        if (x is null) return -1;
        if (y is null) return 1;
        if (x is SoftwareVersion vx && y is SoftwareVersion vy) return Compare(vx, vy);
        if (x is string sx && y is string sy) return Compare(new(sx), new(sy));
        if (x is IComparable cx) return cx.CompareTo(y);
        throw new ArgumentException();
    }

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
    public bool Equals(SoftwareVersion x, SoftwareVersion y) => Compare(x, y) == 0;

    /* --------------------------------------------------------------------- */
    ///
    /// Equals
    ///
    /// <summary>
    /// Determines whether two objects are equal.
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
    public new bool Equals(object x, object y)
    {
        if (x == y) return true;
        if (x is null || y is null) return false;
        if (x is SoftwareVersion vx && y is SoftwareVersion vy) return Equals(vx, vy);
        if (x is string sx && y is string sy) return Equals(new(sx), new(sy));
        return x.Equals(y);
    }

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
    public int GetHashCode(SoftwareVersion obj) => HashCode.Create(
        obj.Prefix.ToLowerInvariant().GetHashCode(),
        Zero(obj.Number.Major),
        Zero(obj.Number.Minor),
        Zero(obj.Number.Build),
        Zero(obj.Number.Revision),
        obj.Suffix.ToLowerInvariant().GetHashCode()
    );

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
    public int GetHashCode(object obj) => obj switch
    {
        SoftwareVersion v => GetHashCode(v),
        string s          => GetHashCode(new(s)),
        _                 => obj.GetHashCode()
    };

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// Zero
    ///
    /// <summary>
    /// Returns zero if the specified value is negative.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private int Zero(int src) => src > 0 ? src : 0;

    /* --------------------------------------------------------------------- */
    ///
    /// CompareValue
    ///
    /// <summary>
    /// Compares the specified values.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private int CompareValue(int src, int cmp)
    {
        var value = src - cmp;
        return value < 0 ? -1 :
               value > 0 ? 1 : 0;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// CompareValue
    ///
    /// <summary>
    /// Compares the specified values.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private int CompareValue(string src, string cmp) =>
        string.Compare(src, cmp, StringComparison.InvariantCultureIgnoreCase);

    #endregion
}
