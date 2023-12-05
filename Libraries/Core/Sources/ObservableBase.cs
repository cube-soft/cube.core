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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

/* ------------------------------------------------------------------------- */
///
/// ObservableBase
///
/// <summary>
/// Represents the base class that has features of DisposableBase and
/// ObservableBase classes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public abstract class ObservableBase : DisposableBase, INotifyPropertyChanged
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// ObservableBase
    ///
    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected ObservableBase() : this(0) { }

    /* --------------------------------------------------------------------- */
    ///
    /// ObservableBase
    ///
    /// <summary>
    /// Initializes a new instance with the specified arguments.
    /// </summary>
    ///
    /// <param name="capacity">
    /// Capacity of the internal hash table. If zero is specified,
    /// the initial capacity of the Hashtable class will be used.
    /// </param>
    ///
    /// <remarks>
    /// Due to the specification of the Hashtable class, the actual
    /// capacity will be as follows:
    ///
    /// [0,   3] to  2.16 ( 3 * 0.72),
    /// [4,   7] to  5.04 ( 7 * 0.72),
    /// [8,  11] to  7.92 (11 * 0.72),
    /// [12, 17] to 12.24 (17 * 0.72),
    /// [18, 23] to 16.56 (23 * 0.72),
    /// [24, 29] to 20.88 (29 * 0.72),
    /// [30, 37] to 26.64 (37 * 0.72).
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    protected ObservableBase(int capacity) : this(capacity, Dispatcher.Vanilla) { }

    /* --------------------------------------------------------------------- */
    ///
    /// ObservableBase
    ///
    /// <summary>
    /// Initializes a new instance with the specified arguments.
    /// </summary>
    ///
    /// <param name="dispatcher">Dispatcher object.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected ObservableBase(Dispatcher dispatcher) : this(0, dispatcher) { }

    /* --------------------------------------------------------------------- */
    ///
    /// ObservableBase
    ///
    /// <summary>
    /// Initializes a new instance with the specified arguments.
    /// </summary>
    ///
    /// <param name="capacity">
    /// Capacity of the internal hash table. If zero is specified,
    /// the initial capacity of the Hashtable class will be used.
    /// </param>
    ///
    /// <param name="dispatcher">Dispatcher object.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected ObservableBase(int capacity, Dispatcher dispatcher)
    {
        Dispatcher = dispatcher;
        _fields    = new(capacity);
    }

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
    /// PropertyChanged
    ///
    /// <summary>
    /// Occurs when a property is changed.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public event PropertyChangedEventHandler PropertyChanged;

    /* --------------------------------------------------------------------- */
    ///
    /// OnPropertyChanged
    ///
    /// <summary>
    /// Raises the PropertyChanged event with the provided arguments.
    /// </summary>
    ///
    /// <param name="e">Arguments of the event being raised.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        if (PropertyChanged is null) return;
        Dispatcher.Invoke(() => PropertyChanged(this, e));
    }

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Refresh
    ///
    /// <summary>
    /// Notifies the update of the specified property by raising
    /// the PropertyChanged event.
    /// </summary>
    ///
    /// <param name="name">Property name.</param>
    ///
    /* --------------------------------------------------------------------- */
    public void Refresh(string name) => OnPropertyChanged(new(name));

    /* --------------------------------------------------------------------- */
    ///
    /// Refresh
    ///
    /// <summary>
    /// Notifies the update of the specified properties by raising
    /// the PropertyChanged event.
    /// </summary>
    ///
    /// <param name="name">Property name.</param>
    /// <param name="more">More property names.</param>
    ///
    /* --------------------------------------------------------------------- */
    public void Refresh(string name, params string[] more)
    {
        OnPropertyChanged(new(name));
        Refresh(more.AsEnumerable());
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Refresh
    ///
    /// <summary>
    /// Notifies the update of the specified properties by raising
    /// the PropertyChanged event.
    /// </summary>
    ///
    /// <param name="names">Property names.</param>
    ///
    /* --------------------------------------------------------------------- */
    public void Refresh(IEnumerable<string> names)
    {
        foreach (var s in names) OnPropertyChanged(new(s));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Get
    ///
    /// <summary>
    /// Gets the value of the specified property name. The specified
    /// property will be initialized with the specified creator object
    /// as needed.
    /// </summary>
    ///
    /// <param name="creator">Function to create an initial value.</param>
    /// <param name="name">Name of the property.</param>
    ///
    /// <returns>Value of the property.</returns>
    ///
    /* --------------------------------------------------------------------- */
    protected T Get<T>(Func<T> creator, [CallerMemberName] string name = null)
    {
        if (name is null) return default;

        // ReSharper disable InconsistentlySynchronizedField
        // for performance reasons.
        if (!_fields.ContainsKey(name))
        {
            lock (_fields.SyncRoot)
            {
                if (!_fields.ContainsKey(name)) _fields[name] = creator();
            }
        }
        return (T)_fields[name];
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Get
    ///
    /// <summary>
    /// Gets the value of the specified property name.
    /// </summary>
    ///
    /// <param name="name">Name of the property.</param>
    ///
    /// <returns>Value of the property.</returns>
    ///
    /* --------------------------------------------------------------------- */
    protected T Get<T>([CallerMemberName] string name = null) =>
        Get(() => default(T), name);

    /* --------------------------------------------------------------------- */
    ///
    /// Set
    ///
    /// <summary>
    /// Sets the specified value to the inner field of the specified
    /// name if they are not equal.
    /// </summary>
    ///
    /// <param name="value">Value being set.</param>
    /// <param name="name">Name of the property.</param>
    ///
    /// <returns>True for done; false for cancel.</returns>
    ///
    /* --------------------------------------------------------------------- */
    protected bool Set<T>(T value, [CallerMemberName] string name = null) =>
        Set(value, EqualityComparer<T>.Default, name);

    /* --------------------------------------------------------------------- */
    ///
    /// Set
    ///
    /// <summary>
    /// Sets the specified value to the inner field of the specified
    /// name if they are not equal.
    /// </summary>
    ///
    /// <param name="value">Value being set.</param>
    /// <param name="compare">Function to compare.</param>
    /// <param name="name">Name of the property.</param>
    ///
    /// <returns>True for done; false for cancel.</returns>
    ///
    /* --------------------------------------------------------------------- */
    protected bool Set<T>(T value, IEqualityComparer<T> compare, [CallerMemberName] string name = null)
    {
        if (name is null) return false;

        var src = Get<T>(name);
        var set = SetCore(ref src, value, compare);
        if (set)
        {
            lock (_fields.SyncRoot) _fields[name] = src;
            Refresh(name);
        }
        return set;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Set
    ///
    /// <summary>
    /// Sets the specified value to the specified field if they are
    /// not equal.
    /// </summary>
    ///
    /// <param name="field">Reference to the target field.</param>
    /// <param name="value">Value being set.</param>
    /// <param name="name">Name of the property.</param>
    ///
    /// <returns>True for done; false for cancel.</returns>
    ///
    /* --------------------------------------------------------------------- */
    protected bool Set<T>(ref T field, T value, [CallerMemberName] string name = null) =>
        Set(ref field, value, EqualityComparer<T>.Default, name);

    /* --------------------------------------------------------------------- */
    ///
    /// Set
    ///
    /// <summary>
    /// Set the specified value in the specified field if they are not
    /// equal.
    /// </summary>
    ///
    /// <param name="field">Reference to the target field.</param>
    /// <param name="value">Value being set.</param>
    /// <param name="compare">Function to compare.</param>
    /// <param name="name">Name of the property.</param>
    ///
    /// <returns>True for done; false for cancel.</returns>
    ///
    /* --------------------------------------------------------------------- */
    protected bool Set<T>(ref T field, T value, IEqualityComparer<T> compare,
        [CallerMemberName] string name = null)
    {
        var set = SetCore(ref field, value, compare);
        if (set) Refresh(name);
        return set;
    }

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// SetCore
    ///
    /// <summary>
    /// Sets the specified value to the specified field if they are
    /// not equal.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static bool SetCore<T>(ref T field, T value, IEqualityComparer<T> compare)
    {
        if (compare.Equals(field, value)) return false;
        field = value;
        return true;
    }

    #endregion

    #region Fields
    private readonly Hashtable _fields;
    #endregion
}
