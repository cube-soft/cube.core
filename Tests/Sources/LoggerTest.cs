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
using Cube.Mixin.Logger;
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
    /// Represents tests for the Logger class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class LoggerTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Debug
        ///
        /// <summary>
        /// Executes the test of the LogDebug extended methods.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Debug()
        {
            Logger.Separator = ",";
            this.LogDebug();
            this.LogDebug("Debug");
            this.LogDebug("Debug", "multiple", "messages");
            this.LogDebug(() => { }, "Action");
            Assert.That(this.LogDebug(() => 1, "Func"), Is.EqualTo(1));

            try { this.LogDebug(() => throw new ArgumentException("Debug (throw)"), "Error"); }
            catch (ArgumentException err) { this.LogDebug(err); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Info
        ///
        /// <summary>
        /// Executes the test of the LogInfo extended methods.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Info()
        {
            Logger.Separator = "\t";
            this.LogInfo();
            this.LogInfo("Info");
            this.LogInfo("Info", "multiple", "messages");
            this.LogInfo(Assembly.GetExecutingAssembly());

            try { throw new ArgumentException("Info (throw)"); }
            catch (ArgumentException err) { this.LogInfo(err); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Warn
        ///
        /// <summary>
        /// Executes the test of the LogWarn extended methods.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Warn()
        {
            Logger.Separator = ":";
            this.LogWarn();
            this.LogWarn("Warn");
            this.LogWarn("Warn", "multiple", "messages");

            var error = new ArgumentException("Warn (throw)");
            this.LogWarn(() => throw error);
            Assert.That(this.LogWarn(() => 3), Is.EqualTo(3));
            Assert.That(this.LogWarn(() => throw error, -3), Is.EqualTo(-3));

            try { throw error; }
            catch (ArgumentException err) { this.LogWarn(err); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Error
        ///
        /// <summary>
        /// Executes the test of the LogError extended methods.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Error()
        {
            this.LogError();
            this.LogError("Error");
            this.LogError("Error", "multiple", "messages");

            var error = new Win32Exception(0);
            this.LogError(() => throw error);
            Assert.That(this.LogError(() => 4), Is.EqualTo(4));
            Assert.That(this.LogError(() => throw error, -4), Is.EqualTo(-4));

            try { throw error; }
            catch (Win32Exception err) { this.LogError(err); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Fatal
        ///
        /// <summary>
        /// Executes the test of the LogFatal extended methods.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Fatal()
        {
            this.LogFatal();
            this.LogFatal("Fatal");
            this.LogFatal("Fatal", "multiple", "messages");

            var error = new Win32Exception(1);
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
        /// Executes the test of the ObserveTaskException method.
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
