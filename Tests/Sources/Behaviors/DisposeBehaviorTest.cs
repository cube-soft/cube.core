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
    /// DisposeBehaviorTest
    ///
    /// <summary>
    /// Tests the DisposeBehavior class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    class DisposeBehaviorTest : ViewFixture
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// Tests the create, attach, and detach methods.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create()
        {
            var n    = 0;
            var view = Hack(new Window());
            var src  = Attach(view, new DisposeBehavior());

            view.DataContext = Disposable.Create(() => ++n);
            view.Show();
            view.Close();

            Assert.That(n, Is.EqualTo(1));
            Assert.That(view.DataContext, Is.Not.Null);

            src.Detach();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create_Null
        ///
        /// <summary>
        /// Tests the create, attach, and detach methods in the case when
        /// DataContext is null.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_Null()
        {
            var view = Hack(new Window());
            var src  = Attach(view, new DisposeBehavior());

            Assert.That(view.DataContext, Is.Null);

            view.Show();
            view.Close();
            src.Detach();
        }

        #endregion
    }
}
