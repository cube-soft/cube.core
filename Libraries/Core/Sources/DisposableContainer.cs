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
using System.Collections.Concurrent;
using Cube.Mixin.Collections;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// DisposableContainer
    ///
    /// <summary>
    /// Provides functionality to invoke the provided IDisposable objects
    /// at once.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class DisposableContainer : DisposableBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// DisposableContainer
        ///
        /// <summary>
        /// Initializes a new instance of the DisposableContainer class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DisposableContainer() { }

        /* ----------------------------------------------------------------- */
        ///
        /// DisposableContainer
        ///
        /// <summary>
        /// Initializes a new instance of the DisposableContainer class with
        /// the specified IDisposable objects.
        /// </summary>
        ///
        /// <param name="src">IDisposable object.</param>
        /// <param name="latter">IDisposable objects.</param>
        ///
        /* ----------------------------------------------------------------- */
        public DisposableContainer(IDisposable src, params IDisposable[] latter)
        {
            Add(src);
            Add(latter);
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Add
        ///
        /// <summary>
        /// Adds the specified disposable objects.
        /// </summary>
        ///
        /// <param name="src">IDisposable objects.</param>
        ///
        /// <remarks>
        /// If the object has already been disposed when called, the Dispose
        /// method of the specified object will be invoked immediately.
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public void Add(params IDisposable[] src)
        {
            foreach (var e in src.Compact())
            {
                if (Disposed) e.Dispose();
                else _core.Enqueue(e);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Add
        ///
        /// <summary>
        /// Converts the specified action to an IDisposable object and adds it.
        /// </summary>
        ///
        /// <param name="action">
        /// Action to be invoked when disposing.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public void Add(Action action) => Add(Disposable.Create(action));

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases the all IDisposable objects. The class will always
        /// invoke the dispose operation, regardless of the disposing
        /// parameter.
        /// </summary>
        ///
        /// <param name="disposing">
        /// Note that the class ignores the parameter.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Dispose(bool disposing)
        {
            while (_core.TryDequeue(out var e)) e.Dispose();
        }

        #endregion

        #region Fields
        private readonly ConcurrentQueue<IDisposable> _core = new();
        #endregion
    }
}
