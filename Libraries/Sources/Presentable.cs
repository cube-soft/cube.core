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
using Cube.Mixin.Tasks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// Presentable
    ///
    /// <summary>
    /// Represents the base presentable class with a model object, which is
    /// the facade of other models.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class Presentable<TModel> : PresentableBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Presentable
        ///
        /// <summary>
        /// Initializes a new instance of the Presentable class with the
        /// specified model.
        /// </summary>
        ///
        /// <param name="facade">Model object.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected Presentable(TModel facade) : this(facade, new Aggregator()) { }

        /* ----------------------------------------------------------------- */
        ///
        /// Presentable
        ///
        /// <summary>
        /// Initializes a new instance of the Presentable class with the
        /// specified arguments.
        /// </summary>
        ///
        /// <param name="facade">Model object.</param>
        /// <param name="aggregator">Message aggregator.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected Presentable(TModel facade, Aggregator aggregator) :
            this(facade, aggregator, SynchronizationContext.Current) { }

        /* ----------------------------------------------------------------- */
        ///
        /// Presentable
        ///
        /// <summary>
        /// Initializes a new instance of the Presentable class with the
        /// specified arguments.
        /// </summary>
        ///
        /// <param name="facade">Model object.</param>
        /// <param name="aggregator">Message aggregator.</param>
        /// <param name="context">Synchronization context.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected Presentable(TModel facade, Aggregator aggregator, SynchronizationContext context) :
            base(aggregator, context) { Facade = facade; }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Facade
        ///
        /// <summary>
        /// Gets the facade of model objects.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected TModel Facade { get; }

        #endregion

        #region Methods

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
            if (disposing && Facade is IDisposable obj) obj.Dispose();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetMessage
        ///
        /// <summary>
        /// Creates a new instance of the DialogMessage class with the
        /// specified exception.
        /// </summary>
        ///
        /// <param name="src">Source exception.</param>
        ///
        /// <returns>DialogMessage object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual DialogMessage GetMessage(Exception src) => DialogMessage.Create(src);

        #region Track

        /* ----------------------------------------------------------------- */
        ///
        /// Track
        ///
        /// <summary>
        /// Invokes the specified action as an asynchronous method, and
        /// will send the error message if any exceptions are thrown.
        /// </summary>
        ///
        /// <param name="action">
        /// Action to be invoked as an asynchronous method.
        /// </param>
        ///
        /// <returns>Task object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected void Track(Action action) => Track(action, false);

        /* ----------------------------------------------------------------- */
        ///
        /// Track
        ///
        /// <summary>
        /// Invokes the specified action, and will send the error message
        /// if any exceptions are thrown.
        /// </summary>
        ///
        /// <param name="action">
        /// Action to be invoked.
        /// </param>
        ///
        /// <param name="synchronous">
        /// Value indicating whether to invoke the specified action as a
        /// synchronous method.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected void Track(Action action, bool synchronous)
        {
            if (synchronous) TrackCore(action);
            else Task.Run(() => TrackCore(action)).Forget();
        }

        #endregion

        #region Send

        /* ----------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// Sends the specified message, and then invokes the specified
        /// action as an asynchronous method if the specified function
        /// returns true.
        /// </summary>
        ///
        /// <param name="message">Message to be sent.</param>
        /// <param name="next">Action to be invoked.</param>
        /// <param name="predicate">
        /// Function to determine whether to invoke the specified action.
        /// </param>
        ///
        /// <returns>Task object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected void Send<T>(Message<T> message, Action<T> next, Func<T, bool> predicate)
        {
            Send(message);
            if (predicate(message.Value)) Track(() => next(message.Value));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// Sends the specified message, and then invokes the specified
        /// action as an asynchronous method if the Cancel property is
        /// set to false.
        /// </summary>
        ///
        /// <param name="message">Message to be sent.</param>
        /// <param name="next">Action to be invoked.</param>
        ///
        /// <returns>Task object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected void Send<T>(CancelMessage<T> message, Action<T> next)
        {
            Send(message);
            if (!message.Cancel) Track(() => next(message.Value));
        }

        #endregion

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// TrackCore
        ///
        /// <summary>
        /// Invokes the specified action, and will send the error message
        /// if any exceptions are thrown.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void TrackCore(Action action)
        {
            try { action(); }
            catch (Exception err) { Send(GetMessage(err)); }
        }

        #endregion
    }
}
