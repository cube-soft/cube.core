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

using System.Threading;
using System.Windows;
using Cube.Xui.Behaviors;
using Microsoft.Xaml.Behaviors;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// DialogBehaviorTest
///
/// <summary>
/// Tests for the DialogBehavior, OpenFileBehavior, SaveFileBehavior,
/// and OpenDirectoryBehavior classes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
[Apartment(ApartmentState.STA)]
class DialogBehaviorTest : ViewFixture
{
    /* --------------------------------------------------------------------- */
    ///
    /// Test
    ///
    /// <summary>
    /// Tests to create behaviors.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Test()
    {
        var view = new Window();
        var src  = new Behavior<FrameworkElement>[]
        {
            Attach(view, new DialogBehavior()),
            Attach(view, new OpenDirectoryBehavior()),
            Attach(view, new OpenFileBehavior()),
            Attach(view, new SaveFileBehavior())
        };

        foreach (var obj in src) obj.Detach();
    }
}
