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

using System.Linq;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// SafePathTest
///
/// <summary>
/// Tests the SafePath class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
internal class SafePathTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Escape
    ///
    /// <summary>
    /// Tests the escaping process.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(@"C:\windows\dir\file.txt",       '_', 4, ExpectedResult = @"C:\windows\dir\file.txt")]
    [TestCase(@"C:\windows\dir\file*?<>|.txt",  '_', 4, ExpectedResult = @"C:\windows\dir\file_____.txt")]
    [TestCase(@"C:\windows\dir\file:.txt",      '+', 4, ExpectedResult = @"C:\windows\dir\file+.txt")]
    [TestCase(@"C:\windows\dir:\file.txt",      '+', 4, ExpectedResult = @"C:\windows\dir+\file.txt")]
    [TestCase(@"C:\windows\dir\\file.txt.",     '+', 4, ExpectedResult = @"C:\windows\dir\file.txt")]
    [TestCase(@"C:\windows\dir\file.txt.",      '+', 4, ExpectedResult = @"C:\windows\dir\file.txt")]
    [TestCase(@"C:\windows\dir\file.txt. ... ", '+', 4, ExpectedResult = @"C:\windows\dir\file.txt")]
    [TestCase(@"C:\CON\PRN\AUX.txt",            '_', 4, ExpectedResult = @"C:\_CON\_PRN\_AUX.txt")]
    [TestCase(@"C:\COM0\com1\Com2.txt",         '_', 4, ExpectedResult = @"C:\_COM0\_com1\_Com2.txt")]
    [TestCase(@"C:\LPT1\LPT10\LPT5.txt",        '_', 4, ExpectedResult = @"C:\_LPT1\LPT10\_LPT5.txt")]
    [TestCase(@"C:\LPT2\:LPT3:\LPT4.txt",       '_', 4, ExpectedResult = @"C:\_LPT2\_LPT3_\_LPT4.txt")]
    [TestCase(@"/unix/foo/bar.txt",             '_', 3, ExpectedResult = @"unix\foo\bar.txt")]
    [TestCase(@"",                              '_', 0, ExpectedResult = @"")]
    [TestCase(null,                             '_', 0, ExpectedResult = @"")]
    public string Escape(string src, char replaced, int count)
    {
        var dest = new SafePath(src)
        {
            AllowDriveLetter      = true,
            AllowParentDirectory  = false,
            AllowCurrentDirectory = false,
            AllowInactivation     = false,
            AllowUnc              = false,
            EscapeChar            = replaced,
        };

        Assert.That(dest.Parts.Count(), Is.EqualTo(count));
        return dest.Value;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Escape_DriveLetter
    ///
    /// <summary>
    /// Confirms the results according to the drive letter settings.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(@"C:\windows\dir\allow.txt",    true,  ExpectedResult = @"C:\windows\dir\allow.txt")]
    [TestCase(@"C:\C:\windows\dir\allow.txt", true,  ExpectedResult = @"C:\C_\windows\dir\allow.txt")]
    [TestCase(@"C:\windows\dir\deny.txt",     false, ExpectedResult = @"C_\windows\dir\deny.txt")]
    [TestCase(@"C:\C:\windows\dir\deny.txt",  false, ExpectedResult = @"C_\C_\windows\dir\deny.txt")]
    public string Escape_DriveLetter(string src, bool drive) => new SafePath(src)
    {
        AllowDriveLetter = drive
    }.Value;

    /* --------------------------------------------------------------------- */
    ///
    /// Escape_CurrentDirectory
    ///
    /// <summary>
    /// Confirms the results according to the "." letter settings.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(@"C:\windows\dir\.\allow.txt", true, ExpectedResult = @"C:\windows\dir\.\allow.txt")]
    [TestCase(@"C:\windows\dir\.\deny.txt", false, ExpectedResult = @"C:\windows\dir\deny.txt")]
    public string Escape_CurrentDirectory(string src, bool allow) => new SafePath(src)
    {
        AllowInactivation     = false,
        AllowCurrentDirectory = allow,
    }.Value;

    /* --------------------------------------------------------------------- */
    ///
    /// Escape_ParentDirectory
    ///
    /// <summary>
    /// Confirms the results according to the ".." letter settings.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(@"C:\windows\dir\..\allow.txt", true, ExpectedResult = @"C:\windows\dir\..\allow.txt")]
    [TestCase(@"C:\windows\dir\..\deny.txt", false, ExpectedResult = @"C:\windows\dir\deny.txt")]
    public string Escape_ParentDirectory(string src, bool allow) => new SafePath(src)
    {
        AllowInactivation    = false,
        AllowParentDirectory = allow,
    }.Value;

    /* --------------------------------------------------------------------- */
    ///
    /// Escape_Inactivation
    ///
    /// <summary>
    /// Confirms the results according to the inactivation settings.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(@"\\?\C:\windows\dir\deny.txt",  false, ExpectedResult = @"C:\windows\dir\deny.txt")]
    [TestCase(@"\\?\C:\windows\dir\allow.txt",  true, ExpectedResult = @"\\?\C:\windows\dir\allow.txt")]
    [TestCase(@"\\?\C:\windows\.\..\allow.txt", true, ExpectedResult = @"\\?\C:\windows\allow.txt")]
    public string Escape_Inactivation(string src, bool allow) => new SafePath(src)
    {
        AllowInactivation     = allow,
        AllowDriveLetter      = true,
        AllowCurrentDirectory = true,
        AllowParentDirectory  = true,
        AllowUnc              = true,
    }.Value;

    /* --------------------------------------------------------------------- */
    ///
    /// Escape_Unc
    ///
    /// <summary>
    /// Confirms the results according to the UNC path settings.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(@"\\domain\dir\allow.txt", true, false, ExpectedResult = @"\\domain\dir\allow.txt")]
    [TestCase(@"\\domain\dir\allow.txt", true,  true, ExpectedResult = @"domain\dir\allow.txt")]
    [TestCase(@"\\domain\dir\deny.txt", false, false, ExpectedResult = @"domain\dir\deny.txt")]
    public string Escape_Unc(string src, bool unc, bool inactivation) => new SafePath(src)
    {
        AllowInactivation = inactivation,
        AllowUnc          = unc
    }.Value;

    #endregion
}
