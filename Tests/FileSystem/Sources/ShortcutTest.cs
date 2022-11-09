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
namespace Cube.FileSystem.Tests;

using System.Collections.Generic;
using System.Reflection;
using Cube.Tests;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// ShortcutTest
///
/// <summary>
/// Tests for the Shortcut class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class ShortcutTest : FileFixture
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Create
    ///
    /// <summary>
    /// Tests the Create method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCaseSource(nameof(TestCases))]
    public bool Create(string name, string link, int index, string args)
    {
        var src  = Get(name);
        var dest = GetTargetPath(link);
        var sc   = new Shortcut
        {
            FullName     = src,
            Target       = dest,
            Arguments    = args,
            IconLocation = $"{dest},{index}",
        };

        Assert.That(sc.Exists, Is.False);
        Assert.That(sc.FullName, Does.EndWith(".lnk"));
        Assert.That(Io.GetBaseName(sc.FullName), Does.Not.EndWith(".lnk"));

        sc.Create();
        return sc.Exists;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Create_Empty
    ///
    /// <summary>
    /// Tests the Create method with the empty target path.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Create_Empty()
    {
        var sc = new Shortcut
        {
            FullName     = Get("ScEmpty"),
            Target       = string.Empty,
            Arguments    = null,
            IconLocation = string.Empty,
        };

        sc.Create();
        Assert.That(sc.IconFileName, Is.Empty);
        Assert.That(sc.IconIndex,    Is.EqualTo(0));
        Assert.That(sc.Exists,       Is.False);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Create_Null
    ///
    /// <summary>
    /// Tests the Create method with the null target path.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Create_Null()
    {
        var sc = new Shortcut
        {
            FullName     = Get("ScEmpty"),
            Target       = null,
            Arguments    = null,
            IconLocation = null,
        };

        sc.Create();
        Assert.That(sc.IconFileName, Is.Null);
        Assert.That(sc.IconIndex,    Is.EqualTo(0));
        Assert.That(sc.Exists,       Is.False);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Delete
    ///
    /// <summary>
    /// Tests the Delete method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Delete()
    {
        var src  = Get("DeleteTest");
        var dest = GetTargetPath("Cube.Core.dll");
        var sc   = new Shortcut
        {
            FullName     = src,
            Target       = dest,
            Arguments    = null,
            IconLocation = dest,
        };

        sc.Create();
        Assert.That(sc.Exists, Is.True);

        sc.Delete();
        sc.Delete(); // ignore
        Assert.That(sc.Exists, Is.False);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Resolve
    ///
    /// <summary>
    /// Tests the Resolve method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Resolve()
    {
        var link = Get("ResolveTest");
        var path = GetTargetPath("Cube.Core.dll");
        var args = "/foo bar /bas";

        new Shortcut
        {
            FullName     = link,
            Target       = path,
            Arguments    = "/foo bar /bas",
            IconLocation = path,
        }.Create();

        var dest = Shortcut.Resolve(link);
        Assert.That(dest.Target,       Is.EqualTo(path));
        Assert.That(dest.IconLocation, Is.EqualTo(path));
        Assert.That(dest.IconFileName, Is.EqualTo(path));
        Assert.That(dest.IconIndex,    Is.EqualTo(0));
        Assert.That(dest.Arguments,    Is.EqualTo(args));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Resolve_Error
    ///
    /// <summary>
    /// Tests to confirm the result with the non-existent link path.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Resolve_Error() => Assert.That(Shortcut.Resolve("dummy"), Is.Null);

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
    private static IEnumerable<TestCaseData> TestCases
    {
        get
        {
            yield return new TestCaseData("ScNormal", "Cube.Core.dll", 0, "/foo bar /bas").Returns(true);
            yield return new TestCaseData("ScNullArgs", "Cube.Core.dll", 0, null).Returns(true);
            yield return new TestCaseData("ScEmptyArgs", "Cube.Core.dll", 0, "").Returns(true);
            yield return new TestCaseData("ScWrongIconIndex", "Cube.Core.dll", 3, "/foo").Returns(true);
            yield return new TestCaseData("ScWrongLink", "dummy.exe", 0, "/foo").Returns(false);
            yield return new TestCaseData("ScWithLnk.lnk", "Cube.Core.dll", 0, "args").Returns(true);
            yield return new TestCaseData("日本語ショートカット", "Cube.Core.dll", 0, "args").Returns(true);
        }
    }

    #endregion

    #region Helper

    /* --------------------------------------------------------------------- */
    ///
    /// GetTargetPath
    ///
    /// <summary>
    /// Gets the target path.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private string GetTargetPath(string filename)
    {
        var asm = Assembly.GetExecutingAssembly().Location;
        var dir = Io.GetDirectoryName(asm);
        return Io.Combine(dir, filename);
    }

    #endregion
}
