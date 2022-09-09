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

using System.ComponentModel;
using Cube.Generics.Extensions;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// GenericsTest
///
/// <summary>
/// Tests the extended methods of generic classes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class GenericsTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// TryCast
    ///
    /// <summary>
    /// Tests the TryCast extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void TryCast()
    {
        var c0 = new Person { Number = 1 };
        Assert.That(c0, Is.Not.Null);
        var c1 = c0.TryCast<INotifyPropertyChanged>();
        Assert.That(c1, Is.Not.Null);
        var c2 = c1.TryCast<ObservableBase>();
        Assert.That(c2, Is.Not.Null);
        var c3 = c0.TryCast<Person>();
        Assert.That(c3, Is.Not.Null);
        var c4 = c3.TryCast<GenericsTest>();
        Assert.That(c4, Is.Null);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// TryCast_Integer
    ///
    /// <summary>
    /// Tests the TryCast extended method with the int value.
    /// </summary>
    ///
    /// <remarks>
    /// TryCast for types that are not inherited, such as int to double,
    /// will always fail.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void TryCast_Integer()
    {
        Assert.That(10.TryCast(-1L),     Is.EqualTo(-1L));
        Assert.That(10.TryCast(-1.0),    Is.EqualTo(-1.0));
        Assert.That(10.TryCast("error"), Is.EqualTo("error"));
    }

    #endregion
}
