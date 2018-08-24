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
using System.Threading.Tasks;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// OnceActionTest
    ///
    /// <summary>
    /// OnceAction のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class OnceActionTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// OnceAction のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Invoke()
        {
            var value = 0;
            var once  = new OnceAction(() => value++);
            var tasks = new[]
            {
                Task.Run(() => once.Invoke()),
                Task.Run(() => once.Invoke()),
                Task.Run(() => once.Invoke()),
            };

            Task.WaitAll(tasks);
            Assert.That(once.Invoked, Is.True);
            Assert.That(value, Is.EqualTo(1));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke_Twice
        ///
        /// <summary>
        /// 複数回実行した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Invoke_Twice() => Assert.That(() =>
        {
            var dummy = 0;
            var once  = new OnceAction(() => dummy++) { IgnoreTwice = false };
            var tasks = new[]
            {
                Task.Run(() => once.Invoke()),
                Task.Run(() => once.Invoke()),
                Task.Run(() => once.Invoke()),
            };

            Task.WaitAll(tasks);
        },
        Throws.TypeOf<AggregateException>().And.InnerException.TypeOf<TwiceException>());

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// OnceAction(T) のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("once")]
        public void Invoke(string obj)
        {
            var value = "";
            var once  = new OnceAction<string>(s => value += s);
            var tasks = new[]
            {
                Task.Run(() => once.Invoke(obj)),
                Task.Run(() => once.Invoke(obj)),
                Task.Run(() => once.Invoke(obj)),
            };

            Task.WaitAll(tasks);
            Assert.That(once.Invoked, Is.True);
            Assert.That(value, Is.EqualTo(obj));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke_Twice
        ///
        /// <summary>
        /// 複数回実行した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("twice")]
        public void Invoke_Twice(string obj) => Assert.That(() =>
        {
            var dummy = "";
            var once = new OnceAction<string>(s => dummy += s) { IgnoreTwice = false };
            var tasks = new[]
            {
                Task.Run(() => once.Invoke(obj)),
                Task.Run(() => once.Invoke(obj)),
                Task.Run(() => once.Invoke(obj)),
            };

            Task.WaitAll(tasks);
        },
        Throws.TypeOf<AggregateException>().And.InnerException.TypeOf<TwiceException>());
    }
}
