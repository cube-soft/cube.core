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
namespace Cube.Forms.Behaviors;

/* ------------------------------------------------------------------------- */
///
/// MessageBehaviorBase(TMessage)
///
/// <summary>
/// Represents the behavior that communicates with a presentable
/// object via a message.
/// </summary>
///
/// <typeparam name="TMessage">Message type.</typeparam>
///
/* ------------------------------------------------------------------------- */
public abstract class MessageBehaviorBase<TMessage> : DisposableBase
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// MessageBehavior
    ///
    /// <summary>
    /// Initializes a new instance of the MessageBehavior class
    /// with the specified presentable object.
    /// </summary>
    ///
    /// <param name="aggregator">Message aggregator.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected MessageBehaviorBase(IAggregator aggregator) =>
        Assets.Add(aggregator.Subscribe<TMessage>(Invoke));

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Assets
    ///
    /// <summary>
    /// Gets the collection to dispose.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected DisposableContainer Assets { get; } = new();

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Invoke
    ///
    /// <summary>
    /// Invokes the user action.
    /// </summary>
    ///
    /// <param name="message">Message object.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected abstract void Invoke(TMessage message);

    /* --------------------------------------------------------------------- */
    ///
    /// Dispose
    ///
    /// <summary>
    /// Releases the unmanaged resources used by the object and optionally
    /// releases the managed resources.
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

    #endregion
}
