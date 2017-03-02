/* ------------------------------------------------------------------------- */
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
using Microsoft.Win32;
using NUnit.Framework;
using Cube.Settings;
using IoEx = System.IO;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// SettingsTest
    /// 
    /// <summary>
    /// Settings のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Parallelizable]
    [TestFixture]
    class SettingsTest : FileResource
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Load_Registry
        /// 
        /// <summary>
        /// レジストリから設定を読み込むテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Load_Registry()
        {
            var s = new SettingsFolder<Person>(Company, Product);
            s.AutoSave = false;
            s.Load();

            Assert.That(s.Value.Name,           Is.EqualTo("佐藤栄作"));
            Assert.That(s.Value.Age,            Is.EqualTo(52));
            Assert.That(s.Value.Sex,            Is.EqualTo(Sex.Male));
            Assert.That(s.Value.Reserved,       Is.EqualTo(true));
            Assert.That(s.Value.Creation,       Is.EqualTo(new DateTime(2015, 3, 16, 2, 32, 26, DateTimeKind.Utc).ToLocalTime()));
            Assert.That(s.Value.Identification, Is.EqualTo(1357));
            Assert.That(s.Value.Secret,         Is.EqualTo("secret message"));
            Assert.That(s.Value.Phone.Type,     Is.EqualTo("Mobile"));
            Assert.That(s.Value.Phone.Value,    Is.EqualTo("090-1234-5678"));
            Assert.That(s.Value.Email.Type,     Is.EqualTo("Email"));
            Assert.That(s.Value.Email.Value,    Is.Null.Or.Empty);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ReLoad
        /// 
        /// <summary>
        /// レジストリから設定を 2 回以上読み込むテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void ReLoad()
        {
            var count = 0;

            var settings = new SettingsFolder<Person>(Company, Product);
            settings.AutoSave = false;

            settings.Load();
            settings.PropertyChanged += (s, e) => count++;
            settings.Value.Name = "Before ReLoad";
            Assert.That(count, Is.EqualTo(1));

            settings.Load();
            settings.Value.Name = "After ReLoad";
            Assert.That(count, Is.EqualTo(2));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Load_File
        /// 
        /// <summary>
        /// ファイルから設定を読み込むテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(FileType.Xml,  "Settings.xml",     ExpectedResult = "John Lennon")]
        [TestCase(FileType.Json, "Settings.json",    ExpectedResult = "Mike Davis")]
        [TestCase(FileType.Xml,  "Settings.ja.xml",  ExpectedResult = "鈴木一朗")]
        [TestCase(FileType.Json, "Settings.ja.json", ExpectedResult = "山田太郎")]
        public string Load_File(FileType type, string filename)
            => type.Load<Person>(IoEx.Path.Combine(Examples, filename)).Name;

        /* ----------------------------------------------------------------- */
        ///
        /// Save_Registry
        /// 
        /// <summary>
        /// レジストリに設定を保存するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Save_Registry()
        {
            using (var key = CreateSaveKey()) Cube.Settings.Operations.Save(key, CreatePerson());
            using (var key = OpenSaveKey())
            {
                Assert.That(key.GetValue("Name"),     Is.EqualTo("山田花子"));
                Assert.That(key.GetValue("Age"),      Is.EqualTo(15));
                Assert.That(key.GetValue("Sex"),      Is.EqualTo(1));
                Assert.That(key.GetValue("Reserved"), Is.EqualTo(1));
                Assert.That(key.GetValue("Creation"), Is.EqualTo(1420035930));
                Assert.That(key.GetValue("ID"),       Is.EqualTo(123));
                Assert.That(key.GetValue("Secret"),   Is.Null);

                using (var subkey = key.OpenSubKey("Phone"))
                {
                    Assert.That(subkey.GetValue("Type"),  Is.EqualTo("Mobile"));
                    Assert.That(subkey.GetValue("Value"), Is.EqualTo("080-9876-5432"));
                }

                using (var subkey = key.OpenSubKey("Email"))
                {
                    Assert.That(subkey.GetValue("Type"),  Is.EqualTo("PC"));
                    Assert.That(subkey.GetValue("Value"), Is.EqualTo("dummy@example.com"));
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Save_File
        /// 
        /// <summary>
        /// オブジェクトの内容がファイルに書き込まれた事を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(FileType.Xml,  "Person.xml")]
        [TestCase(FileType.Json, "Person.json")]
        public void Save_File(FileType type, string filename)
        {
            var dest = IoEx.Path.Combine(Results, filename);
            type.Save(dest, CreatePerson());
            Assert.That(IoEx.File.Exists(dest), Is.True);
        }

        #endregion

        #region Helper methods

        /* ----------------------------------------------------------------- */
        ///
        /// Setup
        ///
        /// <summary>
        /// 各テスト前に実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [SetUp]
        public void Setup()
        {
            Registry.CurrentUser.DeleteSubKeyTree(SaveKeyName, false);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OneTimeSetup
        ///
        /// <summary>
        /// 初期化処理を一度だけ実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            using (var key = CreateLoadKey())
            {
                key.SetValue("ID", 1357);
                key.SetValue("Name", "佐藤栄作");
                key.SetValue("Sex", 0);
                key.SetValue("Age", 52);
                key.SetValue("Creation", 0x550640ba);
                key.SetValue("Reserved", 1);

                using (var subkey = key.CreateSubKey("Phone"))
                {
                    subkey.SetValue("Type", "Mobile");
                    subkey.SetValue("Value", "090-1234-5678");
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OneTimeTearDown
        ///
        /// <summary>
        /// 終了処理を一度だけ実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Registry.CurrentUser.DeleteSubKeyTree(LoadKeyName, true);
            Registry.CurrentUser.DeleteSubKeyTree(SaveKeyName, false);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CreatePerson
        /// 
        /// <summary>
        /// Person オブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Person CreatePerson() => new Person
        {
            Identification = 123,
            Name           = "山田花子",
            Sex            = Tests.Sex.Female,
            Age            = 15,
            Creation       = new DateTime(2014, 12, 31, 23, 25, 30),
            Phone          = new Address { Type = "Mobile", Value = "080-9876-5432" },
            Email          = new Address { Type = "PC", Value = "dummy@example.com" },
            Reserved       = true,
            Secret         = "dummy data"
        };

        #region Registry information
        private string Company => "CubeSoft";
        private string Product => "Settings_LoadTest";
        private string LoadKeyName => $@"Software\{Company}\{Product}";
        private string SaveKeyName => $@"Software\{Company}\Settings_SaveTest";
        private RegistryKey CreateLoadKey() => Registry.CurrentUser.CreateSubKey(LoadKeyName);
        private RegistryKey CreateSaveKey() => Registry.CurrentUser.CreateSubKey(SaveKeyName);
        private RegistryKey OpenSaveKey() => Registry.CurrentUser.OpenSubKey(SaveKeyName, false);
        #endregion

        #endregion
    }
}
