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
using NUnit.Framework;
using System;

namespace Cube.Xui.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// MockViewModel
    ///
    /// <summary>
    /// Represents the ViewModel for tests.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    class MockViewModel : IMessengerRegistrar
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Register
        ///
        /// <summary>
        /// Registers the action when the message of type T is received.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IDisposable Register<T>(object receiver, Action<T> action)
        {
            Assert.That(receiver, Is.Not.Null);
            Assert.That(action,   Is.Not.Null);
            return Disposable.Create(() => { });
        }

        #endregion
    }
}
