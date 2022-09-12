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
namespace Cube.Xui.Behaviors;

using System.Linq;
using Cube.Text.Extensions;
using Microsoft.Win32;

/* ------------------------------------------------------------------------- */
///
/// OpenFileBehavior
///
/// <summary>
/// Represents the behavior to show a dialog using an OpenFileMessage.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class OpenFileBehavior : MessageBehavior<OpenFileMessage>
{
    /* --------------------------------------------------------------------- */
    ///
    /// Invoke
    ///
    /// <summary>
    /// Invokes the action.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected override void Invoke(OpenFileMessage e)
    {
        var view = new OpenFileDialog
        {
            CheckPathExists = e.CheckPathExists,
            Multiselect     = e.Multiselect,
            Filter          = e.GetFilterText(),
            FilterIndex     = e.GetFilterIndex(),
        };

        if (e.Text.HasValue() && e.Text != nameof(OpenFileMessage)) view.Title = e.Text;
        if (e.Value.Any()) view.FileName = e.Value.First();
        if (e.InitialDirectory.HasValue()) view.InitialDirectory = e.InitialDirectory;

        e.Cancel = !view.ShowDialog() ?? true;
        if (!e.Cancel) e.Value = view.FileNames;
    }
}
