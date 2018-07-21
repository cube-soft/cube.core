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
using System.Threading.Tasks;

namespace Cube.Xui
{
    /* --------------------------------------------------------------------- */
    ///
    /// MessengerViewModel
    ///
    /// <summary>
    /// Provides a Messenger that can be accessible from the other classes.
    /// </summary>
    ///
    /// <remarks>
    /// ViewModel の Messenger を Binding する事を前提とした Behavior を
    /// 数多く定義しているため、Messenger オブジェクトを外部からアクセス
    /// 可能にしています。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class MessengerViewModel : ViewModelBase, IDisposable
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
        protected MessengerViewModel() : this(GalaSoft.MvvmLight.Messaging.Messenger.Default) { }

        /* --------------------------------------------------------------------- */
        ///
        /// MessengerViewModel
        ///
        /// <summary>
        /// Initializes a new instance of the MessengerViewModel class
        /// with the specified Messenger.
        /// </summary>
        ///
        /// <param name="messenger">メッセージ伝搬用オブジェクト</param>
        ///
        /* --------------------------------------------------------------------- */
        protected MessengerViewModel(IMessenger messenger) : base(messenger)
        {
            _dispose = new OnceAction<bool>(Dispose);
        }

        #endregion

        #region Properties

        /* --------------------------------------------------------------------- */
        ///
        /// Messenger
        ///
        /// <summary>
        /// Gets the Messenger instance.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public IMessenger Messenger => MessengerInstance;

        #endregion

        #region Methods

        #region Send

        /* --------------------------------------------------------------------- */
        ///
        /// Send(T)
        ///
        /// <summary>
        /// Sends a message through the Messenger.
        /// </summary>
        ///
        /// <param name="message">Message.</param>
        ///
        /* --------------------------------------------------------------------- */
        protected void Send<T>(T message) => Messenger.Send(message);

        /* --------------------------------------------------------------------- */
        ///
        /// Send(T)
        ///
        /// <summary>
        /// Sends an empty message of the specified type through the Messenger.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected void Send<T>() where T : new() => Send(new T());

        /* ----------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// Sends a message if an exception occurs.
        /// </summary>
        ///
        /// <param name="action">Action.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected void Send(Action action) => Send(action, string.Empty);

        /* ----------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// Sends a message if an exception occurs.
        /// </summary>
        ///
        /// <param name="action">Action.</param>
        /// <param name="message">Error message.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected void Send(Action action, string message) => Send(action, message, GetTitle());

        /* ----------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// Sends a message if an exception occurs.
        /// </summary>
        ///
        /// <param name="action">Action.</param>
        /// <param name="message">Error message.</param>
        /// <param name="title">Title for the error dialog.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected void Send(Action action, string message, string title)
        {
            try { action(); }
            catch (Exception err) { Send(err, message, title); }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// Sends an error message.
        /// </summary>
        ///
        /// <param name="err">Exception.</param>
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
        /// <param name="err">Exception.</param>
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
        /// <param name="err">Exception.</param>
        /// <param name="message">Error message.</param>
        /// <param name="title">Title for the error dialog.</param>
        ///
        /* --------------------------------------------------------------------- */
        protected void Send(Exception err, string message, string title)
        {
            var ss = new System.Text.StringBuilder();
            if (message.HasValue())
            {
                ss.AppendLine(message);
                this.LogError(message);
            }
            this.LogError(err.ToString(), err);

            ss.Append($"{err.Message} ({err.GetType().Name})");
            Send(new DialogMessage(ss.ToString(), title)
            {
                Button = System.Windows.MessageBoxButton.OK,
                Image  = System.Windows.MessageBoxImage.Error,
                Result = true,
            });
        }

        /* --------------------------------------------------------------------- */
        ///
        /// Async
        ///
        /// <summary>
        /// Executes the specified action as an asynchronous operation and
        /// returns immeidately.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected void Async(Action action) => Task.Run(action).Forget();

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
        protected abstract void Dispose(bool disposing);

        #endregion

        #endregion

        #region Implementations

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
