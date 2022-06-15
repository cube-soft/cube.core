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

using System.Linq;

/* ------------------------------------------------------------------------- */
///
/// HashCode
///
/// <summary>
/// Provides functionality to calculate the value used in the
/// GetHashCode method.
/// </summary>
///
/// <remarks>
/// Implementation is referenced System.Numerics.Hashing.HashHelpers
/// in the .NET CoreRT project.
/// </remarks>
///
/// <seealso href="https://github.com/dotnet/corert" />
///
/* ------------------------------------------------------------------------- */
public static class HashCode
{
    /* --------------------------------------------------------------------- */
    ///
    /// Create
    ///
    /// <summary>
    /// Creates a value used in the GetHashCode method with the
    /// specified values.
    /// </summary>
    ///
    /// <param name="values">Source values</param>
    ///
    /// <returns>Calculated hash.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static int Create(params int[] values) => values.Aggregate((hash, e) =>
    {
        var rol5 = ((uint)hash << 5) | ((uint)hash >> 27);
        return ((int)rol5 + hash) ^ e;
    });
}
