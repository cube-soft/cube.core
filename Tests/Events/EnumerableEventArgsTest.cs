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
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Cube.Tests.Events
{
    /* --------------------------------------------------------------------- */
    ///
    /// EnumerableEventArgsTest
    /// 
    /// <summary>
    /// EnumerableEventArgs のテスト用クラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class EnumerableEventArgsTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Create_Array
        ///
        /// <summary>
        /// 配列で EnumerableEventArgs(T) を初期化するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_Array()
        {
            var args = EnumerableEventArgs.Create(new[]
            {
                3, 1, 4, 1, 5, 9, 2, 6,
            });

            Assert.That(args.Value.Count(), Is.EqualTo(8));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create_List
        ///
        /// <summary>
        /// List(T) で EnumerableCancelEventArgs(T) を初期化するテストを
        /// 実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_List()
        {
            var args = EnumerableEventArgs.Create(new List<int>
            {
                3, 1, 4, 1, 5, 9,
            }, true);

            Assert.That(args.Value.Count(), Is.EqualTo(6));
            Assert.That(args.Cancel, Is.True);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create_Cancel
        ///
        /// <summary>
        /// EnumerableCancelEventArgs(T) を初期化するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_Cancel()
        {
            var args = new EnumerableCancelEventArgs<string>(new List<string>
            {
                "Hello", "world", "This", "is", "a", "test", "program",
            });

            Assert.That(args.Value.Count(), Is.EqualTo(7));
            Assert.That(args.Cancel, Is.False);
        }
    }
}
