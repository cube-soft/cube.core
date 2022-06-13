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
namespace Cube.Tests.Events;

using System;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// EventBehaviorTest
///
/// <summary>
/// Tests methods of the EventBehavior class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class EventBehaviorTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Subscribe
    ///
    /// <summary>
    /// Tests the EventBehavior class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Subscribe()
    {
        var n   = 0;
        var foo = new Foo();
        var src = new EventBehavior(foo, nameof(Foo.Fired), () => ++n);

        foo.Fire();
        foo.Fire();
        Assert.That(n, Is.EqualTo(2));

        src.Dispose();
        foo.Fire();
        Assert.That(n, Is.EqualTo(2));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Subscribe_Throws
    ///
    /// <summary>
    /// Tests the EventBehavior class with null arguments.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Subscribe_Throws()
    {
        Assert.That(() => new EventBehavior(null, null, () => { }), Throws.ArgumentNullException);
        Assert.That(() => new EventBehavior(new Foo(), "dummy", () => { }), Throws.ArgumentNullException);
    }

    #endregion

    #region Others

    private class Foo
    {
        public event EventHandler Fired;
        public void Fire() => Fired?.Invoke(this, EventArgs.Empty);
    }

    #endregion
}
