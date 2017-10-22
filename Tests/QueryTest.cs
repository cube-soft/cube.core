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
using System.Threading;
using NUnit.Framework;

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
        public bool Request(IList<string> results)
        {
            var index = 0;
            var query = new Query<string, string>(x =>
            {
                if (index >= results.Count) x.Cancel = true;
                else
                {
                    x.Cancel = false;
                    x.Result = results[index++];
                }
            });

            return new QueryContoroller().Execute(query);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Request_SynchronizationContext
        ///
        /// <summary>
        /// SynchronizationContext オブジェクトが null ではない時の
        /// テストを実行します。
        /// </summary>
        /// 
        /// <remarks>
        /// NUnit 経由で実行する場合 SynchronizationContext.Current が
        /// null である事が確認されているので、明示的に設定します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public bool Request_SynchronizationContext(IList<string> results)
        {
            var index = 0;
            var query = new Query<string, string>(x =>
            {
                if (index >= results.Count) x.Cancel = true;
                else
                {
                    x.Cancel = false;
                    x.Result = results[index++];
                }
            });

            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            return new QueryContoroller().Execute(query);
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
        public void Request_None()
            => Assert.That(
                new QueryContoroller().Execute(new Query<string, string>()),
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
                yield return new TestCaseData(new List<string> { "first", "second", "success" }).Returns(true);
                yield return new TestCaseData(new List<string> { "first", "failed" }).Returns(false);
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
            public bool Execute(IQuery<string, string> query)
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
