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
        /// Load
        /// 
        /// <summary>
        /// 設定を読み込むテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        #region Load

        [TestCase(ExpectedResult = "佐藤栄作")]
        public string Load_Registry_String()
            => Loaded.Name;

        [TestCase(ExpectedResult = 52)]
        public int Load_Registry_Integer()
            => Loaded.Age;

        [TestCase(ExpectedResult = Sex.Male)]
        public Sex Load_Registry_Enum()
            => Loaded.Sex;

        [TestCase(ExpectedResult = true)]
        public bool Load_Registry_Boolean()
            => Loaded.Reserved;

        [TestCase(2015, 3, 16, 2, 32, 26)]
        public void Load_Registry_DateTime(int y, int m, int d, int hh, int mm, int ss)
            => Assert.That(
                Loaded.Creation,
                Is.EqualTo(new DateTime(y, m, d, hh, mm, ss, DateTimeKind.Utc).ToLocalTime())
            );

        [TestCase(ExpectedResult = 1357)]
        public int Load_Registry_Alias()
            => Loaded.Identification;

        [TestCase(ExpectedResult = "secret message")]
        public string Load_Registry_NonMember()
            => Loaded.Secret;

        [Test]
        public void Load_Registry_Class()
        {
            Assert.That(Loaded.Phone.Type,  Is.EqualTo("Mobile"));
            Assert.That(Loaded.Phone.Value, Is.EqualTo("090-1234-5678"));
            Assert.That(Loaded.Email.Type,  Is.EqualTo("Email"));
            Assert.That(Loaded.Email.Value, Is.Null.Or.Empty);
        }

        [TestCase(FileType.Xml, "Settings.xml", ExpectedResult = "John Lennon")]
        [TestCase(FileType.Json, "Settings.json", ExpectedResult = "Mike Davis")]
        [TestCase(FileType.Xml, "SettingsJapanese.xml", ExpectedResult = "鈴木一朗")]
        [TestCase(FileType.Json, "SettingsJapanese.json", ExpectedResult = "山田太郎")]
        public string Load_File(FileType type, string filename)
            => type.Load<Person>(IoEx.Path.Combine(Examples, filename)).Name;

        #endregion

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        /// 
        /// <summary>
        /// 設定を保存するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        #region Save

        [TestCase(ExpectedResult = "山田花子")]
        public string Save_Registry_String()
            => Saved.GetValue("Name") as string;

        [TestCase(ExpectedResult = 15)]
        public int Save_Registry_Integer()
            => (int)Saved.GetValue("Age");

        [TestCase(ExpectedResult = 1)]
        public int Save_Registry_Enum()
            => (int)Saved.GetValue("Sex");

        [TestCase(ExpectedResult = 1)]
        public int Save_Registry_Boolean()
            => (int)Saved.GetValue("Reserved");

        [TestCase(ExpectedResult = 1420035930)]
        public int Save_Registry_DateTime()
            => (int)Saved.GetValue("Creation");

        [TestCase(ExpectedResult = 123)]
        public int Save_Registry_Alias()
            => (int)Saved.GetValue("ID");

        [Test]
        public void Save_Registry_NonMember()
            => Assert.That(Saved.GetValue("Secred"), Is.Null);

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

        [TestCase(FileType.Xml, "Person.xml")]
        [TestCase(FileType.Json, "Person.json")]
        public void Save_File_Exists(FileType type, string filename)
        {
            var dest = IoEx.Path.Combine(Results, filename);
            type.Save(dest, CreatePerson());
            Assert.That(IoEx.File.Exists(dest), Is.True);
        }

        #endregion

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
            using (var registrar = new Registrar(LoadKeyName))
            {
                Loaded = Cube.Settings.Operations.Load<Person>(registrar.RegistryKey);
            }

            var saved = Registry.CurrentUser.CreateSubKey(SaveKeyName);
            saved.Save(CreatePerson());
            Saved = saved;
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
        /// Registrar クラスを用いてロードした Person オブジェクトを
        /// 取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Person Loaded { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// LoadKeyName
        /// 
        /// <summary>
        /// ロードテスト用のレジストリのサブキー名を生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private string LoadKeyName => @"Software\CubeSoft\Settings_SaveTest";

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
        /// SaveKeyName
        /// 
        /// <summary>
        /// 保存テスト用のレジストリのサブキー名を生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private string SaveKeyName => @"Software\CubeSoft\Settings_SaveTest";

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
