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
using Cube.Mixin.String;
using System.Windows.Forms;

namespace Cube.Forms.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// DirectoryDialogBehavior
    ///
    /// <summary>
    /// Pvovides functionality to show a directory dialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class DirectoryDialogBehavior : SubscribeBehavior<OpenDirectoryMessage>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// DirectoryDialogBehavior
        ///
        /// <summary>
        /// Initializes a new instance of the DirectoryDialogBehavior class
        /// with the specified presentable object.
        /// </summary>
        ///
        /// <param name="src">Presentable object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public DirectoryDialogBehavior(IPresentable src) : base(src) { }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// 処理を実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Invoke(OpenDirectoryMessage e)
        {
            var dialog = new FolderBrowserDialog { ShowNewFolderButton = e.NewButton };

            if (e.Title.HasValue()) dialog.Description  = e.Title;
            if (e.Value.HasValue()) dialog.SelectedPath = e.Value;

            e.Status = dialog.ShowDialog() == DialogResult.OK;
            if (e.Status) e.Value = dialog.SelectedPath;
            e.Callback?.Invoke(e);
        }

        #endregion
    }
}
