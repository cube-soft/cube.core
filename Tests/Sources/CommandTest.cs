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
using Cube.Xui.Mixin;
using GalaSoft.MvvmLight.Command;
using NUnit.Framework;

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

        /* ----------------------------------------------------------------- */
        ///
        /// Execute_Alias
        ///
        /// <summary>
        /// Executes the test of CanExecute and Execute extended methods.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Execute_Alias()
        {
            var count = 0;
            var src   = new RelayCommand(() => ++count);

            Assert.That(src.CanExecute(), Is.True);

            src.Execute();
            src.Execute();
            src.Execute();

            Assert.That(count, Is.EqualTo(3));
        }

        #endregion
    }
}
