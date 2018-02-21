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
    class BindableCommandTest
    {
        #region Tests

        #region BindableCommand

        /* ----------------------------------------------------------------- */
        ///
        /// Create_Destruct
        ///
        /// <summary>
        /// BindableCommand の生成から破棄までのテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_Destruct()
        {
            var src  = new Person().ToBindable();
            var dest = new BindableCommand(
                () => src.Value.Name = "Done",
                () => src.Value.Age > 0,
                src
            );

            src.Value.Age = 20;
            dest.Execute(null);
            Assert.That(src.Value.Name, Is.EqualTo("Done"));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RaiseCanExecuteChanged
        ///
        /// <summary>
        /// CanExecuteChanged イベントの挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void RaiseCanExecuteChanged()
        {
            var src  = new Person().ToBindable();
            using (var dest = new BindableCommand(
                () => src.Value.Name = "Done",
                () => src.Value.Age > 0,
                src
            ))
            {
                Assert.That(dest.CanExecute(null), Is.False);
                src.Value.Age = 10;
                Assert.That(dest.CanExecute(null), Is.True);
                src.Value.Age = -1;
                Assert.That(dest.CanExecute(null), Is.False);
                src.Value.Age = 20;
                Assert.That(dest.CanExecute(null), Is.True);
                dest.Execute(null);
                Assert.That(src.Value.Name, Is.EqualTo("Done"));
            }
        }

        #endregion

        #region BindableCommand<T>

        /* ----------------------------------------------------------------- */
        ///
        /// Create_Destruct_Generic
        ///
        /// <summary>
        /// BindableCommand(T) の生成から破棄までのテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_Destruct_Generic()
        {
            var src  = new Person().ToBindable();
            var dest = new BindableCommand<int>(
                e => src.Value.Name = $"Done:{e}",
                e => e > 0 && src.Value.Age > 0,
                src
            );

            src.Value.Age = 20;
            dest.Execute(1);
            Assert.That(src.Value.Name, Is.EqualTo("Done:1"));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RaiseCanExecuteChanged_Generic
        ///
        /// <summary>
        /// CanExecuteChanged イベントの挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void RaiseCanExecuteChanged_Generic()
        {
            var src = new Person().ToBindable();
            using (var dest = new BindableCommand<int>(
                e => src.Value.Name = $"Done:{e}",
                e => e > 0 && src.Value.Age > 0,
                src
            ))
            {
                Assert.That(dest.CanExecute(-1), Is.False);
                Assert.That(dest.CanExecute(1),  Is.False);
                src.Value.Age = 10;
                Assert.That(dest.CanExecute(-2), Is.False);
                Assert.That(dest.CanExecute(2),  Is.True);
                src.Value.Age = -1;
                Assert.That(dest.CanExecute(-3), Is.False);
                Assert.That(dest.CanExecute(3),  Is.False);
                src.Value.Age = 20;
                Assert.That(dest.CanExecute(-4), Is.False);
                Assert.That(dest.CanExecute(4),  Is.True);
                dest.Execute(4);
                Assert.That(src.Value.Name, Is.EqualTo("Done:4"));
            }
        }

        #endregion

        #endregion
    }
}
