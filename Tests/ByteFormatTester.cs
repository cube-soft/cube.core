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
        [TestCase(123456789UL,    "118 MB")]
        [TestCase( 12345678UL,   "11.8 MB")]
        [TestCase(  1234567UL,   "1.18 MB")]
        [TestCase(   123456UL,    "121 KB")]
        [TestCase(    12345UL,   "12.1 KB")]
        [TestCase(     1234UL,   "1.21 KB")]
        [TestCase(      123UL, "123 Bytes")]
        [TestCase(       12UL,  "12 Bytes")]
        [TestCase(        1UL,   "1 Bytes")]
        public void TestToPrettyBytes(UInt64 src, string expected)
        {
            Assert.That(src.ToPrettyBytes(), Is.EqualTo(expected));
        }
    }
}
