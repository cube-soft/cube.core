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

using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// QueryTest
///
/// <summary>
/// Tests the IQuery(T, U) implemented classes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class QueryTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Request
    ///
    /// <summary>
    /// Tests the Query(T) class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCaseSource(nameof(TestCases))]
    public bool Request(int id, IList<string> seq, SynchronizationContext ctx)
    {
        SynchronizationContext.SetSynchronizationContext(ctx);

        var src = new Query<int>(e =>
        {
            if (e.Value >= seq.Count)
            {
                e.Cancel = true;
                e.Value = -1;
            }
            else if (seq[e.Value] == "success") e.Cancel = true;
            else e.Value++;
        });

        var msg = Query.NewMessage(id);
        Assert.That(msg.Source, Is.EqualTo(id));
        Assert.That(msg.Value,  Is.EqualTo(0));
        Assert.That(msg.Cancel, Is.False);

        while (!msg.Cancel) src.Request(msg);
        return msg.Value != -1;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Request
    ///
    /// <summary>
    /// Tests the OnceQuery(T) class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Request_TwiceException()
    {
        var src = new OnceQuery<int>(e => e.Value++);
        var msg = Query.NewMessage(10);
        Assert.That(msg.Source, Is.EqualTo(10));
        Assert.That(msg.Value,  Is.EqualTo(0));
        src.Request(msg);
        Assert.That(msg.Value,  Is.EqualTo(1));
        Assert.That(() => src.Request(msg), Throws.TypeOf<TwiceException>());
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Create_ArgumentNullException
    ///
    /// <summary>
    /// Tests the Query(T) class with the null object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Create_ArgumentNullException()
    {
        Assert.That(() => new Query<int>(null), Throws.ArgumentNullException);
    }

    #endregion

    #region TestCases

    /* --------------------------------------------------------------------- */
    ///
    /// TestCases
    ///
    /// <summary>
    /// Gets test cases.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static IEnumerable<TestCaseData> TestCases
    {
        get
        {
            var n = 0;

            yield return new TestCaseData(n++,
                new List<string> { "first", "second", "success" },
                new SynchronizationContext()
            ).Returns(true);

            yield return new TestCaseData(n++,
                new List<string> { "first", "second", "success" },
                new SynchronizationContext()
            ).Returns(true);

            yield return new TestCaseData(n++,
                new List<string> { "first", "failed" },
                default(SynchronizationContext)
            ).Returns(false);

            yield return new TestCaseData(n++,
                new List<string> { "first", "failed" },
                default(SynchronizationContext)
            ).Returns(false);
        }
    }

    #endregion
}
