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
using System.Windows;

namespace Cube.Xui.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// ShowBehavior(TView, TViewModel)
    ///
    /// <summary>
    /// Represents the behavior to show a TView window using a TViewModel
    /// message.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ShowBehavior<TView, TViewModel> : MessageBehavior<TViewModel>
        where TView : Window, new()
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Modal
        ///
        /// <summary>
        /// Gets or sets a value indicating whether to show a dialog as
        /// modal.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Modal
        {
            get => (bool)GetValue(ModalProperty);
            set => SetValue(ModalProperty, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ModalProperty
        ///
        /// <summary>
        /// Gets the DependencyProperty object for the Modal property.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static readonly DependencyProperty ModalProperty =
            DependencyFactory.Create<ShowBehavior<TView, TViewModel>, bool>(
                nameof(Modal), (s, e) => s.Modal = e);

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the action.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Invoke(TViewModel e)
        {
            var dest = new TView { DataContext = e };
            if (AssociatedObject is Window wnd) dest.Owner = wnd;
            if (Modal) _ = dest.ShowDialog();
            else dest.Show();
        }

        #endregion
    }
}
