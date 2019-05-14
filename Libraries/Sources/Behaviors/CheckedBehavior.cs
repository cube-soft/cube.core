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
using Cube.Mixin.Commands;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Cube.Xui.Behaviors
{
    #region CheckedBehavior

    /* --------------------------------------------------------------------- */
    ///
    /// CheckedBehavior
    ///
    /// <summary>
    /// Represents the behavior when the item is checked.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class CheckedBehavior : CommandBehavior<ToggleButton>
    {
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
            AssociatedObject.Checked += WhenChecked;
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
            AssociatedObject.Checked -= WhenChecked;
            base.OnDetaching();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenChecked
        ///
        /// <summary>
        /// Occurs when the Checked event is fired.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenChecked(object s, RoutedEventArgs e)
        {
            if (Command.CanExecute()) Command.Execute();
        }

        #endregion
    }

    #endregion

    #region CheckedBehavior<T>

    /* --------------------------------------------------------------------- */
    ///
    /// CheckedBehavior
    ///
    /// <summary>
    /// Represents the behavior when the item is checked.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class CheckedBehavior<T> : CommandBehavior<ToggleButton, T>
    {
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
            AssociatedObject.Checked += WhenChecked;
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
            AssociatedObject.Checked -= WhenChecked;
            base.OnDetaching();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenChecked
        ///
        /// <summary>
        /// Occurs when the Checked event is fired.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenChecked(object s, RoutedEventArgs e)
        {
            if (Command?.CanExecute(CommandParameter) ?? false) Command.Execute(CommandParameter);
        }

        #endregion
    }

    #endregion

    #region UncheckedBehavior

    /* --------------------------------------------------------------------- */
    ///
    /// UncheckedBehavior
    ///
    /// <summary>
    /// Represents the behavior when the item is unchecked.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class UncheckedBehavior : CommandBehavior<ToggleButton>
    {
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
            AssociatedObject.Unchecked += WhenUnchecked;
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
            AssociatedObject.Unchecked -= WhenUnchecked;
            base.OnDetaching();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenUnchecked
        ///
        /// <summary>
        /// Occurs when the Unchecked event is fired.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenUnchecked(object s, RoutedEventArgs e)
        {
            if (Command?.CanExecute() ?? false) Command.Execute();
        }

        #endregion
    }

    #endregion

    #region UncheckedBehavior<T>

    /* --------------------------------------------------------------------- */
    ///
    /// UncheckedBehavior
    ///
    /// <summary>
    /// Represents the behavior when the item is unchecked.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class UncheckedBehavior<T> : CommandBehavior<ToggleButton, T>
    {
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
            AssociatedObject.Unchecked += WhenUnchecked;
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
            AssociatedObject.Unchecked -= WhenUnchecked;
            base.OnDetaching();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenUnchecked
        ///
        /// <summary>
        /// Occurs when the Unchecked event is fired.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenUnchecked(object s, RoutedEventArgs e)
        {
            if (Command?.CanExecute(CommandParameter) ?? false) Command.Execute(CommandParameter);
        }

        #endregion
    }

    #endregion
}
