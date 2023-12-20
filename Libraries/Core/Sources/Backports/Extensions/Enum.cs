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
namespace Cube.Backports;

using System;

/* ------------------------------------------------------------------------- */
///
/// EnumExtensions
///
/// <summary>
/// Provides extended methods for the Enum class for compatibility with
/// .NET Framework 3.5.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class EnumExtensions
{
    /* --------------------------------------------------------------------- */
    ///
    /// HasFlag
    ///
    /// <summary>
    /// Determines whether one or more bit fields are set in the current
    /// instance.
    /// </summary>
    ///
    /// <param name="src">A source Enum object.</param>
    /// <param name="flag">An enumeration value.</param>
    ///
    /// <returns>
    /// true if the bit field or bit fields that are set in flag are also
    /// set in the current instance; otherwise, false.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public static bool HasFlag(this Enum src, Enum flag)
    {
        if (src.GetType() != flag.GetType()) throw new ArgumentException();

        var n0 = Convert.ToUInt64(src);
        var n1 = Convert.ToUInt64(flag);

        return (n0 & n1) == n1;
    }
}
