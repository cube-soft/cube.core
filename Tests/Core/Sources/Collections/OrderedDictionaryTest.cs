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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cube.Collections;
using Cube.Generics.Extensions;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// OrderedDictionaryTest
///
/// <summary>
/// Tests the OrderedDictionary class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class OrderedDictionaryTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Access_WithKey
    ///
    /// <summary>
    /// Tests the [] operator.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Access_WithKey()
    {
        var src = Create();
        Assert.That(src.IsReadOnly, Is.False);
        Assert.That(src.Count,      Is.EqualTo(5));
        Assert.That(src["Linus"],   Is.EqualTo("Torvalds"));

        src["Richard"] = "Rossum";
        Assert.That(src["Richard"], Is.EqualTo("Rossum"));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Access_WithKey_Throws
    ///
    /// <summary>
    /// Tests the [] operator with an invalid key.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Access_WithKey_Throws()
    {
        var src = Create();
        Assert.That(() => src["John"],       Throws.TypeOf<KeyNotFoundException>());
        Assert.That(() => src["John"] = "a", Throws.TypeOf<KeyNotFoundException>());
        Assert.That(() => src[null],         Throws.TypeOf<ArgumentNullException>());
        Assert.That(() => src[null] = "a",   Throws.TypeOf<ArgumentNullException>());
    }


    /* --------------------------------------------------------------------- */
    ///
    /// Access_WithIndex
    ///
    /// <summary>
    /// Tests the [] operator with an index.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Access_WithIndex()
    {
        var src = Create();
        Assert.That(src.IsReadOnly, Is.False);
        Assert.That(src.Count,      Is.EqualTo(5));
        Assert.That(src[0],         Is.EqualTo("Ritchie"));
        Assert.That(src[1],         Is.EqualTo("Kernighan"));
        Assert.That(src[2],         Is.EqualTo("Thompson"));
        Assert.That(src[3],         Is.EqualTo("Torvalds"));
        Assert.That(src[4],         Is.EqualTo("Stallman"));

        src[2] = "Gutmans";
        Assert.That(src[2],         Is.EqualTo("Gutmans"));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Access_WithIndex_Throws
    ///
    /// <summary>
    /// Tests the [] operator with an index that is out of range.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(5)]
    [TestCase(-1)]
    public void Access_WithIndex_Throws(int index)
    {
        var src = Create();
        Assert.That(() => src[index],      Throws.TypeOf<ArgumentOutOfRangeException>());
        Assert.That(() => src[index] = "", Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Add_Throws
    ///
    /// <summary>
    /// Tests the Add method with an invalid value.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Add_Throws() => Assert.That(
        () => Create().Add(null, null),
        Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("key")
    );

    /* --------------------------------------------------------------------- */
    ///
    /// Remove
    ///
    /// <summary>
    /// Tests the Remove method with a KeyValuePair(string, string)
    /// object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("Linus", "Torvalds", ExpectedResult = true)]
    [TestCase("Linus", "Stallman", ExpectedResult = false)]
    [TestCase("",      "Torvalds", ExpectedResult = false)]
    [TestCase(null,   null,        ExpectedResult = false)]
    public bool Remove(string key, string value) =>
        Create().Remove(new KeyValuePair<string, string>(key, value));

    /* --------------------------------------------------------------------- */
    ///
    /// CopyFrom
    ///
    /// <summary>
    /// Tests the copy constructor.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void CopyFrom()
    {
        var src  = Create();
        var dest = new OrderedDictionary<string, string>(src);
        Assert.That(dest.Count, Is.EqualTo(src.Count));

        src["Brian"] = "Kay";
        Assert.That(src["Brian"],  Is.EqualTo("Kay"));
        Assert.That(dest["Brian"], Is.EqualTo("Kernighan"));

        Assert.That(new OrderedDictionary<string, string>(null).Count, Is.EqualTo(0));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// CopyTo
    ///
    /// <summary>
    /// Tests the CopyTo method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void CopyTo()
    {
        var src  = Create();
        var dest = new KeyValuePair<string, string>[5];

        src.CopyTo(dest, 0);
        Assert.That(dest[0].Key, Is.EqualTo("Dennis"));
        Assert.That(dest[1].Key, Is.EqualTo("Brian"));
        Assert.That(dest[2].Key, Is.EqualTo("Kenneth"));
        Assert.That(dest[3].Key, Is.EqualTo("Linus"));
        Assert.That(dest[4].Key, Is.EqualTo("Richard"));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// CopyTo_Throws
    ///
    /// <summary>
    /// Tests the CopyTo method with invalid values.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void CopyTo_Throws()
    {
        var src  = Create();
        var dest = new KeyValuePair<string, string>[7];

        Assert.That(() => src.CopyTo(null, 0),  Throws.TypeOf<ArgumentNullException>());
        Assert.That(() => src.CopyTo(dest, -1), Throws.TypeOf<ArgumentOutOfRangeException>());
        Assert.That(() => src.CopyTo(dest, 7),  Throws.TypeOf<ArgumentOutOfRangeException>());
        Assert.That(() => src.CopyTo(dest, 3),  Throws.TypeOf<ArgumentException>());
    }

    /* --------------------------------------------------------------------- */
    ///
    /// KeysAndValues
    ///
    /// <summary>
    /// Tests the Keys and Values properties.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void KeysAndValues()
    {
        var src    = Create();
        var keys   = src.Keys;
        var values = src.Values;

        Assert.That(() => keys.Add("a"),   Throws.TypeOf<NotSupportedException>());
        Assert.That(() => values.Add("a"), Throws.TypeOf<NotSupportedException>());

        src.Add("Bjarne", "Stroustrup");
        src.Add(new("Anders", "Hejlsberg"));
        Assert.That(src.Count,    Is.EqualTo(7));
        Assert.That(keys.Count,   Is.EqualTo(5));
        Assert.That(values.Count, Is.EqualTo(5));

        Assert.That(src.Remove("Richard"), Is.True);
        Assert.That(src.Remove("Dennis"),  Is.True);
        Assert.That(src.Remove("Kenneth"), Is.True);
        Assert.That(src.Remove("Dummy"),   Is.False);
        Assert.That(src.Count,    Is.EqualTo(4));
        Assert.That(keys.Count,   Is.EqualTo(5));
        Assert.That(values.Count, Is.EqualTo(5));

        src.Clear();
        Assert.That(src.Count,      Is.EqualTo(0));
        Assert.That(keys.Count,     Is.EqualTo(5));
        Assert.That(values.Count,   Is.EqualTo(5));
        Assert.That(keys.First(),   Is.EqualTo("Dennis"));
        Assert.That(values.First(), Is.EqualTo("Ritchie"));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// TryGetValue
    ///
    /// <summary>
    /// Tests the TryGetValue method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void TryGetValue()
    {
        var src = Create();

        Assert.That(src.TryGetValue("Linus", out var s0), Is.True);
        Assert.That(s0, Is.EqualTo("Torvalds"));

        Assert.That(src.TryGetValue("Bjarne", out var s1), Is.False);
        Assert.That(s1, Is.Null);

        Assert.That(src.TryGetValue("", out var s2), Is.False);
        Assert.That(s2, Is.Null);

        Assert.That(src.TryGetValue(null, out var s3), Is.False);
        Assert.That(s3, Is.Null);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetEnumerator
    ///
    /// <summary>
    /// Tests the IEnumerable.GetEnumerator method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void GetEnumerator()
    {
        var src = ((IEnumerable)Create()).GetEnumerator();
        Assert.That(src.MoveNext(), Is.True);
        Assert.That(
            src.Current.TryCast<KeyValuePair<string, string>>().Key,
            Is.EqualTo("Dennis")
        );
    }

    #endregion

    #region Others

    /* --------------------------------------------------------------------- */
    ///
    /// Create
    ///
    /// <summary>
    /// Creates a new instance of the OrderedDictionary(string, string)
    /// class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private OrderedDictionary<string, string> Create() => new()
    {
        { "Dennis",  "Ritchie"   },
        { "Brian",   "Kernighan" },
        { "Kenneth", "Thompson"  },
        { "Linus",   "Torvalds"  },
        { "Richard", "Stallman"  },
    };

    #endregion
}
