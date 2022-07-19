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
namespace Cube.Mixin.Iteration;

using System;
using System.Collections.Generic;
using System.Linq;

/* ------------------------------------------------------------------------- */
///
/// Extension
///
/// <summary>
/// Provides extended methods about iteration.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class Extension
{
    /* --------------------------------------------------------------------- */
    ///
    /// Make
    ///
    /// <summary>
    /// Makes the specified number of sequence with the specified
    /// function.
    /// </summary>
    ///
    /// <param name="n">Number of sequence.</param>
    /// <param name="func">Function to create an element.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static IEnumerable<T> Make<T>(this int n, Func<int, T> func) =>
        Enumerable.Range(0, n).Select(i => func(i));
}
