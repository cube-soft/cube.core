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

using System;
using System.Windows;

/* ------------------------------------------------------------------------- */
///
/// ShowDialogBehavior(TView, TViewModel)
///
/// <summary>
/// Represents the behavior to show a TView window using a TViewModel
/// message.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class ShowDialogBehavior<TView, TViewModel> :
    MessageBehavior<CancelMessage<TViewModel>>
    where TView : Window, new()
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Invoke
    ///
    /// <summary>
    /// Invokes the action.
    /// </summary>
    ///
    /// <param name="e">Message object.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected override void Invoke(CancelMessage<TViewModel> e)
    {
        var dest = new TView { DataContext = e.Value };
        if (AssociatedObject is Window wnd) dest.Owner = wnd;
        e.Cancel = !dest.ShowDialog() ?? true;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// OnSubscribe
    ///
    /// <summary>
    /// Subscribes the action defined by inherited classes.
    /// </summary>
    ///
    /// <param name="src">Source aggregator.</param>
    ///
    /// <returns>Object to dispose.</returns>
    ///
    /* --------------------------------------------------------------------- */
    protected override IDisposable OnSubscribe(IAggregator src) =>
        src is null ? default :
        new DisposableContainer(
            src.Subscribe<CancelMessage<TViewModel>>(Invoke),
            src.Subscribe<TViewModel>(e => Invoke(new() { Value = e }))
        );

    #endregion
}
