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
using System.IO;
using NUnit.Framework;

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
    class SettingsTest : FileResource
    {
        #region Test methods

        #region Load

        /* ----------------------------------------------------------------- */
        ///
        /// LoadString
        /// 
        /// <summary>
        /// レジストリから文字列を読み込むテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void LoadString()
        {
            using (var registrar = new Registrar())
            {
                var settings = Cube.Settings.Load<Person>(registrar.TargetKey);
                Assert.That(settings, Is.Not.Null);
                Assert.That(settings.Name, Is.EqualTo("Harry Potter"));
                Assert.That(settings.Secret, Is.EqualTo("secret message"));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LoadInteger
        /// 
        /// <summary>
        /// レジストリから整数値を読み込むテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void LoadInteger()
        {
            using (var registrar = new Registrar())
            {
                var settings = Cube.Settings.Load<Person>(registrar.TargetKey);
                Assert.That(settings, Is.Not.Null);
                Assert.That(settings.Age, Is.EqualTo(11));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LoadEnum
        /// 
        /// <summary>
        /// レジストリから列挙体を読み込むテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void LoadEnum()
        {
            using (var registrar = new Registrar())
            {
                var settings = Cube.Settings.Load<Person>(registrar.TargetKey);
                Assert.That(settings, Is.Not.Null);
                Assert.That(settings.Sex, Is.EqualTo(Sex.Male));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LoadBoolean
        /// 
        /// <summary>
        /// レジストリから真偽値を読み込むテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void LoadBoolean()
        {
            using (var registrar = new Registrar())
            {
                var settings = Cube.Settings.Load<Person>(registrar.TargetKey);
                Assert.That(settings, Is.Not.Null);
                Assert.That(settings.Reserved, Is.True);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LoadDateTime
        /// 
        /// <summary>
        /// レジストリから時刻オブジェクトを読み込むテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void LoadDateTime()
        {
            using (var registrar = new Registrar())
            {
                var settings = Cube.Settings.Load<Person>(registrar.TargetKey);
                Assert.That(settings, Is.Not.Null);
                Assert.That(settings.Creation.Year,   Is.EqualTo(2015));
                Assert.That(settings.Creation.Month,  Is.EqualTo(3));
                Assert.That(settings.Creation.Day,    Is.EqualTo(16));
                Assert.That(settings.Creation.Hour,   Is.EqualTo(11));
                Assert.That(settings.Creation.Minute, Is.EqualTo(32));
                Assert.That(settings.Creation.Second, Is.EqualTo(26));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LoadClass
        /// 
        /// <summary>
        /// レジストリからクラスオブジェクトの値を読み込むテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void LoadClass()
        {
            using (var registrar = new Registrar())
            {
                var settings = Cube.Settings.Load<Person>(registrar.TargetKey);
                Assert.That(settings, Is.Not.Null);
                Assert.That(settings.Phone.Type, Is.EqualTo("Mobile"));
                Assert.That(settings.Phone.Value, Is.EqualTo("090-1234-5678"));
                Assert.That(settings.Email.Type, Is.EqualTo("Email"));
                Assert.That(settings.Email.Value, Is.Null.Or.Empty);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LoadAlias
        /// 
        /// <summary>
        /// レジストリから別名が設定されている値を読み込むテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void LoadAlias()
        {
            using (var registrar = new Registrar())
            {
                var settings = Cube.Settings.Load<Person>(registrar.TargetKey);
                Assert.That(settings, Is.Not.Null);
                Assert.That(settings.Identification, Is.EqualTo(1357));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LoadFile
        /// 
        /// <summary>
        /// ファイルから設定を読み込むテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(Cube.Settings.FileType.Xml, "Settings.xml", "John Lennon")]
        [TestCase(Cube.Settings.FileType.Json, "Settings.json", "Mike Davis")]
        [TestCase(Cube.Settings.FileType.Xml, "SettingsJapanese.xml", "鈴木一朗")]
        [TestCase(Cube.Settings.FileType.Json, "SettingsJapanese.json", "山田太郎")]
        public void LoadFile(Cube.Settings.FileType type, string filename, string expected)
        {
            var src = System.IO.Path.Combine(Examples, filename);
            Assert.That(File.Exists(src), Is.True);

            var settings = Cube.Settings.Load<Person>(src, type);
            Assert.That(settings.Name, Is.EqualTo(expected));
            Assert.That(settings.Secret, Is.Null);
        }

        #endregion

        #region Save

        /* ----------------------------------------------------------------- */
        ///
        /// SaveString
        /// 
        /// <summary>
        /// レジストリから文字列を保存するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void SaveString()
        {
            var person = CreatePerson();
            using (var root = CreateSubKey())
            {
                Cube.Settings.Save<Person>(person, root);
                Assert.That(root.GetValue("Name"), Is.EqualTo("山田花子"));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SaveInteger
        /// 
        /// <summary>
        /// レジストリから数値を保存するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void SaveInteger()
        {
            var person = CreatePerson();
            using (var root = CreateSubKey())
            {
                Cube.Settings.Save<Person>(person, root);
                Assert.That(root.GetValue("Age"), Is.EqualTo(15));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SaveEnum
        /// 
        /// <summary>
        /// レジストリから列挙体を保存するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void SaveEnum()
        {
            var person = CreatePerson();
            using (var root = CreateSubKey())
            {
                Cube.Settings.Save<Person>(person, root);
                Assert.That(root.GetValue("Sex"), Is.EqualTo(1));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SaveBoolean
        /// 
        /// <summary>
        /// レジストリから真偽値を保存するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void SaveBoolean()
        {
            var person = CreatePerson();
            using (var root = CreateSubKey())
            {
                Cube.Settings.Save<Person>(person, root);
                Assert.That(root.GetValue("Reserved"), Is.EqualTo(1));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SaveDateTime
        /// 
        /// <summary>
        /// レジストリからDateTime オブジェクトを保存するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void SaveDateTime()
        {
            var person = CreatePerson();
            using (var root = CreateSubKey())
            {
                Cube.Settings.Save<Person>(person, root);
                Assert.That(root.GetValue("Creation"), Is.EqualTo(1420035930));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SaveClass
        /// 
        /// <summary>
        /// レジストリからクラスオブジェクトを保存するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void SaveClass()
        {
            var person = CreatePerson();
            using (var root = CreateSubKey())
            {
                Cube.Settings.Save<Person>(person, root);

                using (var subkey = root.OpenSubKey("Phone"))
                {
                    Assert.That(subkey.GetValue("Type"), Is.EqualTo("Mobile"));
                    Assert.That(subkey.GetValue("Value"), Is.EqualTo("080-9876-5432"));
                }

                using (var subkey = root.OpenSubKey("Email"))
                {
                    Assert.That(subkey.GetValue("Type"), Is.EqualTo("PC"));
                    Assert.That(subkey.GetValue("Value"), Is.EqualTo("dummy@example.com"));
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SaveAlias
        /// 
        /// <summary>
        /// レジストリから別名が設定されている値を保存するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void SaveAlias()
        {
            var person = CreatePerson();
            using (var root = CreateSubKey())
            {
                Cube.Settings.Save<Person>(person, root);
                Assert.That(root.GetValue("ID"), Is.EqualTo(123));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SaveFile
        /// 
        /// <summary>
        /// 設定をファイルへ保存するテストを行います。
        /// </summary>
        /// 
        /// <remarks>
        /// NOTE: 未実装
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        //[TestCase("Person.xml", Cube.Settings.FileType.Xml)]
        //[TestCase("Person.json", Cube.Settings.FileType.Json)]
        //public void SaveFile(string filename, Cube.Settings.FileType type)
        //{
        //    var dest = Path.Combine(Results, filename);
        //    if (File.Exists(dest)) File.Delete(dest);

        //    var person = CreatePerson();
        //    Cube.Settings.Save<Person>(person, dest, type);
        //    Assert.That(File.Exists(dest), Is.True);
        //}

        #endregion

        #endregion

        #region Helper methods

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

        /* ----------------------------------------------------------------- */
        ///
        /// CreateSubKey
        /// 
        /// <summary>
        /// レジストリのサブキーを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Microsoft.Win32.RegistryKey CreateSubKey()
        {
            var name = @"Software\CubeSoft\SettingsTester";
            return Microsoft.Win32.Registry.CurrentUser.CreateSubKey(name);
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
            #region Constructors

            public Registrar() { Register(); }

            #endregion

            #region Properties

            public Microsoft.Win32.RegistryKey TargetKey
            {
                get { return _target; }
            }

            #endregion

            #region IDisposable

            ~Registrar() { Dispose(false); }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool diposing)
            {
                lock (this)
                {
                    if (_disposed) return;
                    _disposed = true;
                    if (diposing) Delete();
                }
            }

            #endregion

            #region Implementations

            private void Register()
            {
                var name = @"Software\CubeSoft";
                _parent = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(name);
                _target = _parent.CreateSubKey(_root);
                _target.SetValue("ID", 1357);
                _target.SetValue("Name", "Harry Potter");
                _target.SetValue("Sex", 0);
                _target.SetValue("Age", 11);
                _target.SetValue("Creation", 0x550640ba);
                _target.SetValue("Reserved", 1);

                using (var child = _target.CreateSubKey("Phone"))
                {
                    child.SetValue("Type", "Mobile");
                    child.SetValue("Value", "090-1234-5678");
                }
            }

            private void Delete()
            {
                _target.Close();
                _parent.DeleteSubKeyTree(_root);
                _parent.Close();
            }

            #endregion

            #region Fields

            private Microsoft.Win32.RegistryKey _parent = null;
            private Microsoft.Win32.RegistryKey _target = null;
            private string _root = "SettingsTester";
            private bool _disposed = false;

            #endregion
        }

        #endregion
    }
}
