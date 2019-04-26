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
using System.Threading;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// ObservableBase
    ///
    /// <summary>
    /// Provides an implementation of the INotifyPropertyChanged interface.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [DataContract]
    [Serializable]
    public abstract class ObservableBase : INotifyPropertyChanged
    {
        #region Constructor

        /* ----------------------------------------------------------------- */
        ///
        /// ObservableBase
        ///
        /// <summary>
        /// Initializes a new instance of the ObservableProperty class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected ObservableBase() { }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Context
        ///
        /// <summary>
        /// Gets or sets the synchronization context.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [IgnoreDataMember]
        public SynchronizationContext Context
        {
            get => _context;
            set => _context = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Synchronous
        ///
        /// <summary>
        /// Gets or sets the value indicating whether the event is fired
        /// as synchronously.
        /// </summary>
        ///
        /// <remarks>
        /// true の場合は Send メソッド、false の場合は Post メソッドを
        /// 用いてイベントを伝搬します。Context が null の場合、
        /// このプロパティは無視されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [IgnoreDataMember]
        public bool Synchronous
        {
            get => _synchronous;
            set => _synchronous = value;
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
            if (PropertyChanged != null) Invoke(() => PropertyChanged(this, e));
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Refresh
        ///
        /// <summary>
        /// Notifies the update of the specified property by raising the
        /// PropertyChanged event.
        /// </summary>
        ///
        /// <param name="name">Property name.</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Refresh(string name) => OnPropertyChanged(new PropertyChangedEventArgs(name));

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the specified action with the Synchronization context.
        /// </summary>
        ///
        /// <param name="action">Invoked action.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected void Invoke(Action action)
        {
            if (Context != null)
            {
                if (Synchronous) Context.Send(e => action(), null);
                else Context.Post(e => action(), null);
            }
            else action();
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

        #region Fields
        [NonSerialized] private SynchronizationContext _context;
        [NonSerialized] private bool _synchronous = true;
        #endregion
    }
}
