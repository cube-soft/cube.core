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
using System.Threading;

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
            Aggregator = aggregator;
            Context    = context ?? throw new ArgumentNullException(nameof(context));
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

        #endregion

        #region Events

        #region PropertyChanged

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
        /// Raises the PropertyChanged event with the specified arguments.
        /// </summary>
        ///
        /// <param name="e">Arguments of the event being raised.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null) Context.Post(z => PropertyChanged(this, e), null);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RaisePropertyChanged
        ///
        /// <summary>
        /// Raises the PropertyChanged event with the specified name.
        /// </summary>
        ///
        /// <param name="name">Property name.</param>
        /// <param name="more">More property names.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected void RaisePropertyChanged(string name, params string[] more)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(name));
            foreach (var s in more) OnPropertyChanged(new PropertyChangedEventArgs(name));
        }

        #endregion

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
        protected IDispatcher GetDispatcher(bool synchronous) => new Dispatcher(Context, synchronous);

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
        protected void Send<T>(T message) => Context.Send(e => Aggregator.Publish(message), null);

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
        protected void Post<T>(T message) => Context.Post(e => Aggregator.Publish(message), null);

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

        #region SetProperty

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

        #endregion
    }

    #endregion
}
