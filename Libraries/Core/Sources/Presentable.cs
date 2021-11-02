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
using System.Threading;
using System.Threading.Tasks;
using Cube.Mixin.Collections;
using Cube.Mixin.Tasks;

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
        protected Presentable(TModel facade) : this(facade, new()) { }

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
            try { if (disposing && Facade is IDisposable obj) obj.Dispose(); }
            finally { base.Dispose(disposing); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnMessage
        ///
        /// <summary>
        /// Converts the specified exception to a new instance of the
        /// DialogMessage class.
        /// </summary>
        ///
        /// <param name="src">Source exception.</param>
        ///
        /// <returns>DialogMessage object.</returns>
        ///
        /// <remarks>
        /// The Method is called from the Track methods.
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual DialogMessage OnMessage(Exception src) =>
            src is OperationCanceledException ? null : DialogMessage.From(src);

        #region Send

        /* ----------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// Sends the specified message, and then invokes the specified
        /// action if the Cancel property is set to false.
        /// </summary>
        ///
        /// <param name="message">Message to be sent.</param>
        /// <param name="next">
        /// Action to be invoked if the Cancel property of the message is
        /// set to false.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected void Send<T>(CancelMessage<T> message, Action<T> next)
        {
            Send(message);
            if (!message.Cancel) next(message.Value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// Sends a CancelMessage(T) object with the specified source wrapped,
        /// and then invokes the specified action if the Cancel property is
        /// set to false.
        /// </summary>
        ///
        /// <param name="src">Source bindable object.</param>
        /// <param name="next">
        /// Action to be invoked if the Cancel property is set to false.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected void Send<T>(T src, Action<T> next) where T : IBindable =>
            Send(new CancelMessage<T>() { Value = src }, next);

        #endregion

        #region Run

        /* ----------------------------------------------------------------- */
        ///
        /// Run
        ///
        /// <summary>
        /// Invokes the specified actions as an asynchronous method, and
        /// will send the error message if any exceptions are thrown.
        /// All the specified actions will always be invoked.
        /// If an action throws an exception, the method will send a
        /// DialogMessage object corresponding to the exception, and then
        /// invoke the next action.
        /// </summary>
        ///
        /// <param name="actions">Sequence of actions to be invoked.</param>
        ///
        /// <returns>Task object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected void Run(params Action[] actions) => RunCore(actions, false);

        /* ----------------------------------------------------------------- */
        ///
        /// Run
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
        protected void Run(Action action, bool synchronous) =>
            RunCore(action.ToEnumerable(), synchronous);

        #endregion

        #region Close

        /* ----------------------------------------------------------------- */
        ///
        /// Close
        ///
        /// <summary>
        /// Invokes the specified action, and finally sends the close message.
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
        protected void Close(Action action, bool synchronous) =>
            RunCore(new[] { action, () => Send(new CloseMessage()) }, synchronous);

        #endregion

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// RunCore
        ///
        /// <summary>
        /// Invokes the specified actions and will send the error message
        /// if any exceptions are thrown. All the specified actions will
        /// always be invoked. If an action throws an exception, the method
        /// will send a DialogMessage object corresponding to the exception,
        /// and then invoke the next action.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void RunCore(IEnumerable<Action> actions, bool synchronous)
        {
            void invoke() { foreach (var e in actions) RunCore(e); }
            if (synchronous) invoke();
            else Task.Run(invoke).Forget();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RunCore
        ///
        /// <summary>
        /// Invokes the specified action, and will send the error message
        /// if any exceptions are thrown.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void RunCore(Action action)
        {
            try { action(); }
            catch (Exception e)
            {
                GetType().LogWarn(e);
                if (OnMessage(e) is DialogMessage msg) Send(msg);
            }
        }

        #endregion
    }
}
