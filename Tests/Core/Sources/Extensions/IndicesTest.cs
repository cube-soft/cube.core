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

using Cube.Collections.Extensions;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// IndicesTest
///
/// <summary>
/// Tests extended methods of the IEnumerable(int) class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class IndicesTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Within
    ///
    /// <summary>
    /// Executes the test of the Within method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Within() => Assert.That(
        new[] { 3, 1, 4, 1, 5, 9, 2, 6, 0, 0 }.Within(5),
        Is.EquivalentTo(new[] { 3, 1, 4, 1, 2, 0, 0 })
    );

    #endregion
}
