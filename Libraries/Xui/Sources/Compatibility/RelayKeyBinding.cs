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
using System.Windows.Input;

namespace Cube.Xui
{
    /* --------------------------------------------------------------------- */
    ///
    /// RelayKeyBinding
    ///
    /// <summary>
    /// Provides support for KeyBinding.
    /// </summary>
    ///
    /// <remarks>
    /// Binding to KeyBinding.Command does not work in .NET Framework 3.5,
    /// so it is set through the utility class.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class RelayKeyBinding : KeyBinding
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// CommandEx
        ///
        /// <summary>
        /// Gets or sets the ICommand object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ICommand CommandEx
        {
            get => (ICommand)GetValue(CommandExProperty);
            set => SetValue(CommandExProperty, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CommandExProperty
        ///
        /// <summary>
        /// Gets the DependencyProperty object to store the CommadEx object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static readonly DependencyProperty CommandExProperty =
            DependencyProperty.Register(
                nameof(CommandEx),
                typeof(ICommand),
                typeof(RelayKeyBinding),
                new FrameworkPropertyMetadata((s, e) =>
                {
                    if (s is RelayKeyBinding rkb) rkb.Command = e.NewValue as ICommand;
                })
            );

        #endregion
    }
}
