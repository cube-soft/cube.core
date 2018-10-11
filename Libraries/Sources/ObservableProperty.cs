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
    /// ObservableProperty
    ///
    /// <summary>
    /// Represents an implementation of the INotifyPropertyChanged
    /// interface.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [DataContract]
    [Serializable]
    public abstract class ObservableProperty : INotifyPropertyChanged
    {
        #region Constructor

        /* ----------------------------------------------------------------- */
        ///
        /// ObservableProperty
        ///
        /// <summary>
        /// Initializes a new instance of the <c>ObservableProperty</c>
        /// class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected ObservableProperty() { }

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
        public SynchronizationContext Context { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// IsSynchronous
        ///
        /// <summary>
        /// Gets or sets the value indicating whether the event is fired
        /// as synchronously.
        /// </summary>
        ///
        /// <remarks>
        /// true の場合は Send メソッド、false の場合は Post メソッドを
        /// 用いてイベントを伝搬します。また、Context が null の場合、
        /// このプロパティは無視されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [IgnoreDataMember]
        public bool IsSynchronous { get; set; } = true;

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// PropertyChanged
        ///
        /// <summary>
        /// Occurs when a property of the class is changed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event PropertyChangedEventHandler PropertyChanged;

        /* ----------------------------------------------------------------- */
        ///
        /// OnPropertyChanged
        ///
        /// <summary>
        /// Raises the <c>PropertyChanged</c> event with the provided
        /// arguments.
        /// </summary>
        ///
        /// <param name="e">Arguments of the event being raised.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) =>
            PropertyChanged?.Invoke(this, e);

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// RaisePropertyChanged
        ///
        /// <summary>
        /// Raises the <c>PropertyChanged</c> event with the specified
        /// name.
        /// </summary>
        ///
        /// <param name="name">Name of the property.</param>
        ///
        /* ----------------------------------------------------------------- */
        public void RaisePropertyChanged(string name)
        {
            var e = new PropertyChangedEventArgs(name);
            if (Context != null)
            {
                if (IsSynchronous) Context.Send(_ => OnPropertyChanged(e), null);
                else Context.Post(_ => OnPropertyChanged(e), null);
            }
            else OnPropertyChanged(e);
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
            RaisePropertyChanged(name);
            return true;
        }

        #endregion
    }
}
