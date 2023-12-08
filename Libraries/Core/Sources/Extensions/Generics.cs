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
namespace Cube.Generics.Extensions;

/* ------------------------------------------------------------------------- */
///
/// Methods
///
/// <summary>
/// Provides extended methods of generic classes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class Methods
{
    /* --------------------------------------------------------------------- */
    ///
    /// TryCast
    ///
    /// <summary>
    /// Tries to cast the specified object to the specified type.
    /// </summary>
    ///
    /// <typeparam name="T">Type to be cast.</typeparam>
    ///
    /// <param name="src">Source object.</param>
    ///
    /// <returns>Cast object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static T TryCast<T>(this object src) => TryCast(src, default(T));

    /* --------------------------------------------------------------------- */
    ///
    /// TryCast
    ///
    /// <summary>
    /// Tries to cast the specified object to the specified type.
    /// </summary>
    ///
    /// <typeparam name="T">Type to be cast.</typeparam>
    ///
    /// <param name="src">Source object.</param>
    /// <param name="error">
    /// Returned object when the cast is failed.
    /// </param>
    ///
    /// <returns>Cast object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static T TryCast<T>(this object src, T error) => src is T dest ? dest : error;
}
