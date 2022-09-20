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
namespace Cube.Tests.Extensions;

using Cube.ByteFormat;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// ByteFormatTest
///
/// <summary>
/// Tests the extended methods defined in the Cube.ByteFormat namespace.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class ByteFormatTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// ToPrettyBytes
    ///
    /// <summary>
    /// Tests the ToPrettyBytes extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(1L,                   ExpectedResult = "1 Bytes")]
    [TestCase(1234L,                ExpectedResult = "1.21 KB")]
    [TestCase(12345L,               ExpectedResult = "12.1 KB")]
    [TestCase(123456L,              ExpectedResult = "121 KB")]
    [TestCase(1234567L,             ExpectedResult = "1.18 MB")]
    [TestCase(1234567890L,          ExpectedResult = "1.15 GB")]
    [TestCase(1234567890123L,       ExpectedResult = "1.12 TB")]
    [TestCase(1234567890123456L,    ExpectedResult = "1.1 PB")]
    [TestCase(1234567890123456789L, ExpectedResult = "1.07 EB")]
    public string ToPrettyBytes(long src) => src.ToPrettyBytes();

    /* --------------------------------------------------------------------- */
    ///
    /// ToRoughBytes
    ///
    /// <summary>
    /// Tests the ToRoughBytes extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(0L,    ExpectedResult = "0 Bytes")]
    [TestCase(1023L, ExpectedResult = "1 KB")]
    public string ToRoughBytes(long src) => src.ToRoughBytes();

    #endregion
}
