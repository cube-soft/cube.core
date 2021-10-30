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
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Microsoft.Xaml.Behaviors;

namespace Cube.Xui.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// WindowsFormsBehavior
    ///
    /// <summary>
    /// Provides functionality to apply behaviors to WinForms components.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class WindowsFormsBehavior<TControl> : Behavior<WindowsFormsHost>
        where TControl : Control
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Source
        ///
        /// <summary>
        /// Gets the source control.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TControl Source => AssociatedObject?.Child as TControl;

        /* ----------------------------------------------------------------- */
        ///
        /// Parent
        ///
        /// <summary>
        /// Gets the parecont control of the source.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public WindowsFormsHost Parent => AssociatedObject;

        #endregion
    }
}
