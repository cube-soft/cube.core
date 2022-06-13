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
/// ValueEventArgsTest
///
/// <summary>
/// Tests the ValueEventArgs class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class ValueEventArgsTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Create_ValueEventArgs
    ///
    /// <summary>
    /// Tests the ValueEventArgs.Create(T) method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(10)]
    [TestCase(3.1415926)]
    [TestCase("Hello, world")]
    public void Create_ValueEventArgs<T>(T value)
    {
        var args = ValueEventArgs.Create(value);
        Assert.That(args.Value, Is.EqualTo(value));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Create_ValueCancelEventArgs
    ///
    /// <summary>
    /// Tests the ValueEventArgs.Create(T, bool) method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(-1, true)]
    [TestCase(1.41421356, false)]
    [TestCase("Cancel!Cancel!", true)]
    [TestCase(true, false)]
    public void Create_ValueCancelEventArgs<T>(T value, bool cancel)
    {
        var args = ValueEventArgs.Create(value, cancel);
        Assert.That(args.Value, Is.EqualTo(value));
        Assert.That(args.Cancel, Is.EqualTo(cancel));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Create_ValueChangedEventArgs
    ///
    /// <summary>
    /// Executes the test to create a new instance of the
    /// ValueChangedEventArgs(T) class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(2, 20)]
    [TestCase(3.1415926, 1.41421356)]
    [TestCase("Hello, world", "Changed world")]
    [TestCase(false, true)]
    public void Create_ValueChangedEventArgs<T>(T before, T after)
    {
        var args = ValueEventArgs.Create(before, after);
        Assert.That(args.OldValue, Is.EqualTo(before));
        Assert.That(args.NewValue, Is.EqualTo(after));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// ValueCancelEventArgs_Cancel
    ///
    /// <summary>
    /// Confirms the default value of the Cancel property.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void ValueCancelEventArgs_Cancel()
    {
        Assert.That(new ValueCancelEventArgs<int>(1).Cancel, Is.False);
    }

    #endregion
}
