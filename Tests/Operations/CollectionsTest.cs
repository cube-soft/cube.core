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
using Cube.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// CollectionsTest
    ///
    /// <summary>
    /// CollectionOperator のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class CollectionsTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// LastIndex
        ///
        /// <summary>
        /// 最後のインデックスを取得するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(10, ExpectedResult = 9)]
        [TestCase( 1, ExpectedResult = 0)]
        [TestCase( 0, ExpectedResult = 0)]
        public int LastIndex(int count) => Create(count).LastIndex();

        /* ----------------------------------------------------------------- */
        ///
        /// LastIndex_Null
        ///
        /// <summary>
        /// 最後のインデックスを取得するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(ExpectedResult = 0)]
        public int LastIndex_Null()
        {
            IList<int> collection = null;
            return collection.LastIndex();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Clamp_Null
        ///
        /// <summary>
        /// 指定されたインデックスを [0, IList(T).Count) の範囲に丸める
        /// テストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(10,  5, ExpectedResult = 5)]
        [TestCase(10, 20, ExpectedResult = 9)]
        [TestCase(10, -1, ExpectedResult = 0)]
        [TestCase( 0, 10, ExpectedResult = 0)]
        [TestCase( 0, -1, ExpectedResult = 0)]
        public int Clamp(int count, int index) => Create(count).Clamp(index);

        /* ----------------------------------------------------------------- */
        ///
        /// Clamp_Null
        ///
        /// <summary>
        /// 指定されたインデックスを [0, IList(T).Count) の範囲に丸める
        /// テストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(ExpectedResult = 0)]
        public int Clamp_Null()
        {
            IList<int> collection = null;
            return collection.Clamp(100);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetOrDefault
        ///
        /// <summary>
        /// GetOrDefault の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetOrDefault()
        {
            var sum = 0;
            var src = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach (var i in src.GetOrDefault()) sum += i;
            Assert.That(sum, Is.EqualTo(55));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetOrDefault_Null
        ///
        /// <summary>
        /// null を指定した場合の GetOrDefault の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetOrDefault_Null()
        {
            var sum = 0;
            var src = default(List<int>);
            foreach (var i in src.GetOrDefault()) sum += i;
            Assert.That(sum, Is.EqualTo(0));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToObservable
        ///
        /// <summary>
        /// IList(int) を ObservableCollection(int) に変換するテストを
        /// 実行しますます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(100)]
        public void ToObservable(int count)
        {
            var src = Create(count);
            Assert.That(src.ToObservable(), Is.EquivalentTo(src));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Flatten
        ///
        /// <summary>
        /// ツリー構造を平坦化するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Flatten()
        {
            var src = new[]
            {
                new Tree { Name = "1st" },
                new Tree
                {
                    Name     = "2nd",
                    Children = new[]
                    {
                        new Tree
                        {
                            Name     = "2nd-1st",
                            Children = new[] { new Tree { Name = "2nd-1st-1st" } },
                        },
                        new Tree { Name = "2nd-2nd" },
                        new Tree
                        {
                            Name     = "2nd-3rd",
                            Children = new[]
                            {
                                new Tree { Name = "2nd-3rd-1st" },
                                new Tree { Name = "2nd-3rd-2nd" },
                            },
                        },
                    },
                },
                new Tree { Name = "3rd" },
            };

            var dest = src.Flatten(e => e.Children).ToList();
            Assert.That(dest.Count, Is.EqualTo(9));
            Assert.That(dest[0].Name, Is.EqualTo("1st"));
            Assert.That(dest[1].Name, Is.EqualTo("2nd"));
            Assert.That(dest[2].Name, Is.EqualTo("3rd"));
            Assert.That(dest[3].Name, Is.EqualTo("2nd-1st"));
            Assert.That(dest[4].Name, Is.EqualTo("2nd-2nd"));
            Assert.That(dest[5].Name, Is.EqualTo("2nd-3rd"));
            Assert.That(dest[6].Name, Is.EqualTo("2nd-1st-1st"));
            Assert.That(dest[7].Name, Is.EqualTo("2nd-3rd-1st"));
            Assert.That(dest[8].Name, Is.EqualTo("2nd-3rd-2nd"));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Flatten_Empty
        ///
        /// <summary>
        /// 空の配列に対して Flatten を実行した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Flatten_Empty() => Assert.That(
            new Tree[0].Flatten(e => e.Children).Count(),
            Is.EqualTo(0)
        );

        #endregion

        #region Helper methods

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// テスト用のコレクションオブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private IList<int> Create(int count)
        {
            var dest = new List<int>();
            for (int i = 0; i < count; ++i) dest.Add(i);
            return dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Tree
        ///
        /// <summary>
        /// テスト用のツリー構造を持つクラスです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        class Tree
        {
            public string Name { get; set; }
            public IEnumerable<Tree> Children { get; set; }
        }

        #endregion
    }
}
