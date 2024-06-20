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
using System.Text;

/* ------------------------------------------------------------------------- */
///
/// NumericStringComparer
///
/// <summary>
/// Represents a string comparison operation that considers numeric
/// values.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class NumericStringComparer : StringComparer
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// NumericStringComparer
    ///
    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public NumericStringComparer() : this(Ordinal) { }

    /* --------------------------------------------------------------------- */
    ///
    /// NumericStringComparer
    ///
    /// <summary>
    /// Initializes a new instance with the specified arguments.
    /// </summary>
    ///
    /// <param name="src">Raw string comparer.</param>
    ///
    /* --------------------------------------------------------------------- */
    public NumericStringComparer(StringComparer src) => _inner = src;

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Compare
    ///
    /// <summary>
    /// Compares two string objects and returns an indication of their
    /// relative sort order.
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
    public override int Compare(string x, string y)
    {
        if (x is null && y is null) return 0;
        if (x is null) return -1;
        if (y is null) return 1;

        return Compare(x.GetEnumerator(), y.GetEnumerator());
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Equals
    ///
    /// <summary>
    /// Determines whether two string objects are equal.
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
    public override bool Equals(string x, string y) => _inner.Equals(x, y);

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
    public override int GetHashCode(string obj) => _inner.GetHashCode(obj);

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// Compare
    ///
    /// <summary>
    /// Compares two objects and returns an indication of their relative
    /// sort order.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private int Compare(CharEnumerator x, CharEnumerator y)
    {
        while (true)
        {
            var zx = x.MoveNext();
            var zy = y.MoveNext();

            if (!zx && !zy) return  0;
            if (!zx)        return -1;
            if (!zy)        return  1;

            if (char.IsDigit(x.Current) && char.IsDigit(y.Current))
            {
                var sx = GetNumericString(x);
                var sy = GetNumericString(y);

                // 1. compare as numeric
                if (long.TryParse(sx, out var nx) && long.TryParse(sy, out var ny))
                {
                    var z0 = nx.CompareTo(ny);
                    if (z0 != 0) return z0;
                }

                // 2. compare as string
                var z1 = _inner.Compare(sx, sy);
                if (z1 != 0) return z1;
            }
            else
            {
                var z = _inner.Compare(x.Current.ToString(), y.Current.ToString());
                if (z != 0) return z;
            }
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetNumericString
    ///
    /// <summary>
    /// Gets a substring of the specified char enumerator object
    /// that represents a numeric value.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private string GetNumericString(CharEnumerator src)
    {
        var dest = new StringBuilder();
        while (char.IsNumber(src.Current))
        {
            _ = dest.Append(src.Current);
            if (!src.MoveNext()) break;
        }
        return dest.ToString();
    }

    #endregion

    #region Fields
    private readonly StringComparer _inner;
    #endregion
}
