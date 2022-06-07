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
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

/* ------------------------------------------------------------------------- */
///
/// SelectionBehavior
///
/// <summary>
/// Provides functionality to bind a ViewModel to either the
/// ListBoxItem or the ListViewItem.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class SelectionBehavior : Behavior<ListBox>
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
        AssociatedObject.SelectionChanged -= WhenSelectionChanged;
        AssociatedObject.SelectionChanged += WhenSelectionChanged;
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
        AssociatedObject.SelectionChanged -= WhenSelectionChanged;
        base.OnDetaching();
    }

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// WhenSelectionChanged
    ///
    /// <summary>
    /// Called after the selected items of the AssociatedObject is
    /// changed.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private void WhenSelectionChanged(object s, SelectionChangedEventArgs e)
    {
        foreach (var ri in e.RemovedItems.OfType<IListItem>()) ri.Selected = false;
        foreach (var ai in e.AddedItems.OfType<IListItem>()) ai.Selected = true;
    }

    #endregion
}
