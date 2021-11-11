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
using System.Windows;
using Cube.Xui.Behaviors;
using NUnit.Framework;

namespace Cube.Xui.Tests.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// FileDropBehaviorTest
    ///
    /// <summary>
    /// Tests for the FileDropBehavior class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    class FileDropBehaviorTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Properties
        ///
        /// <summary>
        /// Confirms default values of properties.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Properties()
        {
            var view = new Window();
            var src  = new FileDropToCommand<Window>();

            src.Attach(view);
            Assert.That(src.Command, Is.Null);
            src.Detach();
        }

        #endregion
    }
}
