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
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Cube.Xui.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// CommandBehavior
    ///
    /// <summary>
    /// Represents the inherited Behavior(T) class that has a Command
    /// bindable property.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class CommandBehavior<T> : Behavior<T> where T : DependencyObject
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Command
        ///
        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ICommand Command
        {
            get => GetValue(CommandProperty) as ICommand;
            set => SetValue(CommandProperty, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CommandParameter
        ///
        /// <summary>
        /// Gets or sets the command parameter.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        #endregion

        #region Dependencies

        /* ----------------------------------------------------------------- */
        ///
        /// CommandProperty
        ///
        /// <summary>
        /// Gets the DependencyProperty object for the Command property.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static readonly DependencyProperty CommandProperty =
            Create<ICommand>(nameof(Command), (s, e) => s.Command = e);

        /* ----------------------------------------------------------------- */
        ///
        /// CommandParameterProperty
        ///
        /// <summary>
        /// Gets the DependencyProperty object for the CommandParameter
        /// property.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static readonly DependencyProperty CommandParameterProperty =
            Create<object>(nameof(CommandParameter), (s, e) => s.CommandParameter = e);

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Creates
        ///
        /// <summary>
        /// Creates a new instance of the DependencyProperty class
        /// with the specified arguments.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static DependencyProperty Create<U>(string name, Action<CommandBehavior<T>, U> action) =>
            DependencyFactory.Create(name, action);

        #endregion
    }
}
