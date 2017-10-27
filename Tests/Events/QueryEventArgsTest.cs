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

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// QueryEventArgs
    /// 
    /// <summary>
    /// QueryEventArgs のテスト用クラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class QueryEventArgsTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Create_QueryEventArgs
        ///
        /// <summary>
        /// QueryEventArgs(T, U) オブジェクトを生成するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(1)]
        [TestCase("pi")]
        [TestCase(true)]
        public void Create_QueryEventArgs<T>(T query)
        {
            var args = new QueryEventArgs<T, string>(query);
            Assert.That(args.Query, Is.EqualTo(query));
            Assert.That(args.Result, Is.Null);
            Assert.That(args.Cancel, Is.False);

            args.Result = nameof(Create_QueryEventArgs);
            Assert.That(args.Result, Is.EqualTo(nameof(Create_QueryEventArgs)));
        }
    }
}
