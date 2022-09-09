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

using System.Collections.Generic;
using System.Linq;
using Cube.Collections.Differences;
using Cube.Collections.Extensions;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// DifferencesTest
///
/// <summary>
/// Tests of extended methods defined in the Cube.Collections.Extensions
/// namespace.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class DifferencesTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Diff
    ///
    /// <summary>
    /// Tests of the Diff extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCaseSource(nameof(TestCases))]
    public void Diff(string older, string newer, Result<char> expected)
    {
        var dest = newer.Diff(older).First();
        Assert.That(dest.Condition, Is.EqualTo(expected.Condition));
        Assert.That(dest.Older, Is.EquivalentTo(expected.Older));
        Assert.That(dest.Newer, Is.EquivalentTo(expected.Newer));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Diff_OlderIsEmpty
    ///
    /// <summary>
    /// Tests of the Diff extended method with the null or empty older
    /// value.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("")]
    [TestCase(null)]
    public void Diff_OlderIsEmpty(string older)
    {
        var newer = "empty";
        var dest  = newer.Diff(older).First();
        Assert.That(dest.Condition, Is.EqualTo(Condition.Inserted));
        Assert.That(dest.Older, Is.Null);
        Assert.That(dest.Newer, Is.EquivalentTo("empty"));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Diff_NewerIsEmpty
    ///
    /// <summary>
    /// Tests of the Diff extended method with the null or empty newer
    /// value.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("")]
    [TestCase(null)]
    public void Diff_NewerIsEmpty(string newer)
    {
        var dest = newer.Diff("empty").First();
        Assert.That(dest.Condition, Is.EqualTo(Condition.Deleted));
        Assert.That(dest.Older, Is.EquivalentTo("empty"));
        Assert.That(dest.Newer, Is.Null);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Diff_IgnoreCase
    ///
    /// <summary>
    /// Tests of the Diff extended method with the user predicate
    /// function.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Diff_IgnoreCase()
    {
        var newer = "AbCdEfG";
        var older = "aBcDeFg";
        var dest  = newer.Diff(older, (x, y) => char.ToLower(x) == char.ToLower(y), Condition.DiffOnly);

        Assert.That(dest.Count(), Is.EqualTo(0));
    }

    #endregion

    #region TestCases

    /* --------------------------------------------------------------------- */
    ///
    /// TestCases
    ///
    /// <summary>
    /// Gets test cases.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static IEnumerable<TestCaseData> TestCases
    {
        get
        {
            yield return TestCase("Hello, world.", "Hello, sunset.", Condition.Changed, "world", "sunset");
            yield return TestCase("Hello, sunset.", "Hello, world.", Condition.Changed, "sunset", "world");
        }
    }

    #endregion

    #region Others

    /* --------------------------------------------------------------------- */
    ///
    /// TestCase
    ///
    /// <summary>
    /// Creates a new instance of the TestCaseData class with the
    /// specified arguments.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static TestCaseData TestCase(string older, string newer,
        Condition cond, string oresult, string nresult) =>
        new(older, newer, new Result<char>(cond, oresult, nresult));

    #endregion
}
