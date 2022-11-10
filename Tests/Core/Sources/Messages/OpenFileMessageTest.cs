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
using Cube.FileSystem;
using Cube.Generics.Extensions;
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
    /// Test_WithFile
    ///
    /// <summary>
    /// Tests the constructor and confirms values of some properties.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("Sample.txt",     1)]
    [TestCase("InExistent.dat", 1)]
    [TestCase("",               0)]
    public void Test(string filename, int n)
    {
        var dest = new OpenFileMessage();
        dest.Set(IoEx.GetOrDefault(GetSource(filename)));

        Assert.That(dest.Text,             Is.EqualTo(nameof(OpenFileMessage)));
        Assert.That(dest.Value.Count(),    Is.EqualTo(n));
        Assert.That(dest.InitialDirectory, Is.EqualTo(Examples));
        Assert.That(dest.Filters.Count(),  Is.EqualTo(1));
        Assert.That(dest.CheckPathExists,  Is.True,  nameof(dest.CheckPathExists));
        Assert.That(dest.Multiselect,      Is.False, nameof(dest.Multiselect));
        Assert.That(dest.Cancel,           Is.False, nameof(dest.Cancel));
        Assert.That(dest.GetFilterText(),  Is.EqualTo("All Files (*.*)|*.*"));
        Assert.That(dest.GetFilterIndex(), Is.EqualTo(0));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// TestFilter
    ///
    /// <summary>
    /// Tests the GetFilterText and GetFilterIndex methods.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("Sample.txt",    1)]
    [TestCase("Sample.Jpg",    2)]
    [TestCase("Sample.tar.gz", 3)]
    [TestCase("Sample",        0)]
    public void TestFilter(string filename, int index)
    {
        var dest = new OpenFileMessage()
        {
            Value   = new[] { Get(filename) },
            Filters = new FileDialogFilter[]
            {
                new("Texts", ".txt"),
                new("Images", "*.png", "*.jpg", "*.jpeg", "*.bmp"),
                new("Archives", "zip", "tar.gz"),
                new("All", "*"),
            }
        };

        Assert.That(dest.GetFilterText(),  Does.StartWith("Texts (*.txt)|*.txt;*.TXT;*.Txt|"));
        Assert.That(dest.GetFilterText(),  Does.EndWith("|All (*.*)|*.*"));
        Assert.That(dest.GetFilterIndex(), Is.EqualTo(index));
    }

    #endregion
}
