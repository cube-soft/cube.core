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

using Cube.Observable.Extensions;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// BindableValueTest
///
/// <summary>
/// Tests the BindableValue class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class BindableValueTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Set
    ///
    /// <summary>
    /// Tests the setter method of the Value property.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Set()
    {
        var n   = 5;
        var src = new BindableValue<int>(() => n, e => n = e, Dispatcher.Vanilla);

        Assert.That(src.Value, Is.EqualTo(n).And.EqualTo(5));
        src.Value = 10;
        Assert.That(src.Value, Is.EqualTo(n).And.EqualTo(10));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Set_InvalidOperationException
    ///
    /// <summary>
    /// Confirms the behavior when setting value without any setter
    /// functions.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Set_InvalidOperationException()
    {
        var src = new BindableValue<int>(() => 8, Dispatcher.Vanilla);

        Assert.That(src.Value, Is.EqualTo(8));
        Assert.That(() => src.Value = 7, Throws.InvalidOperationException);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Refresh
    ///
    /// <summary>
    /// Confirms the behavior of the PropertyChanged event.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Refresh()
    {
        var src = new BindableValue<Person>(Dispatcher.Vanilla);

        var count = 0;
        src.PropertyChanged += (s, e) => ++count;
        var value = new Person();
        src.Value = value;

        value.Name = "Jack";
        value.Age  = 20;
        src.Refresh();
        Assert.That(count, Is.EqualTo(2));

        src.Value = value;
        Assert.That(count, Is.EqualTo(2));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Observe
    ///
    /// <summary>
    /// Tests the Observe method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Observe()
    {
        var n   = 0;
        var obj = new Person();

        using (var src = new BindableValue<int>(Dispatcher.Vanilla))
        {
            src.PropertyChanged += (s, e) => ++n;
            src.Hook(obj)
               .Observe(obj, nameof(Person.Name));

            obj.Name = "Mike"; // 2 times
            obj.Name = "Jack"; // 2 times
            obj.Name = "Jack"; // ignore
            obj.Age  = 10;     // 1 time
            obj.Age  = 10;     // ignore
        }

        Assert.That(n, Is.EqualTo(5));
    }

    #endregion
}
