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
namespace Cube.Collections;

using System.Collections.Specialized;

/* ------------------------------------------------------------------------- */
///
/// ObservableCollectionBase
///
/// <summary>
/// Represents the base class of a dynamic data collection that
/// provides notifications when items get added, removed, or when the
/// whole list is refreshed.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public abstract class ObservableCollectionBase<T> : EnumerableBase<T>, INotifyCollectionChanged
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// ObservableCollectionBase
    ///
    /// <summary>
    /// Initializes a new instance of the ObservableCollectionBase class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected ObservableCollectionBase() : this(Dispatcher.Vanilla) { }

    /* --------------------------------------------------------------------- */
    ///
    /// ObservableCollectionBase
    ///
    /// <summary>
    /// Initializes a new instance of the ObservableCollectionBase class
    /// with the specified dispatcher.
    /// </summary>
    ///
    /// <param name="dispatcher">Dispatcher object.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected ObservableCollectionBase(Dispatcher dispatcher) => Dispatcher = dispatcher;

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Dispatcher
    ///
    /// <summary>
    /// Gets or sets the dispatcher object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected Dispatcher Dispatcher { get; set; }

    #endregion

    #region Events

    /* --------------------------------------------------------------------- */
    ///
    /// CollectionChanged
    ///
    /// <summary>
    /// Occurs when an item is added, removed, changed, moved,
    /// or the entire list is refreshed.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public event NotifyCollectionChangedEventHandler CollectionChanged;

    /* --------------------------------------------------------------------- */
    ///
    /// OnCollectionChanged
    ///
    /// <summary>
    /// Raises the CollectionChanged event with the provided arguments.
    /// </summary>
    ///
    /// <param name="e">Arguments of the event being raised.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        if (CollectionChanged is not null) Dispatcher.Invoke(() => CollectionChanged(this, e));
    }

    #endregion
}
