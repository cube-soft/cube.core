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
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Threading;

namespace Cube.Collections
{
    /* --------------------------------------------------------------------- */
    ///
    /// EnumerableBase
    ///
    /// <summary>
    /// Represents the base class of a dynamic data collection that
    /// provides notifications when items get added, removed, or when the
    /// whole list is refreshed.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [DataContract]
    [Serializable]
    public abstract class ObservableBase<T> : EnumerableBase<T>, INotifyCollectionChanged
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ObservableBase
        ///
        /// <summary>
        /// Initializes a new instance of the ObservableBase class.
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

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// CollectionChanged
        ///
        /// <summary>
        /// Occurs when an item is added, removed, changed, moved,
        /// or the entire list is refreshed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /* ----------------------------------------------------------------- */
        ///
        /// OnCollectionChanged
        ///
        /// <summary>
        /// Raises the CollectionChanged event with the provided arguments.
        /// </summary>
        ///
        /// <param name="e">Arguments of the event being raised.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged == null) return;
            if (Context != null) Context.Send(z => CollectionChanged(this, e), null);
            else CollectionChanged(this, e);
        }

        #endregion

        #region Fields
        [NonSerialized] private SynchronizationContext _context;
        #endregion
    }
}
