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

using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// OpenDirectoryMessageTest
///
/// <summary>
/// Tests the OpenDirectoryMessage class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class OpenDirectoryMessageTest : FileFixture
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
        var dest = new OpenDirectoryMessage();

        Assert.That(dest.Text,      Is.EqualTo(nameof(OpenDirectoryMessage)));
        Assert.That(dest.Value,     Is.Empty);
        Assert.That(dest.NewButton, Is.True);
        Assert.That(dest.Cancel,    Is.False);
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
        var dest = new OpenDirectoryMessage(src);

        Assert.That(dest.Text,      Is.EqualTo(nameof(OpenDirectoryMessage)));
        Assert.That(dest.Value,     Is.Empty);
        Assert.That(dest.NewButton, Is.True);
        Assert.That(dest.Cancel,    Is.False);
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
        var dest = new OpenDirectoryMessage(src);

        Assert.That(dest.Value, Is.EqualTo(Examples));
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
    public void Test_WithDirectory() => Assert.That(
        new OpenDirectoryMessage(Results).Value,
        Is.EqualTo(Results)
    );

    #endregion
}
