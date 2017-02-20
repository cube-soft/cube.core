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

namespace Cube.Tests.Events
{
    /* --------------------------------------------------------------------- */
    ///
    /// ValueEventArgsTest
    /// 
    /// <summary>
    /// ValueEventArgs のテスト用クラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    [Parallelizable]
    [TestFixture]
    class ValueEventArgsTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Create_ValueEventArgs
        ///
        /// <summary>
        /// ValueEventArgs.Create(T) のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(10)]
        [TestCase(3.1415926)]
        [TestCase("Hello, world")]
        public void Create_ValueEventArgs<T>(T value)
        {
            var args = ValueEventArgs.Create(value);
            Assert.That(args.Value, Is.EqualTo(value));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create_ValueCancelEventArgs
        ///
        /// <summary>
        /// ValueEventArgs.Create(T, bool) のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(-1, true)]
        [TestCase(1.41421356, false)]
        [TestCase("Cancel!Cancel!", true)]
        public void Create_ValueCancelEventArgs<T>(T value, bool cancel)
        {
            var args = ValueEventArgs.Create(value, cancel);
            Assert.That(args.Value, Is.EqualTo(value));
            Assert.That(args.Cancel, Is.EqualTo(cancel));
        }
    }
}
