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
        protected ObservableBase() : this(Invoker.Vanilla) { }

        /* ----------------------------------------------------------------- */
        ///
        /// ObservableBase
        ///
        /// <summary>
        /// Initializes a new instance of the ObservableBasee class
        /// with the specified invoker.
        /// </summary>
        ///
        /// <param name="invoker">Invoker object.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected ObservableBase(Invoker invoker) : base()
        {
            Invoker = invoker;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Invoker
        ///
        /// <summary>
        /// Gets or sets the invoker object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected Invoker Invoker { get; set; }

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
            Invoker.Invoke(() => PropertyChanged(this, e));
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
        /// <param name="name">Property name.</param>
        /// <param name="more">More property names.</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Refresh(string name, params string[] more)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(name));
            foreach (var s in more) OnPropertyChanged(new PropertyChangedEventArgs(s));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetProperty
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
        protected T GetProperty<T>(Func<T> creator, [CallerMemberName] string name = null)
        {
            if (!_fields.ContainsKey(name)) _fields.Add(name, creator());
            return (T)_fields[name];
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetProperty
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
        protected T GetProperty<T>([CallerMemberName] string name = null) =>
            GetProperty(() => default(T), name);

        /* ----------------------------------------------------------------- */
        ///
        /// SetProperty
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
        protected bool SetProperty<T>(T value, [CallerMemberName] string name = null) =>
            SetProperty(value, EqualityComparer<T>.Default, name);

        /* ----------------------------------------------------------------- */
        ///
        /// SetProperty
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
        protected bool SetProperty<T>(T value, IEqualityComparer<T> compare, [CallerMemberName] string name = null)
        {
            var src = GetProperty<T>(name);
            var set = compare.Set(ref src, value);
            if (set)
            {
                _fields[name] = src;
                Refresh(name);
            }
            return set;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetProperty
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
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string name = null) =>
            SetProperty(ref field, value, EqualityComparer<T>.Default, name);

        /* ----------------------------------------------------------------- */
        ///
        /// SetProperty
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
        protected bool SetProperty<T>(ref T field, T value, IEqualityComparer<T> compare, [CallerMemberName] string name = null)
        {
            var set = compare.Set(ref field, value);
            if (set) Refresh(name);
            return set;
        }

        #endregion

        #region Fields
        private readonly Hashtable _fields = new Hashtable();
        #endregion
    }
}
