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
using System.Windows.Controls;

namespace Cube.Xui.Tests.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// CheckedToCommandTest
    ///
    /// <summary>
    /// Tests the CheckedToCommand class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    class CheckedToCommandTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Properties_Checked
        ///
        /// <summary>
        /// Confirms default values of properties.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Properties_Checked()
        {
            var view = new CheckBox();
            var src  = new CheckedToCommand();

            src.Attach(view);
            Assert.That(src.Command, Is.Null);
            src.Detach();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Properties_Unchecked
        ///
        /// <summary>
        /// Confirms default values of properties.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Properties_Unchecked()
        {
            var view = new CheckBox();
            var src  = new UncheckedToCommand();

            src.Attach(view);
            Assert.That(src.Command, Is.Null);
            src.Detach();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Properties_NonNullable
        ///
        /// <summary>
        /// Confirms default values of properties.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Properties_NonNullable()
        {
            var view = new RadioButton();
            var src  = new CheckedToCommand<int>();

            src.Attach(view);
            Assert.That(src.Command, Is.Null);
            Assert.That(src.CommandParameter, Is.EqualTo(0));
            src.Detach();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Properties_Nullable
        ///
        /// <summary>
        /// Confirms default values of properties.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Properties_Nullable()
        {
            var view = new RadioButton();
            var src  = new UncheckedToCommand<string>();

            src.Attach(view);
            Assert.That(src.Command, Is.Null);
            Assert.That(src.CommandParameter, Is.Null);
            src.Detach();
        }

        #endregion
    }
}
