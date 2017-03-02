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
        /// Load_Registry_String
        /// 
        /// <summary>
        /// string オブジェクトを読み込むテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(ExpectedResult = "佐藤栄作")]
        public string Load_Registry_String()
            => Loaded.Value.Name;

        /* ----------------------------------------------------------------- */
        ///
        /// Load_Registry_Integer
        /// 
        /// <summary>
        /// int オブジェクトを読み込むテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(ExpectedResult = 52)]
        public int Load_Registry_Integer()
            => Loaded.Value.Age;

        /* ----------------------------------------------------------------- */
        ///
        /// Enum
        /// 
        /// <summary>
        /// Enum オブジェクトを読み込むテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(ExpectedResult = Sex.Male)]
        public Sex Load_Registry_Enum()
            => Loaded.Value.Sex;

        /* ----------------------------------------------------------------- */
        ///
        /// Load_Registry_Boolean
        /// 
        /// <summary>
        /// bool オブジェクトを読み込むテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(ExpectedResult = true)]
        public bool Load_Registry_Boolean()
            => Loaded.Value.Reserved;

        /* ----------------------------------------------------------------- */
        ///
        /// Load_Registry_DateTime
        /// 
        /// <summary>
        /// DateTime オブジェクトを読み込むテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(2015, 3, 16, 2, 32, 26)]
        public void Load_Registry_DateTime(int y, int m, int d, int hh, int mm, int ss)
            => Assert.That(
                Loaded.Value.Creation,
                Is.EqualTo(new DateTime(y, m, d, hh, mm, ss, DateTimeKind.Utc).ToLocalTime())
            );

        /* ----------------------------------------------------------------- */
        ///
        /// Load_Registry_Alias
        /// 
        /// <summary>
        /// Name 属性で別名を付けられたオブジェクトを読み込むテストを
        /// 実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(ExpectedResult = 1357)]
        public int Load_Registry_Alias()
            => Loaded.Value.Identification;

        /* ----------------------------------------------------------------- */
        ///
        /// Load_Registry_NoMember
        /// 
        /// <summary>
        /// DataMember 属性のないプロパティは読み込まれない事を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(ExpectedResult = "secret message")]
        public string Load_Registry_NoMember()
            => Loaded.Value.Secret;

        /* ----------------------------------------------------------------- */
        ///
        /// Load_Registry_Class
        /// 
        /// <summary>
        /// ユーザ定義クラスのオブジェクトを読み込むテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Load_Registry_Class()
        {
            Assert.That(Loaded.Value.Phone.Type,  Is.EqualTo("Mobile"));
            Assert.That(Loaded.Value.Phone.Value, Is.EqualTo("090-1234-5678"));
            Assert.That(Loaded.Value.Email.Type,  Is.EqualTo("Email"));
            Assert.That(Loaded.Value.Email.Value, Is.Null.Or.Empty);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Load_File
        /// 
        /// <summary>
        /// ファイルから読み込むテストを実行します。
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
        /// Save_Registry_String
        /// 
        /// <summary>
        /// string オブジェクトが書き込まれた事を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(ExpectedResult = "山田花子")]
        public string Save_Registry_String()
            => Saved.GetValue("Name") as string;

        /* ----------------------------------------------------------------- */
        ///
        /// Save_Registry_Integer
        /// 
        /// <summary>
        /// int オブジェクトが書き込まれた事を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(ExpectedResult = 15)]
        public int Save_Registry_Integer()
            => (int)Saved.GetValue("Age");

        /* ----------------------------------------------------------------- */
        ///
        /// Save_Registry_Enum
        /// 
        /// <summary>
        /// Enum オブジェクトが書き込まれた事を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(ExpectedResult = 1)]
        public int Save_Registry_Enum()
            => (int)Saved.GetValue("Sex");

        /* ----------------------------------------------------------------- */
        ///
        /// Save_Registry_Boolean
        /// 
        /// <summary>
        /// bool オブジェクトが書き込まれた事を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(ExpectedResult = 1)]
        public int Save_Registry_Boolean()
            => (int)Saved.GetValue("Reserved");

        /* ----------------------------------------------------------------- */
        ///
        /// Save_Registry_DateTime
        /// 
        /// <summary>
        /// DateTime オブジェクトが書き込まれた事を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(ExpectedResult = 1420035930)]
        public int Save_Registry_DateTime()
            => (int)Saved.GetValue("Creation");

        /* ----------------------------------------------------------------- */
        ///
        /// Save_Registry_Alias
        /// 
        /// <summary>
        /// Name 属性を持つオブジェクトが書き込まれた事を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(ExpectedResult = 123)]
        public int Save_Registry_Alias()
            => (int)Saved.GetValue("ID");

        /* ----------------------------------------------------------------- */
        ///
        /// Save_Registry_NonMember
        /// 
        /// <summary>
        /// DataMember 属性のないプロパティの内容は書き込まれなかった事を
        /// 確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Save_Registry_NonMember()
            => Assert.That(Saved.GetValue("Secred"), Is.Null);

        /* ----------------------------------------------------------------- */
        ///
        /// Save_Registry_Class
        /// 
        /// <summary>
        /// ユーザ定義クラスのオブジェクトが書き込まれた事を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Save_Registry_Class()
        {
            using (var subkey = Saved.OpenSubKey("Phone"))
            {
                Assert.That(subkey.GetValue("Type"),  Is.EqualTo("Mobile"));
                Assert.That(subkey.GetValue("Value"), Is.EqualTo("080-9876-5432"));
            }

            using (var subkey = Saved.OpenSubKey("Email"))
            {
                Assert.That(subkey.GetValue("Type"),  Is.EqualTo("PC"));
                Assert.That(subkey.GetValue("Value"), Is.EqualTo("dummy@example.com"));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Save_File_Exists
        /// 
        /// <summary>
        /// オブジェクトの内容がファイルに書き込まれた事を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(FileType.Xml,  "Person.xml")]
        [TestCase(FileType.Json, "Person.json")]
        public void Save_File_Exists(FileType type, string filename)
        {
            var dest = IoEx.Path.Combine(Results, filename);
            type.Save(dest, CreatePerson());
            Assert.That(IoEx.File.Exists(dest), Is.True);
        }

        #endregion

        #region Helper methods

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
            Loaded = new SettingsFolder<Person>(Company, Product);
            using (var _ = new Registrar(LoadKeyName)) Loaded.Load();

            Saved = Registry.CurrentUser.CreateSubKey(SaveKeyName);
            Saved.Save(CreatePerson());
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
            Saved.Close();
            Loaded.Dispose();
            Registry.CurrentUser.DeleteSubKeyTree(SaveKeyName);
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

        #region Internal resources

        /* ----------------------------------------------------------------- */
        ///
        /// Loaded
        /// 
        /// <summary>
        /// Registrar クラスを用いてロードした SettingsFolder(Person)
        /// オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private SettingsFolder<Person> Loaded { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Saved
        /// 
        /// <summary>
        /// Settings クラスを用いて Person オブジェクトを保存した後の
        /// RegistryKey オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private RegistryKey Saved { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// <summary>
        /// レジストリに関する情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private string Company => "CubeSoft";
        private string Product => "Settings_SaveTest";
        private string LoadKeyName => $@"Software\{Company}\{Product}";
        private string SaveKeyName => $@"Software\{Company}\{Product}";

        /* ----------------------------------------------------------------- */
        ///
        /// Registrar
        /// 
        /// <summary>
        /// レジストリにテスト用のデータを登録・削除するためのクラスです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        internal class Registrar : IDisposable
        {
            public Registrar(string name)
            {
                RegistryKeyName = name;
                Register();
            }

            public RegistryKey RegistryKey { get; private set; }

            public string RegistryKeyName { get; }

            ~Registrar() { Dispose(false); }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool diposing)
            {
                if (_disposed) return;
                _disposed = true;
                if (!diposing) return;
                RegistryKey.Close();
                Registry.CurrentUser.DeleteSubKeyTree(RegistryKeyName);
            }

            private void Register()
            {
                RegistryKey = Registry.CurrentUser.CreateSubKey(RegistryKeyName);
                RegistryKey.SetValue("ID", 1357);
                RegistryKey.SetValue("Name", "佐藤栄作");
                RegistryKey.SetValue("Sex", 0);
                RegistryKey.SetValue("Age", 52);
                RegistryKey.SetValue("Creation", 0x550640ba);
                RegistryKey.SetValue("Reserved", 1);

                using (var subkey = RegistryKey.CreateSubKey("Phone"))
                {
                    subkey.SetValue("Type", "Mobile");
                    subkey.SetValue("Value", "090-1234-5678");
                }
            }

            #region Fields
            private bool _disposed = false;
            #endregion
        }

        #endregion

        #endregion
    }
}
