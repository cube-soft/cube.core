/* ------------------------------------------------------------------------- */
///
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///  http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
///
/* ------------------------------------------------------------------------- */
using NUnit.Framework;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// MessengerTest
    /// 
    /// <summary>
    /// Messenger クラスのテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    //[Parallelizable]
    [TestFixture]
    class MessengerTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Send
        /// 
        /// <summary>
        /// サーバにメッセージを送信するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("Hello, world!")]
        public void Send(string message)
        {
            var id = nameof(MessengerTest);
            var result = string.Empty;

            using (var server = new Cube.Processes.MessengerServer<string>(id))
            {
                server.Subscribe(x => result = x);
                using (var client = new Cube.Processes.MessengerClient<string>(id))
                {
                    client.Send(message);
                }
            }

            Assert.That(result, Is.EqualTo(message));
        }

        #endregion
    }
}
