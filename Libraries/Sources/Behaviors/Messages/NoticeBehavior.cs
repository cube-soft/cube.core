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
using Cube.Forms.Controls;

namespace Cube.Forms.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// NoticeBehavior
    ///
    /// <summary>
    /// Provides functionality to show a notice window with the specified
    /// information.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class NoticeBehavior : MessageBehavior<NoticeMessage>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// NoticeBehavior
        ///
        /// <summary>
        /// Initializes a new instance of the NoticeBehavior class with
        /// the specified arguments.
        /// </summary>
        ///
        /// <param name="vm">Presentable object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public NoticeBehavior(IPresentable vm) : base(vm) { }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Shows a new window with the specified notice information.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Invoke(NoticeMessage message)
        {
            var view = new NoticeWindow();
            view.Selected += (s, e) => view.Close();
            view.SetTopMost(false);
            view.Set(message.Text, message.Title);
            view.Set(message.Style);
            view.Set(message.Location);
            view.Show();
        }

        #endregion
    }
}
