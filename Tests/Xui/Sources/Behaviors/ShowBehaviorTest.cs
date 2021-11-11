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
using Cube.Xui.Behaviors;
using NUnit.Framework;

namespace Cube.Xui.Tests.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// ShowBehaviorTest
    ///
    /// <summary>
    /// Tests the ShowBehavior class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    class ShowBehaviorTest : ViewFixture
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Test
        ///
        /// <summary>
        /// Tests the create, attach, and detach methods.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Test()
        {
            var view = Hack(new MockWindow());
            var src  = Attach(view, new ShowBehavior<MockWindow, MockViewModel>());
            src.Detach();
        }

        #endregion
    }
}
