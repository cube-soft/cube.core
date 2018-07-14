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
using Cube.FileSystem.TestService;
using Cube.Generics;
using NUnit.Framework;
using System.Reflection;

namespace Cube.FileSystem.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// StartupTest
    ///
    /// <summary>
    /// Startup のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class StartupTest : FileFixture
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// スタートアップ設定の読み込みテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Load()
        {
            var exec    = Assembly.GetExecutingAssembly().GetReader().Location;
            var name    = IO.Get(exec).NameWithoutExtension;
            var startup = new Startup(name, exec.Quote());
            startup.Load();
            Assert.That(startup.Enabled, Is.False);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Save_Delete
        ///
        /// <summary>
        /// スタートアップ設定の保存テストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Save_Delete()
        {
            var exec    = Assembly.GetExecutingAssembly().GetReader().Location;
            var name    = IO.Get(exec).NameWithoutExtension;
            var command = exec.Quote();

            var s0 = new Startup(name)
            {
                Command = command,
                Enabled = true
            };
            s0.Save();

            var s1 = new Startup(name);
            s1.Load();
            Assert.That(s1.Enabled, Is.True);
            Assert.That(s1.Command, Is.EqualTo(command));

            s1.Enabled = false;
            s1.Save();

            var s2 = new Startup(name);
            s2.Load();
            Assert.That(s2.Enabled, Is.False);
        }
    }
}
