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
namespace Cube.Tests.Icons;

using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using Cube.FileSystem;
using Cube.Icons;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// StockIconTest
///
/// <summary>
/// Tests the FileIcon class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class FileIconTest : FileFixture
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Get
    ///
    /// <summary>
    /// Tests of the GetImage method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("Sample.txt", IconSize.ExtraLarge, ExpectedResult = 48)]
    [TestCase("Dummy.dat",  IconSize.Small,      ExpectedResult = 16)]
    public int Get(string filename, IconSize size)
    {
        var src  = GetSource(filename);
        var dest = Get($"{Io.GetBaseName(filename)}-{size}.png");
        using (var e = FileIcon.GetImage(src, size)) e.Save(dest, ImageFormat.Png);
        using (var e = Image.FromFile(dest)) return e.Width;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Get_WithExtension
    ///
    /// <summary>
    /// Tests of the GetImage method with the specified extension.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(".pdf", IconSize.Jumbo,      ExpectedResult = 256)]
    [TestCase(".txt", IconSize.Large,      ExpectedResult =  32)]
    [TestCase(".exe", IconSize.ExtraLarge, ExpectedResult =  48)]
    [TestCase(".dll", IconSize.ExtraLarge, ExpectedResult =  48)]
    public int Get_WithExtension(string src, IconSize size)
    {
        var cvt  = CultureInfo.InvariantCulture.TextInfo;
        var dest = Get($"{cvt.ToTitleCase(src.Substring(1))}-{size}.png");
        using (var e = FileIcon.GetImage(src, size)) e.Save(dest, ImageFormat.Png);
        using (var e = Image.FromFile(dest)) return e.Width;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Get_WithAssembly
    ///
    /// <summary>
    /// Tests of the GetImage method with the current assembly.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Get_WithAssembly()
    {
        var size = IconSize.Jumbo;
        var src  = GetType().Assembly.Location;
        var dest = Get($"Assembly-{size}.png");
        using (var e = FileIcon.GetImage(src, size)) e.Save(dest, ImageFormat.Png);
        Assert.That(Io.Exists(dest));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Get_WithNull
    ///
    /// <summary>
    /// Tests of the GetImage method with null.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Get_WithNull()
    {
        var size = IconSize.ExtraLarge;
        var dest = Get($"Null-{size}.png");
        using (var e = FileIcon.GetImage(null, size)) e.Save(dest, ImageFormat.Png);
        Assert.That(Io.Exists(dest));
    }

    #endregion
}
