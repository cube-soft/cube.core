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
using System;
using System.Windows;
using System.Windows.Interactivity;

namespace Cube.Xui.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// MessengerBehavior(T)
    ///
    /// <summary>
    /// Represents the behavior that communicates with a ViewModel object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class MessengerBehavior<T> : Behavior<FrameworkElement>
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
        /// <param name="e">Parameter object.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected abstract void Invoke(T e);

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
            Register();
            AssociatedObject.DataContextChanged -= WhenDataContextChanged;
            AssociatedObject.DataContextChanged += WhenDataContextChanged;
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
            AssociatedObject.DataContextChanged -= WhenDataContextChanged;
            _registry?.Dispose();
            base.OnDetaching();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Register
        ///
        /// <summary>
        /// Registers the action that is defined by inherited classes.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Register()
        {
            _registry?.Dispose();
            _registry = AssociatedObject
                        .DataContext
                        .TryCast<IMessengerRegistrar>()?
                        .Register<T>(AssociatedObject, e => Invoke(e));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenDataContextChanged
        ///
        /// <summary>
        /// Occurs when the DataContext property of the AssociatedObject
        /// is changed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenDataContextChanged(object s, DependencyPropertyChangedEventArgs e) => Register();

        #endregion

        #region Fields
        private IDisposable _registry;
        #endregion
    }
}
