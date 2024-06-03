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
namespace Cube.DataContract;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

/* ------------------------------------------------------------------------- */
///
/// SerializableBase
///
/// <summary>
/// Provides an implementation of the INotifyPropertyChanged interface
/// with the Serializable and DataContract attributes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[Serializable]
[DataContract]
public abstract class SerializableBase : INotifyPropertyChanged
{
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
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) =>
        PropertyChanged?.Invoke(this, e);

    #endregion

    #region Methods

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
        foreach (var s in more) OnPropertyChanged(new(s));
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
        // ReSharper disable once InvertIf
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
    protected T Get<T>([CallerMemberName] string name = null) => Get(() => default(T), name);

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
    /// OnDeserializing
    ///
    /// <summary>
    /// Occurs before deserializing.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [OnDeserializing]
    private void OnDeserializing(StreamingContext context)
    {
        if (_fields is null) _fields = [];
        else _fields.Clear();
    }

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
    private bool SetCore<T>(ref T field, T value, IEqualityComparer<T> compare)
    {
        if (compare.Equals(field, value)) return false;
        field = value;
        return true;
    }

    #endregion

    #region Fields
    private Hashtable _fields = [];
    #endregion
}
