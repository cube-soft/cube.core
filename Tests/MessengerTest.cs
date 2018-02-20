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
using GalaSoft.MvvmLight.Messaging;
using NUnit.Framework;

namespace Cube.Xui.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// MessengerTest
    ///
    /// <summary>
    /// Messenger に関連するテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class MessengerTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Send_CloseMessage
        ///
        /// <summary>
        /// CloseMessage を送信するテストを実行します。。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Send_CloseMessage()
        {
            var src = new Messenger();
            var dest = default(CloseMessage);

            src.Register<CloseMessage>(this, e => dest = e);
            src.Send<CloseMessage>();
            Assert.That(dest, Is.Not.Null);
        }

        #endregion
    }
}
