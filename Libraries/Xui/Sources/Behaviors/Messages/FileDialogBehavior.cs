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
using System.Linq;
using Cube.Mixin.String;
using Microsoft.Win32;
using WinForms = System.Windows.Forms;

namespace Cube.Xui.Behaviors
{
    #region OpenFileBehavior

    /* --------------------------------------------------------------------- */
    ///
    /// OpenFileBehavior
    ///
    /// <summary>
    /// Represents the behavior to show a dialog using an OpenFileMessage.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class OpenFileBehavior : MessageBehavior<OpenFileMessage>
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the action.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Invoke(OpenFileMessage e)
        {
            var view = new OpenFileDialog
            {
                CheckPathExists = e.CheckPathExists,
                Multiselect     = e.Multiselect,
                FilterIndex     = e.FilterIndex,
            };

            if (e.Text.HasValue()) view.Title = e.Text;
            if (e.Value.Any()) view.FileName = e.Value.First();
            if (e.Filter.HasValue()) view.Filter = e.Filter;
            if (e.InitialDirectory.HasValue()) view.InitialDirectory = e.InitialDirectory;

            var ok = view.ShowDialog() ?? false;
            e.Cancel = !ok;
            if (ok) e.Value = view.FileNames;
        }
    }

    #endregion

    #region SaveFileBehavior

    /* --------------------------------------------------------------------- */
    ///
    /// SaveFileBehavior
    ///
    /// <summary>
    /// Represents the behavior to show a dialog using a SaveFileMessage.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class SaveFileBehavior : MessageBehavior<SaveFileMessage>
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the action.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Invoke(SaveFileMessage e)
        {
            var view = new SaveFileDialog
            {
                CheckPathExists = e.CheckPathExists,
                OverwritePrompt = e.OverwritePrompt,
                FilterIndex     = e.FilterIndex,
            };

            if (e.Text.HasValue()) view.Title = e.Text;
            if (e.Value.HasValue()) view.FileName = e.Value;
            if (e.Filter.HasValue()) view.Filter = e.Filter;
            if (e.InitialDirectory.HasValue()) view.InitialDirectory = e.InitialDirectory;

            var ok = view.ShowDialog() ?? false;
            e.Cancel = !ok;
            if (ok) e.Value = view.FileName;
        }
    }

    #endregion

    #region OpenDirectoryBehavior

    /* --------------------------------------------------------------------- */
    ///
    /// OpenDirectoryBehavior
    ///
    /// <summary>
    /// Represents the behavior to show a dialog using an OpenDirectoryMessage.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class OpenDirectoryBehavior : MessageBehavior<OpenDirectoryMessage>
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the action.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Invoke(OpenDirectoryMessage e)
        {
            var view = new WinForms.FolderBrowserDialog { ShowNewFolderButton = e.NewButton };

            if (e.Text.HasValue()) view.Description = e.Text;
            if (e.Value.HasValue()) view.SelectedPath = e.Value;

            var ok = view.ShowDialog() == WinForms.DialogResult.OK;
            e.Cancel = !ok;
            if (ok) e.Value = view.SelectedPath;
        }
    }

    #endregion
}
