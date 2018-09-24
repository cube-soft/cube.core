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

        #region Query(T, U)

        /* ----------------------------------------------------------------- */
        ///
        /// Request
        ///
        /// <summary>
        /// Query(T, U) オブジェクトのテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public bool Request(int id, IList<string> seq, SynchronizationContext ctx)
        {
            SynchronizationContext.SetSynchronizationContext(ctx);

            var index = 0;
            var query = new Query<int, string>(e =>
            {
                if (e.Result == "success" || index >= seq.Count) e.Cancel = true;
                else e.Result = seq[index++];
            });

            var args = new QueryEventArgs<int, string>(id);
            Assert.That(args.Query,  Is.EqualTo(id));
            Assert.That(args.Result, Is.Null);
            Assert.That(args.Cancel, Is.False);

            while (!args.Cancel) query.Request(args);
            return args.Result == "success";
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Request_None
        ///
        /// <summary>
        /// Query(T, U) にコールバック関数が指定されなかった時の挙動を
        /// 確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Request_None()
        {
            var query = new Query<int, string>();
            var args  = new QueryEventArgs<int, string>(200);

            Assert.That(args.Query,  Is.EqualTo(200));
            Assert.That(args.Result, Is.Null);
            Assert.That(args.Cancel, Is.False);

            query.Request(args);

            Assert.That(args.Cancel, Is.True);
            Assert.That(args.Result, Is.Null);
        }

        #endregion

        #region Query(T)

        /* ----------------------------------------------------------------- */
        ///
        /// Request_Count
        ///
        /// <summary>
        /// Query(T) オブジェクトのテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public bool Request_Count(int id, IList<string> seq, SynchronizationContext ctx)
        {
            SynchronizationContext.SetSynchronizationContext(ctx);

            var query = new Query<int>(e =>
            {
                if (e.Result >= seq.Count)
                {
                    e.Cancel = true;
                    e.Result = -1;
                }
                else if (seq[e.Result] == "success") e.Cancel = true;
                else e.Result++;
            });

            var args = QueryEventArgs.Create(id);
            Assert.That(args.Query,  Is.EqualTo(id));
            Assert.That(args.Result, Is.EqualTo(0));
            Assert.That(args.Cancel, Is.False);

            while (!args.Cancel) query.Request(args);
            return args.Result != -1;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Request_Count_None
        ///
        /// <summary>
        /// Query(T) にコールバック関数が指定されなかった時の挙動を
        /// 確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Request_Count_None()
        {
            var query = new Query<int>();
            var args  = QueryEventArgs.Create(100);

            Assert.That(args.Query,  Is.EqualTo(100));
            Assert.That(args.Result, Is.EqualTo(0));
            Assert.That(args.Cancel, Is.False);

            query.Request(args);

            Assert.That(args.Cancel, Is.True);
            Assert.That(args.Result, Is.EqualTo(0));
        }

        #endregion

        #endregion

        #region TestCases

        /* ----------------------------------------------------------------- */
        ///
        /// TestCases
        ///
        /// <summary>
        /// テスト用データを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                yield return new TestCaseData(0,
                    new List<string> { "first", "second", "success" },
                    new SynchronizationContext()
                ).Returns(true);

                yield return new TestCaseData(1,
                    new List<string> { "first", "second", "success" },
                    new SynchronizationContext()
                ).Returns(true);

                yield return new TestCaseData(2,
                    new List<string> { "first", "failed" },
                    default(SynchronizationContext)
                ).Returns(false);

                yield return new TestCaseData(3,
                    new List<string> { "first", "failed" },
                    default(SynchronizationContext)
                ).Returns(false);
            }
        }

        #endregion
    }
}
