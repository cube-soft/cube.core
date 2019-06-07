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
using Cube.Xui.Behaviors;
using NUnit.Framework;
using System.Threading;
using System.Windows;

namespace Cube.Xui.Tests.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// EventToCommandTest
    ///
    /// <summary>
    /// Tests event to command behaviors.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    class EventToCommandTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// ShownToCommand
        ///
        /// <summary>
        /// Tests the ShownToCommand class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void ShownToCommand()
        {
            var view = new Window();
            var src  = new ShownToCommand();

            src.Attach(view);
            Assert.That(src.Command, Is.Null);
            src.Detach();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ClosedToCommand
        ///
        /// <summary>
        /// Tests the ClosedToCommand class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void ClosedToCommand()
        {
            var view = new Window();
            var src  = new ClosedToCommand();

            src.Attach(view);
            Assert.That(src.Command, Is.Null);
            src.Detach();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ClosingToCommand
        ///
        /// <summary>
        /// Tests the ClosingToCommand class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void ClosingToCommand()
        {
            var view = new Window();
            var src  = new ClosingToCommand();

            src.Attach(view);
            Assert.That(src.Command, Is.Null);
            src.Detach();
        }

        #endregion
    }
}
