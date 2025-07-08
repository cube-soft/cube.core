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
using System.Collections.Generic;
using Cube.Reflection.Extensions;
using Cube.Web.Extensions;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// UriTest
///
/// <summary>
/// Tests extended methods of the Uri class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class UriTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// ToUri
    ///
    /// <summary>
    /// Tests the ToUri extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("http://www.cube-soft.jp/1.html", ExpectedResult = "http://www.cube-soft.jp/1.html")]
    [TestCase("https://www.cube-soft.jp/2.html", ExpectedResult = "https://www.cube-soft.jp/2.html")]
    [TestCase("www.cube-soft.jp/3.html", ExpectedResult = "http://www.cube-soft.jp/3.html")]
    [TestCase("//www.cube-soft.jp/4.html", ExpectedResult = "http://www.cube-soft.jp/4.html")]
    [TestCase("/5.html", ExpectedResult = "http://localhost/5.html")]
    [TestCase("", ExpectedResult = "")]
    public string ToUri(string src) => src.ToUri()?.ToString() ?? string.Empty;

    /* --------------------------------------------------------------------- */
    ///
    /// With_Value
    ///
    /// <summary>
    /// Tests the With extended method with various values.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("string", "value")]
    [TestCase("int", 5)]
    [TestCase("double", 3.14)]
    public void With_Value<T>(string key, T value)
    {
        var dest = $"{Create()}?{key}={value}";
        var src  = Create().With(key, value);
        Assert.That(src.ToString(), Is.EqualTo(dest));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// With_DateTime
    ///
    /// <summary>
    /// Tests the With extended method with DateTime objects.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(2015, 3, 19, 14, 57, 57, 1426777077)]
    public void With_DateTime(int y, int m, int d, int hh, int mm, int ss, long unix)
    {
        var dest = $"{Create()}?ts={unix}";
        var src  = Create().With(new DateTime(y, m, d, hh, mm, ss, DateTimeKind.Utc));
        Assert.That(src.ToString(), Is.EqualTo(dest));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// With_Query
    ///
    /// <summary>
    /// Tests the With extended method with the specified dictionary.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void With_Query()
    {
        var dest = $"{Create()}?key1=value1&key2=value2&key3=value3";
        var src  = Create().With(new Dictionary<string, string>
        {
            { "key1", "value1" },
            { "key2", "value2" },
            { "key3", "value3" },
        });

        Assert.That(src.ToString(), Is.EqualTo(dest));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// With_MultiQuery
    ///
    /// <summary>
    /// Tests the With extended method multiple times.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void With_MultiQuery()
    {
        var dest = $"{Create()}?key1=value1&key2=value2";
        var src  = Create().With("key1", "value1").With("key2", "value2");
        Assert.That(src.ToString(), Is.EqualTo(dest));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// With_Null
    ///
    /// <summary>
    /// Tests the With extended method with a null object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void With_Null()
    {
        Assert.That(default(Uri).With("key", "value"), Is.Null);
        Assert.That(Create().With(default(Dictionary<string, string>)), Is.EqualTo(Create()));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// With_Assembly
    ///
    /// <summary>
    /// Tests the With extended method with an Assembly object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void With_Assembly()
    {
        var dest = $"{Create()}?ver=9.5.0";
        var src = Create().With(GetType().Assembly);

        Assert.That(src.ToString(), Is.EqualTo(dest));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// With_SoftwareVersion
    ///
    /// <summary>
    /// Tests the With extended method with a SoftwareVersion object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void With_SoftwareVersion()
    {
        var asm  = GetType().Assembly;
        var dest = $"{Create()}?ver=9.5.0-beta";
        var src  = Create().With(new SoftwareVersion(asm) { Suffix = "-beta" });

        Assert.That(src.ToString(), Is.EqualTo(dest));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// With_Utm
    ///
    /// <summary>
    /// Tests the With extended method with a Utm object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void With_Utm()
    {
        var dest = $"{Create()}?utm_source=cube&utm_medium=tests&utm_campaign=january&utm_term=dummy&utm_content=content";
        var src  = Create().With(new Utm
        {
            Source   = "cube",
            Medium   = "tests",
            Campaign = "january",
            Term     = "dummy",
            Content  = "content"
        });

        Assert.That(src.ToString(), Is.EqualTo(dest));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// With_Utm_Null
    ///
    /// <summary>
    /// Tests the With extended method with a null Utm object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void With_Utm_Null()
    {
        var dest = Create();
        var src  = Create().With(default(Utm));
        Assert.That(src, Is.EqualTo(dest));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// WithoutQuery
    ///
    /// <summary>
    /// Tests the WithoutQuery extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("http://www.example.com/index.html?foo&bar=bas")]
    [TestCase("http://www.example.net/")]
    [TestCase("http://www.example.net")]
    public void WithoutQuery(string url)
    {
        var src  = new Uri(url);
        var dest = new Uri($"{src.Scheme}://{src.Host}{src.AbsolutePath}");
        Assert.That(src.WithoutQuery(), Is.EqualTo(dest));
    }

    #endregion

    #region Others

    /* --------------------------------------------------------------------- */
    ///
    /// Create
    ///
    /// <summary>
    /// Creates a new instance of the Uri class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private Uri Create() => new("http://www.cube-soft.jp/");

    #endregion
}
