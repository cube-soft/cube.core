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
using System.Linq;
using NUnit.Framework;

namespace Cube.Tests.Messages
{
    /* --------------------------------------------------------------------- */
    ///
    /// SaveFileMessageTest
    ///
    /// <summary>
    /// Tests the SaveFileMessage class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class SaveFileMessageTest : FileFixture
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
            var dest = new SaveFileMessage();

            Assert.That(dest.Text,             Is.Empty);
            Assert.That(dest.Value,            Is.Empty);
            Assert.That(dest.InitialDirectory, Is.Empty);
            Assert.That(dest.Filters.Count(),  Is.EqualTo(1));
            Assert.That(dest.FilterIndex,      Is.EqualTo(0));
            Assert.That(dest.CheckPathExists,  Is.False);
            Assert.That(dest.OverwritePrompt,  Is.True);
            Assert.That(dest.Cancel,           Is.False);
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
            var dest = new SaveFileMessage(src);

            Assert.That(dest.Text,             Is.Empty);
            Assert.That(dest.Value,            Is.Empty);
            Assert.That(dest.InitialDirectory, Is.Empty);
            Assert.That(dest.Filters.Count(),  Is.EqualTo(1));
            Assert.That(dest.FilterIndex,      Is.EqualTo(0));
            Assert.That(dest.CheckPathExists,  Is.False);
            Assert.That(dest.OverwritePrompt,  Is.True);
            Assert.That(dest.Cancel,           Is.False);
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
            var dest = new SaveFileMessage(src);

            Assert.That(dest.Value, Is.EqualTo(filename));
            Assert.That(dest.InitialDirectory, Is.EqualTo(Examples));
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
        public void Init_WithDirectory()
        {
            var dest = new SaveFileMessage(Results);

            Assert.That(dest.Value, Is.Empty);
            Assert.That(dest.InitialDirectory, Is.EqualTo(Results));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// FilterIndex
        ///
        /// <summary>
        /// Tests the Filters and FilterIndex properties.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("Sample.txt", ExpectedResult = 1)]
        [TestCase("Sample.Jpg", ExpectedResult = 2)]
        [TestCase("Sample", ExpectedResult = 0)]
        public int FilterIndex(string filename)
        {
            var dest =new SaveFileMessage(Get(filename))
            {
                Filters = new FileDialogFilter[]
                {
                    new("Texts", ".txt"),
                    new("Images", "*.png", "*.jpg", "*.jpeg", "*.bmp"),
                    new("Archives", "zip", "tar.gz"),
                    new("All files", "*"),
                }
            };

            dest.Update();
            return dest.FilterIndex;
        }

        #endregion
    }
}
