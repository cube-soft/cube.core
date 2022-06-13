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

/* ------------------------------------------------------------------------- */
///
/// DisposableExtension
///
/// <summary>
/// Provides extended methods of the DisposableBase and inherited
/// classes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class DisposableExtension
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Hook
    ///
    /// <summary>
    /// Adds the specified object and returns it.
    /// </summary>
    ///
    /// <param name="src">Source container.</param>
    /// <param name="obj">Object to be added.</param>
    ///
    /// <returns>Same as the specified object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static T Hook<T>(this DisposableContainer src, T obj) where T : IDisposable
    {
        src.Add(obj);
        return obj;
    }

    #endregion
}
