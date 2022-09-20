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
namespace Cube.Xui.Tests;

using System.Windows.Input;
using Cube.Xui.Commands.Extensions;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// CommandTest
///
/// <summary>
/// Represents tests of ICommand iplemented classes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class CommandTest
{
    #region Tests

    #region BindableCommand

    /* --------------------------------------------------------------------- */
    ///
    /// Create_ArgumentNullException
    ///
    /// <summary>
    /// Tests constructors with null objects.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Create_ArgumentNullException()
    {
        Assert.That(() => new DelegateCommand(null), Throws.ArgumentNullException);
        Assert.That(() => new DelegateCommand(() => { }, null), Throws.ArgumentNullException);
        Assert.That(() => new DelegateCommand<int>(null), Throws.ArgumentNullException);
        Assert.That(() => new DelegateCommand<int>(e => { }, null), Throws.ArgumentNullException);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Execute
    ///
    /// <summary>
    /// Tests methods of the DelegateCommand class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Execute()
    {
        var n = 0;
        var src = new Person();

        using var dest = new DelegateCommand(
            () => src.Name = "Done",
            () => src.Age > 0
        );

        dest.Refresh();
        dest.CanExecuteChanged += (s, e) => ++n;
        dest.Observe(src);
        Assert.That(dest.CanExecute(), Is.False);
        Assert.That(n, Is.EqualTo(0));
        src.Age = 10;
        Assert.That(dest.CanExecute(), Is.True);
        Assert.That(n, Is.EqualTo(1));
        src.Age = -20;
        Assert.That(dest.CanExecute(), Is.False);
        Assert.That(n, Is.EqualTo(2));
        dest.Refresh();
        dest.Execute();
        Assert.That(src.Name, Is.EqualTo("Done"));
        Assert.That(n, Is.EqualTo(4));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Execute_Generic
    ///
    /// <summary>
    /// Tests methods of the DelegateCommand(T) class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Execute_Generic()
    {
        var n = 0;
        var src = new Person();

        using var dest = new DelegateCommand<int>(
            e => src.Name = $"Done:{e}",
            e => e > 0 && src.Age > 0
        );

        dest.Refresh();
        dest.CanExecuteChanged += (s, e) => ++n;
        dest.Observe(src);
        Assert.That(dest.CanExecute(-10), Is.False);
        Assert.That(dest.CanExecute(10),  Is.False);
        Assert.That(n, Is.EqualTo(0));
        src.Age = 10;
        Assert.That(dest.CanExecute(-20), Is.False);
        Assert.That(dest.CanExecute(20),  Is.True);
        Assert.That(n, Is.EqualTo(1));
        src.Age = -20;
        Assert.That(dest.CanExecute(-30), Is.False);
        Assert.That(dest.CanExecute(30),  Is.False);
        Assert.That(n, Is.EqualTo(2));
        dest.Refresh();
        dest.Execute(40);
        Assert.That(src.Name, Is.EqualTo("Done:40"));
        Assert.That(n, Is.EqualTo(4));
    }

    #endregion

    #region Extension

    /* --------------------------------------------------------------------- */
    ///
    /// Execute_Extended
    ///
    /// <summary>
    /// Tests the CanExecute and Execute extended methods.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Execute_Extension()
    {
        var count = 0;
        var src = new DelegateCommand(() => ++count) as ICommand;

        Assert.That(src.CanExecute(), Is.True);

        src.Execute();
        src.Execute();
        src.Execute();

        Assert.That(count, Is.EqualTo(3));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Execute_Extension_Null
    ///
    /// <summary>
    /// Tests the CanExecute and Execute extended methods with a
    /// null object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Execute_Extension_Null()
    {
        var src = default(ICommand);
        Assert.That(src.CanExecute(), Is.False);
        Assert.DoesNotThrow(() => src.Execute());
    }

    #endregion

    #endregion
}
