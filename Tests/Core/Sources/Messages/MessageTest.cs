﻿/* ------------------------------------------------------------------------- */
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
using System;
using System.Linq;
using NUnit.Framework;

namespace Cube.Tests.Messages
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
            var src = DialogMessage.From(new ArgumentException("TEST"));
            Assert.That(src.Text,    Is.EqualTo("TEST (ArgumentException)"));
            Assert.That(src.Title,   Is.Not.Null.And.Not.Empty);
            Assert.That(src.Icon,    Is.EqualTo(DialogIcon.Error));
            Assert.That(src.Buttons, Is.EqualTo(DialogButtons.Ok));
            Assert.That(src.Value,   Is.EqualTo(DialogStatus.Ok));
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
            Assert.That(new CloseMessage(),    Is.Not.Null);
            Assert.That(new ActivateMessage(), Is.Not.Null);
            Assert.That(new ApplyMessage(),    Is.Not.Null);
        }

        #endregion
    }
}