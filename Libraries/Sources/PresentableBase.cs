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
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Cube
{
    #region IPresentable

    /* --------------------------------------------------------------------- */
    ///
    /// IPresentable
    ///
    /// <summary>
    /// Represents the interface of presentable (Controller, Presenter,
    /// ViewModel, and so on) components.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public interface IPresentable : INotifyPropertyChanged, IDisposable
    {
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
        IDisposable Subscribe<T>(Action<T> callback);
    }

    #endregion

    #region PresentableBase

    /* --------------------------------------------------------------------- */
    ///
    /// PresentableBase
    ///
    /// <summary>
    /// Represents the base behavior of presentable (Controller, Presenter,
    /// ViewModel, and so on) components.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class PresentableBase : DisposableBase, IPresentable
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// PresentableBase
        ///
        /// <summary>
        /// Initializes a new instance of the PresentableBase class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected PresentableBase() : this(new Aggregator()) { }

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
        ///
        /* ----------------------------------------------------------------- */
        protected PresentableBase(Aggregator aggregator) :
            this(aggregator, SynchronizationContext.Current) { }

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
            Aggregator  = aggregator;
            _dispatcher = new DispatcherCore(context);
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
        /// Dispatcher
        ///
        /// <summary>
        /// Gets the dispatcher object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected IDispatcher Dispatcher => _dispatcher;

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
            if (PropertyChanged != null) Dispatcher.Invoke(() => PropertyChanged(this, e));
        }

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

        #region Send

        /* ----------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// Invokes the specified action with the provided dispatcher.
        /// </summary>
        ///
        /// <param name="action">Action to be invoked.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected void Send(Action action) => _dispatcher.Send(action);

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
        protected void Send<T>(T message) => Send(() => Aggregator.Publish(message));

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
        /// Invokes the specified action with the provided dispatcher.
        /// </summary>
        ///
        /// <param name="action">Action to be invoked.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected void Post(Action action) => _dispatcher.Post(action);

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
        protected void Post<T>(T message) => Post(() => Aggregator.Publish(message));

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

        #region Track

        /* ----------------------------------------------------------------- */
        ///
        /// Track
        ///
        /// <summary>
        /// Invokes the specified action as an asynchronous manner, and
        /// will send the error message if any exceptions are thrown.
        /// </summary>
        ///
        /// <param name="action">
        /// Action to be invoked as asynchronous.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected Task Track(Action action) => Track(action, Convert);

        /* ----------------------------------------------------------------- */
        ///
        /// Track
        ///
        /// <summary>
        /// Invokes the specified action as an asynchronous manner, and
        /// will send the error message if any exceptions are thrown.
        /// </summary>
        ///
        /// <param name="action">
        /// Action to be invoked as asynchronous.
        /// </param>
        ///
        /// <param name="converter">
        /// Function to convert to the error message.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected Task Track(Action action, Func<Exception, ExceptionMessage> converter) => Task.Run(() =>
        {
            try { action(); }
            catch (Exception err) { Send(converter(err)); }
        });

        #endregion

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Convert
        ///
        /// <summary>
        /// Creates a new instance of the Exception class with the specified
        /// exception.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private ExceptionMessage Convert(Exception src) => new ExceptionMessage
        {
            Title = "Error",
            Text  = $"{src.Message} ({src.GetType().Name})",
            Value = src,
        };

        /* ----------------------------------------------------------------- */
        ///
        /// DispatcherCore
        ///
        /// <summary>
        /// Represents the dispatcher that is used in the class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private class DispatcherCore : Dispatcher
        {
            public DispatcherCore(SynchronizationContext ctx) : base(ctx, false) { }
            public void Send(Action action) => Context.Send(e => action(), null);
            public void Post(Action action) => Context.Post(e => action(), null);
        }

        #endregion

        #region Fields
        private readonly DispatcherCore _dispatcher;
        #endregion
    }

    #endregion
}
