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
using System.Collections.Generic;
using NUnit.Framework;
using Cube.Collections;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// CollectionsTest
    /// 
    /// <summary>
    /// Cube.Collections.Operations のテスト用クラスです。
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
        public int LastIndex(int count)
            => Create(count).LastIndex();

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
        public int Clamp(int count, int index)
            => Create(count).Clamp(index);

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

        #endregion
    }
}
