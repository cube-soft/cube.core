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
using System;
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
        /// Match
        ///
        /// <summary>
        /// Tests the Any extended method of the DialogStatus class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Match()
        {
            Assert.That(DialogStatus.Ok.Any(DialogStatus.Ok, DialogStatus.Cancel), Is.True);
            Assert.That(DialogStatus.Ok.Any(DialogStatus.No, DialogStatus.Cancel), Is.False);
            Assert.That(DialogStatus.Empty.Any(DialogStatus.Empty), Is.True);
        }

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
            Assert.That(src.Text,    Is.Empty);
            Assert.That(src.Title,   Is.Not.Null.And.Not.Empty);
            Assert.That(src.Icon,    Is.EqualTo(DialogIcon.Error));
            Assert.That(src.Buttons, Is.EqualTo(DialogButtons.Ok));
            Assert.That(src.Value,   Is.EqualTo(DialogStatus.Ok));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create_ErrorMessage
        ///
        /// <summary>
        /// Tests the Create method of the DialogMessage class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_ErrorMessage()
        {
            var src = DialogMessage.Create(new ArgumentException("TEST"));
            Assert.That(src.Text,    Is.EqualTo("TEST (ArgumentException)"));
            Assert.That(src.Title,   Is.Not.Null.And.Not.Empty);
            Assert.That(src.Icon,    Is.EqualTo(DialogIcon.Error));
            Assert.That(src.Buttons, Is.EqualTo(DialogButtons.Ok));
            Assert.That(src.Value,   Is.EqualTo(DialogStatus.Ok));
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
            Assert.That(src.Text,            Is.Empty);
            Assert.That(src.Value.Count(),    Is.EqualTo(0));
            Assert.That(src.InitialDirectory, Is.Empty);
            Assert.That(src.Filter,           Is.EqualTo("All Files (*.*)|*.*"));
            Assert.That(src.FilterIndex,      Is.EqualTo(0));
            Assert.That(src.CheckPathExists,  Is.True);
            Assert.That(src.Multiselect,      Is.False);
            Assert.That(src.Cancel,           Is.False);
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
            Assert.That(src.Text,            Is.Empty);
            Assert.That(src.Value,            Is.Empty);
            Assert.That(src.InitialDirectory, Is.Empty);
            Assert.That(src.Filter,           Is.EqualTo("All Files (*.*)|*.*"));
            Assert.That(src.FilterIndex,      Is.EqualTo(0));
            Assert.That(src.CheckPathExists,  Is.False);
            Assert.That(src.OverwritePrompt,  Is.True);
            Assert.That(src.Cancel,           Is.False);
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
            Assert.That(src.Text,     Is.Empty);
            Assert.That(src.Value,     Is.Empty);
            Assert.That(src.NewButton, Is.True);
            Assert.That(src.Cancel,    Is.False);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create_EmptyMessages
        ///
        /// <summary>
        /// Tests to create messages that have no properties.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_EmptyMessages()
        {
            Assert.That(new CloseMessage(), Is.Not.Null);
            Assert.That(new ApplyMessage(), Is.Not.Null);
        }

        #endregion
    }
}
