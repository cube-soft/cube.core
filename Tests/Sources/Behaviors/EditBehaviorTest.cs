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
using System.Windows.Controls;

namespace Cube.Xui.Tests.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// EditBehaviorTest
    ///
    /// <summary>
    /// Tests for the EditBehavior class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    class EditBehaviorTest
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
        [TestCase(true)]
        [TestCase(false)]
        public void Create(bool init)
        {
            var view = new TextBox();
            var src  = new EditBehavior { Editing = init };

            src.Attach(view);
            Assert.That(src.Editing,     Is.EqualTo(init));
            Assert.That(view.Visibility, Is.EqualTo(Convert(src.Editing)));

            src.Editing = true;
            Assert.That(view.Visibility, Is.EqualTo(Convert(src.Editing)));

            src.Editing = false;
            Assert.That(view.Visibility, Is.EqualTo(Convert(src.Editing)));
            src.Detach();
        }

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// Convert
        ///
        /// <summary>
        /// Converts the specified value to the corresponding Visibility.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Visibility Convert(bool value) => value ? Visibility.Visible : Visibility.Collapsed;

        #endregion
    }
}
