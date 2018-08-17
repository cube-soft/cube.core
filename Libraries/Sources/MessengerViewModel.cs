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
using Cube.Generics;
using Cube.Log;
using Cube.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Cube.Xui
{
    /* --------------------------------------------------------------------- */
    ///
    /// IMessengerViewModel
    ///
    /// <summary>
    /// Represents interface for communicating with a view component
    /// through a messenger object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public interface IMessengerViewModel
    {
        /* --------------------------------------------------------------------- */
        ///
        /// Register(T)
        ///
        /// <summary>
        /// Registers a receiver for a type of message T.
        /// </summary>
        ///
        /// <param name="receiver">Receiver for a message.</param>
        /// <param name="action">
        /// Action that will be executed when a message of type T is sent.
        /// </param>
        ///
        /// <returns>Object to unregister.</returns>
        ///
        /* --------------------------------------------------------------------- */
        IDisposable Register<T>(object receiver, Action<T> action);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// MessengerViewModel
    ///
    /// <summary>
    /// Provides functionality to communicate with the related view
    /// component through the messenger object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class MessengerViewModel : ViewModelBase, IMessengerViewModel, IDisposable
    {
        #region Constructors

        /* --------------------------------------------------------------------- */
        ///
        /// MessengerViewModel
        ///
        /// <summary>
        /// Initializes a new instance of the MessengerViewModel class.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected MessengerViewModel() : this(Messenger.Default) { }

        /* --------------------------------------------------------------------- */
        ///
        /// MessengerViewModel
        ///
        /// <summary>
        /// Initializes a new instance of the MessengerViewModel class
        /// with the specified Messenger.
        /// </summary>
        ///
        /// <param name="messenger">Messenger.</param>
        ///
        /* --------------------------------------------------------------------- */
        protected MessengerViewModel(IMessenger messenger) : this(
            messenger,
            SynchronizationContext.Current ?? new SynchronizationContext()
        ) { }

        /* --------------------------------------------------------------------- */
        ///
        /// MessengerViewModel
        ///
        /// <summary>
        /// Initializes a new instance of the MessengerViewModel class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="messenger">Messenger.</param>
        /// <param name="context">Synchronization context.</param>
        ///
        /* --------------------------------------------------------------------- */
        protected MessengerViewModel(IMessenger messenger, SynchronizationContext context) : base(messenger)
        {
            _dispose = new OnceAction<bool>(Dispose);
            Context  = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region Properties

        /* --------------------------------------------------------------------- */
        ///
        /// Context
        ///
        /// <summary>
        /// Gets the synchronization context.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected SynchronizationContext Context { get; }

        #endregion

        #region Methods

        #region IMessengerViewModel

        /* --------------------------------------------------------------------- */
        ///
        /// Register(T)
        ///
        /// <summary>
        /// Registers a receiver for a type of message T.
        /// </summary>
        ///
        /// <param name="receiver">Receiver for a message.</param>
        /// <param name="action">
        /// Action that will be executed when a message of type T is sent.
        /// </param>
        ///
        /// <returns>Object to unregister.</returns>
        ///
        /* --------------------------------------------------------------------- */
        public IDisposable Register<T>(object receiver, Action<T> action)
        {
            MessengerInstance.Register(receiver, action);
            return Disposable.Create(() => MessengerInstance.Unregister<T>(receiver));
        }

        #endregion

        #region Send

        /* --------------------------------------------------------------------- */
        ///
        /// Send(T)
        ///
        /// <summary>
        /// Sends an empty message of type T through the messenger.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected void Send<T>() where T : new() => Send(new T());

        /* --------------------------------------------------------------------- */
        ///
        /// Send(T)
        ///
        /// <summary>
        /// Sends a message of type T through the messenger.
        /// </summary>
        ///
        /// <param name="message">Message object.</param>
        ///
        /* --------------------------------------------------------------------- */
        protected void Send<T>(T message) => Context.Send(_ => MessengerInstance.Send(message), null);

        /* --------------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// Sends an error message.
        /// </summary>
        ///
        /// <param name="err">Exception object.</param>
        ///
        /* --------------------------------------------------------------------- */
        protected void Send(Exception err) => Send(err, string.Empty);

        /* --------------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// Sends an error message.
        /// </summary>
        ///
        /// <param name="err">Exception object.</param>
        /// <param name="message">Error message.</param>
        ///
        /* --------------------------------------------------------------------- */
        protected void Send(Exception err, string message) => Send(err, message, GetTitle());

        /* --------------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// Sends an error message.
        /// </summary>
        ///
        /// <param name="err">Exception object.</param>
        /// <param name="message">Error message.</param>
        /// <param name="title">Title for the error dialog.</param>
        ///
        /* --------------------------------------------------------------------- */
        protected void Send(Exception err, string message, string title) => Send(Create(err, message, title));

        #endregion

        #region Post

        /* --------------------------------------------------------------------- */
        ///
        /// Post(T)
        ///
        /// <summary>
        /// Posts an empty message of type T through the messenger.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected void Post<T>() where T : new() => Post(new T());

        /* --------------------------------------------------------------------- */
        ///
        /// Post(T)
        ///
        /// <summary>
        /// Posts a message of type T through the messenger.
        /// </summary>
        ///
        /// <param name="message">Message.</param>
        ///
        /* --------------------------------------------------------------------- */
        protected void Post<T>(T message) => Context.Post(_ => MessengerInstance.Send(message), null);

        /* --------------------------------------------------------------------- */
        ///
        /// Post
        ///
        /// <summary>
        /// Posts an error message.
        /// </summary>
        ///
        /// <param name="error">Exception object.</param>
        ///
        /* --------------------------------------------------------------------- */
        protected void Post(Exception error) => Post(error, string.Empty);

        /* --------------------------------------------------------------------- */
        ///
        /// Post
        ///
        /// <summary>
        /// Posts an error message.
        /// </summary>
        ///
        /// <param name="error">Exception object.</param>
        /// <param name="message">Error message.</param>
        ///
        /* --------------------------------------------------------------------- */
        protected void Post(Exception error, string message) => Post(error, message, GetTitle());

        /* --------------------------------------------------------------------- */
        ///
        /// Post
        ///
        /// <summary>
        /// Posts an error message.
        /// </summary>
        ///
        /// <param name="error">Exception object.</param>
        /// <param name="message">Error message.</param>
        /// <param name="title">Title for the error dialog.</param>
        ///
        /* --------------------------------------------------------------------- */
        protected void Post(Exception error, string message, string title) => Post(Create(error, message, title));

        /* ----------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// Executes the specified action as an asynchronous operation and
        /// returns immeidately. When an error occurs, the message of
        /// type DialogMessage is sent.
        /// </summary>
        ///
        /// <param name="action">Action as asynchronously.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected void Post(Action action) => Post(action, string.Empty);

        /* ----------------------------------------------------------------- */
        ///
        /// Post
        ///
        /// <summary>
        /// Executes the specified action as an asynchronous operation and
        /// returns immeidately. When an error occurs, the message of
        /// type DialogMessage is sent.
        /// </summary>
        ///
        /// <param name="action">Action as asynchronously.</param>
        /// <param name="message">Error message.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected void Post(Action action, string message) => Post(action, message, GetTitle());

        /* ----------------------------------------------------------------- */
        ///
        /// Post
        ///
        /// <summary>
        /// Executes the specified action as an asynchronous operation and
        /// returns immeidately. When an error occurs, the message of
        /// type DialogMessage is sent.
        /// </summary>
        ///
        /// <param name="action">Action as asynchronously.</param>
        /// <param name="message">Error message.</param>
        /// <param name="title">Title for the error dialog.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected void Post(Action action, string message, string title) => Task.Run(() =>
        {
            try { action(); }
            catch (Exception err) { Post(err, message, title); }
        }).Forget();

        #endregion

        #region IDisposable

        /* ----------------------------------------------------------------- */
        ///
        /// ~MessengerViewModel
        ///
        /// <summary>
        /// Finalizes the MessengerViewModel.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        ~MessengerViewModel() { _dispose.Invoke(false); }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases all resources used by the MessengerViewModel.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Dispose()
        {
            _dispose.Invoke(true);
            GC.SuppressFinalize(this);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases the unmanaged resources used by the MessengerViewModel
        /// and optionally releases the managed resources.
        /// </summary>
        ///
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources;
        /// false to release only unmanaged resources.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void Dispose(bool disposing) { }

        #endregion

        #endregion

        #region Implementations

        /* --------------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// Creates a new instance of the DialogMessage class with the
        /// specified error information.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private DialogMessage Create(Exception error, string message, string title)
        {
            var ss = new System.Text.StringBuilder();
            if (message.HasValue())
            {
                ss.AppendLine(message);
                this.LogError(message);
            }
            this.LogError(error.ToString(), error);
            ss.Append($"{error.Message} ({error.GetType().Name})");

            return new DialogMessage(ss.ToString(), title)
            {
                Button = System.Windows.MessageBoxButton.OK,
                Image = System.Windows.MessageBoxImage.Error,
                Result = true,
            };
        }

        /* --------------------------------------------------------------------- */
        ///
        /// GetTitle
        ///
        /// <summary>
        /// Gets the assembly title.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private string GetTitle()
        {
            var asm = Assembly.GetEntryAssembly() ??
                      Assembly.GetExecutingAssembly();
            Debug.Assert(asm != null);
            return asm.GetReader().Title;
        }

        #endregion

        #region Fields
        private readonly OnceAction<bool> _dispose;
        #endregion
    }
}
