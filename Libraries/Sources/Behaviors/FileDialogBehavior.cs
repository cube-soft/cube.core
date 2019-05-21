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
using System.Linq;
using System.Windows.Forms;

namespace Cube.Forms.Behaviors
{
    #region OpenFileDialogBehavior

    /* --------------------------------------------------------------------- */
    ///
    /// OpenFileDialogBehavior
    ///
    /// <summary>
    /// Pvovides functionality to show a open-file dialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class OpenFileDialogBehavior : SubscribeBehavior<OpenFileMessage>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// OpenFileDialogBehavior
        ///
        /// <summary>
        /// Initializes a new instance of the OpenFileDialogBehavior class
        /// with the specified presentable object.
        /// </summary>
        ///
        /// <param name="src">Presentable object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public OpenFileDialogBehavior(IPresentable src) :base (src) { }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Shows a open-file dialog.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Invoke(OpenFileMessage e)
        {
            var dialog = new OpenFileDialog
            {
                CheckPathExists = e.CheckPathExists,
                Multiselect     = e.Multiselect,
                FilterIndex     = e.FilterIndex,
            };

            if (e.Title.HasValue()) dialog.Title = e.Title;
            if (e.Value.Any()) dialog.FileName = e.Value.First();
            if (e.Filter.HasValue()) dialog.Filter = e.Filter;
            if (e.InitialDirectory.HasValue()) dialog.InitialDirectory = e.InitialDirectory;

            e.Status = dialog.ShowDialog() == DialogResult.OK;
            if (e.Status) e.Value = dialog.FileNames;
            e.Callback?.Invoke(e);
        }

        #endregion
    }

    #endregion

    #region SaveFileDialogBehavior

    /* --------------------------------------------------------------------- */
    ///
    /// SaveFileDialogBehavior
    ///
    /// <summary>
    /// Pvovides functionality to show a save-file dialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class SaveFileDialogBehavior : SubscribeBehavior<SaveFileMessage>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// SaveFileDialogBehavior
        ///
        /// <summary>
        /// Initializes a new instance of the SaveFileDialogBehavior class
        /// with the specified presentable object.
        /// </summary>
        ///
        /// <param name="src">Presentable object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public SaveFileDialogBehavior(IPresentable src) : base(src) { }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Shows a save-file dialog.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Invoke(SaveFileMessage e)
        {
            var dialog = new SaveFileDialog
            {
                CheckPathExists = e.CheckPathExists,
                OverwritePrompt = e.OverwritePrompt,
                FilterIndex     = e.FilterIndex,
            };

            if (e.Title.HasValue()) dialog.Title = e.Title;
            if (e.Value.HasValue()) dialog.FileName = e.Value;
            if (e.Filter.HasValue()) dialog.Filter = e.Filter;
            if (e.InitialDirectory.HasValue()) dialog.InitialDirectory = e.InitialDirectory;

            e.Status = dialog.ShowDialog() == DialogResult.OK;
            if (e.Status) e.Value = dialog.FileName;
            e.Callback?.Invoke(e);
        }

        #endregion
    }

    #endregion
}
