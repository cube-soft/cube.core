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
using Cube.DataContract;
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
    class SettingsFolderTest : RegistryHelper
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
            Assert.That(dest.Format,         Is.EqualTo(Format.Registry));
            Assert.That(dest.Location,       Does.StartWith("Software"));
            Assert.That(dest.Location,       Does.EndWith(AssemblyReader.Default.Product));
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
            var dest = new SettingsFolder<Person>(Format.Json);
            var root = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            Assert.That(dest.Format,         Is.EqualTo(Format.Json));
            Assert.That(dest.Location,       Does.StartWith(root));
            Assert.That(dest.Location,       Does.EndWith(AssemblyReader.Default.Product));
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
            var name = CreateSubKeyName(DefaultSubKeyName);
            using (var src = new SettingsFolder<Person>(Format.Registry, name))
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
            var dest     = default(RegistryKey).Deserialize<Person>();
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
        [TestCase(Format.Xml,  "Settings.xml",     ExpectedResult = "John Lennon")]
        [TestCase(Format.Json, "Settings.json",    ExpectedResult = "Mike Davis")]
        [TestCase(Format.Xml,  "Settings.ja.xml",  ExpectedResult = "鈴木一朗")]
        [TestCase(Format.Json, "Settings.ja.json", ExpectedResult = "山田太郎")]
        public string Load_File(Format format, string filename) =>
            format.Deserialize<Person>(Example(filename)).Name;

        /* ----------------------------------------------------------------- */
        ///
        /// Load_Stream_Throws
        ///
        /// <summary>
        /// Format.Registry を指定した状態でストリームから読み込んだ時の
        /// 挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Load_Stream_Throws() => Assert.That(
            () =>
            {
                using (var ss = File.OpenRead(Example("Settings.xml")))
                {
                    Format.Registry.Deserialize<Person>(ss);
                }
            },
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
            var name = CreateSubKeyName(nameof(Save_Registry));

            Format.Registry.Serialize(name, Person.CreateDummy());
            Format.Registry.Serialize(name, default(Person)); // ignore

            using (var k = OpenSubKey(nameof(Save_Registry)))
            {
                var time = new DateTime(2014, 12, 31, 23, 25, 30).ToUniversalTime();

                Assert.That(k.GetValue("Name"),     Is.EqualTo("山田花子"));
                Assert.That(k.GetValue("Age"),      Is.EqualTo(15));
                Assert.That(k.GetValue("Sex"),      Is.EqualTo(1));
                Assert.That(k.GetValue("Reserved"), Is.EqualTo(1));
                Assert.That(k.GetValue("Creation"), Is.EqualTo(time.ToString("o")));
                Assert.That(k.GetValue("ID"),       Is.EqualTo(123));
                Assert.That(k.GetValue("Secret"),   Is.Null);

                using (var sk = k.OpenSubKey("Contact", false))
                {
                    Assert.That(sk.GetValue("Type"),  Is.EqualTo("Phone"));
                    Assert.That(sk.GetValue("Value"), Is.EqualTo("080-9876-5432"));
                }

                using (var sk = k.OpenSubKey("Others", false))
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
        public void Save_RegistryIsNull() => Assert.DoesNotThrow(
            () => default(RegistryKey).Serialize(Person.CreateDummy())
        );

        /* ----------------------------------------------------------------- */
        ///
        /// Save_File
        ///
        /// <summary>
        /// 設定内容をファイルに書き込むテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(Format.Xml,  "Person.xml")]
        [TestCase(Format.Json, "Person.json")]
        public void Save_File(Format format, string filename)
        {
            var dest = Result(filename);
            format.Serialize(dest, Person.CreateDummy());
            Assert.That(new FileInfo(dest).Length, Is.AtLeast(1));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Save_Stream_Throws
        ///
        /// <summary>
        /// Format.Registry を指定した状態でストリームに保存した時の挙動を
        /// 確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Save_Stream_Throws() => Assert.That(
            () =>
            {
                using (var ss = File.Create(Result("Person.reg")))
                {
                    Format.Registry.Serialize(ss, Person.CreateDummy());
                }
            },
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
            var key    = nameof(AutoSave);
            var save   = 0;
            var change = 0;
            var delay  = TimeSpan.FromMilliseconds(100);

            using (var src = new SettingsFolder<Person>(Format.Registry, CreateSubKeyName(key)))
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

            using (var dest = OpenSubKey(key))
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
            var key  = CreateSubKeyName(DefaultSubKeyName);
            var src  = new SettingsFolder<Person>(Format.Registry, key);
            src.Loaded += (s, e) => ++save;
            src.AutoSave = false;

            src.Load();
            src.Value.Name = "Before ReLoad";
            src.Load();

            Assert.That(save,           Is.EqualTo(2), "Saved");
            Assert.That(src.Value.Name, Is.EqualTo("山田太郎"));
        }

        #endregion
    }
}
