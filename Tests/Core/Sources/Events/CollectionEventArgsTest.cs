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
namespace Cube.Tests.Events;

using System.Collections.Generic;
using System.Linq;
using Cube.Syntax.Extensions;
using NUnit.Framework;

/* --------------------------------------------------------------------- */
///
/// CollectionEventArgsTest
///
/// <summary>
/// Tests methods of the CollectionEventArgs class.
/// </summary>
///
/* --------------------------------------------------------------------- */
[TestFixture]
class CollectionEventArgsTest
{
    #region Tests

    /* ----------------------------------------------------------------- */
    ///
    /// Create_Array
    ///
    /// <summary>
    /// Tests to create a new instance of the CollectionEventArgs(T)
    /// class with the array object.
    /// </summary>
    ///
    /* ----------------------------------------------------------------- */
    [Test]
    public void Create_Array()
    {
        var src = CollectionEventArgs.Create(10.Make(i => i));
        Assert.That(src.Value.Count(), Is.EqualTo(10));
    }

    /* ----------------------------------------------------------------- */
    ///
    /// Create_List
    ///
    /// <summary>
    /// Tests to create a new instance of the CollectionEventArgs(T)
    /// class with the List(T) object.
    /// </summary>
    ///
    /* ----------------------------------------------------------------- */
    [Test]
    public void Create_List()
    {
        var args = CollectionEventArgs.Create(5.Make(i => i), true);
        Assert.That(args.Value.Count(), Is.EqualTo(5));
        Assert.That(args.Cancel, Is.True);
    }

    /* ----------------------------------------------------------------- */
    ///
    /// Create_Cancel
    ///
    /// <summary>
    /// Tests to create a new instance of the CollectionCancelEventArgs(T)
    /// class with the provided arguments.
    /// </summary>
    ///
    /* ----------------------------------------------------------------- */
    [Test]
    public void Create_Cancel()
    {
        var args = CollectionEventArgs.Create(new List<string>
        {
            "Hello", "world", "This", "is", "a", "test", "program",
        }, true);

        Assert.That(args.Value.Count(), Is.EqualTo(7));
        Assert.That(args.Cancel, Is.True);
    }

    /* ----------------------------------------------------------------- */
    ///
    /// Create_Cancel_Default
    ///
    /// <summary>
    /// Tests to create a new instance of the CollectionCancelEventArgs(T)
    /// class.
    /// </summary>
    ///
    /* ----------------------------------------------------------------- */
    [Test]
    public void Create_Cancel_Default()
    {
        var args = new CollectionCancelEventArgs<string>(Enumerable.Empty<string>());
        Assert.That(args.Value.Count(), Is.EqualTo(0));
        Assert.That(args.Cancel, Is.False);
    }

    #endregion
}
