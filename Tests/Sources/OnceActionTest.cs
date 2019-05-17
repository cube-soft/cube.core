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
using NUnit.Framework;
using System;
using System.Linq;
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
        #region Tests

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
            var dest  = 0;
            var once  = new OnceAction(() => dest++);
            var tasks = 5.Make(i => Task.Run(() => once.Invoke()));

            Task.WaitAll(tasks.ToArray());
            Assert.That(once.Invoked, Is.True);
            Assert.That(dest, Is.EqualTo(1));
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
        public void Invoke_Twice()
        {
            var dest  = 0;
            var once  = new OnceAction(() => dest++) { IgnoreTwice = false };
            var tasks = 5.Make(i => Task.Run(() => once.Invoke()));

            Assert.That(() => Task.WaitAll(tasks.ToArray()),
                Throws.TypeOf<AggregateException>().And.InnerException
                      .TypeOf<TwiceException>());
            Assert.That(dest, Is.EqualTo(1));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// OnceAction(T) のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Invoke_Generics()
        {
            var src   = "once";
            var dest  = "";
            var once  = new OnceAction<string>(s => dest += s);
            var tasks = 5.Make(i => Task.Run(() => once.Invoke(src)));

            Task.WaitAll(tasks.ToArray());
            Assert.That(once.Invoked, Is.True);
            Assert.That(dest, Is.EqualTo(src));
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
        public void Invoke_Generics_Twice()
        {
            var src   = "twice";
            var dest  = "";
            var once  = new OnceAction<string>(s => dest += s) { IgnoreTwice = false };
            var tasks = 5.Make(i => Task.Run(() => once.Invoke(src)));

            Assert.That(() => Task.WaitAll(tasks.ToArray()),
                Throws.TypeOf<AggregateException>().And.InnerException
                      .TypeOf<TwiceException>());
            Assert.That(dest, Is.EqualTo(src));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create_Null_Throws
        ///
        /// <summary>
        /// Confirms exceptions when creating a new instance of the
        /// OnceAction class with a null object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_Null_Throws()
        {
            Assert.That(() => new OnceAction(null),      Throws.ArgumentNullException);
            Assert.That(() => new OnceAction<int>(null), Throws.ArgumentNullException);
        }

        #endregion
    }
}
