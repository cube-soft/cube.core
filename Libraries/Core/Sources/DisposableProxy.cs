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
/// DisposableProxy
///
/// <summary>
/// Provides functionality to invoke the provided IDisposable object
/// when disposing.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public abstract class DisposableProxy : DisposableBase
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// DisposableProxy
    ///
    /// <summary>
    /// Initializes a new instance of the DisposableProxy with the
    /// specified arguments.
    /// </summary>
    ///
    /// <param name="func">
    /// Function to create an IDisposable object.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    protected DisposableProxy(Func<IDisposable> func) : this(func()) { }

    /* --------------------------------------------------------------------- */
    ///
    /// DisposableProxy
    ///
    /// <summary>
    /// Initializes a new instance of the DisposableProxy with the
    /// specified arguments.
    /// </summary>
    ///
    /// <param name="disposable">IDisposable object.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected DisposableProxy(IDisposable disposable) =>
        _disposable = disposable ?? throw new ArgumentNullException();

    #endregion

    #region Methods

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
    protected override void Dispose(bool disposing)
    {
        if (disposing) _disposable.Dispose();
    }

    #endregion

    #region Fields
    private readonly IDisposable _disposable;
    #endregion
}
