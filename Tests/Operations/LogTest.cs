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
using Cube.Log;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// LogTest
    ///
    /// <summary>
    /// LogOperator のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class LogTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// LogDebug
        ///
        /// <summary>
        /// Debug 系メソッドのテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void LogDebug() => Assert.DoesNotThrow(() =>
        {
            try
            {
                this.LogDebug(nameof(LogInfo));
                this.LogDebug("Action", () => { });
                Assert.That(this.LogDebug("Func", () => 1), Is.EqualTo(1));
                this.LogDebug("Error", () => throw new ArgumentException($"{nameof(LogInfo)} (throw)"));
            }
            catch (ArgumentException err) { this.LogDebug(err.Message, err); }
        });

        /* ----------------------------------------------------------------- */
        ///
        /// LogInfo
        ///
        /// <summary>
        /// Info 系メソッドのテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void LogInfo() => Assert.DoesNotThrow(() =>
        {
            try
            {
                this.LogInfo(nameof(LogInfo));
                this.LogInfo(AssemblyReader.Default.Assembly);
                this.LogInfo("Action", () => { });
                Assert.That(this.LogInfo("Func", () => 2), Is.EqualTo(2));
                this.LogInfo("Error", () => throw new ArgumentException($"{nameof(LogInfo)} (throw)"));
            }
            catch (ArgumentException err) { this.LogInfo(err.Message, err); }
        });

        /* ----------------------------------------------------------------- */
        ///
        /// LogWarn
        ///
        /// <summary>
        /// Warn 系メソッドのテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void LogWarn() => Assert.DoesNotThrow(() =>
        {
            var error = new ArgumentException($"{nameof(LogWarn)} (throw)");
            this.LogWarn(nameof(LogWarn));
            this.LogWarn(() => throw error);
            Assert.That(this.LogWarn(() => 3, -1), Is.EqualTo(3));

            try { throw error; }
            catch (ArgumentException err) { this.LogWarn(err.Message, err); }
        });

        /* ----------------------------------------------------------------- */
        ///
        /// LogError
        ///
        /// <summary>
        /// Error 系メソッドのテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void LogError() => Assert.DoesNotThrow(() =>
        {
            var error = new ArgumentException($"{nameof(LogError)} (throw)");
            this.LogError(nameof(LogError));
            this.LogError(() => throw error);
            Assert.That(this.LogError(() => 4, -1), Is.EqualTo(4));

            try { throw error; }
            catch (ArgumentException err) { this.LogError(err.Message, err); }
        });

        /* ----------------------------------------------------------------- */
        ///
        /// LogFatal
        ///
        /// <summary>
        /// Fatal 系メソッドのテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void LogFatal() => Assert.DoesNotThrow(() =>
        {
            var error = new ArgumentException($"{nameof(LogFatal)} (throw)");
            this.LogFatal(nameof(LogFatal));
            this.LogFatal(() => throw error);
            Assert.That(this.LogFatal(() => 5, -1), Is.EqualTo(5));

            try { throw error; }
            catch (ArgumentException err) { this.LogFatal(err.Message, err); }
        });

        /* ----------------------------------------------------------------- */
        ///
        /// ObserveTaskException
        ///
        /// <summary>
        /// ObserveTaskException のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void ObserveTaskException() => Assert.DoesNotThrow(() =>
        {
            using (var _ = LogOperator.ObserveTaskException())
            {
                Task.Run(() => throw new ArgumentException("Test for ObserveTaskException"));
                Task.Delay(100).Wait();
            }
        });

        #endregion
    }
}
