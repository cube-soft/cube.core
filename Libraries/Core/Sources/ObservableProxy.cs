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
using Cube.Mixin.Observing;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// ObservableProxy
    ///
    /// <summary>
    /// Provides functionality to forward the PropertyChanged event to
    /// another object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ObservableProxy : DisposableBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ObservableProxy
        ///
        /// <summary>
        /// Creates a new instance of the ObservableProxy class with the
        /// specified arguments.
        /// </summary>
        ///
        /// <param name="src">Source object to forward.</param>
        /// <param name="dest">Target object to forward.</param>
        ///
        /* ----------------------------------------------------------------- */
        public ObservableProxy(INotifyPropertyChanged src, ObservableBase dest) :
            this(src, dest, new Dictionary<string, IEnumerable<string>>()) { }

        /* ----------------------------------------------------------------- */
        ///
        /// ObservableProxy
        ///
        /// <summary>
        /// Creates a new instance of the ObservableProxy class with the
        /// specified arguments.
        /// </summary>
        ///
        /// <param name="src">Source object to forward.</param>
        /// <param name="dest">Target object to forward.</param>
        /// <param name="rules">Forwarding rules.</param>
        ///
        /* ----------------------------------------------------------------- */
        public ObservableProxy(INotifyPropertyChanged src, ObservableBase dest,
            IDictionary<string, IEnumerable<string>> rules)
        {
            Rules = rules;
            _disposable = src.Subscribe(e => {
                if (Rules.TryGetValue(e, out var v)) dest.Refresh(v);
                else if (!MatchOnly) dest.Refresh(e);
            });
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Rules
        ///
        /// <summary>
        /// Get the forwarding rules.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IDictionary<string, IEnumerable<string>> Rules { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// MatchOnly
        ///
        /// <summary>
        /// Gets or sets a value indicating whether to forward property names
        /// that do not match the provided rules.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool MatchOnly { get; set; } = false;

        #endregion

        #region Implementations

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
            if (disposing) _disposable.Dispose();
        }

        #endregion

        #region Fields
        private readonly IDisposable _disposable;
        #endregion
    }
}
