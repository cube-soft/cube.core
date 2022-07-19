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
namespace Cube.Xui.Tests.Behaviors;

using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using Cube.Xui.Behaviors;
using Microsoft.Xaml.Behaviors;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// EventToCommandTest
///
/// <summary>
/// Tests event to command behaviors.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
[Apartment(ApartmentState.STA)]
class EventToCommandTest : ViewFixture
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Show
    ///
    /// <summary>
    /// Tests the ShownToCommand class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Show()
    {
        var count = 0;
        var view  = Hack(new MockWindow());
        var src   = Attach(view, new ShownToCommand
        {
            Command = new DelegateCommand(() =>
            {
                ++count;
                view.Close();
            })
        });

        Assert.That(view.ShowDialog().Value, Is.False);
        src.Detach();
        Assert.That(count, Is.EqualTo(1));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Close
    ///
    /// <summary>
    /// Tests the ClosingToCommand, ClosedToCommand, and DisposeBehavior
    /// classes.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Close()
    {
        var closing  = 0;
        var closed   = 0;

        var view = Hack(new MockWindow());
        var vm   = (MockViewModel)view.DataContext;
        var src  = new List<Behavior>
        {
            Attach(view, new ClosingToCommand { Command = new DelegateCommand<CancelEventArgs>(e => e.Cancel = ++closing % 2 == 1) }),
            Attach(view, new ClosedToCommand  { Command = new DelegateCommand(() => ++closed) }),
            Attach(view, new ClosedToDispose())
        };

        view.Show();
        for (var i = 0; i < 2; ++i) view.Close();
        foreach (var obj in src) obj.Detach();

        Assert.That(closing,     Is.EqualTo(2));
        Assert.That(closed,      Is.EqualTo(1));
        Assert.That(vm.Disposed, Is.True);
    }

    #endregion
}
