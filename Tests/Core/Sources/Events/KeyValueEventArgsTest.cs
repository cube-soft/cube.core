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

using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// KeyValueEventArgsTest
///
/// <summary>
/// Tests the KeyValueEventArgs class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class KeyValueEventArgsTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Create_KeyValueEventArgs
    ///
    /// <summary>
    /// Tests the KeyValueEventArgs.Create(T, U) method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(1, "foo")]
    [TestCase("pi", 3.1415926)]
    [TestCase(1, true)]
    public void Create_KeyValueEventArgs<T, U>(T key, U value)
    {
        var args = KeyValueEventArgs.Create(key, value);
        Assert.That(args.Key, Is.EqualTo(key));
        Assert.That(args.Value, Is.EqualTo(value));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Create_KeyValueCancelEventArgs
    ///
    /// <summary>
    /// Tests the KeyValueEventArgs.Create(T, U, bool) method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(5, "cancel", true)]
    [TestCase("No cancel", 1.7320508, false)]
    [TestCase(true, false, false)]
    public void Create_KeyValueCancelEventArgs<T, U>(T key, U value, bool cancel)
    {
        var args = KeyValueEventArgs.Create(key, value, cancel);
        Assert.That(args.Key, Is.EqualTo(key));
        Assert.That(args.Value, Is.EqualTo(value));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// KeyValueCancelEventArgs_Cancel
    ///
    /// <summary>
    /// Confirms the default value of the Cancel property.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void KeyValueCancelEventArgs_Cancel()
    {
        Assert.That(new KeyValueCancelEventArgs<int, int>(1, 2).Cancel, Is.False);
    }

    #endregion
}
