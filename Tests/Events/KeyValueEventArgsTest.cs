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
    /// KeyValueEventArgsTest
    /// 
    /// <summary>
    /// KeyValueEventArgs のテスト用クラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    [Parallelizable]
    [TestFixture]
    class KeyValueEventArgsTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Create_KeyValueEventArgs
        ///
        /// <summary>
        /// KeyValueEventArgs.Create(T, U) のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(1, "foo")]
        [TestCase("pi", 3.1415926)]
        [TestCase(1, true)]
        public void Create_KeyValueEventArgs<T, U>(T key, U value)
        {
            var args = KeyValueEventArgs.Create(key, value);
            Assert.That(args.Key, Is.EqualTo(key));
            Assert.That(args.Value, Is.EqualTo(value));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create_KeyValueCancelEventArgs
        ///
        /// <summary>
        /// KeyValueEventArgs.Create(T, U, bool) のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(5, "cancel", true)]
        [TestCase("No cancel", 1.7320508, false)]
        [TestCase(true, false, false)]
        public void Create_KeyValueCancelEventArgs<T, U>(T key, U value, bool cancel)
        {
            var args = KeyValueEventArgs.Create(key, value, cancel);
            Assert.That(args.Key, Is.EqualTo(key));
            Assert.That(args.Value, Is.EqualTo(value));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// KeyValueCancelEventArgs_Cancel
        ///
        /// <summary>
        /// Cancel プロパティの初期値を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(ExpectedResult = false)]
        public bool KeyValueCancelEventArgs_Cancel()
            => new KeyValueCancelEventArgs<int, int>(1, 2).Cancel;
    }
}
