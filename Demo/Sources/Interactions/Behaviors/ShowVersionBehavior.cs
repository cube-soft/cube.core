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
using System.Drawing;
using Cube.Forms.Behaviors;

namespace Cube.Forms.Demo
{
    /* --------------------------------------------------------------------- */
    ///
    /// ShowVersionBehavior
    ///
    /// <summary>
    /// Represents the behavior to show a version dialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public sealed class ShowVersionBehavior : MessageBehaviorBase<AboutMessage>
    {
        #region Constructors

        /* --------------------------------------------------------------------- */
        ///
        /// ShowVersionBehavior
        ///
        /// <summary>
        /// Initializes a new instance of the ShowVersionBehavior class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="vm">ViewModel object.</param>
        /// <param name="view">View object.</param>
        ///
        /* --------------------------------------------------------------------- */
        public ShowVersionBehavior(BindableWindow view, IPresentable vm) : base(vm)
        {
            _icon = view.Icon;
            _text = view.Text;
        }

        #endregion

        #region Implementations

        /* --------------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the action.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected override void Invoke(AboutMessage message)
        {
            using var view = new VersionWindow(message.Value)
            {
                Icon = _icon,
                Text = _text,
            };
            view.ShowDialog();
        }

        #endregion

        #region Fields
        private readonly Icon _icon;
        private readonly string _text;
        #endregion
    }
}
