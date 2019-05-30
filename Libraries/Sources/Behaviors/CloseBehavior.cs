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
using System.ComponentModel;
using System.Windows;
using System.Windows.Interactivity;

namespace Cube.Xui.Behaviors
{
    #region CloseBehavior

    /* --------------------------------------------------------------------- */
    ///
    /// CloseBehavior
    ///
    /// <summary>
    /// Represents the behavior of closing window when received a
    /// CloseMessage.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class CloseBehavior : SubscribeBehavior<CloseMessage>
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the operations.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Invoke(CloseMessage e)
        {
            if (AssociatedObject is Window w) w.Close();
        }
    }

    #endregion

    #region ClosingBehavior

    /* --------------------------------------------------------------------- */
    ///
    /// ClosingBehavior
    ///
    /// <summary>
    /// Represents the behavior when the Closing event is fired.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ClosingBehavior : CommandBehavior<Window>
    {
        /* ----------------------------------------------------------------- */
        ///
        /// OnAttached
        ///
        /// <summary>
        /// Occurs when the instance is attached to the Window.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Closing -= WhenClosing;
            AssociatedObject.Closing += WhenClosing;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnDetaching
        ///
        /// <summary>
        /// Occurs when the instance is detaching from the Window.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnDetaching()
        {
            AssociatedObject.Closing -= WhenClosing;
            base.OnDetaching();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenClosing
        ///
        /// <summary>
        /// Occurs when the Closing event is fired.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenClosing(object s, CancelEventArgs e)
        {
            if (Command?.CanExecute(e) ?? false) Command.Execute(e);
        }
    }

    #endregion

    #region ClosedBehavior

    /* --------------------------------------------------------------------- */
    ///
    /// ClosedBehavior
    ///
    /// <summary>
    /// Represents the behavior of disposing the DataContext when the
    /// Closed event is fired.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ClosedBehavior : Behavior<Window>
    {
        /* ----------------------------------------------------------------- */
        ///
        /// OnAttached
        ///
        /// <summary>
        /// Occurs when the instance is attached to the Window.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Closed -= WhenClosed;
            AssociatedObject.Closed += WhenClosed;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnDetaching
        ///
        /// <summary>
        /// Occurs when the instance is detaching from the Window.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnDetaching()
        {
            AssociatedObject.Closing -= WhenClosed;
            base.OnDetaching();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenClosed
        ///
        /// <summary>
        /// Occurs when the Closed event is fired.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenClosed(object s, EventArgs e)
        {
            if (AssociatedObject == null) return;
            var dc = AssociatedObject.DataContext as IDisposable;
            AssociatedObject.DataContext = DependencyProperty.UnsetValue;
            dc?.Dispose();
        }
    }

    #endregion
}
