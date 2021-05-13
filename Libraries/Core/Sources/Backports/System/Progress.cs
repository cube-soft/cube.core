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
using System.Threading;

namespace System
{
    #region IProgress<T>

    /* --------------------------------------------------------------------- */
    ///
    /// IProgress(T)
    ///
    /// <summary>
    /// Defines a provider for progress updates.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public interface IProgress<T>
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Report
        ///
        /// <summary>
        /// Reports a progress update.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        void Report(T value);
    }

    #endregion

    #region Progress<T>

    /* --------------------------------------------------------------------- */
    ///
    /// Progress(T)
    ///
    /// <summary>
    /// Provides an IProgress(T) that invokes callbacks for each reported
    /// progress value.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Progress<T> : IProgress<T> where T : EventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Progress
        ///
        /// <summary>
        /// Initializes the Progress(T) object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Progress()
        {
            _context = SynchronizationContext.Current;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Progress
        ///
        /// <summary>
        /// Initializes the Progress(T) object with the specified callback.
        /// </summary>
        ///
        /// <param name="handler">
        /// A handler to invoke for each reported progress value.
        /// This handler will be invoked in addition to any delegates
        /// registered with the ProgressChanged event. Depending on the
        /// SynchronizationContext instance captured by the Progress(T)
        /// at construction, it is possible that this handler instance
        /// could be invoked concurrently with itself.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public Progress(Action<T> handler) : this()
        {
            ProgressChanged += (s, e) => handler(e);
        }

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// ProgressChanged
        ///
        /// <summary>
        /// Raised for each reported progress value.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler<T> ProgressChanged;

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Report
        ///
        /// <summary>
        /// Reports a progress change.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Report(T value) => OnReport(value);

        /* ----------------------------------------------------------------- */
        ///
        /// OnReport
        ///
        /// <summary>
        /// Reports a progress change.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnReport(T value)
        {
            if (ProgressChanged == null) return;
            if (_context != null) _context.Post(_ => ProgressChanged(this, value), null);
            else ProgressChanged(this, value);
        }

        #endregion

        #region Fields
        private readonly SynchronizationContext _context;
        #endregion
    }

    #endregion
}
