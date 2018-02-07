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
using NUnit.Framework;

namespace Cube.Xui.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// BindableCollectionTest
    ///
    /// <summary>
    /// BindableCollection のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class BindableCollectionTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// RaiseCollectionChanged
        ///
        /// <summary>
        /// CollectionChanged イベントの挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(true, ExpectedResult = 4)]
        [TestCase(false, ExpectedResult = 3)]
        public int RaiseCollectionChanged(bool redirect)
        {
            var count = 0;
            var src   = Create().ToBindable();

            src.IsRedirected = redirect;
            src.CollectionChanged += (s, e) => ++count;

            src.Add(new Person { Name = "Ken", Age = 20 });
            src.Move(0, 2);
            src.RemoveAt(1);
            src[0].Name = "Magic";

            return count;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create_EmptyCollection
        ///
        /// <summary>
        /// 空のコレクションに対して ToBindable を実行した時の挙動を
        /// 確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_EmptyCollection() => Assert.That(
            new Person[0].ToBindable().Count,
            Is.EqualTo(0)
        );

        /* ----------------------------------------------------------------- */
        ///
        /// Create_NullCollection
        ///
        /// <summary>
        /// null に対して ToBindable を実行した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_NullCollection() => Assert.That(
            default(Person[]).ToBindable().Count,
            Is.EqualTo(0)
        );

        #endregion

        #region Helpers

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// テストデータを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Person[] Create() => new[]
        {
            new Person { Name = "Alice", Age = 13 },
            new Person { Name = "Bob",   Age = 15 },
            new Person { Name = "Mike",  Age = 45 },
            new Person { Name = "Ada",   Age = 40 },
        };

        #endregion
    }
}
