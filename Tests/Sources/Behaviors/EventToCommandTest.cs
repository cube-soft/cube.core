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
using Cube.Mixin.Iteration;
using Cube.Xui.Behaviors;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Interactivity;

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
        /// Show
        ///
        /// <summary>
        /// Tests the ShownToCommand class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Show()
        {
            var count = 0;
            var view  = new MockWindow();
            var src   = Attach(view, new ShownToCommand
            {
                Command = new DelegateCommand(() =>
                {
                    ++count;
                    view.Close();
                })
            });

            view.ShowDialog();
            src.Detach();
            Assert.That(count, Is.EqualTo(1));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Close
        ///
        /// <summary>
        /// Tests the ClosingToCommand and ClosedToCommand classes.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Close()
        {
            var closing = 0;
            var closed  = 0;

            var view = new MockWindow();
            var src  = new List<CommandBehavior<Window>>
            {
                Attach(view, new ClosedToCommand  { Command = new DelegateCommand(() => ++closed) }),
                Attach(view, new ClosingToCommand { Command = new DelegateCommand<CancelEventArgs>(e => e.Cancel = ++closing % 2 == 1) })
            };

            view.Show();
            2.Times(i => view.Close());
            foreach (var obj in src) obj.Detach();

            Assert.That(closing, Is.EqualTo(2));
            Assert.That(closed,  Is.EqualTo(1));
        }

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// Attach
        ///
        /// <summary>
        /// Attaches the specified view and behavior object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private T Attach<T>(Window view, T src) where T : Behavior<Window>
        {
            src.Attach(view);
            return src;
        }

        #endregion
    }
}
