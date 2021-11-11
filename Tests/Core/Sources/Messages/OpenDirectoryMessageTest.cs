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
using NUnit.Framework;

namespace Cube.Tests.Messages
{
    /* --------------------------------------------------------------------- */
    ///
    /// OpenDirectoryMessageTest
    ///
    /// <summary>
    /// Tests the OpenDirectoryMessage class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class OpenDirectoryMessageTest : FileFixture
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Init
        ///
        /// <summary>
        /// Tests the constructor and confirms values of some properties.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Init()
        {
            var dest = new OpenDirectoryMessage();

            Assert.That(dest.Text,      Is.Empty);
            Assert.That(dest.Value,     Is.Empty);
            Assert.That(dest.NewButton, Is.True);
            Assert.That(dest.Cancel,    Is.False);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Init_WithNullOrEmpty
        ///
        /// <summary>
        /// Tests the constructor and confirms values of some properties.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("")]
        [TestCase(null)]
        public void Init_WithNullOrEmpty(string src)
        {
            var dest = new OpenDirectoryMessage(src);

            Assert.That(dest.Text,      Is.Empty);
            Assert.That(dest.Value,     Is.Empty);
            Assert.That(dest.NewButton, Is.True);
            Assert.That(dest.Cancel,    Is.False);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Init_WithFile
        ///
        /// <summary>
        /// Tests the constructor and confirms values of some properties.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("Sample.txt")]
        [TestCase("InExistent.dat")]
        public void Init_WithFile(string filename)
        {
            var src  = GetSource(filename);
            var dest = new OpenDirectoryMessage(src);

            Assert.That(dest.Value, Is.EqualTo(Examples));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Init_WithDirectory
        ///
        /// <summary>
        /// Tests the constructor and confirms values of some properties.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Init_WithDirectory() => Assert.That(
            new OpenDirectoryMessage(Results).Value,
            Is.EqualTo(Results)
        );

        #endregion
    }
}
