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
using System.IO;
using System.Threading.Tasks;
using Microsoft.Win32;
using NUnit.Framework;
using Cube.Settings;

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
    [TestFixture]
    class SettingsTest : FileHandler
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// SettingsFolder
        /// 
        /// <summary>
        /// SettingsFolder を規定値で初期化するテストを実行します。
        /// </summary>
        /// 
        /// <remarks>
        /// 通常は実行時の Assembly オブジェクトを基に初期化されますが、
        /// NUnit 経由では Assembly オブジェクトが見つからないため、
        /// null で初期化されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void SettingsFolder()
        {
            var count    = 0;
            var settings = new SettingsFolder<Person>();
            settings.Loaded += (s, e) => ++count;
            settings.Load();

            Assert.That(count, Is.EqualTo(1));
            Assert.That(settings.Company, Is.EqualTo(""));
            Assert.That(settings.Product, Is.EqualTo(""));
            Assert.That(settings.Version.ToString(false), Is.EqualTo("1.0.0.0"));
            Assert.That(settings.Value, Is.Not.Null);
        }

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
            using (var s = new SettingsFolder<Person>(Company, Product))
            {
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
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Load_RegistryIsNull
        /// 
        /// <summary>
        /// 無効なレジストリキーを設定した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Load_RegistryIsNull()
        {
            var actual   = default(RegistryKey).Load<Person>();
            var expected = new Person();

            Assert.That(actual.Identification, Is.EqualTo(expected.Identification));
            Assert.That(actual.Name,           Is.EqualTo(expected.Name));
            Assert.That(actual.Age,            Is.EqualTo(expected.Age));
            Assert.That(actual.Sex,            Is.EqualTo(expected.Sex));
            Assert.That(actual.Reserved,       Is.EqualTo(expected.Reserved));
            Assert.That(actual.Creation,       Is.EqualTo(expected.Creation));
            Assert.That(actual.Secret,         Is.EqualTo(expected.Secret));
            Assert.That(actual.Phone.Type,     Is.EqualTo(expected.Phone.Type));
            Assert.That(actual.Phone.Value,    Is.EqualTo(expected.Phone.Value));
            Assert.That(actual.Email.Type,     Is.EqualTo(expected.Email.Type));
            Assert.That(actual.Email.Value,    Is.EqualTo(expected.Email.Value));
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
        [TestCase(FileType.Xml,     "Settings.xml",     ExpectedResult = "John Lennon")]
        [TestCase(FileType.Json,    "Settings.json",    ExpectedResult = "Mike Davis")]
        [TestCase(FileType.Xml,     "Settings.ja.xml",  ExpectedResult = "鈴木一朗")]
        [TestCase(FileType.Json,    "Settings.ja.json", ExpectedResult = "山田太郎")]
        [TestCase(FileType.Unknown, "Settings.xml",     ExpectedResult = null)]
        public string Load_File(FileType type, string filename)
            => type.Load<Person>(Example(filename))?.Name;

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
            using (var key = CreateSaveKey()) Cube.Settings.Operations.Save(key, default(Person)); // ignore
            using (var key = OpenSaveKey())
            {
                var time = new DateTime(2014, 12, 31, 23, 25, 30).ToUniversalTime();

                Assert.That(key.GetValue("Name"),     Is.EqualTo("山田花子"));
                Assert.That(key.GetValue("Age"),      Is.EqualTo(15));
                Assert.That(key.GetValue("Sex"),      Is.EqualTo(1));
                Assert.That(key.GetValue("Reserved"), Is.EqualTo(1));
                Assert.That(key.GetValue("Creation"), Is.EqualTo(time.ToString("o")));
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
        /// Save_RegistryIsNull
        /// 
        /// <summary>
        /// 無効なレジストリに保存した時の挙動を確認します。
        /// </summary>
        /// 
        /// <remarks>
        /// RegistryKey が null の場合、処理は無視されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Save_RegistryIsNull()
            => Assert.DoesNotThrow(() => default(RegistryKey).Save(CreatePerson()));

        /* ----------------------------------------------------------------- */
        ///
        /// Save_File
        /// 
        /// <summary>
        /// オブジェクトの内容がファイルに書き込まれた事を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(FileType.Xml,     "Person.xml")]
        [TestCase(FileType.Json,    "Person.json")]
        public void Save_File(FileType type, string filename)
        {
            var dest = Result(filename);
            type.Save(dest, CreatePerson());
            Assert.That(File.Exists(dest), Is.True);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Save_Unknown
        /// 
        /// <summary>
        /// 無効な FileType を指定して保存した時のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Save_Unknown()
        {
            var dest = Result("Person.txt");
            Assert.That(
                () => FileType.Unknown.Save(dest, CreatePerson()),
                Throws.TypeOf<ArgumentException>()
            );
            Assert.That(File.Exists(dest), Is.False);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AutoSave
        /// 
        /// <summary>
        /// 自動保存機能のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public async Task AutoSave()
        {
            var count  = 0;
            var change = 0;
            var delay  = TimeSpan.FromMilliseconds(100);

            using (var settings = new SettingsFolder<Person>(Company, "Settings_SaveTest"))
            {
                settings.Saved           += (s, e) => ++count;
                settings.PropertyChanged += (s, e) => ++change;

                settings.AutoSave      = true;
                settings.AutoSaveDelay = delay;
                settings.Value.Name    = "AutoSave";
                settings.Value.Age     = 77;
                settings.Value.Sex     = Sex.Female;

                await Task.Delay(TimeSpan.FromTicks(delay.Ticks * 2));
            }

            using (var key = OpenSaveKey())
            {
                Assert.That(count,  Is.EqualTo(1));
                Assert.That(change, Is.EqualTo(3));
                Assert.That(key.GetValue("Name"), Is.EqualTo("AutoSave"));
                Assert.That(key.GetValue("Age"),  Is.EqualTo(77));
                Assert.That(key.GetValue("Sex"),  Is.EqualTo(1));
            }
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

            var settings = new SettingsFolder<Person>(Company, Product) { AutoSave = false };
            settings.Loaded += (s, e) => ++count;

            settings.Load();
            settings.Value.Name = "Before ReLoad";
            settings.Load();
            
            Assert.That(count, Is.EqualTo(2));
            Assert.That(settings.Value.Name, Is.EqualTo("佐藤栄作"));
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
                key.SetValue("Creation", "2015/03/16 02:32:26");
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
