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

using System;
using Cube.Observable.Extensions;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// ObserverTest
///
/// <summary>
/// Test the IObservePropertyChanged implemented class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class ObserverTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Observe
    ///
    /// <summary>
    /// Tests Observe and related methods.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Observe()
    {
        var n   = new Accessor<int>();
        var src = new Person();

        using (new Mock(n)
            .Hook(src) // All
            .Hook(src, nameof(src.Name))
            .Hook(default) // Ignore
        ) {
            Assert.That(src.Age,  Is.EqualTo(0));
            src.Age = 10;
            Assert.That(n.Get(),  Is.EqualTo(1));
            Assert.That(src.Age,  Is.EqualTo(10));
            src.Age = 10;
            Assert.That(n.Get(),  Is.EqualTo(1));
            Assert.That(src.Age,  Is.EqualTo(10));

            Assert.That(src.Name, Is.Empty);
            src.Name = "Test";
            Assert.That(n.Get(),  Is.EqualTo(3));
            Assert.That(src.Name, Is.EqualTo("Test"));
            src.Name = "Test";
            Assert.That(n.Get(),  Is.EqualTo(3));
            Assert.That(src.Name, Is.EqualTo("Test"));
        }

        src.Age = 20;
        Assert.That(n.Get(),  Is.EqualTo(3));
        Assert.That(src.Age,  Is.EqualTo(20));

        src.Name = "";
        Assert.That(n.Get(),  Is.EqualTo(3));
        Assert.That(src.Name, Is.Empty);
    }

    #endregion

    #region Others

    /* --------------------------------------------------------------------- */
    ///
    /// Mock
    ///
    /// <summary>
    /// Represents an ObserverBase inherited class for testing.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private class Mock : ObserverBase
    {
        public Mock(Accessor<int> count) { _count = count; }
        protected override void Receive(Type s, string e) => _count.Set(_count.Get() + 1);
        private readonly Accessor<int> _count;
    }

    #endregion
}
