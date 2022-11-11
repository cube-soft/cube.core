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
namespace Cube.Tests;

using System.ComponentModel;
using System.Threading;
using Cube.Observable.Extensions;

/* ------------------------------------------------------------------------- */
///
/// PropertyChangedCounter
///
/// <summary>
/// Provides functionality to count the number of times PropertyChanged
/// events occur.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class PropertyChangedCounter : DisposableBase
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// PropertyChangedCounter
    ///
    /// <summary>
    /// Initializes a new instance of the PropertyChangedCounter class with
    /// the specified arguments.
    /// </summary>
    ///
    /// <param name="src">Target objects.</param>
    ///
    /* --------------------------------------------------------------------- */
    public PropertyChangedCounter(params INotifyPropertyChanged[] src)
    {
        foreach (var e in src)
        {
            _disposable.Add(e.Subscribe(_ => Interlocked.Increment(ref _value)));
        }
    }

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Value
    ///
    /// <summary>
    /// Gets the current number of times PropertyChanged events occur.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public int Value => _value;

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
        Logger.Debug(Value.ToString());
        _disposable.Dispose();
    }

    #endregion

    #region Fields
    private readonly DisposableContainer _disposable = new();
    private int _value = 0;
    #endregion
}
