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
namespace Cube.Tests;

using System;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// AccessorTest
///
/// <summary>
/// Tests the AssemblyReader class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class AccessorTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Create_Integer
    ///
    /// <summary>
    /// Tests the default constructor with int type.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Create_Integer()
    {
        var src = new Accessor<int>();
        Assert.That(src.Get(), Is.EqualTo(0));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Create_String
    ///
    /// <summary>
    /// Tests the default constructor with string type.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Create_String()
    {
        var src = new Accessor<string>();
        Assert.That(src.Get(), Is.Null);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Create_DateTime
    ///
    /// <summary>
    /// Tests the default constructor with DateTime type.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Create_DateTime()
    {
        var src = new Accessor<DateTime>();
        Assert.That(src.Get(), Is.EqualTo(DateTime.MinValue));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Create_Nullable
    ///
    /// <summary>
    /// Tests the default constructor with Nullable type.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Create_Nullable()
    {
        var src = new Accessor<DateTime?>();
        Assert.That(src.Get(), Is.Null);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Create_Delegate
    ///
    /// <summary>
    /// Tests the constructor with setter and getter delegates.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Create_Delegate()
    {
        var n   = 100;
        var src = new Accessor<int>(() => n, e => n = e);

        Assert.That(src.Get(), Is.EqualTo(100));
        src.Set(200);
        Assert.That(src.Get(), Is.EqualTo(200));
        Assert.That(src.Get(), Is.EqualTo(n));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Set_InvalidOperationException
    ///
    /// <summary>
    /// Tests the Set method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Set_InvalidOperationException()
    {
        var src = new Accessor<int>(() => 10);
        Assert.That(() => src.Set(1000), Throws.InvalidOperationException);
    }

    #endregion
}
