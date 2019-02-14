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
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// LoggerTest
    ///
    /// <summary>
    /// Logger のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class LoggerTest
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
        public void LogDebug()
        {
            try
            {
                this.LogDebug(nameof(LogInfo));
                this.LogDebug(() => { }, "Action");
                Assert.That(this.LogDebug(() => 1, "Func"), Is.EqualTo(1));
                this.LogDebug(() => throw new ArgumentException($"{nameof(LogInfo)} (throw)"), "Error");
            }
            catch (ArgumentException err) { this.LogDebug(err); }
        }

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
        public void LogInfo()
        {
            var error = new ArgumentException($"{nameof(LogInfo)} (throw)");
            this.LogInfo(nameof(LogInfo));
            this.LogInfo(Assembly.GetExecutingAssembly());

            try { throw error; }
            catch (ArgumentException err) { this.LogInfo(err); }
        }

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
        public void LogWarn()
        {
            var error = new ArgumentException($"{nameof(LogWarn)} (throw)");
            this.LogWarn(nameof(LogWarn));
            this.LogWarn(() => throw error);
            Assert.That(this.LogWarn(() => 3), Is.EqualTo(3));
            Assert.That(this.LogWarn(() => throw error, -3), Is.EqualTo(-3));

            try { throw error; }
            catch (ArgumentException err) { this.LogWarn(err); }
        }

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
        public void LogError()
        {
            var error = new Win32Exception(0);
            this.LogError(nameof(LogError));
            this.LogError(() => throw error);
            Assert.That(this.LogError(() => 4), Is.EqualTo(4));
            Assert.That(this.LogError(() => throw error, -4), Is.EqualTo(-4));

            try { throw error; }
            catch (Win32Exception err) { this.LogError(err); }
        }

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
        public void LogFatal()
        {
            var error = new Win32Exception(0);
            this.LogFatal(nameof(LogFatal));
            this.LogFatal(() => throw error);
            Assert.That(this.LogFatal(() => 5), Is.EqualTo(5));
            Assert.That(this.LogFatal(() => throw error, -5), Is.EqualTo(-5));

            try { throw error; }
            catch (Win32Exception err) { this.LogFatal(err); }
        }

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
            using (Logger.ObserveTaskException())
            {
                Task.Run(() => throw new ArgumentException("Test for ObserveTaskException"));
                Task.Delay(100).Wait();
            }
        });

        #endregion
    }
}
