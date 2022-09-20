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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Cube.Collections;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// ObservableCollectionTest
///
/// <summary>
/// Represents the test for the ObservableBase(T) collection.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class ObservableCollectionTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Invoke
    ///
    /// <summary>
    /// Confirms that CollectinChanged events are fired.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Invoke()
    {
        var src = new TestCollection<int>();

        for (var i = 0; i < 3; ++i) src.Add(i);
        var count = 0;
        src.CollectionChanged += (s, e) => ++count;
        for (var i = 0; i < 3; ++i) src.Add(i);

        Assert.That(count, Is.EqualTo(3), "CollectionChanged");
        Assert.That(src.Count(), Is.EqualTo(6), "Count");
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Invoke_SynchronizationContext
    ///
    /// <summary>
    /// Confirms that CollectinChanged events are fired with the
    /// provided dispatcher.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Invoke_SynchronizationContext()
    {
        var src = new TestCollection<int>();
        src.Set(new ContextDispatcher(new(), true));

        for (var i = 0; i < 10; ++i) src.Add(i);
        var count = 0;
        src.CollectionChanged += (s, e) => ++count;
        for (var i = 0; i < 10; ++i) src.Add(i);

        Assert.That(count,       Is.EqualTo(10), "CollectionChanged");
        Assert.That(src.Count(), Is.EqualTo(20), "Count");
    }

    #endregion

    #region Others

    /* --------------------------------------------------------------------- */
    ///
    /// TestCollection
    ///
    /// <summary>
    /// Represents the dummy collection that implements IEnumerable(T)
    /// and INotifiCollectionChanged interfaces.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private class TestCollection<T> : ObservableCollectionBase<T>
    {
        public TestCollection() => _inner.CollectionChanged += (s, e) => OnCollectionChanged(e);
        public void Set(Dispatcher dispatcher) => Dispatcher = dispatcher;
        public void Add(T value) => _inner.Add(value);
        public override IEnumerator<T> GetEnumerator() => _inner.GetEnumerator();
        protected override void Dispose(bool disposing) => _inner.Clear();
        private readonly ObservableCollection<T> _inner = new();
    }

    #endregion
}
