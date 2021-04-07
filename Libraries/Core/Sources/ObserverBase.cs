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

namespace Cube
{
    #region IObservePropertyChanged

    /* --------------------------------------------------------------------- */
    ///
    /// IObservePropertyChanged
    ///
    /// <summary>
    /// Provides interface to observe the PropertyChanged event of
    /// INotifyPropertyChanged objects.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public interface IObservePropertyChanged
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Observe
        ///
        /// <summary>
        /// Observes the PropertyChanged event of the specified object.
        /// </summary>
        ///
        /// <param name="src">Observed object.</param>
        /// <param name="names">Target property names.</param>
        ///
        /* ----------------------------------------------------------------- */
        void Observe(INotifyPropertyChanged src, params string[] names);
    }

    #endregion

    #region ObserverBase

    /* --------------------------------------------------------------------- */
    ///
    /// ObserverBase
    ///
    /// <summary>
    /// Represents a base class of the IObservePropertyChnaged interface.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class ObserverBase : DisposableBase, IObservePropertyChanged
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ObserverBase
        ///
        /// <summary>
        /// Initializes a new instance of the ObserverBase class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected ObserverBase() { }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Observe
        ///
        /// <summary>
        /// Observes the PropertyChanged event of the specified object.
        /// </summary>
        ///
        /// <param name="src">Observed object.</param>
        /// <param name="names">
        /// Target property names. If no name is set, the class responds
        /// to all PropertyChanged events.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public void Observe(INotifyPropertyChanged src, params string[] names)
        {
            if (src == null) return;

            var set = new HashSet<string>(names);
            void handler(object s, PropertyChangedEventArgs e) {
                if (set.Count <= 0 || set.Contains(e.PropertyName)) Receive(src.GetType(), e.PropertyName);
            }

            src.PropertyChanged += handler;
            _observer.Add(Disposable.Create(() => src.PropertyChanged -= handler));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Receive
        ///
        /// <summary>
        /// Invokes when the PropertyChanged event of an observed object
        /// is fired.
        /// </summary>
        ///
        /// <param name="type">
        /// Type of object that raises the PropetyChanged event.
        /// </param>
        ///
        /// <param name="name">
        /// Property name associated with the PropetyChanged event.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected abstract void Receive(Type type, string name);

        /* ----------------------------------------------------------------- */
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
        /* ----------------------------------------------------------------- */
        protected override void Dispose(bool disposing)
        {
            if (!disposing) return;
            foreach (var obj in _observer) obj.Dispose();
            _observer.Clear();
        }

        #endregion

        #region Fields
        private readonly List<IDisposable> _observer = new List<IDisposable>();
        #endregion
    }

    #endregion
}
