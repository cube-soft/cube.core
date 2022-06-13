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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cube.Collections;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// ArgumentCollectionTest
///
/// <summary>
/// Tests the ArgumentCollection class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class ArgumentCollectionTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Parse_Simple
    ///
    /// <summary>
    /// Tests to parse the very simple arguments.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Parse_Simple()
    {
        var src = new ArgumentCollection(new[] { "foo", "bar", "baz" });

        Assert.That(src.Count,          Is.EqualTo(3));
        Assert.That(src.Options.Count,  Is.EqualTo(0));
        Assert.That(src[0],             Is.EqualTo("foo"));
        Assert.That(src[src.Count - 1], Is.EqualTo("baz"));

        foreach (var item in (IEnumerable)src) Assert.That(item, Is.Not.Null);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Parse_Empty
    ///
    /// <summary>
    /// Tests to parse the empty arguments.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Parse_Empty()
    {
        var src = new ArgumentCollection(Enumerable.Empty<string>(), Argument.Dos);
        Assert.That(src.Count,         Is.EqualTo(0));
        Assert.That(src.Options.Count, Is.EqualTo(0));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Parse_ArgumentException
    ///
    /// <summary>
    /// Tests the constructor with the invalid ArgumentKind value.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Parse_ArgumentException()
    {
        var src  = Enumerable.Empty<string>();
        var type = (Argument)999;
        Assert.That(() => new ArgumentCollection(src, type), Throws.ArgumentException);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Parse
    ///
    /// <summary>
    /// Tests to parse the specified arguments.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCaseSource(nameof(TestCases))]
    public string Parse(
        int id,
        IEnumerable<string> args, Argument kind, bool ignoreCase,
        int operands, int options, string key
    ) {
        using var src = new ArgumentCollection(args, kind, ignoreCase);
        var msg = $"No.{id}";

        Assert.That(src.IgnoreCase, Is.EqualTo(ignoreCase), msg);
        Assert.That(src.Count, Is.EqualTo(operands), msg);
        Assert.That(src.Options.Count, Is.EqualTo(options), msg);

        try { return src.Options[key]; }
        catch { return "exception"; }
    }

    #endregion

    #region TestCases

    /* --------------------------------------------------------------------- */
    ///
    /// TestCases
    ///
    /// <summary>
    /// Gets the test cases.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static IEnumerable<TestCaseData> TestCases
    {
        get
        {
            var n = 0;

            yield return new TestCaseData(n++,
                new List<string> { "foo", "bar", "baz" },
                Argument.Windows,
                false,  // ignore case
                3,      // number of operands
                0,      // number of options
                "dummy"
            ).Returns("exception");

            yield return new TestCaseData(n++,
                new List<string> { "foo", "/bar", "baz", "-hoge", "--fuga" },
                Argument.Windows,
                false,  // ignore case
                1,      // number of operands
                3,      // number of options
                "bar"
            ).Returns("baz");

            yield return new TestCaseData(n++,
                new List<string> { "foo", "/bar", "baz", "-hoge", "--fuga" },
                Argument.Dos,
                false,  // ignore case
                3,      // number of operands (foo, -hoge, --fuga)
                1,      // number of options
                "bar"
            ).Returns("baz");

            yield return new TestCaseData(n++,
                new List<string> { "foo", "/bar", "-baz", "--hoge", "fuga" },
                Argument.Posix,
                false,  // ignore case
                2,      // number of operands (foo, /bar)
                8,      // number of options (b, a, z, -, h, o, g, e)
                "e"
            ).Returns("fuga");

            yield return new TestCaseData(n++,
                new List<string> { "foo", "/bar", "-baz", "--hoge", "fuga" },
                Argument.Gnu,
                false,  // ignore case
                2,      // number of operands (foo, /bar)
                4,      // number of options (b, a, z, hoge)
                "hoge"
            ).Returns("fuga");

            yield return new TestCaseData(n++,
                new List<string> { "foo", "--bar", "baz" },
                Argument.Windows,
                true,   // ignore case
                1,      // number of operands
                1,      // number of options
                "BAR"
            ).Returns("baz");

            yield return new TestCaseData(n++,
                new List<string> { "foo", "--bar", "baz" },
                Argument.Windows,
                false,  // ignore case
                1,      // number of operands
                1,      // number of options
                "BAR"
            ).Returns("exception");

            yield return new TestCaseData(n++,
                new List<string> { "foo", "--bar", "baz" },
                Argument.Windows,
                false,  // ignore case
                1,      // number of operands
                1,      // number of options
                "baz"
            ).Returns("exception");

            yield return new TestCaseData(n++,
                new List<string> { "foo", "--bar" },
                Argument.Windows,
                false,  // ignore case
                1,      // number of operands
                1,      // number of options
                "bar"
            ).Returns("");

            yield return new TestCaseData(n++,
                new List<string> { "foo", "--bar", "--hoge", "fuga" },
                Argument.Windows,
                false,  // ignore case
                1,      // number of operands
                2,      // number of options
                "bar"
            ).Returns("");

            yield return new TestCaseData(n++,
                new List<string> { "foo", "--bar", "--hoge", "fuga" },
                Argument.Windows,
                false,  // ignore case
                1,      // number of operands
                2,      // number of options
                "hoge"
            ).Returns("fuga");

            yield return new TestCaseData(n++,
                new List<string> { "foo", "--Bar", "baz", "--bar", "hoge" },
                Argument.Windows,
                true,   // ignore case
                1,      // number of operands
                1,      // number of options
                "Bar"
            ).Returns("hoge");

            yield return new TestCaseData(n++,
                new List<string> { "foo", "--Bar", "baz", "--bar", "hoge" },
                Argument.Windows,
                false,  // ignore case
                1,      // number of operands
                2,      // number of options
                "Bar"
            ).Returns("baz");

            yield return new TestCaseData(n++,
                new List<string> { "foo", "--", "baz" },
                Argument.Windows,
                false,  // ignore case
                2,      // number of operands
                0,      // number of options
                "dummy"
            ).Returns("exception");

            yield return new TestCaseData(n++,
                new List<string> { "foo", "--bar", "/", "baz" },
                Argument.Windows,
                false,  // ignore case
                2,      // number of operands
                1,      // number of options
                "bar"
            ).Returns("");

            yield return new TestCaseData(n++,
                new List<string> { "foo", "--bar", "-", "baz" },
                Argument.Windows,
                false,  // ignore case
                2,      // number of operands
                1,      // number of options
                "bar"
            ).Returns("");

            yield return new TestCaseData(n++,
                new List<string> { "foo", "--bar", "--", "baz" },
                Argument.Windows,
                false,  // ignore case
                2,      // number of operands
                1,      // number of options
                "bar"
            ).Returns("");

            yield return new TestCaseData(n++,
                new List<string> { "foo", "/bar", "/", "baz" },
                Argument.Dos,
                false,  // ignore case
                2,      // number of operands
                1,      // number of options
                "bar"
            ).Returns("");

            yield return new TestCaseData(n++,
                new List<string> { "foo", "/bar", "-", "baz" },
                Argument.Dos,
                false,  // ignore case
                2,      // number of operands
                1,      // number of options
                "bar"
            ).Returns("-");

            yield return new TestCaseData(n++,
                new List<string> { "foo", "--bar", "", "baz" },
                Argument.Windows,
                false,  // ignore case
                1,      // number of operands
                1,      // number of options
                "bar"
            ).Returns("baz");
        }
    }

    #endregion
}
