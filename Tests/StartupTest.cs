/* ------------------------------------------------------------------------- */
///
/// StartupTest.cs
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
using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace Cube.Tests
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
    class StartupTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// スタートアップ設定の読み込みテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Load()
        {
            var exec = Assembly.GetExecutingAssembly().Location;
            var name = Path.GetFileNameWithoutExtension(exec);
            var startup = new Startup(name);
            Assert.DoesNotThrow(() => startup.Load());
            Assert.That(startup.Enabled, Is.False);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// スタートアップ設定の保存テストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Save()
        {
            var exec = Assembly.GetExecutingAssembly().Location;
            var name = Path.GetFileNameWithoutExtension(exec);
            var command = '"' + exec + '"';

            var s0 = new Startup(name);
            s0.Command = command;
            s0.Enabled = true;
            Assert.DoesNotThrow(() => s0.Save());

            var s1 = new Startup(name);
            Assert.DoesNotThrow(() => s1.Load());
            Assert.That(s1.Enabled, Is.True);
            Assert.That(s1.Command, Is.EqualTo(command));

            s1.Enabled = false;
            Assert.DoesNotThrow(() => s1.Save());

            var s2 = new Startup(name);
            Assert.DoesNotThrow(() => s2.Load());
            Assert.That(s2.Enabled, Is.False);
        }
    }
}
