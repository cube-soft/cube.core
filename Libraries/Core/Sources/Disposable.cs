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
/// Disposable
///
/// <summary>
/// Provides functionality to create a IDisposable object.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class Disposable
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Create
    ///
    /// <summary>
    /// Creates a IDisposable object from the specified action.
    /// </summary>
    ///
    /// <param name="dispose">Invoke when disposed.</param>
    ///
    /// <returns>IDisposable object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static IDisposable Create(Action dispose)
    {
        if (dispose is null) throw new ArgumentNullException(nameof(dispose));
        return new DisposableCore(dispose);
    }

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// DisposableCore
    ///
    /// <summary>
    /// Represents an implementation to execute the provided action
    /// as an IDisposable manner.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private class DisposableCore : IDisposable
    {
        public DisposableCore(Action src) => _dispose = new(src);
        public void Dispose() => _dispose.Invoke();
        private readonly OnceAction _dispose;
    }

    #endregion
}
