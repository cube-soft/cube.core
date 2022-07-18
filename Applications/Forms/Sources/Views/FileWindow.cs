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
namespace Cube.Forms.Demo;

using System.Windows.Forms;
using Cube.Forms.Behaviors;
using Cube.Forms.Binding;

/* ------------------------------------------------------------------------- */
///
/// FileWindow
///
/// <summary>
/// Represents the window to test the OpenFileDialog, SaveFileDialog,
/// and SaveDirectoryDialog.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public partial class FileWindow : Window
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// FileWindow
    ///
    /// <summary>
    /// Initializes a new instance of theFileWindow class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public FileWindow()
    {
        InitializeComponent();
        ActiveControl = PathTextBox;
    }

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// OnBind
    ///
    /// <summary>
    /// Binds the window with the specified presenter.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected override void OnBind(IBindable src)
    {
        base.OnBind(src);
        if (src is not FileViewModel vm) return;

        var obj = Behaviors.Hook(new BindingSource(vm, ""));
        obj.Bind(nameof(vm.Value), PathTextBox, nameof(TextBox.Text));

        Behaviors.Add(new ClickEventBehavior(OpenFileButton, vm.ShowOpenFileDialog));
        Behaviors.Add(new ClickEventBehavior(SaveFileButton, vm.ShowSaveFileDialog));
        Behaviors.Add(new ClickEventBehavior(OpenDirectoryButton, vm.ShowOpenDirectoryDialog));
        Behaviors.Add(new OpenFileBehavior(vm));
        Behaviors.Add(new SaveFileBehavior(vm));
        Behaviors.Add(new OpenDirectoryBehavior(vm));
        Behaviors.Add(new PathLintBehavior(PathTextBox));
    }

    #endregion
}
