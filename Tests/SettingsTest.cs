/* ------------------------------------------------------------------------- */
///
/// SettingsTest.cs
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
        #region Test methods

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

        [TestCase("佐藤栄作")]
        public void Load_Registry_String(string expected)
        {
            Assert.That(
                Loaded.Name,
                Is.EqualTo(expected)
            );
        }

        [TestCase(52)]
        public void Load_Registry_Integer(int expected)
        {
            Assert.That(
                Loaded.Age,
                Is.EqualTo(expected)
            );
        }

        [TestCase(Sex.Male)]
        public void Load_Registry_Enum(Sex expected)
        {
            Assert.That(
                Loaded.Sex,
                Is.EqualTo(expected)
            );
        }

        [TestCase(true)]
        public void Load_Registry_Boolean(bool expected)
        {
            Assert.That(
                Loaded.Reserved,
                Is.EqualTo(expected)
            );
        }

        [TestCase(2015, 3, 16, 2, 32, 26)]
        public void Load_Registry_DateTime(int y, int m, int d, int hh, int mm, int ss)
        {
            Assert.That(
                Loaded.Creation,
                Is.EqualTo(new DateTime(y, m, d, hh, mm, ss, DateTimeKind.Utc))
            );
        }

        [TestCase(1357)]
        public void Load_Registry_Alias(int expected)
        {
            Assert.That(
                Loaded.Identification,
                Is.EqualTo(expected)
            );
        }

        [TestCase("secret message")]
        public void Load_Registry_NonMember(string expected)
        {
            Assert.That(
                Loaded.Secret,
                Is.EqualTo(expected)
            );
        }

        [Test]
        public void Load_Registry_Class()
        {
            Assert.That(Loaded.Phone.Type,  Is.EqualTo("Mobile"));
            Assert.That(Loaded.Phone.Value, Is.EqualTo("090-1234-5678"));
            Assert.That(Loaded.Email.Type,  Is.EqualTo("Email"));
            Assert.That(Loaded.Email.Value, Is.Null.Or.Empty);
        }

        [TestCase(Settings.FileType.Xml,  "Settings.xml", "John Lennon")]
        [TestCase(Settings.FileType.Json, "Settings.json", "Mike Davis")]
        [TestCase(Settings.FileType.Xml,  "SettingsJapanese.xml", "鈴木一朗")]
        [TestCase(Settings.FileType.Json, "SettingsJapanese.json", "山田太郎")]
        public void Load_File(Settings.FileType type, string name, string expected)
        {
            var src = IoEx.Path.Combine(Examples, name);
            var settings = Cube.Settings.Load<Person>(src, type);
            Assert.That(
                settings.Name,
                Is.EqualTo(expected)
            );
        }

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

        [TestCase("山田花子")]
        public void Save_Registry_String(string expected)
        {
            Assert.That(
                Saved.GetValue("Name"),
                Is.EqualTo(expected)
            );
        }

        [TestCase(15)]
        public void Save_Registry_Integer(int expected)
        {
            Assert.That(
                Saved.GetValue("Age"),
                Is.EqualTo(expected)
            );
        }

        [TestCase(1)]
        public void Save_Registry_Enum(int expected)
        {
            Assert.That(
                Saved.GetValue("Sex"),
                Is.EqualTo(expected)
            );
        }

        [TestCase(1)]
        public void Save_Registry_Boolean(int expected)
        {
            Assert.That(
                Saved.GetValue("Reserved"),
                Is.EqualTo(expected)
            );
        }

        [TestCase(1420035930)]
        public void Save_Registry_DateTime(int expected)
        {
            Assert.That(
                Saved.GetValue("Creation"),
                Is.EqualTo(expected)
            );
        }

        [TestCase(123)]
        public void Save_Registry_Alias(int expected)
        {
            Assert.That(
                Saved.GetValue("ID"),
                Is.EqualTo(expected)
            );
        }

        [Test]
        public void Save_Registry_NonMember()
        {
            Assert.That(
                Saved.GetValue("Secred"),
                Is.Null
            );
        }

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

        [TestCase(Settings.FileType.Xml, "Person.xml")]
        [TestCase(Settings.FileType.Json, "Person.json")]
        public void Save_File(Settings.FileType type, string name)
        {
            var dest = IoEx.Path.Combine(Results, name);
            Cube.Settings.Save(CreatePerson(), dest, type);
            Assert.That(
                IoEx.File.Exists(dest),
                Is.True
            );
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
                Loaded = Cube.Settings.Load<Person>(registrar.RegistryKey);
            }

            var saved = Registry.CurrentUser.CreateSubKey(SaveKeyName);
            Cube.Settings.Save(CreatePerson(), saved);
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
        /// Loaded
        /// 
        /// <summary>
        /// Registrar クラスを用いてロードした Person オブジェクトを
        /// 取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Person Loaded { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// LoadKeyName
        /// 
        /// <summary>
        /// ロードテスト用のレジストリのサブキー名を生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string LoadKeyName
        {
            get { return @"Software\CubeSoft\Settings_SaveTest"; }
        }

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
        public RegistryKey Saved { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// SaveKeyName
        /// 
        /// <summary>
        /// 保存テスト用のレジストリのサブキー名を生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string SaveKeyName
        {
            get { return @"Software\CubeSoft\Settings_SaveTest"; }
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
        private Person CreatePerson()
        {
            return new Person {
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
        }

        #endregion

        #region Internal resources

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
    }
}
