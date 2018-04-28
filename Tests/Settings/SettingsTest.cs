﻿/* ------------------------------------------------------------------------- */
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
using Cube.Settings;
using Microsoft.Win32;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

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
    class SettingsTest : FileHelper
    {
        #region Tests

        #region Create

        /* ----------------------------------------------------------------- */
        ///
        /// Create_Registry
        ///
        /// <summary>
        /// SettingsFolder を規定値で初期化するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_Registry()
        {
            var count = 0;
            var dest = new SettingsFolder<Person>();
            dest.Loaded += (s, e) => ++count;
            dest.Load();

            Assert.That(count,               Is.EqualTo(1));
            Assert.That(dest.Type,           Is.EqualTo(SettingsType.Registry));
            Assert.That(dest.Path,           Does.StartWith("Software"));
            Assert.That(dest.Path,           Does.EndWith(AssemblyReader.Default.Product));
            Assert.That(dest.Company,        Is.EqualTo(AssemblyReader.Default.Company));
            Assert.That(dest.Product,        Is.EqualTo(AssemblyReader.Default.Product));
            Assert.That(dest.Version.Number, Is.EqualTo(AssemblyReader.Default.Version));
            Assert.That(dest.Value,          Is.Not.Null);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create_Json
        ///
        /// <summary>
        /// SettingsFolder に SettingsType.Json 指定して初期化する
        /// テストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_Json()
        {
            var dest = new SettingsFolder<Person>(SettingsType.Json);
            var root = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            Assert.That(dest.Type,           Is.EqualTo(SettingsType.Json));
            Assert.That(dest.Path,           Does.StartWith(root));
            Assert.That(dest.Path,           Does.EndWith(AssemblyReader.Default.Product));
            Assert.That(dest.Company,        Is.EqualTo(AssemblyReader.Default.Company));
            Assert.That(dest.Product,        Is.EqualTo(AssemblyReader.Default.Product));
            Assert.That(dest.Version.Number, Is.EqualTo(AssemblyReader.Default.Version));
            Assert.That(dest.Value,          Is.Not.Null);
        }

        #endregion

        #region Load

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
            using (var src = new SettingsFolder<Person>(SettingsType.Registry, LoadKeyName))
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
            var dest     = default(RegistryKey).Load<Person>();
            var expected = new Person();

            Assert.That(dest.Identification, Is.EqualTo(expected.Identification));
            Assert.That(dest.Name,           Is.EqualTo(expected.Name));
            Assert.That(dest.Age,            Is.EqualTo(expected.Age));
            Assert.That(dest.Sex,            Is.EqualTo(expected.Sex));
            Assert.That(dest.Reserved,       Is.EqualTo(expected.Reserved));
            Assert.That(dest.Creation,       Is.EqualTo(expected.Creation));
            Assert.That(dest.Secret,         Is.EqualTo(expected.Secret));
            Assert.That(dest.Contact.Type,   Is.EqualTo(expected.Contact.Type));
            Assert.That(dest.Contact.Value,  Is.EqualTo(expected.Contact.Value));
            Assert.That(dest.Others.Count,   Is.EqualTo(0));
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
        [TestCase(SettingsType.Xml,  "Settings.xml",     ExpectedResult = "John Lennon")]
        [TestCase(SettingsType.Json, "Settings.json",    ExpectedResult = "Mike Davis")]
        [TestCase(SettingsType.Xml,  "Settings.ja.xml",  ExpectedResult = "鈴木一朗")]
        [TestCase(SettingsType.Json, "Settings.ja.json", ExpectedResult = "山田太郎")]
        public string Load_File(SettingsType type, string filename) =>
            type.Load<Person>(Example(filename)).Name;

        /* ----------------------------------------------------------------- */
        ///
        /// Load_File_Throws
        ///
        /// <summary>
        /// ファイルから設定を読み込みに失敗するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(SettingsType.Unknown, "Settings.xml")]
        public void Load_File_Throws(SettingsType type, string filename) => Assert.That(
            () => type.Load<Person>(Example(filename)),
            Throws.TypeOf<ArgumentException>()
        );

        /* ----------------------------------------------------------------- */
        ///
        /// Load_File_Default
        ///
        /// <summary>
        /// SettingsFolder.Load 失敗時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(SettingsType.Unknown, "Settings.xml")]
        public void Load_File_Default(SettingsType type, string filename)
        {
            var src = new SettingsFolder<Person>(type, Example(filename));
            src.Load();
            Assert.That(src.Value, Is.Null);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Load_Stream
        ///
        /// <summary>
        /// ストリームから設定を読み込むテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(SettingsType.Xml,  "Settings.xml",     ExpectedResult = "John Lennon")]
        [TestCase(SettingsType.Json, "Settings.json",    ExpectedResult = "Mike Davis")]
        [TestCase(SettingsType.Xml,  "Settings.ja.xml",  ExpectedResult = "鈴木一朗")]
        [TestCase(SettingsType.Json, "Settings.ja.json", ExpectedResult = "山田太郎")]
        public string Load_Stream(SettingsType type, string filename)
        {
            using (var ss = File.OpenRead(Example(filename))) return type.Load<Person>(ss).Name;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Load_Stream_Throws
        ///
        /// <summary>
        /// ストリームから設定を読み込みに失敗するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(SettingsType.Registry, "Settings.xml")]
        [TestCase(SettingsType.Unknown,  "Settings.xml")]
        public void Load_Stream_Throws(SettingsType type, string filename) => Assert.That(
            () => { using (var ss = File.OpenRead(Example(filename))) type.Load<Person>(ss); },
            Throws.TypeOf<ArgumentException>()
        );

        #endregion

        #region Save

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
            SettingsType.Registry.Save(SaveKeyName, CreatePerson());
            SettingsType.Registry.Save(SaveKeyName, default(Person)); // ignore

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

                using (var sk = key.OpenSubKey("Contact", false))
                {
                    Assert.That(sk.GetValue("Type"),  Is.EqualTo("Phone"));
                    Assert.That(sk.GetValue("Value"), Is.EqualTo("080-9876-5432"));
                }

                using (var sk = key.OpenSubKey("Others", false))
                {
                    using (var ssk = sk.OpenSubKey("0", false))
                    {
                        Assert.That(ssk.GetValue("Type"),  Is.EqualTo("PC"));
                        Assert.That(ssk.GetValue("Value"), Is.EqualTo("pc@example.com"));
                    }

                    using (var ssk = sk.OpenSubKey("1", false))
                    {
                        Assert.That(ssk.GetValue("Type"),  Is.EqualTo("Mobile"));
                        Assert.That(ssk.GetValue("Value"), Is.EqualTo("mobile@example.com"));
                    }
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
        public void Save_RegistryIsNull() =>
            Assert.DoesNotThrow(() => default(RegistryKey).Save(CreatePerson()));

        /* ----------------------------------------------------------------- */
        ///
        /// Save_File
        ///
        /// <summary>
        /// 設定内容をファイルに書き込むテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(SettingsType.Xml,  "Person.xml")]
        [TestCase(SettingsType.Json, "Person.json")]
        public void Save_File(SettingsType type, string filename)
        {
            var dest = Result(filename);
            type.Save(dest, CreatePerson());
            Assert.That(new FileInfo(dest).Length, Is.AtLeast(1));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Save_File_Throws
        ///
        /// <summary>
        /// 設定の保存に失敗するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(SettingsType.Unknown,  "Person.txt")]
        public void Save_File_Throws(SettingsType type, string filename) => Assert.That(
            () => type.Save(Result(filename), CreatePerson()),
            Throws.TypeOf<ArgumentException>()
        );

        /* ----------------------------------------------------------------- */
        ///
        /// Save_Stream
        ///
        /// <summary>
        /// 設定内容をストリームに書き込むテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(SettingsType.Xml,  "PersonStream.xml")]
        [TestCase(SettingsType.Json, "PersonStream.json")]
        public void Save_Stream(SettingsType type, string filename)
        {
            var dest = Result(filename);
            using (var ss = File.Create(dest)) type.Save(ss, CreatePerson());
            Assert.That(new FileInfo(dest).Length, Is.AtLeast(1));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Save_Stream_Throws
        ///
        /// <summary>
        /// 設定の保存に失敗するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(SettingsType.Registry, "Person.reg")]
        [TestCase(SettingsType.Unknown,  "Person.txt")]
        public void Save_Stream_Throws(SettingsType type, string filename) => Assert.That(
            () => { using (var ss = File.Create(Result(filename))) type.Save(ss, CreatePerson()); },
            Throws.TypeOf<ArgumentException>()
        );

        #endregion

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
        public void AutoSave()
        {
            var save   = 0;
            var change = 0;
            var delay  = TimeSpan.FromMilliseconds(100);

            using (var src = new SettingsFolder<Person>(SettingsType.Registry, SaveKeyName))
            {
                src.Saved           += (s, e) => ++save;
                src.PropertyChanged += (s, e) => ++change;

                src.AutoSave      = true;
                src.AutoSaveDelay = delay;
                src.Value.Name    = "AutoSave";
                src.Value.Age     = 77;
                src.Value.Sex     = Sex.Female;

                Task.Delay(TimeSpan.FromTicks(delay.Ticks * 2)).Wait();
            }

            Assert.That(save,   Is.EqualTo(2), "Saved");
            Assert.That(change, Is.EqualTo(3), "PropertyChanged");

            using (var dest = OpenSaveKey())
            {
                Assert.That(dest.GetValue("Name"), Is.EqualTo("AutoSave"));
                Assert.That(dest.GetValue("Age"),  Is.EqualTo(77));
                Assert.That(dest.GetValue("Sex"),  Is.EqualTo(1));
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
            var save = 0;
            var src  = new SettingsFolder<Person>(SettingsType.Registry, LoadKeyName);
            src.Loaded += (s, e) => ++save;
            src.AutoSave = false;

            src.Load();
            src.Value.Name = "Before ReLoad";
            src.Load();

            Assert.That(save,           Is.EqualTo(2), "Saved");
            Assert.That(src.Value.Name, Is.EqualTo("山田太郎"));
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
        public void Setup() => Registry.CurrentUser.DeleteSubKeyTree(SaveKeyName, false);

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
                key.SetValue("ID",                    1357);
                key.SetValue(nameof(Person.Name),     "山田太郎");
                key.SetValue(nameof(Person.Sex),      0);
                key.SetValue(nameof(Person.Age),      52);
                key.SetValue(nameof(Person.Creation), "2015/03/16 02:32:26");
                key.SetValue(nameof(Person.Reserved), 1);

                using (var sk = key.CreateSubKey(nameof(Person.Contact)))
                {
                    sk.SetValue(nameof(Address.Type),  "Phone");
                    sk.SetValue(nameof(Address.Value), "090-1234-5678");
                }

                using (var sk = key.CreateSubKey(nameof(Person.Others)))
                {
                    using (var ssk = sk.CreateSubKey("0")) SetAddress(ssk, "PC", "pc@example.com");
                    using (var ssk = sk.CreateSubKey("1")) SetAddress(ssk, "Mobile", "mobile@example.com");
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
            Sex            = Sex.Female,
            Age            = 15,
            Creation       = new DateTime(2014, 12, 31, 23, 25, 30),
            Contact        = new Address { Type = "Phone", Value = "080-9876-5432" },
            Others         = CreateOthers(),
            Reserved       = true,
            Secret         = "dummy data"
        };

        /* ----------------------------------------------------------------- */
        ///
        /// CreateOthers
        ///
        /// <summary>
        /// Address[] オブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Address[] CreateOthers() => new[]
        {
            new Address { Type = "PC",     Value = "pc@example.com" },
            new Address { Type = "Mobile", Value = "mobile@example.com" }
        };

        /* ----------------------------------------------------------------- */
        ///
        /// SetAddress
        ///
        /// <summary>
        /// レジストリに Address オブジェクトに対応する値を設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void SetAddress(RegistryKey src, string type, string value)
        {
            src.SetValue(nameof(Address.Type),  type);
            src.SetValue(nameof(Address.Value), value);
        }

        #region Registry information
        private string LoadKeyName => $@"Software\CubeSoft\Settings_LoadTest";
        private string SaveKeyName => $@"Software\CubeSoft\Settings_SaveTest";
        private RegistryKey CreateLoadKey() => Registry.CurrentUser.CreateSubKey(LoadKeyName);
        private RegistryKey OpenSaveKey() => Registry.CurrentUser.OpenSubKey(SaveKeyName, false);
        #endregion

        #endregion
    }
}
