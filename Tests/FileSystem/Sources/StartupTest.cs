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
using System.Reflection;
using Cube.Mixin.String;
using Cube.Tests;
using NUnit.Framework;

namespace Cube.FileSystem.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// StartupTest
    ///
    /// <summary>
    /// Tests the Startup class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class StartupTest : FileFixture
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// Confirms the default values of properties.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create()
        {
            var src = new Startup("Test");
            Assert.That(src.Name,    Is.EqualTo("Test"));
            Assert.That(src.Command, Is.Null);
            Assert.That(src.Enabled, Is.False);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// Tests the Load method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Load()
        {
            var exec = Assembly.GetExecutingAssembly().Location;
            var name = IO.Get(exec).BaseName;
            var src  = new Startup(name) { Command = exec };
            src.Load();
            Assert.That(src.Enabled, Is.False);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// Tests the Save method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Save()
        {
            var exec = Assembly.GetExecutingAssembly().Location;
            var name = IO.Get(exec).BaseName;
            var cmd  = exec.Quote();

            var s0 = new Startup(name)
            {
                Command = cmd,
                Enabled = true
            };
            s0.Save();

            var s1 = new Startup(name);
            s1.Load();
            Assert.That(s1.Enabled, Is.True);
            Assert.That(s1.Command, Is.EqualTo(cmd));

            s1.Enabled = false;
            s1.Save();

            var s2 = new Startup(name);
            s2.Load();
            Assert.That(s2.Enabled, Is.False);
        }

        #endregion
    }
}
