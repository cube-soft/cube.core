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

using System;

/* ------------------------------------------------------------------------- */
///
/// MessageBehavior(TMessage)
///
/// <summary>
/// Provides functionality to invoke the provided action when a message
/// is received.
/// </summary>
///
/// <typeparam name="TMessage">Message type.</typeparam>
///
/* ------------------------------------------------------------------------- */
public class MessageBehavior<TMessage> : MessageBehaviorBase<TMessage>
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// MessageBehavior
    ///
    /// <summary>
    /// Initializes a new instance of the MessageBehavior class
    /// with the specified arguments.
    /// </summary>
    ///
    /// <param name="aggregator">Message aggregator.</param>
    /// <param name="action">
    /// Action to be invoked when a message is received.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    public MessageBehavior(IAggregator aggregator, Action<TMessage> action) :
        base(aggregator) => _action = action;

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
    protected override void Invoke(TMessage message) => _action(message);

    #endregion

    #region Fields
    private readonly Action<TMessage> _action;
    #endregion
}
