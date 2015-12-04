/* ------------------------------------------------------------------------- */
///
/// ByteFormatTester.cs
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
using NUnit.Framework;
using Cube.Extensions;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Tests.ByteFormatTester
    /// 
    /// <summary>
    /// バイトサイズの書式に関するテストを行うためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class ByteFormatTester
    {
        /* ----------------------------------------------------------------- */
        ///
        /// TestToPrettyBytes
        /// 
        /// <summary>
        /// ToPrettyBytes のテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(123456789L,    "118 MB")]
        [TestCase( 12345678L,   "11.8 MB")]
        [TestCase(  1234567L,   "1.18 MB")]
        [TestCase(   123456L,    "121 KB")]
        [TestCase(    12345L,   "12.1 KB")]
        [TestCase(     1234L,   "1.21 KB")]
        [TestCase(      123L, "123 Bytes")]
        [TestCase(       12L,  "12 Bytes")]
        [TestCase(        1L,   "1 Bytes")]
        public void TestToPrettyBytes(long src, string expected)
        {
            Assert.That(src.ToPrettyBytes(), Is.EqualTo(expected));
        }
    }
}
