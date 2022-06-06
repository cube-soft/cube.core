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
/// DisposableBase
///
/// <summary>
/// Represents an implementation of the IDisposable interface.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public abstract class DisposableBase : IDisposable
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// DisposableBase
    ///
    /// <summary>
    /// Creates a new instance of the DisposableBase class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected DisposableBase() => _dispose = new(Dispose);

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Disposed
    ///
    /// <summary>
    /// Gets the value indicating whether the object is disposed.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public bool Disposed => _dispose.Invoked;

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// ~DisposableBase
    ///
    /// <summary>
    /// Finalizes the DisposableBase.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    ~DisposableBase() => _dispose?.Invoke(false);

    /* --------------------------------------------------------------------- */
    ///
    /// Dispose
    ///
    /// <summary>
    /// Releases all resources used by the object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public void Dispose()
    {
        _dispose.Invoke(true);
        GC.SuppressFinalize(this);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Dispose
    ///
    /// <summary>
    /// Releases the unmanaged resources used by the object and
    /// optionally releases the managed resources.
    /// </summary>
    ///
    /// <param name="disposing">
    /// true to release both managed and unmanaged resources;
    /// false to release only unmanaged resources.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    protected abstract void Dispose(bool disposing);

    #endregion

    #region Fields
    private readonly OnceAction<bool> _dispose;
    #endregion
}
