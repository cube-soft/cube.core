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
namespace Cube.Tests.Collections;

using Cube.Collections;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// NumericStringComparerTest
///
/// <summary>
/// Tests the NumericStringComparer class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class NumericStringComparerTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Compare
    ///
    /// <summary>
    /// Tests the Compare method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase( "2",  "2", ExpectedResult =  0)]
    [TestCase( "2",  "1", ExpectedResult =  1)]
    [TestCase( "2", "01", ExpectedResult =  1)]
    [TestCase( "2", "02", ExpectedResult =  2)] // >= 1
    [TestCase( "2", "10", ExpectedResult = -1)]
    [TestCase( "2", "20", ExpectedResult = -1)]
    [TestCase( "2", "-2", ExpectedResult =  5)] // >= 1
    [TestCase("02", "01", ExpectedResult =  1)]
    [TestCase("02", "10", ExpectedResult = -1)]
    [TestCase("1.2.3-alpha",  "1.2.3-beta", ExpectedResult = -1)]
    [TestCase("1.2.13-alpha", "1.2.3-beta", ExpectedResult =  1)]
    [TestCase("sample", "test", ExpectedResult = -1)]
    [TestCase("sample", "Sample", ExpectedResult = 32)] // >= 1
    public int Compare(string x, string y) => new NumericStringComparer().Compare(x, y);

    /* --------------------------------------------------------------------- */
    ///
    /// Compare
    ///
    /// <summary>
    /// Tests the Compare method with the Japanese full-width numeric
    /// characters.
    /// </summary>
    ///
    /// <remarks>
    /// When the Japanese full-width numeric characters are specified,
    /// these are compared as string (not number).
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("２", "２", ExpectedResult = 0)]
    [TestCase("２", "１", ExpectedResult = 1)]
    [TestCase("２", "０１", ExpectedResult = 2)]
    [TestCase("２", "０２", ExpectedResult = 2)]
    [TestCase("２", "１０", ExpectedResult = 1)] // not < 0
    [TestCase("２", "－２", ExpectedResult = 5)]
    [TestCase("０２", "０１", ExpectedResult = 1)]
    [TestCase("０２", "１０", ExpectedResult = -1)]
    public int Compare_WithFullWidth(string x, string y) => new NumericStringComparer().Compare(x, y);

    #endregion
}
