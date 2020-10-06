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
using System.Threading.Tasks;
using Cube.DataContract;
using Cube.Mixin.Assembly;
using NUnit.Framework;

namespace Cube.FileSystem.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// SettingFolderTest
    ///
    /// <summary>
    /// Tests the SettingFolder class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class SettingFolderTest : RegistryFixture
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// Executes the test for creating a new instance of the
        /// SettingsFolder class with the specified format.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(Format.Registry, 1)]
        [TestCase(Format.Json    , 0)]
        [TestCase(Format.Xml     , 0)]
        public void Create(Format format, int count)
        {
            var n    = 0;
            var dest = new SettingFolder<Person>(Assembly, format);

            dest.Loaded += (s, e) => ++n;

            Assert.That(dest.TryLoad(), Is.EqualTo(count > 0));
            Assert.That(n, Is.EqualTo(count));
            Assert.That(dest.Format, Is.EqualTo(format));
            Assert.That(dest.Value, Is.Not.Null);
            Assert.That(dest.Version.ToString(), Is.EqualTo("3.0.0.0"));
            Assert.That(dest.Location, Does.EndWith("Cube.FileSystem.Tests"));

            var asm = dest.Assembly;
            Assert.That(asm.GetCompany(), Is.EqualTo("CubeSoft"));
            Assert.That(asm.GetProduct(), Is.EqualTo("Cube.FileSystem.Tests"));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// Executes the test for loading from registry.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Load()
        {
            var fmt  = Format.Registry;
            var name = GetKeyName(Default);

            using (var src = new SettingFolder<Person>(Assembly, fmt, name))
            {
                src.AutoSave = false;
                src.Load();

                var dest = src.Value;
                Assert.That(dest.Name,            Is.EqualTo("山田太郎"));
                Assert.That(dest.Age,             Is.EqualTo(52));
                Assert.That(dest.Sex,             Is.EqualTo(Sex.Male));
                Assert.That(dest.Reserved,        Is.EqualTo(true));
                Assert.That(dest.Creation,        Is.EqualTo(new DateTime(2015, 3, 16, 2, 32, 26, DateTimeKind.Utc).ToLocalTime()));
                Assert.That(dest.Identification,  Is.EqualTo(1357));
                Assert.That(dest.Secret,          Is.EqualTo("secret message"));
                Assert.That(dest.Contact.Type,    Is.EqualTo("Phone"));
                Assert.That(dest.Contact.Value,   Is.EqualTo("090-1234-5678"));
                Assert.That(dest.Others.Count,    Is.EqualTo(2));
                Assert.That(dest.Others[0].Type,  Is.EqualTo("PC"));
                Assert.That(dest.Others[0].Value, Is.EqualTo("pc@example.com"));
                Assert.That(dest.Others[1].Type,  Is.EqualTo("Mobile"));
                Assert.That(dest.Others[1].Value, Is.EqualTo("mobile@example.com"));
                Assert.That(dest.Messages.Length, Is.EqualTo(3));
                Assert.That(dest.Messages[0],     Is.EqualTo("1st message"));
                Assert.That(dest.Messages[1],     Is.EqualTo("2nd message"));
                Assert.That(dest.Messages[2],     Is.EqualTo("3rd message"));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Load_Throws
        ///
        /// <summary>
        /// Confirms the behavior when the specified file does not exist.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(Format.Json)]
        [TestCase(Format.Xml )]
        public void Load_Throws(Format format) => Assert.That(
            () => new SettingFolder<Person>(Assembly, format).Load(),
            Throws.InstanceOf<System.IO.IOException>()
        );

        /* ----------------------------------------------------------------- */
        ///
        /// ReLoad
        ///
        /// <summary>
        /// Executes the test for loading twice.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void ReLoad()
        {
            var count = 0;
            var name  = GetKeyName(Default);
            var src   = new SettingFolder<Person>(Assembly, Format.Registry, name);

            src.Loaded += (s, e) => ++count;
            src.AutoSave = false;
            src.Load();
            src.Value.Name = "Before ReLoad";
            src.Load();

            Assert.That(count, Is.EqualTo(2), nameof(src.Loaded));
            Assert.That(src.Value.Name, Is.EqualTo("山田太郎"));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AutoSave
        ///
        /// <summary>
        /// Executes the test for automatically saving the settings.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void AutoSave()
        {
            var key    = nameof(AutoSave);
            var name   = GetKeyName(key);
            var save   = 0;
            var change = 0;
            var delay  = TimeSpan.FromMilliseconds(100);

            using (var src = new SettingFolder<Person>(Assembly, Format.Registry, name))
            {
                src.Saved           += (s, e) => ++save;
                src.PropertyChanged += (s, e) => ++change;

                src.AutoSave      = true;
                src.AutoSaveDelay = delay;
                src.Value.Name    = "AutoSave";
                src.Value.Age     = 77;
                src.Value.Sex     = Sex.Female;
                src.Value.Secret  = "SecretChanged";

                Task.Delay(TimeSpan.FromTicks(delay.Ticks * 2)).Wait();
            }

            Assert.That(save,   Is.EqualTo(2), "Saved");
            Assert.That(change, Is.EqualTo(4), "PropertyChanged");

            using (var dest = OpenSubKey(key))
            {
                Assert.That(dest.GetValue("Name"),   Is.EqualTo("AutoSave"));
                Assert.That(dest.GetValue("Age"),    Is.EqualTo(77));
                Assert.That(dest.GetValue("Sex"),    Is.EqualTo(1));
                Assert.That(dest.GetValue("Secret"), Is.Null);
            }
        }

        #endregion
    }
}
