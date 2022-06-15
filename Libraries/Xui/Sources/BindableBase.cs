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
namespace Cube.Xui;

using System.Collections.Generic;
using System.ComponentModel;

/* ------------------------------------------------------------------------- */
///
/// BindableBase
///
/// <summary>
/// Represents the base behavior of Bindable classes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public abstract class BindableBase : ObservableBase, IObservePropertyChanged
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// BindableBase
    ///
    /// <summary>
    /// Initializes a new instance of the BindableBase class with the
    /// specified dispatcher.
    /// </summary>
    ///
    /// <param name="dispatcher">Dispatcher object.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected BindableBase(Dispatcher dispatcher) : base(dispatcher) { }

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Observe
    ///
    /// <summary>
    /// Observes the PropertyChanged event of the specified object.
    /// </summary>
    ///
    /// <param name="src">Object to be observed.</param>
    /// <param name="names">Target property names.</param>
    ///
    /* --------------------------------------------------------------------- */
    public void Observe(INotifyPropertyChanged src, params string[] names)
    {
        var set = new HashSet<string>(names);
        void handler(object s, PropertyChangedEventArgs e)
        {
            if (set.Count <= 0 || set.Contains(e.PropertyName)) React();
        }

        src.PropertyChanged += handler;
        _observable.Add(() => src.PropertyChanged -= handler);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// React
    ///
    /// <summary>
    /// Invokes when the PropertyChanged event of an observed object
    /// is fired.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected abstract void React();

    /* --------------------------------------------------------------------- */
    ///
    /// Dispose
    ///
    /// <summary>
    /// Releases the unmanaged resources used by the object
    /// and optionally releases the managed resources.
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
        if (disposing) _observable.Dispose();
    }

    /* --------------------------------------------------------------------- */
    ///
    /// OnPropertyChanged
    ///
    /// <summary>
    /// Occurs when the PropertyChanged event is fired.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        if (!Disposed) base.OnPropertyChanged(e);
    }

    #endregion

    #region Fields
    private readonly DisposableContainer _observable = new();
    #endregion
}
