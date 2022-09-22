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
using Cube.Reflection.Extensions;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// SoftwareVersionTest
///
/// <summary>
/// Tests for the SoftwareVersion and related classes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class SoftwareVersionTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// GetString
    ///
    /// <summary>
    /// Executes the test for getting the version string.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void GetString()
    {
        var src   = typeof(SoftwareVersionTest).Assembly;
        var major = src.GetVersion().Major;
        var minor = src.GetVersion().Minor;
        var build = src.GetVersion().Build;
        var pf    = src.GetArchitecture();
        var dest  = new SoftwareVersion(src)
        {
            Prefix = "begin-",
            Suffix = "-end"
        };

        Assert.That(dest.Architecture,       Is.EqualTo(pf));
        Assert.That(dest.ToString(1, true),  Is.EqualTo($"begin-{major}-end ({pf})"));
        Assert.That(dest.ToString(2, true),  Is.EqualTo($"begin-{major}.{minor}-end ({pf})"));
        Assert.That(dest.ToString(4, false), Is.EqualTo($"begin-{major}.{minor}.{build}.0-end"));
        Assert.That(dest.ToString(),         Is.EqualTo(dest.ToString(3, false)));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Parse
    ///
    /// <summary>
    /// Tests to create a new instance of the SoftwareVersion class
    /// with the specified string.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("1.0",                   2, ExpectedResult = true )]
    [TestCase("1.0.0",                 3, ExpectedResult = true )]
    [TestCase("1.0.0.0",               4, ExpectedResult = true )]
    [TestCase("1.0.0.0-suffix",        4, ExpectedResult = true )]
    [TestCase("v1.0.0.0-suffix",       4, ExpectedResult = true )]
    [TestCase("v1.0.0.0-p21",          4, ExpectedResult = true )]
    [TestCase("p21-v1.0.0.0-suffix",   4, ExpectedResult = true )]
    public bool Parse(string src, int digit) => new SoftwareVersion(src).ToString(digit, false) == src;

    /* --------------------------------------------------------------------- */
    ///
    /// Parse_Ignore
    ///
    /// <summary>
    /// Tests to create a new instance of the SoftwareVersion class
    /// with the specified invalid string.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("1")]
    [TestCase("InvalidVersionNumber")]
    [TestCase(null)]
    public void Parse_Ignore(string str)
    {
        var src = new SoftwareVersion(str);
        var cmp = new SoftwareVersion();

        Assert.That(src.Number.Major,    Is.EqualTo(cmp.Number.Major));
        Assert.That(src.Number.Minor,    Is.EqualTo(cmp.Number.Minor));
        Assert.That(src.Number.Build,    Is.EqualTo(cmp.Number.Build));
        Assert.That(src.Number.Revision, Is.EqualTo(cmp.Number.Revision));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Compare
    ///
    /// <summary>
    /// Tests the Compare method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("1.0.0.0",     "1.0.0.0",      0)]
    [TestCase("1.0.0",       "1.0",          0)]
    [TestCase("1.0.0",       "2.0.0",       -1)]
    [TestCase("1.0.0",       "1.1.0",       -1)]
    [TestCase("1.0.0",       "1.0.1",       -1)]
    [TestCase("1.0.0",       "1.0.0.1",     -1)]
    [TestCase("1.0.0",       "0.9.9",        1)]
    [TestCase("3.1.4",       "2.2.2",        1)]
    [TestCase("3.1.4",       "4.3.2",       -1)]
    [TestCase("v1.0.0",      "1.0.0",        1)]
    [TestCase("v1.0.0",      "V1.0.0",       0)]
    [TestCase("0.0.1-alpha", "0.0.1-beta",  -1)]
    [TestCase("0.1.0-beta",  "0.1.0-BETA",   0)]
    [TestCase("0.9.2-beta",  "0.10.1-beta", -1)]
    [TestCase("0.11.0-beta", "0.10.1-beta",  1)]
    [TestCase("0.10.1",      "0.10.1-beta", -1)]
    public void Compare(string src, string cmp, int expected)
    {
        var engine = new SoftwareVersionComparer();
        var v1     = new SoftwareVersion(src);
        var v2     = new SoftwareVersion(cmp);
        Assert.That(engine.Compare(v1, v2),   Is.EqualTo(expected), nameof(engine.Compare));
        Assert.That(engine.Compare(src, cmp), Is.EqualTo(expected), "Compare(string)");
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Equals_GetHashCode
    ///
    /// <summary>
    /// Tests the Equals and GetHashCode methods.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("1.0.0",       "1.0.0",      true )]
    [TestCase("1.0.0",       "1.0",        true )]
    [TestCase("1.0.0",       "2.0.0",      false)]
    [TestCase("v1.0.0",      "1.0.0",      false)]
    [TestCase("v1.0.0",      "V1.0.0",     true )]
    [TestCase("0.0.1-alpha", "0.0.1-beta", false)]
    [TestCase("0.1.0-beta",  "0.1.0-BETA", true )]
    public void Equals_GetHashCode(string src, string cmp, bool expected)
    {
        var engine = new SoftwareVersionComparer();

        var v1  = new SoftwareVersion(src);
        var v2  = new SoftwareVersion(cmp);
        Assert.That(engine.Equals(v1, v2),   Is.EqualTo(expected), nameof(engine.Equals));
        Assert.That(engine.Equals(src, cmp), Is.EqualTo(expected), "Equals(string)");

        var vh1 = engine.GetHashCode(v1);
        var vh2 = engine.GetHashCode(v2);
        Assert.That(vh1.Equals(vh2), Is.EqualTo(expected), nameof(engine.GetHashCode));

        var sh1 = engine.GetHashCode(src);
        var sh2 = engine.GetHashCode(cmp);
        Assert.That(sh1.Equals(sh2), Is.EqualTo(expected), "GetHashCode(string)");
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Compare_Object
    ///
    /// <summary>
    /// Tests the Compare method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Compare_Object()
    {
        var src = new SoftwareVersionComparer();
        var v0  = (object)(new SoftwareVersion("1.0.0"));
        var v1  = (object)(new SoftwareVersion("1.0.1"));

        Assert.That(src.Compare(v0, v1),  Is.EqualTo(-1));
        Assert.That(src.Compare(0, 0),    Is.EqualTo(0));
        Assert.That(src.Compare(0, 1),    Is.EqualTo(-1));
        Assert.That(src.Compare(1, 0),    Is.EqualTo(1));
        Assert.That(src.Compare(null, 0), Is.EqualTo(-1));
        Assert.That(src.Compare(0, null), Is.EqualTo(1));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Compare_Throws
    ///
    /// <summary>
    /// Tests the Compare method with error cases.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Compare_Throws()
    {
        var src = new SoftwareVersionComparer();
        Assert.That(() => src.Compare(new object(), "1"), Throws.ArgumentException);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Equals_Object
    ///
    /// <summary>
    /// Tests the CompareTo method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Equals_Object()
    {
        var src = new SoftwareVersionComparer();
        var v0  = (object)(new SoftwareVersion("1.0.0"));
        var v1  = (object)(new SoftwareVersion("1.0.0"));

        Assert.That(src.Equals(src, src), Is.True);
        Assert.That(src.Equals(v0, v1),   Is.True);
        Assert.That(src.Equals(0, 0),     Is.True);
        Assert.That(src.Equals(0, 1),     Is.False);
        Assert.That(src.Equals(1, 0),     Is.False);
        Assert.That(src.Equals(null, 0),  Is.False);
        Assert.That(src.Equals(0, null),  Is.False);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetHashCode_Object
    ///
    /// <summary>
    /// Tests the CompareTo method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void GetHashCode_Object()
    {
        var src = new SoftwareVersionComparer();

        Assert.That(src.GetHashCode(0), Is.EqualTo(0));
        Assert.That(src.GetHashCode(1), Is.Not.EqualTo(0));
        Assert.That(() => src.GetHashCode(null), Throws.TypeOf<NullReferenceException>());
    }

    #endregion
}
