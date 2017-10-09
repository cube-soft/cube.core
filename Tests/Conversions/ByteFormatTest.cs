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
using NUnit.Framework;
using Cube.Conversions;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// ByteFormatTest
    /// 
    /// <summary>
    /// バイトサイズの書式に関するテストを行うためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class ByteFormatTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// ToPrettyBytes
        /// 
        /// <summary>
        /// ToPrettyBytes のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(1L,                   ExpectedResult = "1 Bytes")]
        [TestCase(1234L,                ExpectedResult = "1.21 KB")]
        [TestCase(12345L,               ExpectedResult = "12.1 KB")]
        [TestCase(123456L,              ExpectedResult = "121 KB")]
        [TestCase(1234567L,             ExpectedResult = "1.18 MB")]
        [TestCase(1234567890L,          ExpectedResult = "1.15 GB")]
        [TestCase(1234567890123L,       ExpectedResult = "1.12 TB")]
        [TestCase(1234567890123456L,    ExpectedResult = "1.1 PB")]
        [TestCase(1234567890123456789L, ExpectedResult = "1.07 EB")]
        public string ToPrettyBytes(long src)
            => src.ToPrettyBytes();

        /* ----------------------------------------------------------------- */
        ///
        /// ToRoughBytes
        /// 
        /// <summary>
        /// ToRoughBytes のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(0L,    ExpectedResult = "0 Bytes")]
        [TestCase(1023L, ExpectedResult = "1 KB")]
        public string ToRoughBytes(long src)
            => src.ToRoughBytes();
    }
}
