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
    /// ProgressEventArgsTest
    /// 
    /// <summary>
    /// ProgressEventArgs のテスト用クラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class ProgressEventArgsTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Create_ProgressEventArgs
        ///
        /// <summary>
        /// ProgressEventArgs.Create(double, T) のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(10.0, "Progress")]
        [TestCase(-25.67890, false)]
        [TestCase(0.0, 3.1415926)]
        public void Create_ProgressEventArgs<T>(double ratio, T value)
        {
            var args = ProgressEventArgs.Create(ratio, value);
            Assert.That(args.Ratio, Is.EqualTo(ratio));
            Assert.That(args.Value, Is.EqualTo(value));
        }
    }
}
