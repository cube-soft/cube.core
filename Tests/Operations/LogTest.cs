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
using Cube.Log;

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
                this.LogDebug("OK", () => { });
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
                this.LogInfo("OK", () => { });
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
            this.LogWarn(nameof(LogWarn));
            this.LogWarn(() => { });
            this.LogWarn(() => throw new ArgumentException($"{nameof(LogWarn)} (throw)"));
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
            this.LogError(nameof(LogError));
            this.LogError(() => { });
            this.LogError(() => throw new ArgumentException($"{nameof(LogError)} (throw)"));
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
            this.LogFatal(nameof(LogFatal));
            this.LogFatal(() => { });
            this.LogFatal(() => throw new ArgumentException($"{nameof(LogFatal)} (throw)"));
        });

        #endregion
    }
}
