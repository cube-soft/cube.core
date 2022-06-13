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
using System;
using NUnit.Framework;

namespace Cube.Tests;

/* ------------------------------------------------------------------------- */
///
/// DisposableTest
///
/// <summary>
/// Test the Disposable class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class DisposableTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Make_Disposable
    ///
    /// <summary>
    /// Tests the Disposable.Create method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Make_Disposable()
    {
        var n   = 0;
        var src = Disposable.Create(() => n++);

        src.Dispose();
        Assert.That(n, Is.EqualTo(1));
        src.Dispose(); // ignore
        Assert.That(n, Is.EqualTo(1));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Make_DisposableProxy
    ///
    /// <summary>
    /// Tests the DisposableProxy class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Make_DisposableProxy()
    {
        var n = 0;
        using (var src = new MockDisposableProxy(() => Disposable.Create(() => n++))) { }

        Assert.That(n, Is.EqualTo(1));
        Assert.That(() => new MockDisposableProxy(() => default), Throws.ArgumentNullException);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Make_Throws
    ///
    /// <summary>
    /// Tests the creating methods with the null actions.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Make_Throws()
    {
        Assert.That(() => Disposable.Create(null), Throws.ArgumentNullException);
    }

    #endregion

    #region Others

    /* --------------------------------------------------------------------- */
    ///
    /// MockDisposableProxy
    ///
    /// <summary>
    /// Represents the mock class to test the DisposableProxy class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    class MockDisposableProxy : DisposableProxy
    {
        public MockDisposableProxy(Func<IDisposable> func) : base(func) { }
    }

    #endregion
}
