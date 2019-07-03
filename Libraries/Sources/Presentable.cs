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
    public abstract class Presentable<TModel> : PresentableBase where TModel : IDisposable
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
            if (disposing) Facade?.Dispose();
        }

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
        protected async Task Send<T>(Message<T> message, Action<T> next, Func<T, bool> predicate)
        {
            Send(message);
            if (!predicate(message.Value)) return;
            await Track(() => next(message.Value));
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
        protected async Task Send<T>(CancelMessage<T> message, Action<T> next)
        {
            Send(message);
            if (message.Cancel) return;
            await Track(() => next(message.Value));
        }

        #endregion

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
        protected Task Track(Action action) => Track(action, DialogMessage.Create);

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
        /// Action to be invoked as asynchronous.
        /// </param>
        ///
        /// <param name="converter">
        /// Function to convert from Exception to DialogMessage.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected Task Track(Action action, Converter converter) =>
            Track(action, converter, false);

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
        /// <param name="converter">
        /// Function to convert from Exception to DialogMessage.
        /// </param>
        ///
        /// <param name="synchronous">
        /// Value indicating whether to invoke the specified action as a
        /// synchronous method.
        /// </param>
        ///
        /// <remarks>
        /// Presenter や ViewModel において、直接的に View と関係のない何らかの
        /// 処理を実行する時には原則として 非 UI スレッド上で実行する事が推奨され
        /// ますが、同期問題などの理由でやむを得ず UI スレッド上で実行したい場合、
        /// 第 3 引数を true に設定して下さい。また、第 2 引数には既定の変換規則
        /// として DialogMessage.Create が利用できます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected Task Track(Action action, Converter converter, bool synchronous) =>
            synchronous ? TrackSync(action, converter) : TrackAsync(action, converter);

        #endregion

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// TrackAsync
        ///
        /// <summary>
        /// Invokes the specified action as an asynchronous manner, and
        /// will send the error message if any exceptions are thrown.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Task TrackAsync(Action action, Converter converter) =>
            Task.Run(() => TrackCore(action, converter));

        /* ----------------------------------------------------------------- */
        ///
        /// TrackSync
        ///
        /// <summary>
        /// Invokes the specified action as a synchronous manner, and
        /// will send the error message if any exceptions are thrown.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Task TrackSync(Action action, Converter converter)
        {
            TrackCore(action, converter);
            return Task.FromResult(0);
        }

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
        private void TrackCore(Action action, Converter converter)
        {
            try { action(); }
            catch (Exception err) { Send(converter(err)); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Converter
        ///
        /// <summary>
        /// Represents the delegate to convert from Exception to
        /// DialogMessage.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public delegate DialogMessage Converter(Exception e);

        #endregion
    }
}
