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
namespace Cube.Forms.Behaviors;

using System.Windows.Forms;
using Cube.Text.Extensions;

/* ------------------------------------------------------------------------- */
///
/// SaveFileBehavior
///
/// <summary>
/// Provides functionality to show a save-file dialog.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class SaveFileBehavior : MessageBehavior<SaveFileMessage>
{
    /* --------------------------------------------------------------------- */
    ///
    /// SaveFileBehavior
    ///
    /// <summary>
    /// Initializes a new instance of the SaveFileBehavior class
    /// with the specified presentable object.
    /// </summary>
    ///
    /// <param name="aggregator">Presentable object.</param>
    ///
    /* --------------------------------------------------------------------- */
    public SaveFileBehavior(IAggregator aggregator) : base(aggregator, e =>
    {
        var view = new SaveFileDialog
        {
            CheckPathExists = e.CheckPathExists,
            OverwritePrompt = e.OverwritePrompt,
            Filter          = e.GetFilterText(),
            FilterIndex     = e.GetFilterIndex(),
        };

        if (e.Text.HasValue() && e.Text != nameof(SaveFileMessage)) view.Title = e.Text;
        if (e.Value.HasValue()) view.FileName = e.Value;
        if (e.InitialDirectory.HasValue()) view.InitialDirectory = e.InitialDirectory;

        e.Cancel = view.ShowDialog() != DialogResult.OK;
        if (!e.Cancel) e.Value = view.FileName;
    }) { }
}
