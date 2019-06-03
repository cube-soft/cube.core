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
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// SerializableBase
    ///
    /// <summary>
    /// Provides an implementation of the INotifyPropertyChanged interface
    /// with the Serializable and DataContract attributes.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    [DataContract]
    public abstract class SerializableBase : INotifyPropertyChanged
    {
        #region Constructor

        /* ----------------------------------------------------------------- */
        ///
        /// SerializableBase
        ///
        /// <summary>
        /// Initializes a new instance of the ObservableBase class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected SerializableBase() : this(Cube.Dispatcher.Vanilla) { }

        /* ----------------------------------------------------------------- */
        ///
        /// SerializableBase
        ///
        /// <summary>
        /// Initializes a new instance of the ObservableBase class with
        /// the specified dispatcher.
        /// </summary>
        ///
        /// <param name="dispatcher">Dispatcher object.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected SerializableBase(IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }

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
        [IgnoreDataMember]
        public IDispatcher Dispatcher
        {
            get => _dispatcher;
            set => _dispatcher = value;
        }

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
        /// <param name="name">Property name.</param>
        /// <param name="more">More property names.</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Refresh(string name, params string[] more)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(name));
            foreach (var s in more) OnPropertyChanged(new PropertyChangedEventArgs(name));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetProperty
        ///
        /// <summary>
        /// Sets the specified value for the specified field.
        /// </summary>
        ///
        /// <param name="field">Reference to the target field.</param>
        /// <param name="value">Value being set.</param>
        /// <param name="name">Name of the property.</param>
        ///
        /// <returns>True for done; false for cancel.</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected bool SetProperty<T>(ref T field, T value,
            [CallerMemberName] string name = null) =>
            SetProperty(ref field, value, EqualityComparer<T>.Default, name);

        /* ----------------------------------------------------------------- */
        ///
        /// SetProperty
        ///
        /// <summary>
        /// Sets the specified value for the specified field.
        /// </summary>
        ///
        /// <param name="field">Reference to the target field.</param>
        /// <param name="value">Value being set.</param>
        /// <param name="func">Function object to compare.</param>
        /// <param name="name">Name of the property.</param>
        ///
        /// <returns>True for done; false for cancel.</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected bool SetProperty<T>(ref T field, T value,
            IEqualityComparer<T> func, [CallerMemberName] string name = null)
        {
            if (func.Equals(field, value)) return false;
            field = value;
            Refresh(name);
            return true;
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// OnDeserializing
        ///
        /// <summary>
        /// Occurs before deserializing.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            Dispatcher = Cube.Dispatcher.Vanilla;
        }

        #endregion

        #region Fields
        [NonSerialized] private IDispatcher _dispatcher;
        #endregion
    }
}
