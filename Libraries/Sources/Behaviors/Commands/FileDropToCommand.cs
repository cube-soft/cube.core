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
using Cube.Mixin.Generics;
using System.Windows;

namespace Cube.Xui.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// FileDropToCommand
    ///
    /// <summary>
    /// Represents the behavior of Drag&amp;Drop files.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class FileDropToCommand<T> : CommandBehavior<T> where T : UIElement
    {
        /* ----------------------------------------------------------------- */
        ///
        /// OnAttached
        ///
        /// <summary>
        /// Called after the action is attached to an AssociatedObject.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewDragOver += OnDragOver;
            AssociatedObject.PreviewDrop += OnDrop;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnDetaching
        ///
        /// <summary>
        /// Called when the action is being detached from its
        /// AssociatedObject, but before it has actually occurred.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnDetaching()
        {
            AssociatedObject.PreviewDragOver -= OnDragOver;
            AssociatedObject.PreviewDrop -= OnDrop;
            base.OnDetaching();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnDrop
        ///
        /// <summary>
        /// Occurs when the PreviewDrop event is fired.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void OnDrop(object s, DragEventArgs e)
        {
            var dest = GetData(e.Data);
            e.Handled = (dest != null) && (Command?.CanExecute(dest) ?? false);
            if (e.Handled) Command.Execute(dest);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnDragOver
        ///
        /// <summary>
        /// Occurs when the PreviewDragOver event is fired.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void OnDragOver(object s, DragEventArgs e)
        {
            var dest = GetData(e.Data);
            e.Handled = (dest != null) && (Command?.CanExecute(dest) ?? false);
            e.Effects = e.Handled ? DragDropEffects.Copy : DragDropEffects.None;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetData
        ///
        /// <summary>
        /// Gets the collection of dropped files.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private string[] GetData(IDataObject src) =>
            src.GetDataPresent(DataFormats.FileDrop) ?
            src.GetData(DataFormats.FileDrop).TryCast<string[]>() :
            null;
    }
}
