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
using System.Windows;
using Cube.Mixin.Generics;
using Microsoft.Xaml.Behaviors;

namespace Cube.Xui.Behaviors
{
    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public abstract class MessageBehavior<TMessage> : Behavior<FrameworkElement>
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the user action.
        /// </summary>
        ///
        /// <param name="e">Message object.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected abstract void Invoke(TMessage e);

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// OnAttached
        ///
        /// <summary>
        /// Called after the action is attached to an AssociatedObject.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnAttached()
        {
            base.OnAttached();
            Subscribe();
            AssociatedObject.DataContextChanged -= OnDataContextChanged;
            AssociatedObject.DataContextChanged += OnDataContextChanged;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnDetaching
        ///
        /// <summary>
        /// Called when the action is being detached from its
        /// AssociatedObject, but before it has actually occurred.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnDetaching()
        {
            AssociatedObject.DataContextChanged -= OnDataContextChanged;
            _subscriber?.Dispose();
            base.OnDetaching();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        ///
        /// <summary>
        /// Subscribes the action that is defined by inherited classes.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Subscribe()
        {
            _subscriber?.Dispose();
            _subscriber = AssociatedObject
                ?.DataContext
                ?.TryCast<IAggregator>()
                ?.Subscribe<TMessage>(Invoke);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnDataContextChanged
        ///
        /// <summary>
        /// Occurs when the DataContext property of the AssociatedObject
        /// is changed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void OnDataContextChanged(object s, DependencyPropertyChangedEventArgs e) => Subscribe();

        #endregion

        #region Fields
        private IDisposable _subscriber;
        #endregion
    }
}
