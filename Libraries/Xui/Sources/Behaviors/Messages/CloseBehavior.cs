﻿/* ------------------------------------------------------------------------- */
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
    /// CloseBehavior
    ///
    /// <summary>
    /// Represents the behavior to close the window.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class CloseBehavior : MessageBehavior<CloseMessage>
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
}
