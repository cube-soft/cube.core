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
using System.Linq;
using Cube.Collections.Extensions;
using Cube.Generics.Extensions;
using Cube.Syntax.Extensions;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// EnumerableTest
///
/// <summary>
/// Tests extended methods of the IEnumerable(T) class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class EnumerableTest
{
    #region Tests

    #region FirstIndexOf

    /* --------------------------------------------------------------------- */
    ///
    /// FirstIndexOf
    ///
    /// <summary>
    /// Tests the FirstIndexOf method at the specified condition.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void FirstIndexOf()
    {
        var src = 50.Make(i => i * 2);
        Assert.That(src.FirstIndex(i => i < 20), Is.EqualTo(0));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// FirstIndexOf_Empty
    ///
    /// <summary>
    /// Tests the FirstIndexOf method against the empty List(T) object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void FirstIndexOf_Empty()
    {
        var src = Enumerable.Empty<int>();
        Assert.That(src.FirstIndex(i => i < 20), Is.EqualTo(-1));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// FirstIndexOf_Default
    ///
    /// <summary>
    /// Tests the FirstIndexOf method against the default of List(T).
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void FirstIndexOf_Default()
    {
        var src = default(List<int>);
        Assert.That(src.FirstIndex(i => i < 20), Is.EqualTo(-1));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// FirstIndexOf_NeverMatch
    ///
    /// <summary>
    /// Tests the FirstIndexOf method with the never-matched predicate.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void FirstIndexOf_NeverMatch()
    {
        var src = Enumerable.Range(1, 10);
        Assert.That(src.FirstIndex(i => i > 100), Is.EqualTo(-1));
    }

    #endregion

    #region LastIndexOf

    /* --------------------------------------------------------------------- */
    ///
    /// LastIndexOf
    ///
    /// <summary>
    /// Tests the LastIndexOf method at the specified condition.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(10, ExpectedResult =  9)]
    [TestCase( 1, ExpectedResult =  0)]
    [TestCase( 0, ExpectedResult = -1)]
    public int LastIndexOf(int count) => Enumerable.Range(0, count).LastIndex();

    /* --------------------------------------------------------------------- */
    ///
    /// LastIndexOf
    ///
    /// <summary>
    /// Tests the LastIndexOf method at the specified condition.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void LastIndexOf()
    {
        var src = 50.Make(i => i * 2);
        Assert.That(src.LastIndex(i => i < 20), Is.EqualTo(9));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// LastIndexOf_Default
    ///
    /// <summary>
    /// Tests the LastIndexOf method against the default of List(T).
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void LastIndexOf_Default()
    {
        var src = default(List<int>);
        Assert.That(src.LastIndex(), Is.EqualTo(-1));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// LastIndexOf_NeverMatch
    ///
    /// <summary>
    /// Tests the LastIndexOf method with the never-matched predicate.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void LastIndexOf_NeverMatch()
    {
        var src = Enumerable.Range(1, 10);
        Assert.That(src.LastIndex(i => i > 100), Is.EqualTo(-1));
    }

    #endregion

    #region Clamp

    /* --------------------------------------------------------------------- */
    ///
    /// Clamp
    ///
    /// <summary>
    /// Tests the Clamp method at the specified condition.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(10,  5, ExpectedResult = 5)]
    [TestCase(10, 20, ExpectedResult = 9)]
    [TestCase(10, -1, ExpectedResult = 0)]
    [TestCase( 0, 10, ExpectedResult = 0)]
    [TestCase( 0, -1, ExpectedResult = 0)]
    public int Clamp(int count, int index) => Enumerable.Range(0, count).Clamp(index);

    /* --------------------------------------------------------------------- */
    ///
    /// Clamp_Default
    ///
    /// <summary>
    /// Tests the Clamp method against the default of List(T).
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Clamp_Default()
    {
        var src = default(List<int>);
        Assert.That(src.Clamp(100), Is.EqualTo(0));
    }

    #endregion

    #region Concat

    /* --------------------------------------------------------------------- */
    ///
    /// Concat
    ///
    /// <summary>
    /// Test the Concat extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Concat()
    {
        var src = 10.Make(i => i);
        Assert.That(src.Concat(10, 20).Count(), Is.EqualTo(12));
        Assert.That(src.Concat(30).Count(),     Is.EqualTo(11));
        Assert.That(src.Concat().Count(),       Is.EqualTo(10));
    }

    #endregion

    #region Join

    /* --------------------------------------------------------------------- */
    ///
    /// Join
    ///
    /// <summary>
    /// Tests the Join extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(0, ExpectedResult = "")]
    [TestCase(1, ExpectedResult = "k0=v0")]
    [TestCase(2, ExpectedResult = "k0=v0&k1=v1")]
    [TestCase(3, ExpectedResult = "k0=v0&k1=v1&k2=v2")]
    public string Join(int n) => Enumerable.Range(0, n).Join("&", i => $"k{i}=v{i}");

    #endregion

    #region Flatten

    /* --------------------------------------------------------------------- */
    ///
    /// Flatten
    ///
    /// <summary>
    /// Tests the Flatten extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Flatten()
    {
        var src = new[]
        {
            new Tree { Name = "1st" },
            new Tree
            {
                Name     = "2nd",
                Children = new[]
                {
                    new Tree
                    {
                        Name     = "2nd-1st",
                        Children = new[] { new Tree { Name = "2nd-1st-1st" } },
                    },
                    new Tree { Name = "2nd-2nd" },
                    new Tree
                    {
                        Name     = "2nd-3rd",
                        Children = new[]
                        {
                            new Tree { Name = "2nd-3rd-1st" },
                            new Tree { Name = "2nd-3rd-2nd" },
                        },
                    },
                },
            },
            new Tree { Name = "3rd" },
        };

        var dest = src.Flatten(e => e.Children).ToList();
        Assert.That(dest.Count,   Is.EqualTo(9));
        Assert.That(dest[0].Name, Is.EqualTo("1st"));
        Assert.That(dest[1].Name, Is.EqualTo("2nd"));
        Assert.That(dest[2].Name, Is.EqualTo("3rd"));
        Assert.That(dest[3].Name, Is.EqualTo("2nd-1st"));
        Assert.That(dest[4].Name, Is.EqualTo("2nd-2nd"));
        Assert.That(dest[5].Name, Is.EqualTo("2nd-3rd"));
        Assert.That(dest[6].Name, Is.EqualTo("2nd-1st-1st"));
        Assert.That(dest[7].Name, Is.EqualTo("2nd-3rd-1st"));
        Assert.That(dest[8].Name, Is.EqualTo("2nd-3rd-2nd"));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Flatten_Empty
    ///
    /// <summary>
    /// Tests the Flatten extended method with the empty array.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Flatten_Empty()
    {
        var src = Enumerable.Empty<Tree>();
        Assert.That(src.Flatten(e => e.Children).Count(), Is.EqualTo(0));
    }

    #endregion

    #endregion

    #region Others

    /* --------------------------------------------------------------------- */
    ///
    /// Tree
    ///
    /// <summary>
    /// Represents the tree structure.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    class Tree
    {
        public string Name { get; set; }
        public IEnumerable<Tree> Children { get; set; }
    }

    #endregion
}
