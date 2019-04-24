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
using Microsoft.Win32;

namespace Cube.Xui.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// OpenFileDialogBehavior
    ///
    /// <summary>
    /// OpenFileDialog を表示する Behavior です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class OpenFileDialogBehavior : MessengerBehavior<OpenFileMessage>
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// 処理を実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Invoke(OpenFileMessage e)
        {
            var dialog = new OpenFileDialog
            {
                CheckPathExists = e.CheckPathExists,
                Multiselect     = e.Multiselect,
            };

            if (e.Title.HasValue()) dialog.Title = e.Title;
            if (e.FileName.HasValue()) dialog.FileName = e.FileName;
            if (e.Filter.HasValue()) dialog.Filter = e.Filter;
            if (e.InitialDirectory.HasValue()) dialog.InitialDirectory = e.InitialDirectory;

            e.Result = dialog.ShowDialog() ?? false;

            if (e.Result)
            {
                e.FileName  = dialog.FileName;
                e.FileNames = dialog.FileNames;
            }

            e.Callback?.Invoke(e);
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// SaveFileDialogBehavior
    ///
    /// <summary>
    /// SaveFileDialog を表示する Behavior です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class SaveFileDialogBehavior : MessengerBehavior<SaveFileMessage>
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// 処理を実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Invoke(SaveFileMessage e)
        {
            var dialog = new SaveFileDialog
            {
                CheckPathExists = e.CheckPathExists,
                OverwritePrompt = e.OverwritePrompt,
            };

            if (e.Title.HasValue()) dialog.Title = e.Title;
            if (e.FileName.HasValue()) dialog.FileName = e.FileName;
            if (e.Filter.HasValue()) dialog.Filter = e.Filter;
            if (e.InitialDirectory.HasValue()) dialog.InitialDirectory = e.InitialDirectory;

            e.Result = dialog.ShowDialog() ?? false;
            if (e.Result) e.FileName = dialog.FileName;
            e.Callback?.Invoke(e);
        }
    }
}
