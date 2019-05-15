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
using Cube.Mixin.Iteration;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cube.Xui.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// BindableCollectionTest
    ///
    /// <summary>
    /// Tests methods of the BindableCollection class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class BindableCollectionTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Count
        ///
        /// <summary>
        /// Tests for for creating an object and getting the Count value.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public int Count(int id, BindableCollection<Person> src) => src.Count;

        /* ----------------------------------------------------------------- */
        ///
        /// AddAsync
        ///
        /// <summary>
        /// Tests the Add method as an asynchronous operation.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void AddAsync()
        {
            var dispatcher = new Dispatcher(new SynchronizationContext(), true);
            using (var src = new BindableCollection<Person>(dispatcher))
            {
                var count = 0;
                src.CollectionChanged += (s, e) => ++count;

                var tasks = Create().Select(e => Task.Run(() => src.Add(e)));
                Task.WaitAll(tasks.ToArray());

                Assert.That(src.Count, Is.EqualTo(4));
                Assert.That(count, Is.EqualTo(4));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RemoveAsync
        ///
        /// <summary>
        /// Tests the Remove method as an asynchronous operation.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void RemoveAsync()
        {
            using (var src = Create())
            {
                var count = 0;
                src.CollectionChanged += (s, e) => ++count;

                var tasks = 4.Make(i => Task.Run(() => src.RemoveAt(0)));
                Task.WaitAll(tasks.ToArray());

                Assert.That(src.Count, Is.EqualTo(0));
                Assert.That(count,     Is.EqualTo(4));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RaiseCollectionChanged
        ///
        /// <summary>
        /// Tests the CollectionChanged event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(true,  ExpectedResult = 4)]
        [TestCase(false, ExpectedResult = 3)]
        public int RaiseCollectionChanged(bool redirect)
        {
            using (var src = Create())
            {
                var count = 0;

                src.IsRedirected = redirect;
                src.CollectionChanged += (s, e) => ++count;
                src.Add(new Person { Name = "Ken", Age = 20 });
                src.Move(0, 2);
                src.RemoveAt(1);
                src[0].Name = "Magic";

                return count;
            }
        }

        #endregion

        #region TestCases

        /* ----------------------------------------------------------------- */
        ///
        /// TestCases
        ///
        /// <summary>
        /// Gets test cases.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                var n = 0;
                yield return new TestCaseData(n++, Create()).Returns(4);
                yield return new TestCaseData(n++, new BindableCollection<Person>(Dispatcher.Vanilla)).Returns(0);
                yield return new TestCaseData(n++, new BindableCollection<Person>(Enumerable.Empty<Person>(), Dispatcher.Vanilla)).Returns(0);
                yield return new TestCaseData(n++, new BindableCollection<Person>(default, Dispatcher.Vanilla)).Returns(0);
            }
        }

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// Creates dummy data for tests.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static BindableCollection<Person> Create() => new BindableCollection<Person>(new[]
        {
            new Person { Name = "Alice", Age = 13 },
            new Person { Name = "Bob",   Age = 15 },
            new Person { Name = "Mike",  Age = 45 },
            new Person { Name = "Ada",   Age = 40 },
        }, new Dispatcher(new SynchronizationContext(), true));

        #endregion
    }
}
