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
using System.Collections.Generic;
using System.Threading;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// QueryTest
    ///
    /// <summary>
    /// プログラムオプション等の引数を解析するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class QueryTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Request
        ///
        /// <summary>
        /// Query オブジェクトのテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public bool Request(int id, IList<string> results)
        {
            var index = 0;
            var query = new Query<string>(x =>
            {
                if (index >= results.Count) x.Cancel = true;
                else
                {
                    x.Cancel = false;
                    x.Result = results[index++];
                }
            });

            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            return new QueryContoroller().Invoke(query);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Request_NullSynchronizationContext
        ///
        /// <summary>
        /// SynchronizationContext オブジェクトが null である時の
        /// テストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public bool Request_NullSynchronizationContext(int id, IList<string> results)
        {
            var index = 0;
            var query = new Query<string>(x =>
            {
                if (index >= results.Count) x.Cancel = true;
                else
                {
                    x.Cancel = false;
                    x.Result = results[index++];
                }
            });

            SynchronizationContext.SetSynchronizationContext(default(SynchronizationContext));
            return new QueryContoroller().Invoke(query);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Request_None
        ///
        /// <summary>
        /// Query にコールバック関数が指定されなかった時のテストを
        /// 実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Request_None() => Assert.That(
            new QueryContoroller().Invoke(new Query<string>()),
            Is.False
        );

        #endregion

        #region TestCases

        /* ----------------------------------------------------------------- */
        ///
        /// TestCases
        ///
        /// <summary>
        /// Query オブジェクトのテスト用データです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                yield return new TestCaseData(0, new List<string> { "first", "second", "success" }).Returns(true);
                yield return new TestCaseData(1, new List<string> { "first", "failed" }).Returns(false);
            }
        }

        #endregion

        #region Helper

        /* ----------------------------------------------------------------- */
        ///
        /// QueryContoroller
        ///
        /// <summary>
        /// Query オブジェクトの実行用クラスです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        class QueryContoroller
        {
            public bool Invoke(IQuery<string, string> query)
            {
                var e = new QueryEventArgs<string, string>("contoroller");

                do
                {
                    query.Request(e);
                    if (!e.Cancel && e.Result == "success") return true;
                } while (!e.Cancel);

                return false;
            }
        }

        #endregion
    }
}
