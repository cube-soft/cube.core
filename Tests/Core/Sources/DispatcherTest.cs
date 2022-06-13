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

using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// DispatcherTest
///
/// <summary>
/// Tests the Dispatcher class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class DispatcherTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Create_Throws
    ///
    /// <summary>
    /// Confirms the behavior when creating a new instance.
    /// Note that SynchronizationContext.Current is null.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Create_Throws()
    {
        Assert.That(SynchronizationContext.Current, Is.Null);
        Assert.That(() => new ContextDispatcher(true), Throws.ArgumentNullException);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Post
    ///
    /// <summary>
    /// Tests the Invoke method with the non-synchronous option.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Post()
    {
        var dest = 0;
        var src  = new ContextDispatcher(new(), false);
        src.Invoke(() => dest++);
        Task.Delay(100).Wait();
        Assert.That(dest, Is.EqualTo(1));
    }

    #endregion
}
