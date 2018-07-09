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
using System;
using System.Threading;

namespace Cube.Xui.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// BindableFuncTest
    ///
    /// <summary>
    /// BindableFunc のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class BindableFuncTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// GetValue
        ///
        /// <summary>
        /// Value を取得するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetValue()
        {
            var count = 0;
            var dest  = new BindableFunc<int>(() => count);

            dest.PropertyChanged += (s, e) => ++count;
            Assert.That(dest.IsSynchronous, Is.True);
            Assert.That(dest.HasValue,      Is.True);
            Assert.That(dest.Value,         Is.EqualTo(0));

            dest.RaiseValueChanged();
            Assert.That(dest.IsSynchronous, Is.True);
            Assert.That(dest.HasValue,      Is.True);
            Assert.That(dest.Value,         Is.EqualTo(1));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetValue_Context
        ///
        /// <summary>
        /// SynchronizationContext を指定した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetValue_Context()
        {
            var count = 0;
            var dest  = new BindableFunc<int>(() => count, new SynchronizationContext());

            dest.PropertyChanged += (s, e) => ++count;
            Assert.That(dest.IsSynchronous, Is.True);
            Assert.That(dest.HasValue,      Is.True);
            Assert.That(dest.Value,         Is.EqualTo(0));

            dest.RaiseValueChanged();
            Assert.That(dest.IsSynchronous, Is.True);
            Assert.That(dest.HasValue,      Is.True);
            Assert.That(dest.Value,         Is.EqualTo(1));

            dest.IsSynchronous = false;
            dest.RaiseValueChanged();
            Assert.That(dest.IsSynchronous, Is.False);
            Assert.That(dest.HasValue,      Is.True);
            Assert.That(dest.Value,         Is.AtLeast(1));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetValue_Throws
        ///
        /// <summary>
        /// 無効な関数オブジェクトを指定した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetValue_Throws()
        {
            var dest = new BindableFunc<int>(null);
            Assert.That(dest.HasValue, Is.False);
            Assert.That(() => dest.Value, Throws.TypeOf<NullReferenceException>());
        }

        #endregion
    }
}
