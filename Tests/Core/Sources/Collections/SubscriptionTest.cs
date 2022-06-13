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
namespace Cube.Tests.Collections;

using System;
using System.Threading.Tasks;
using Cube.Collections;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// SubscriptionTest
///
/// <summary>
/// Tests for the Subscription class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class SubscriptionTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Subscribe
    ///
    /// <summary>
    /// Tests to subscribe and clear the subscription.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Subscribe()
    {
        var n = 0;
        using var src = new Subscription<Action>();

        _ = Parallel.For(0, 10, i =>
        {
            var dispose = src.Subscribe(() => ++n);
            dispose.Dispose();
        });

        Assert.That(src.Disposed, Is.False);
        Assert.That(src.Count, Is.EqualTo(0));
    }

    #endregion
}
