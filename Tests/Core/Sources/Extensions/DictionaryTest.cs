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

using System.Collections.Generic;
using Cube.Collections.Extensions;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// DictionaryTest
///
/// <summary>
/// Tests extended methods of the IDictionary(T, U) class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class DictionaryTest
{
    /* --------------------------------------------------------------------- */
    ///
    /// AddOrSet
    ///
    /// <summary>
    /// Tests to add or set values to a dictionary collection.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void AddOrSet()
    {
        var src = new Dictionary<string, int>();
        var key = nameof(AddOrSet);
        for (var i = 0; i < 10; ++i) src.AddOrSet(key, i);
        Assert.That(src[key], Is.EqualTo(9));
    }
}
