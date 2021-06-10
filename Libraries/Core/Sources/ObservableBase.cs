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
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Cube.Mixin.Generics;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// ObservableBase
    ///
    /// <summary>
    /// Represents the base class that has features of DisposableBase and
    /// ObservableBase classes.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class ObservableBase : DisposableBase, INotifyPropertyChanged
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ObservableBase
        ///
        /// <summary>
        /// Initializes a new instance of the DisposableObservable class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected ObservableBase() : this(Dispatcher.Vanilla) { }

        /* ----------------------------------------------------------------- */
        ///
        /// ObservableBase
        ///
        /// <summary>
        /// Initializes a new instance of the ObservableBasee class
        /// with the specified dispatcher.
        /// </summary>
        ///
        /// <param name="dispatcher">Dispatcher object.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected ObservableBase(Dispatcher dispatcher) : base() { Dispatcher = dispatcher; }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Dispatcher
        ///
        /// <summary>
        /// Gets or sets the dispatcher object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected Dispatcher Dispatcher { get; set; }

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// PropertyChanged
        ///
        /// <summary>
        /// Occurs when a property is changed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event PropertyChangedEventHandler PropertyChanged;

        /* ----------------------------------------------------------------- */
        ///
        /// OnPropertyChanged
        ///
        /// <summary>
        /// Raises the PropertyChanged event with the provided arguments.
        /// </summary>
        ///
        /// <param name="e">Arguments of the event being raised.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged == null) return;
            Dispatcher.Invoke(() => PropertyChanged(this, e));
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
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
        /* ----------------------------------------------------------------- */
        public void Refresh(IEnumerable<string> names)
        {
            foreach (var s in names) OnPropertyChanged(new(s));
        }

        /* ----------------------------------------------------------------- */
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
        /* ----------------------------------------------------------------- */
        public void Refresh(string name, params string[] more)
        {
            OnPropertyChanged(new(name));
            Refresh(more.AsEnumerable());
        }

        /* ----------------------------------------------------------------- */
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
        /* ----------------------------------------------------------------- */
        protected T Get<T>(Func<T> creator, [CallerMemberName] string name = null)
        {
            if (!_fields.ContainsKey(name))
            {
                lock (_fields.SyncRoot) _fields[name] = creator();
            }
            return (T)_fields[name];
        }

        /* ----------------------------------------------------------------- */
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
        /* ----------------------------------------------------------------- */
        protected T Get<T>([CallerMemberName] string name = null) =>
            Get(() => default(T), name);

        /* ----------------------------------------------------------------- */
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
        /* ----------------------------------------------------------------- */
        protected bool Set<T>(T value, [CallerMemberName] string name = null) =>
            Set(value, EqualityComparer<T>.Default, name);

        /* ----------------------------------------------------------------- */
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
        /* ----------------------------------------------------------------- */
        protected bool Set<T>(T value, IEqualityComparer<T> compare, [CallerMemberName] string name = null)
        {
            var src = Get<T>(name);
            var set = compare.Set(ref src, value);
            if (set)
            {
                lock (_fields.SyncRoot) _fields[name] = src;
                Refresh(name);
            }
            return set;
        }

        /* ----------------------------------------------------------------- */
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
        /* ----------------------------------------------------------------- */
        protected bool Set<T>(ref T field, T value, [CallerMemberName] string name = null) =>
            Set(ref field, value, EqualityComparer<T>.Default, name);

        /* ----------------------------------------------------------------- */
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
        /* ----------------------------------------------------------------- */
        protected bool Set<T>(ref T field, T value, IEqualityComparer<T> compare,
            [CallerMemberName] string name = null)
        {
            var set = compare.Set(ref field, value);
            if (set) Refresh(name);
            return set;
        }

        #endregion

        #region Fields
        private readonly Hashtable _fields = new();
        #endregion
    }
}
