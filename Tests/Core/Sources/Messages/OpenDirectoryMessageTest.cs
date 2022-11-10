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

using Cube.FileSystem;
using Cube.Text.Extensions;
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
    /// Test_WithFile
    ///
    /// <summary>
    /// Tests the constructor and confirms values of some properties.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("Sample.txt")]
    [TestCase("InExistent.dat")]
    [TestCase("")]
    public void Test(string filename)
    {
        var dest = new OpenDirectoryMessage();
        dest.Set(IoEx.GetOrDefault(GetSource(filename)));

        Assert.That(dest.Text,      Is.EqualTo(nameof(OpenDirectoryMessage)));
        Assert.That(dest.Value,     Is.EqualTo(Examples));
        Assert.That(dest.NewButton, Is.True, nameof(dest.NewButton));
        Assert.That(dest.Cancel,    Is.False, nameof(dest.Cancel));
    }

    #endregion
}
