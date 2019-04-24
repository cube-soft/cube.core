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
using Cube.Xui.Mixin;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cube.Xui.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// BindableCollectionTest
    ///
    /// <summary>
    /// BindableCollection のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class BindableCollectionTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Create_Default
        ///
        /// <summary>
        /// 既定のコンストラクタで初期化した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_Default() => Assert.That(
            new BindableCollection<int>().Count,
            Is.EqualTo(0)
        );

        /* ----------------------------------------------------------------- */
        ///
        /// Create_EmptyCollection
        ///
        /// <summary>
        /// 空のコレクションに対して ToBindable を実行した時の挙動を
        /// 確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_EmptyCollection() => Assert.That(
            ((IEnumerable<Person>)new Person[0]).ToBindable().Count,
            Is.EqualTo(0)
        );

        /* ----------------------------------------------------------------- */
        ///
        /// Create_NullCollection
        ///
        /// <summary>
        /// null に対して ToBindable を実行した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_NullCollection() => Assert.That(
            default(IEnumerable<Person>).ToBindable().Count,
            Is.EqualTo(0)
        );

        /* ----------------------------------------------------------------- */
        ///
        /// Add_Async
        ///
        /// <summary>
        /// 非同期で要素を追加した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Add_Async()
        {
            using (var src = new BindableCollection<Person> { Context = new SynchronizationContext() })
            {
                var count = 0;
                src.CollectionChanged += (s, e) => ++count;

                var tasks = new List<Task>();
                foreach (var item in Create()) tasks.Add(Task.Run(() => src.Add(item)));
                Task.WaitAll(tasks.ToArray());

                Assert.That(src.Count, Is.EqualTo(4));
                Assert.That(count,     Is.EqualTo(4));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Add_Async
        ///
        /// <summary>
        /// 非同期で要素を追加した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Remove_Async()
        {
            using (var src = Create().ToBindable(new SynchronizationContext()))
            {
                var count = 0;
                src.CollectionChanged += (s, e) => ++count;

                var tasks = new List<Task>();
                4.Times(() => tasks.Add(Task.Run(() => src.RemoveAt(0))));
                Task.WaitAll(tasks.ToArray());

                Assert.That(src.Count, Is.EqualTo(0));
                Assert.That(count,     Is.EqualTo(4));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RaiseCollectionChanged
        ///
        /// <summary>
        /// CollectionChanged イベントの挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(true,  ExpectedResult = 4)]
        [TestCase(false, ExpectedResult = 3)]
        public int RaiseCollectionChanged(bool redirect)
        {
            var ctx = SynchronizationContext.Current;
            using (var src = Create().ToBindable(ctx))
            {
                var count = 0;

                src.IsRedirected = redirect;
                src.CollectionChanged += (s, e) => ++count;
                src.Add(new Person { Name = "Ken", Age = 20 });
                src.Move(0, 2);
                src.RemoveAt(1);
                src[0].Name = "Magic";

                return count;
            }
        }

        #endregion

        #region Helpers methods

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// テストデータを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private IEnumerable<Person> Create() => new[]
        {
            new Person { Name = "Alice", Age = 13 },
            new Person { Name = "Bob",   Age = 15 },
            new Person { Name = "Mike",  Age = 45 },
            new Person { Name = "Ada",   Age = 40 },
        };

        #endregion
    }
}
