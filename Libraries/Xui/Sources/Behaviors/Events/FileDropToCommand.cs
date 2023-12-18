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

using System.Windows;
using Cube.Generics.Extensions;

/* ------------------------------------------------------------------------- */
///
/// FileDropToCommand
///
/// <summary>
/// Represents the behavior of Drag&amp;Drop files.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class FileDropToCommand<T> : CommandBehavior<T> where T : FrameworkElement
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// OnAttached
    ///
    /// <summary>
    /// Called after the action is attached to an AssociatedObject.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.PreviewDragOver += WhenDragOver;
        AssociatedObject.PreviewDrop += WhenDrop;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// OnDetaching
    ///
    /// <summary>
    /// Called when the action is being detached from its
    /// AssociatedObject, but before it has actually occurred.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected override void OnDetaching()
    {
        AssociatedObject.PreviewDragOver -= WhenDragOver;
        AssociatedObject.PreviewDrop -= WhenDrop;
        base.OnDetaching();
    }

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// WhenDrop
    ///
    /// <summary>
    /// Occurs when the PreviewDrop event is fired.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private void WhenDrop(object s, DragEventArgs e)
    {
        var dest = GetData(e.Data);
        e.Handled = dest is not null && (Command?.CanExecute(dest) ?? false);
        if (e.Handled) Command?.Execute(dest);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// WhenDragOver
    ///
    /// <summary>
    /// Occurs when the PreviewDragOver event is fired.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private void WhenDragOver(object s, DragEventArgs e)
    {
        var dest = GetData(e.Data);
        e.Handled = dest is not null && (Command?.CanExecute(dest) ?? false);
        e.Effects = e.Handled ? DragDropEffects.Copy : DragDropEffects.None;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetData
    ///
    /// <summary>
    /// Gets the collection of dropped files.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private string[] GetData(IDataObject src) =>
        src.GetDataPresent(DataFormats.FileDrop) ?
        src.GetData(DataFormats.FileDrop).TryCast<string[]>() :
        null;

    #endregion
}
