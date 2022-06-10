﻿/* ------------------------------------------------------------------------- */
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
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// ActivateBehaviorTest
///
/// <summary>
/// Tests the ActivateBehavior class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
[Apartment(ApartmentState.STA)]
class ActivateBehaviorTest : ViewFixture
{
    /* --------------------------------------------------------------------- */
    ///
    /// Test
    ///
    /// <summary>
    /// Tests the create, attach, send, and detach methods.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Test()
    {
        var view = Hack(new MockWindow());
        var vm   = (MockViewModel)view.DataContext;

        view.Show();
        vm.Test(new ActivateMessage());
        view.Close();
    }
}
