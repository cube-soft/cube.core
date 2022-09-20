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
namespace Cube.Tests.Extensions;

using System;
using System.Linq;
using Cube.Syntax.Extensions;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// IterationTest
///
/// <summary>
/// Represents tests for the IterateExtension class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class IterationTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Make
    ///
    /// <summary>
    /// Tests the Make extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Make()
    {
        var src = 10.Make(i => i * 2);
        Assert.That(src.Count(), Is.EqualTo(10));
        Assert.That(src.First(), Is.EqualTo(0));
        Assert.That(src.Last(),  Is.EqualTo(18));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Times
    ///
    /// <summary>
    /// Tests the Times extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Times()
    {
        var dest = 0;
        10.Times(i => dest += i);
        Assert.That(dest, Is.EqualTo(45));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Try
    ///
    /// <summary>
    /// Tests the Try extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Try()
    {
        var n = 0;
        Assert.That(10.Try(i => ThrowIfOdd(++n)), Is.True);
        Assert.That(n, Is.EqualTo(2));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Try_False
    ///
    /// <summary>
    /// Tests the Try extended method with the function that always
    /// fails.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Try_False()
    {
        var errors = 0;
        Assert.That(10.Try(i => ThrowIfOdd(1), (s, e) => ++errors), Is.False);
        Assert.That(errors, Is.EqualTo(10));
    }

    #endregion

    #region Others

    /* --------------------------------------------------------------------- */
    ///
    /// ThrowIfOdd
    ///
    /// <summary>
    /// Throws if the specified value is odd number.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private void ThrowIfOdd(int n) => IsOdd(n).Then(() => throw new ArgumentException("Odd number"));

    /* --------------------------------------------------------------------- */
    ///
    /// IsOdd
    ///
    /// <summary>
    /// Determines whether the specified value is odd number.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private bool IsOdd(int n) => n % 2 != 0;

    #endregion
}
