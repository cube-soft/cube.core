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
namespace Cube.Tests.Mocks;

using System;

/* ------------------------------------------------------------------------- */
///
/// MockMessageBehavior(TMessage)
///
/// <summary>
/// Represents the behavior that communicates with a presentable
/// object via a message.
/// </summary>
///
/// <typeparam name="TMessage">Message type.</typeparam>
///
/* ------------------------------------------------------------------------- */
public class MockMessageBehavior<TMessage> : DisposableBase
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// MockMessageBehavior
    ///
    /// <summary>
    /// Initializes a new instance of the MockMessageBehavior class with the
    /// specified arguments.
    /// </summary>
    ///
    /// <param name="aggregator">Message aggregator.</param>
    /// <param name="action">
    /// Action to be invoked when a message is received.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    public MockMessageBehavior(IAggregator aggregator, Action<TMessage> action) =>
        _disposable = new(aggregator.Subscribe(action));

    #endregion

    #region Methods

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
        if (disposing) _disposable.Dispose();
    }

    #endregion

    #region Fields
    private readonly DisposableContainer _disposable;
    #endregion
}
