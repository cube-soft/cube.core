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
namespace Cube.Tests;

using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// DisposableContainerTest
///
/// <summary>
/// Test the DisposableContainer class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class DisposableContainerTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Add
    ///
    /// <summary>
    /// Tests the Add and Dispose methods.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Add()
    {
        var n   = 0;
        var src = new DisposableContainer(
            Disposable.Create(() => ++n),
            Disposable.Create(() => ++n),
            Disposable.Create(() => ++n)
         );

        src.Add(() => ++n);
        src.Add(() => ++n);

        Assert.That(n, Is.EqualTo(0));
        src.Dispose();
        Assert.That(n, Is.EqualTo(5));
        src.Add(() => ++n);
        Assert.That(n, Is.EqualTo(6));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Contains
    ///
    /// <summary>
    /// Tests the Contains method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Contains()
    {
        var src = new DisposableContainer();
        var n   = 0;
        var e0 = Disposable.Create(() => n++);
        var e1 = Disposable.Create(() => n++);

        Assert.That(src.Contains(e0), Is.False);
        src.Add(e0);
        Assert.That(src.Contains(e0), Is.True);
        Assert.That(src.Contains(e1), Is.False);
        src.Dispose();
        Assert.That(src.Contains(e0), Is.False);
        Assert.That(src.Contains(e1), Is.False);
        src.Add(e1);
        Assert.That(src.Contains(e1), Is.False);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Hook
    ///
    /// <summary>
    /// Tests the Hook extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Hook()
    {
        var n    = 0;
        var src  = new DisposableContainer();
        var dest = src.Hook(Disposable.Create(() => n++));

        Assert.That(src.Contains(dest), Is.True);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Make_Empty
    ///
    /// <summary>
    /// Tests the constructor and Dispose method with no disposable
    /// objects.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Make_Empty() => Assert.DoesNotThrow(() => { using (new DisposableContainer()) { } });

    #endregion
}
