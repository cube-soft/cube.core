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
using Cube.Mixin.Commands;
using Cube.Xui.Commands;
using NUnit.Framework;
using System.Windows.Input;

namespace Cube.Xui.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// CommandTest
    ///
    /// <summary>
    /// Represents tests of ICommand iplemented classes.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class CommandTest
    {
        #region Tests

        #region BindableCommand

        /* ----------------------------------------------------------------- */
        ///
        /// Execute
        ///
        /// <summary>
        /// Tests the Execute method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Execute()
        {
            var src  = new Bindable<Person>(new Person(), Dispatcher.Vanilla);
            var dest = new DelegateCommand(
                () => src.Value.Name = "Done",
                () => src.Value.Age > 0
            ).Observe(src, nameof(src.Value));

            src.Value.Age = 20;
            dest.Execute();
            Assert.That(src.Value.Name, Is.EqualTo("Done"));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RaiseCanExecuteChanged
        ///
        /// <summary>
        /// Tests the CanExecuteChanged event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void RaiseCanExecuteChanged()
        {
            var src = new Bindable<Person>(new Person(), Dispatcher.Vanilla);
            using (var dest = new DelegateCommand(
                () => src.Value.Name = "Done",
                () => src.Value.Age > 0
            ).Observe(src, nameof(src.Value)))
            {
                Assert.That(dest.CanExecute(), Is.False);
                src.Value.Age = 10;
                Assert.That(dest.CanExecute(), Is.True);
                src.Value.Age = -1;
                Assert.That(dest.CanExecute(), Is.False);
                src.Value.Age = 20;
                Assert.That(dest.CanExecute(), Is.True);
                dest.Execute();
                Assert.That(src.Value.Name, Is.EqualTo("Done"));
            }
        }

        #endregion

        #region BindableCommand<T>

        /* ----------------------------------------------------------------- */
        ///
        /// Execute_Generic
        ///
        /// <summary>
        /// Tests the Execute method of the BindableCommand(T) class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Execute_Generic()
        {
            var src  = new Bindable<Person>(new Person(), Dispatcher.Vanilla);
            var dest = new DelegateCommand<int>(
                e => src.Value.Name = $"Done:{e}",
                e => e > 0 && src.Value.Age > 0
            ).Observe(src, nameof(src.Value));

            src.Value.Age = 20;
            dest.Execute(1);
            Assert.That(src.Value.Name, Is.EqualTo("Done:1"));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RaiseCanExecuteChanged_Generic
        ///
        /// <summary>
        /// Tests the CanExecuteChanged event of the BindableCommand(T)
        /// class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void RaiseCanExecuteChanged_Generic()
        {
            var src = new Bindable<Person>(new Person(), Dispatcher.Vanilla);
            using (var dest = new DelegateCommand<int>(
                e => src.Value.Name = $"Done:{e}",
                e => e > 0 && src.Value.Age > 0
            ).Observe(src, nameof(src.Value)))
            {
                Assert.That(dest.CanExecute(-1), Is.False);
                Assert.That(dest.CanExecute(1), Is.False);
                src.Value.Age = 10;
                Assert.That(dest.CanExecute(-2), Is.False);
                Assert.That(dest.CanExecute(2), Is.True);
                src.Value.Age = -1;
                Assert.That(dest.CanExecute(-3), Is.False);
                Assert.That(dest.CanExecute(3), Is.False);
                src.Value.Age = 20;
                Assert.That(dest.CanExecute(-4), Is.False);
                Assert.That(dest.CanExecute(4), Is.True);
                dest.Execute(4);
                Assert.That(src.Value.Name, Is.EqualTo("Done:4"));
            }
        }

        #endregion

        #region Extension

        /* ----------------------------------------------------------------- */
        ///
        /// Execute_Extended
        ///
        /// <summary>
        /// Tests the CanExecute and Execute extended methods.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Execute_Extension()
        {
            var count = 0;
            var src = new DelegateCommand(() => ++count) as ICommand;

            Assert.That(src.CanExecute(), Is.True);

            src.Execute();
            src.Execute();
            src.Execute();

            Assert.That(count, Is.EqualTo(3));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Execute_Extension_Null
        ///
        /// <summary>
        /// Tests the CanExecute and Execute extended methods with a
        /// null object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Execute_Extension_Null()
        {
            var src = default(ICommand);
            Assert.That(src.CanExecute(), Is.False);
            Assert.DoesNotThrow(() => src.Execute());
        }

        #endregion

        #endregion
    }
}
