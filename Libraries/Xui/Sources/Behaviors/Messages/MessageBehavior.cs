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
namespace Cube.Xui.Behaviors;

using System;
using System.Windows;
using Cube.Generics.Extensions;
using Microsoft.Xaml.Behaviors;

/* ------------------------------------------------------------------------- */
///
/// MessageBehavior(TMessage)
///
/// <summary>
/// Represents the behavior of communicating with a presentable object
/// via a message.
/// </summary>
///
/// <typeparam name="TMessage">Message type.</typeparam>
///
/* ------------------------------------------------------------------------- */
public abstract class MessageBehavior<TMessage> : Behavior<FrameworkElement>
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Invoke
    ///
    /// <summary>
    /// Invokes the user action.
    /// </summary>
    ///
    /// <param name="e">Message object.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected abstract void Invoke(TMessage e);

    /* --------------------------------------------------------------------- */
    ///
    /// OnSubscribe
    ///
    /// <summary>
    /// Subscribes the action defined by inherited classes.
    /// </summary>
    ///
    /// <param name="src">Source aggregator.</param>
    ///
    /// <returns>Object to dispose.</returns>
    ///
    /* --------------------------------------------------------------------- */
    protected virtual IDisposable OnSubscribe(IAggregator src) =>
        src?.Subscribe<TMessage>(Invoke);

    /* --------------------------------------------------------------------- */
    ///
    /// OnAttached
    ///
    /// <summary>
    /// Called after the action is attached to an AssociatedObject.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected override void OnAttached()
    {
        base.OnAttached();
        Reset();
        AssociatedObject.DataContextChanged -= WhenContextChanged;
        AssociatedObject.DataContextChanged += WhenContextChanged;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// OnDetaching
    ///
    /// <summary>
    /// Called when the action is being detached from its
    /// AssociatedObject, but before it has actually occurred.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected override void OnDetaching()
    {
        AssociatedObject.DataContextChanged -= WhenContextChanged;
        _subscriber?.Dispose();
        base.OnDetaching();
    }

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// Reset
    ///
    /// <summary>
    /// Resets the subscriptions.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private void Reset()
    {
        _subscriber?.Dispose();
        _subscriber = OnSubscribe(AssociatedObject?.DataContext?.TryCast<IAggregator>());
    }

    /* --------------------------------------------------------------------- */
    ///
    /// WhenContextChanged
    ///
    /// <summary>
    /// Occurs when the DataContext property of the AssociatedObject
    /// is changed.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private void WhenContextChanged(object s, DependencyPropertyChangedEventArgs e) => Reset();

    #endregion

    #region Fields
    private IDisposable _subscriber;
    #endregion
}
