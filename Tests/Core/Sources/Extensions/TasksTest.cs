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

using System;
using System.Threading.Tasks;
using Cube.Tasks.Extensions;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// TasksTest
///
/// <summary>
/// Tests of extended methods of the Task and related classes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class TasksTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Forget
    ///
    /// <summary>
    /// Tests the Forget extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Forget()
    {
        // Assert.DoesNotThrow
        Task.Run(() => throw new InvalidOperationException()).Forget();
        Task.Delay(100).Wait();
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Timeout
    ///
    /// <summary>
    /// Tests the Timeout extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Timeout()
    {
        var n   = 0;
        var src = Task.Run(() => { while (true) { n = 1; } });
        Assert.That(
            () => src.Timeout(TimeSpan.FromMilliseconds(100)).Wait(),
            Throws.TypeOf<AggregateException>().And.InnerException
                  .TypeOf<TimeoutException>()
        );
        Assert.That(n, Is.EqualTo(1));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Timeout_NotThrow
    ///
    /// <summary>
    /// Tests the Timeout extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Timeout_NoThrow()
    {
        var n = Task.Run(() => Fibonacci(5))
                    .Timeout(TimeSpan.FromSeconds(100))
                    .Result;
        Assert.That(n, Is.EqualTo(5L));
    }

    #endregion

    #region Others

    /* --------------------------------------------------------------------- */
    ///
    /// Fibonacci
    ///
    /// <summary>
    /// Executes the dummy operation for tests.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private long Fibonacci(int n)
        => n == 0 ? 0 :
           n == 1 ? 1 :
           Fibonacci(n - 1) + Fibonacci(n - 2);

    #endregion
}
