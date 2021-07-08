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

namespace Cube
{
    #region PresentableBase

    /* --------------------------------------------------------------------- */
    ///
    /// PresentableBase
    ///
    /// <summary>
    /// Represents the base behavior of presentable components. (Controller,
    /// Presenter, ViewModel, and so on)
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class PresentableBase : ObservableBase, IBindable
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// PresentableBase
        ///
        /// <summary>
        /// Initializes a new instance of the PresentableBase class with
        /// the specified arguments.
        /// </summary>
        ///
        /// <param name="aggregator">Message aggregator.</param>
        /// <param name="context">Synchronization context.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected PresentableBase(Aggregator aggregator, SynchronizationContext context)
        {
            Aggregator = aggregator;
            Context    = context ?? throw new ArgumentNullException(nameof(context));
            _send      = new ContextDispatcher(context, true);
            _post      = new ContextDispatcher(context, false);
            Dispatcher = _post;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Aggregator
        ///
        /// <summary>
        /// Gets the message aggregator.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected Aggregator Aggregator { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Context
        ///
        /// <summary>
        /// Gets the synchronization context.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected SynchronizationContext Context { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Assets
        ///
        /// <summary>
        /// Gets the collection of disposable resources that will be
        /// released automatically when disposing.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected DisposableContainer Assets { get; } = new();

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        ///
        /// <summary>
        /// Subscribes the message of type T.
        /// </summary>
        ///
        /// <typeparam name="T">message type.</typeparam>
        ///
        /// <param name="callback">
        /// Action to be invoked when the message of type T is published.
        /// </param>
        ///
        /// <returns>Object to clear the subscription.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public IDisposable Subscribe<T>(Action<T> callback) => Aggregator.Subscribe(callback);

        /* ----------------------------------------------------------------- */
        ///
        /// GetDispatcher
        ///
        /// <summary>
        /// Gets a dispatcher object with the specified arguments.
        /// </summary>
        ///
        /// <param name="synchronous">
        /// Value indicating whether to invoke as synchronous.
        /// </param>
        ///
        /// <returns>Dispatcher object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected Dispatcher GetDispatcher(bool synchronous) => synchronous ? _send : _post;

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
            if (disposing) Assets.Dispose();
        }

        #region Send

        /* ----------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// Sends the specified message.
        /// </summary>
        ///
        /// <param name="message">Message to be sent.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected void Send<T>(T message) => _send.Invoke(() => Aggregator.Publish(message));

        /* ----------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// Sends a default message of type T.
        /// </summary>
        ///
        /// <typeparam name="T">Message type.</typeparam>
        ///
        /* ----------------------------------------------------------------- */
        protected void Send<T>() where T : new() => Send(new T());

        #endregion

        #region Post

        /* ----------------------------------------------------------------- */
        ///
        /// Post
        ///
        /// <summary>
        /// Posts the specified message.
        /// </summary>
        ///
        /// <param name="message">Message to be posted.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected void Post<T>(T message) => _post.Invoke(() => Aggregator.Publish(message));

        /* ----------------------------------------------------------------- */
        ///
        /// Post
        ///
        /// <summary>
        /// Posts a default message of type T.
        /// </summary>
        ///
        /// <typeparam name="T">Message type.</typeparam>
        ///
        /* ----------------------------------------------------------------- */
        protected void Post<T>() where T : new() => Post(new T());

        #endregion

        #endregion

        #region Fields
        private readonly Dispatcher _send;
        private readonly Dispatcher _post;
        #endregion
    }

    #endregion
}
