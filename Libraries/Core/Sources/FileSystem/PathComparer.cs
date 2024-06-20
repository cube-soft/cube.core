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
namespace Cube.FileSystem;

using System;
using System.Collections.Generic;
using System.Linq;
using Cube.Collections;

/* ------------------------------------------------------------------------- */
///
/// PathComparer
///
/// <summary>
/// Represents a string comparison operation as a path.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class PathComparer : StringComparer
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// PathComparer
    ///
    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public PathComparer() : this(Ordinal) { }

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
    public PathComparer(StringComparer src) => _inner = new(src);

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
    public override int Compare(string x,string y)
    {
        if (x is null && y is null) return 0;
        if (x is null) return -1;
        if (y is null) return  1;

        var dx = Io.IsDirectory(x);
        var dy = Io.IsDirectory(y);

        var lx = Split(x).ToList();
        var ly = Split(y).ToList();

        for (var i = 0; i < Math.Max(lx.Count, ly.Count); ++i)
        {
            if (i >= lx.Count) return -1;
            if (i >= ly.Count) return  1;

            Get(lx, i, dx, out var fx, out var ex);
            Get(ly, i, dy, out var fy, out var ey);

            var n0 = _inner.Compare(fx, fy);
            if (n0 != 0) return n0;

            var n1 = _inner.Compare(ex, ey);
            if (n1 != 0) return n1;
        }

        return 0;
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
    /// Split
    ///
    /// <summary>
    /// Splits the specified path.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static IEnumerable<string> Split(string src) => new SafePath(src)
    {
        AllowInactivation     = false,
        AllowDriveLetter      = true,
        AllowCurrentDirectory = false,
        AllowParentDirectory  = false,
        AllowUnc              = false,
    }.Parts;

    /* --------------------------------------------------------------------- */
    ///
    /// Get
    ///
    /// <summary>
    /// Gets the basename and extension from the specified arguments.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void Get(List<string> src, int i, bool dir, out string basename, out string extension)
    {
        var s = src[i];

        if (!dir && i == src.Count - 1)
        {
            basename  = Io.GetBaseName(s);
            extension = Io.GetExtension(s);
        }
        else
        {
            basename  = s;
            extension = string.Empty;
        }
    }

    #endregion

    #region Fields
    private readonly NumericStringComparer _inner;
    #endregion
}
