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
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Cube.Xui.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// PasswordBehavior
    ///
    /// <summary>
    /// Represents behaviors of the PasswordBox.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class PasswordBehavior : Behavior<PasswordBox>
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Password
        ///
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Password
        {
            get => GetValue(PasswordProperty) as string;
            set
            {
                SetValue(PasswordProperty, value);
                SetViewPassword(AssociatedObject, value);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PasswordProperty
        ///
        /// <summary>
        /// Gets the DependencyProperty object for the Password property.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static readonly DependencyProperty PasswordProperty =
            DependencyFactory.Create<PasswordBehavior, string>(nameof(Password), (s, e) => s.Password = e);

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// OnAttached
        ///
        /// <summary>
        /// Occurs when the instance is attached to the PasswordBox.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnAttached()
        {
            base.OnAttached();

            SetViewPassword(AssociatedObject, Password);
            AssociatedObject.PasswordChanged -= WhenViewPasswordChanged;
            AssociatedObject.PasswordChanged += WhenViewPasswordChanged;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnDetaching
        ///
        /// <summary>
        /// Occurs when the instance is detaching from the PasswordBox.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnDetaching()
        {
            AssociatedObject.PasswordChanged -= WhenViewPasswordChanged;

            base.OnDetaching();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetViewPassword
        ///
        /// <summary>
        /// Sets the specified value to the Password property of the
        /// specified PasswordBox object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void SetViewPassword(PasswordBox src, string value)
        {
            if (EqualityComparer<string>.Default.Equals(value, src.Password)) return;

            src.Password = value;
            if (!value.HasValue()) return;

            src.GetType()
               .GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic)
               .Invoke(src, new object[] { 0, value.Length }); // shit!
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenViewPasswordChanged
        ///
        /// <summary>
        /// Occurs when the Password property of the PasswordBox is
        /// changed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenViewPasswordChanged(object s, RoutedEventArgs e) =>
            Password = AssociatedObject.Password;

        #endregion
    }
}
