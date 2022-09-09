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

using System.Windows.Controls;
using Cube.Xui.Behaviors;
using Cube.Xui.Commands.Extensions;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// CommandBehaviorTest
///
/// <summary>
/// Tests for the CommandBehavior class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class CommandBehaviorTest
{
    /* --------------------------------------------------------------------- */
    ///
    /// Test
    ///
    /// <summary>
    /// Confirms default values of properties.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Test()
    {
        var src = new CommandBehavior<TextBox, int>();
        Assert.That(src.Command,          Is.Null);
        Assert.That(src.CommandParameter, Is.EqualTo(0));

        src.Command = new DelegateCommand(() => { });
        src.CommandParameter = 10;
        Assert.That(src.Command,              Is.Not.Null);
        Assert.That(src.Command.CanExecute(), Is.True);
        Assert.That(src.CommandParameter,     Is.EqualTo(10));
    }
}
