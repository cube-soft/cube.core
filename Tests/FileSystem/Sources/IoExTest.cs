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

using System;
using System.Reflection;
using Cube.Text.Extensions;
using Cube.Tests;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// IoExTest
///
/// <summary>
/// Tests the IoEx class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class IoExTest : FileFixture
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Load
    ///
    /// <summary>
    /// Executes the test for loading from a file.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Load() => Assert.That(
        IoEx.Load(GetSource("Sample.txt"), e => e.Length),
        Is.EqualTo(13L)
    );

    /* --------------------------------------------------------------------- */
    ///
    /// Save
    ///
    /// <summary>
    /// Executes the test for saving to a file.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Save()
    {
        var dest = Get(nameof(Save));
        IoEx.Save(dest, e => e.WriteByte((byte)'a'));
        Assert.That(new Entity(dest).Length, Is.EqualTo(1));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Save_Throws
    ///
    /// <summary>
    /// Confirms the behavior when saving to a file being handled by
    /// another process.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Save_Throws()
    {
        var src = Assembly.GetExecutingAssembly().Location;
        Assert.That(() => IoEx.Save(src, e => e.WriteByte((byte)'a')),
            Throws.TypeOf<System.IO.IOException>());
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Touch
    ///
    /// <summary>
    /// Tests the Touch extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Touch()
    {
        var src = Get($"{nameof(Touch)}.txt");
        Assert.That(Io.Exists(src), Is.False);

        IoEx.Touch(src);
        Assert.That(Io.Exists(src), Is.True);

        var cmp = new Entity(src).LastWriteTime;
        System.Threading.Thread.Sleep(1000);
        IoEx.Touch(src);
        Assert.That(new Entity(src).LastWriteTime, Is.GreaterThan(cmp));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetFileName
    ///
    /// <summary>
    /// Tests the GetFileName, GetBaseName, GetExtension, and
    /// GetDirectoryName methods.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(@"relative\to\file.txt",    @"relative\to", "file",     ".txt")]
    [TestCase(@"z:\foo\bar.tmp",          @"z:\foo",      "bar",      ".tmp")]
    [TestCase(@"C:\Windows\explorer.exe", @"C:\Windows",  "explorer", ".exe")]
    public void GetFileName(string src, string dir, string name, string ext)
    {
        Assert.That(Io.GetFileName(src),      Is.EqualTo($"{name}{ext}"));
        Assert.That(Io.GetBaseName(src),      Is.EqualTo(name));
        Assert.That(Io.GetExtension(src),     Is.EqualTo(ext));
        Assert.That(Io.GetDirectoryName(src), Is.EqualTo(dir));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Rename
    ///
    /// <summary>
    /// Tests the Rename extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(@"c:\foo\Sample.txt",   @"Rename.dat",       ExpectedResult = @"c:\foo\Rename.dat")]
    [TestCase(@"c:\dir\Remove.txt",   @"",                 ExpectedResult = @"c:\dir")]
    [TestCase(@"c:\path\to\foo.txt",  @"d:\dummy\bar.txt", ExpectedResult = @"c:\path\to\bar.txt")]
    [TestCase(@"relative\to\foo.txt", @"dummy\bas.txt",    ExpectedResult = @"relative\to\bas.txt")]
    public string Rename(string src, string filename) => IoEx.Rename(src, filename);

    /* --------------------------------------------------------------------- */
    ///
    /// RenameExtension
    ///
    /// <summary>
    /// Tests the RenameExtension extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("Sample.txt", ".new", ExpectedResult = "Sample.new")]
    [TestCase("Sample.txt", "",     ExpectedResult = "Sample")]
    [TestCase("Sample",     ".txt", ExpectedResult = "Sample.txt")]
    [TestCase("Sample.txt", "dat",  ExpectedResult = "Sample.dat")]
    public string RenameExtension(string src, string extension) => IoEx.RenameExtension(src, extension);

    /* --------------------------------------------------------------------- */
    ///
    /// GetTypeName
    ///
    /// <summary>
    /// Executes the test for getting the name of file-type.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("Sample.txt",     ExpectedResult = true)]
    [TestCase("NotFound.dummy", ExpectedResult = true)]
    public bool GetTypeName(string filename) =>
        Shell.GetTypeName(GetSource(filename)).HasValue();

    /* --------------------------------------------------------------------- */
    ///
    /// GetTypeName_Null
    ///
    /// <summary>
    /// Confirms the behavior when null is specified.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void GetTypeName_Null()
    {
        Assert.That(Shell.GetTypeName(string.Empty), Is.Empty);
        Assert.That(Shell.GetTypeName(default),      Is.Empty);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetUniqueName
    ///
    /// <summary>
    /// Executes the test for getting a unique name.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void GetUniqueName()
    {
        var src = Get($"UniqueTest.txt");
        var u1 = IoEx.GetUniqueName(src);
        Assert.That(u1, Is.EqualTo(src));

        Io.Copy(GetSource("Sample.txt"), u1, true);
        var u2 = IoEx.GetUniqueName(src);
        Assert.That(u2, Is.EqualTo(Get($"UniqueTest(1).txt")));

        Io.Copy(GetSource("Sample.txt"), u2, true);
        var u3 = IoEx.GetUniqueName(src);
        Assert.That(u3, Is.EqualTo(Get($"UniqueTest(2).txt")));

        Io.Copy(GetSource("Sample.txt"), u3, true);
        var u4 = IoEx.GetUniqueName(u3); // Not src
        Assert.That(u4, Is.EqualTo(Get($"UniqueTest(2)(1).txt")));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetEntitySource
    ///
    /// <summary>
    /// Tests the GetEntitySource method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void GetEntitySource()
    {
        var src  = GetSource("Sample.txt");
        var dest = new Entity(IoEx.GetEntitySource(src));
        var cmp  = new DateTime(2017, 6, 5);

        Assert.That(dest.RawName,        Is.EqualTo(GetSource("Sample.txt")));
        Assert.That(dest.FullName,       Is.EqualTo(dest.RawName));
        Assert.That(dest.Name,           Is.EqualTo("Sample.txt"));
        Assert.That(dest.BaseName,       Is.EqualTo("Sample"));
        Assert.That(dest.Extension,      Is.EqualTo(".txt"));
        Assert.That(dest.DirectoryName,  Is.EqualTo(Examples));
        Assert.That(dest.CreationTime,   Is.GreaterThan(cmp));
        Assert.That(dest.LastWriteTime,  Is.GreaterThan(cmp));
        Assert.That(dest.LastAccessTime, Is.GreaterThan(cmp));
    }

    #endregion
}
