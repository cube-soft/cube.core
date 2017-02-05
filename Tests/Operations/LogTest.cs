/* ------------------------------------------------------------------------- */
///
/// LogTest.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///  http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
///
/* ------------------------------------------------------------------------- */
using System;
using NUnit.Framework;
using Cube.Log;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// GenericsTest
    /// 
    /// <summary>
    /// 拡張メソッドのテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Parallelizable]
    [TestFixture]
    class LogTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Log_Debug
        ///
        /// <summary>
        /// LogDebug(T, string) 拡張メソッドのテストを行います。。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Log_Debug()
        {
            Assert.DoesNotThrow(() => this.LogDebug("Debug"));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Log_Info
        ///
        /// <summary>
        /// LogInfo(T, string) 拡張メソッドのテストを行います。。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Log_Info()
        {
            Assert.DoesNotThrow(() => this.LogInfo("Info"));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Log_Warn
        ///
        /// <summary>
        /// LogWarn(T, string) 拡張メソッドのテストを行います。。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Log_Warn()
        {
            Assert.DoesNotThrow(() => this.LogWarn("Warn"));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Log_Error
        ///
        /// <summary>
        /// LogError(T, string) 拡張メソッドのテストを行います。。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Log_Error()
        {
            Assert.DoesNotThrow(() => this.LogError("Error"));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Log_Fatal
        ///
        /// <summary>
        /// LogFatal(T, string) 拡張メソッドのテストを行います。。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Log_Fatal()
        {
            Assert.DoesNotThrow(() => this.LogFatal("Fatal"));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Log_Exception
        ///
        /// <summary>
        /// LogException(Action) 拡張メソッドのテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Log_Exception()
        {
            Assert.DoesNotThrow(() => this.LogException(() =>
            {
                throw new ArgumentException("dummy exception");
            }));
        }
    }
}
