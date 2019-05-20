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
using System.Linq;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// MessageTest
    ///
    /// <summary>
    /// Tests the message classes.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class MessageTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Create_DialogMessage
        ///
        /// <summary>
        /// Tests properties of the DialogMessage class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_DialogMessage()
        {
            var src = new DialogMessage();
            Assert.That(src.Title,    Is.Empty);
            Assert.That(src.Value,    Is.Empty);
            Assert.That(src.Callback, Is.Null);
            Assert.That(src.Icon,     Is.EqualTo(DialogIcon.Error));
            Assert.That(src.Buttons,  Is.EqualTo(DialogButtons.Ok));
            Assert.That(src.Result,   Is.EqualTo(DialogResult.Ok));

            void callback(DialogMessage e) { Assert.That(e, Is.EqualTo(src)); }
            src.Callback = callback;
            src.Callback(src);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create_OpenFileMessage
        ///
        /// <summary>
        /// Tests properties of the OpenFileMessage class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_OpenFileMessage()
        {
            var src = new OpenFileMessage();
            Assert.That(src.Title,             Is.Empty);
            Assert.That(src.Value.Count(),     Is.EqualTo(0));
            Assert.That(src.Callback,          Is.Null);
            Assert.That(src.InitialDirectory,  Is.Empty);
            Assert.That(src.Filter,            Is.EqualTo("All Files (*.*)|*.*"));
            Assert.That(src.CheckPathExists,   Is.True);
            Assert.That(src.Multiselect,       Is.False);
            Assert.That(src.Result,            Is.False);

            void callback(OpenFileMessage e) { Assert.That(e, Is.EqualTo(src)); }
            src.Callback = callback;
            src.Callback(src);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create_SaveFileMessage
        ///
        /// <summary>
        /// Tests properties of the SaveFileMessage class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_SaveFileMessage()
        {
            var src = new SaveFileMessage();
            Assert.That(src.Title,            Is.Empty);
            Assert.That(src.Value,            Is.Empty);
            Assert.That(src.Callback,         Is.Null);
            Assert.That(src.InitialDirectory, Is.Empty);
            Assert.That(src.Filter,           Is.EqualTo("All Files (*.*)|*.*"));
            Assert.That(src.CheckPathExists,  Is.False);
            Assert.That(src.OverwritePrompt,  Is.True);
            Assert.That(src.Result,           Is.False);

            void callback(SaveFileMessage e) { Assert.That(e, Is.EqualTo(src)); }
            src.Callback = callback;
            src.Callback(src);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create_OpenDirectoryMessage
        ///
        /// <summary>
        /// Tests properties of the OpenDirectoryMessage class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_OpenDirectoryMessage()
        {
            var src = new OpenDirectoryMessage();
            Assert.That(src.Title,     Is.Empty);
            Assert.That(src.Value,     Is.Empty);
            Assert.That(src.Callback,  Is.Null);
            Assert.That(src.NewButton, Is.True);
            Assert.That(src.Result,    Is.False);

            void callback(OpenDirectoryMessage e) { Assert.That(e, Is.EqualTo(src)); }
            src.Callback = callback;
            src.Callback(src);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create_CloseMessage
        ///
        /// <summary>
        /// Tests properties of the CloseMessage class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_CloseMessage()
        {
            // The class has no properties.
            var src = new CloseMessage();
            Assert.That(src, Is.Not.Null);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create_UpdateSourcesMessage
        ///
        /// <summary>
        /// Tests properties of the UpdateSourcesMessage class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_UpdateSourcesMessage()
        {
            // The class has no properties.
            var src = new UpdateSourcesMessage();
            Assert.That(src, Is.Not.Null);
        }

        #endregion
    }
}
