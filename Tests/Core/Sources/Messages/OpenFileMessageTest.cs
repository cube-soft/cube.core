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
namespace Cube.Tests.Messages;

using System.Linq;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// OpenFileMessage
///
/// <summary>
/// Tests the OpenFileMessage class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class OpenFileMessageTest : FileFixture
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Test
    ///
    /// <summary>
    /// Tests the constructor and confirms values of some properties.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Test()
    {
        var dest = new OpenFileMessage();

        Assert.That(dest.Text,             Is.EqualTo(nameof(OpenFileMessage)));
        Assert.That(dest.Value.Count(),    Is.EqualTo(0));
        Assert.That(dest.InitialDirectory, Is.Empty);
        Assert.That(dest.Filters.Count(),  Is.EqualTo(1));
        Assert.That(dest.CheckPathExists,  Is.True);
        Assert.That(dest.Multiselect,      Is.False);
        Assert.That(dest.Cancel,           Is.False);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Test_WithNullOrEmpty
    ///
    /// <summary>
    /// Tests the constructor and confirms values of some properties.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("")]
    [TestCase(null)]
    public void Test_WithNullOrEmpty(string src)
    {
        var dest = new OpenFileMessage(src);

        Assert.That(dest.Text,             Is.EqualTo(nameof(OpenFileMessage)));
        Assert.That(dest.Value.Count(),    Is.EqualTo(0));
        Assert.That(dest.InitialDirectory, Is.Empty);
        Assert.That(dest.Filters.Count(),  Is.EqualTo(1));
        Assert.That(dest.CheckPathExists,  Is.True);
        Assert.That(dest.Multiselect,      Is.False);
        Assert.That(dest.Cancel,           Is.False);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Test_WithFile
    ///
    /// <summary>
    /// Tests the constructor and confirms values of some properties.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("Sample.txt")]
    [TestCase("InExistent.dat")]
    public void Test_WithFile(string filename)
    {
        var src  = GetSource(filename);
        var dest = new OpenFileMessage(src);

        Assert.That(dest.Value.Count(),    Is.EqualTo(1));
        Assert.That(dest.Value.First(),    Is.EqualTo(filename));
        Assert.That(dest.InitialDirectory, Is.EqualTo(Examples));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Test_WithDirectory
    ///
    /// <summary>
    /// Tests the constructor and confirms values of some properties.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Test_WithDirectory()
    {
        var dest = new OpenFileMessage(Results);

        Assert.That(dest.Value.Count(),    Is.EqualTo(0));
        Assert.That(dest.InitialDirectory, Is.EqualTo(Results));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetFilterIndex
    ///
    /// <summary>
    /// Tests the GetFilterText and GetFilterIndex methods.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("Sample.txt",    ExpectedResult = 1)]
    [TestCase("Sample.Jpg",    ExpectedResult = 2)]
    [TestCase("Sample.tar.gz", ExpectedResult = 3)]
    [TestCase("Sample",        ExpectedResult = 0)]
    public int GetFilterIndex(string filename)
    {
        var dest = new OpenFileMessage(Get(filename))
        {
            Filters = new FileDialogFilter[]
            {
                new("Texts", ".txt"),
                new("Images", "*.png", "*.jpg", "*.jpeg", "*.bmp"),
                new("Archives", "zip", "tar.gz"),
                new("All", "*"),
            }
        };

        var s = dest.GetFilterText();
        Assert.That(s, Does.StartWith("Texts (*.txt)|*.txt;*.TXT;*.Txt|"));
        Assert.That(s, Does.EndWith("|All (*.*)|*.*"));
        return dest.GetFilterIndex();
    }

    #endregion
}
