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
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Cube.Xui.Behaviors;
using NUnit.Framework;

namespace Cube.Xui.Tests.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// WindowsFormsBehaviorTest
    ///
    /// <summary>
    /// Tests for the WindowsFormsBehavior class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    class WindowsFormsBehaviorTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// Executes the test to create, attach, and detach method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create()
        {
            var view = new TextBox();
            var host = new WindowsFormsHost { Child = view };
            var src  = new WindowsFormsBehavior<TextBox>();

            Assert.That(src.Source, Is.Null);
            src.Attach(host);
            Assert.That(src.Source, Is.EqualTo(view));
            Assert.That(src.Parent, Is.EqualTo(host));
            src.Detach();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create_Empty
        ///
        /// <summary>
        /// Confirms the behavior the specified WindowsFormsHost object
        /// has no children.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_Empty()
        {
            var host = new WindowsFormsHost();
            var src  = new WindowsFormsBehavior<TextBox>();

            src.Attach(host);
            Assert.That(src.Source, Is.Null);
            Assert.That(src.Parent, Is.EqualTo(host));
            src.Detach();
        }

        #endregion
    }
}
