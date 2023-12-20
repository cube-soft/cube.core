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
namespace Cube;

using System;
using System.Threading;
using System.Threading.Tasks;
using Cube.Tasks.Extensions;

/* ------------------------------------------------------------------------- */
///
/// PresentableBase
///
/// <summary>
/// Represents the base behavior of presentable components. (Controller,
/// Presenter, ViewModel, and so on)
/// </summary>
///
/* ------------------------------------------------------------------------- */
public abstract class PresentableBase : ObservableBase, IBindable
{
    #region Constructors

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
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

    /* --------------------------------------------------------------------- */
    ///
    /// Aggregator
    ///
    /// <summary>
    /// Gets the message aggregator.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected Aggregator Aggregator { get; }

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

    /* --------------------------------------------------------------------- */
    ///
    /// Assets
    ///
    /// <summary>
    /// Gets the collection of disposable resources that will be
    /// released automatically when disposing.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected DisposableContainer Assets { get; } = new();

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public IDisposable Subscribe<T>(Action<T> callback) => Aggregator.Subscribe(callback);

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    protected Dispatcher GetDispatcher(bool synchronous) => synchronous ? _send : _post;

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    protected override void Dispose(bool disposing)
    {
        if (disposing) Assets.Dispose();
    }

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    protected virtual DialogMessage OnMessage(Exception src) =>
        src is OperationCanceledException ? null : DialogMessage.From(src);

    /* --------------------------------------------------------------------- */
    ///
    /// Post
    ///
    /// <summary>
    /// Posts the specified message.
    /// </summary>
    ///
    /// <param name="message">Message to be posted.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected void Post<T>(T message) => _post.Invoke(() => Aggregator.Publish(message));

    #region Send

    /* --------------------------------------------------------------------- */
    ///
    /// Send
    ///
    /// <summary>
    /// Sends the specified message.
    /// </summary>
    ///
    /// <param name="message">Message to be sent.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected void Send<T>(T message) => _send.Invoke(() => Aggregator.Publish(message));

    /* --------------------------------------------------------------------- */
    ///
    /// Send
    ///
    /// <summary>
    /// Sends the specified message, and then invokes the specified
    /// action if the Cancel property is set to false.
    /// </summary>
    ///
    /// <param name="message">Message to be sent.</param>
    ///
    /// <param name="next">
    /// Action to be invoked if the Cancel property of the message is
    /// set to false.
    /// </param>
    ///
    /// <param name="synchronous">
    /// Value indicating whether to invoke the specified action as a
    /// synchronous method.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    protected void Send<T>(CancelMessage<T> message, Action<T> next, bool synchronous)
    {
        RunCore(true, () => Send(message));
        if (!message?.Cancel ?? false) Run(() => next(message.Value), synchronous);
    }

    /* --------------------------------------------------------------------- */
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
    ///
    /// <param name="next">
    /// Action to be invoked if the Cancel property is set to false.
    /// </param>
    ///
    /// <param name="synchronous">
    /// Value indicating whether to invoke the specified action as a
    /// synchronous method.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    protected void Send<T>(T src, Action<T> next, bool synchronous) where T : IBindable =>
        Send(new CancelMessage<T> { Value = src }, next, synchronous);

    #endregion

    #region Run

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    protected void Run(Action action, bool synchronous) => RunCore(synchronous, action);

    /* --------------------------------------------------------------------- */
    ///
    /// Run
    ///
    /// <summary>
    /// Invokes the specified actions, and will send the error message
    /// if any exceptions are thrown. The next action will always be
    /// invoked even if the first specified action throws an exception.
    /// </summary>
    ///
    /// <param name="first">Action to be invoked.</param>
    ///
    /// <param name="next">
    /// Action to be invoked after the first specified action has finished.
    /// The action will always be invoked even if the first specified
    /// action throws an exception.
    /// </param>
    ///
    /// <param name="synchronous">
    /// Value indicating whether to invoke both of the specified actions
    /// as synchronous methods.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    protected void Run(Action first, Action next, bool synchronous) =>
        RunCore(synchronous, first, next);

    #endregion

    #region Quit

    /* --------------------------------------------------------------------- */
    ///
    /// Quit
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
    /* --------------------------------------------------------------------- */
    protected void Quit(Action action, bool synchronous) =>
        RunCore(synchronous, action, () => Send(new CloseMessage()));

    #endregion

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    private void RunCore(bool synchronous, params Action[] actions)
    {
        void invoke()
        {
            foreach (var action in actions)
            {
                try { action(); }
                catch (Exception e)
                {
                    Logger.Warn(e);
                    var m = OnMessage(e);
                    if (m is not null) Send(m);
                }
            }
        }

        if (synchronous) invoke();
        else Task.Run(invoke).Forget();
    }

    #endregion

    #region Fields
    private readonly Dispatcher _send;
    private readonly Dispatcher _post;
    #endregion
}
