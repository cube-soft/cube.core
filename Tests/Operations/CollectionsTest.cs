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
using System;
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
    [Parallelizable]
    [TestFixture]
    class CollectionsTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// LastIndex
        /// 
        /// <summary>
        /// 最後のインデックスを取得するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(9, 10)]
        [TestCase(0,  1)]
        [TestCase(0,  0)]
        public void LastIndex(int expected, int count)
        {
            Assert.That(
                Create(count).LastIndex(),
                Is.EqualTo(expected)
            );
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LastIndex_Null
        /// 
        /// <summary>
        /// 最後のインデックスを取得するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void LastIndex_Null()
        {
            IList<int> collection = null;
            Assert.That(
                collection.LastIndex(),
                Is.EqualTo(0)
            );
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Clamp_Null
        /// 
        /// <summary>
        /// 指定されたインデックスを [0, IList(T).Count) の範囲に丸めるテストを
        /// 行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(5,   5, 10)]
        [TestCase(9,  20, 10)]
        [TestCase(0,  -1, 10)]
        [TestCase(0,  10,  0)]
        [TestCase(0,  -1,  0)]
        public void Clamp(int expected, int index, int count)
        {
            Assert.That(
                Create(count).Clamp(index),
                Is.EqualTo(expected)
            );
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Clamp_Null
        /// 
        /// <summary>
        /// 指定されたインデックスを [0, IList(T).Count) の範囲に丸めるテストを
        /// 行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Clamp_Null()
        {
            IList<int> collection = null;
            Assert.That(
                collection.Clamp(100),
                Is.EqualTo(0)
            );
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
