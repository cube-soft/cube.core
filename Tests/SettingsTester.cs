/* ------------------------------------------------------------------------- */
///
/// SettingsTester.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
///
/* ------------------------------------------------------------------------- */
using System;
using System.Runtime.Serialization;
using NUnit.Framework;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Tests.SettingsTester
    /// 
    /// <summary>
    /// Settings クラスをテストするためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class SettingsTester : Cube.Development.ResourceInitializer
    {
        /* ----------------------------------------------------------------- */
        ///
        /// SettingsTester
        /// 
        /// <summary>
        /// Settings のテスト用クラスです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SettingsTester() : base() { }

        /* ----------------------------------------------------------------- */
        ///
        /// TestLoad
        /// 
        /// <summary>
        /// ファイルから設定を読み込むテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(Cube.Settings.FileType.Xml,  "Settings.xml",  "John Lennon")]
        [TestCase(Cube.Settings.FileType.Json, "Settings.json", "Mike Davis")]
        [TestCase(Cube.Settings.FileType.Xml,  "SettingsJapanese.xml",  "鈴木一朗")]
        [TestCase(Cube.Settings.FileType.Json, "SettingsJapanese.json", "山田太郎")]
        public void TestLoad(Cube.Settings.FileType type, string filename, string expected)
        {
            Assert.DoesNotThrow(() => {
                var path = System.IO.Path.Combine(Examples, filename);
                var data = Cube.Settings.Load<Person>(path, type);
                Assert.That(data.Name, Is.EqualTo(expected));
                Assert.That(data.Secret, Is.Null);
            });
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestLoadRegistry
        /// 
        /// <summary>
        /// レジストリから設定を読み込むテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestLoadRegistry()
        {
            Assert.DoesNotThrow(() => {
                using (var registrar = new Registrar())
                {
                    var data = Cube.Settings.Load<Person>(registrar.TargetKey);
                    Assert.That(data, Is.Not.Null);
                    Assert.That(data.Name, Is.EqualTo("Harry Potter"));
                    Assert.That(data.Age, Is.EqualTo(11));
                    Assert.That(data.Secret, Is.EqualTo("secret message"));
                }
            });
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestLoadRegistryAlias
        /// 
        /// <summary>
        /// レジストリから別名が設定されている値を読み込むテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestLoadRegistryAlias()
        {
            Assert.DoesNotThrow(() => {
                using (var registrar = new Registrar())
                {
                    var data = Cube.Settings.Load<Person>(registrar.TargetKey);
                    Assert.That(data, Is.Not.Null);
                    Assert.That(data.Identification, Is.EqualTo(1357));
                }
            });
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestLoadRegistryBool
        /// 
        /// <summary>
        /// レジストリから真偽値を読み込むテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestLoadRetistryBool()
        {
            Assert.DoesNotThrow(() => {
                using (var registrar = new Registrar())
                {
                    var data = Cube.Settings.Load<Person>(registrar.TargetKey);
                    Assert.That(data, Is.Not.Null);
                    Assert.That(data.Reserved, Is.True);
                }
            });
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestLoadRegistryDateTime
        /// 
        /// <summary>
        /// レジストリから時刻オブジェクトを読み込むテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestLoadRegistryDateTime()
        {
            Assert.DoesNotThrow(() => {
                using (var registrar = new Registrar())
                {
                    var data = Cube.Settings.Load<Person>(registrar.TargetKey);
                    Assert.That(data, Is.Not.Null);
                    Assert.That(data.Creation.Year,   Is.EqualTo(2015));
                    Assert.That(data.Creation.Month,  Is.EqualTo(3));
                    Assert.That(data.Creation.Day,    Is.EqualTo(16));
                    Assert.That(data.Creation.Hour,   Is.EqualTo(11));
                    Assert.That(data.Creation.Minute, Is.EqualTo(32));
                    Assert.That(data.Creation.Second, Is.EqualTo(26));
                }
            });
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestLoadRegistryClass
        /// 
        /// <summary>
        /// レジストリからクラスオブジェクトの値を読み込むテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestLoadRegistryClass()
        {
            Assert.DoesNotThrow(() => {
                using (var registrar = new Registrar())
                {
                    var data = Cube.Settings.Load<Person>(registrar.TargetKey);
                    Assert.That(data, Is.Not.Null);
                    Assert.That(data.Phone.Type, Is.EqualTo("Mobile"));
                    Assert.That(data.Phone.Data, Is.EqualTo("090-1234-5678"));
                    Assert.That(data.Email.Type, Is.EqualTo("Unknown"));
                    Assert.That(data.Email.Data, Is.EqualTo("Unknown data"));
                }
            });
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestSaveRegistry
        /// 
        /// <summary>
        /// ファイルから設定を読み込むテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestSaveRegistry()
        {
            var person = new Person
            { 
                Identification = 123,
                Name           = "山田太郎",
                Age            = 20,
                Creation       = new DateTime(2014, 12, 31, 23, 25, 30),
                Phone          = new Address { Type = "Mobile", Data = "080-9876-5432" },
                Reserved       = true,
                Secret         = "dummy data"
            };

            var name = @"Software\CubeSoft\SettingsTester";
            using(var root = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(name))
            {
                Cube.Settings.Save<Person>(person, root);

                Assert.That(root.GetValue("ID"), Is.EqualTo(123));
                Assert.That(root.GetValue("Name"), Is.EqualTo("山田太郎"));
                Assert.That(root.GetValue("Age"), Is.EqualTo(20));
                Assert.That(root.GetValue("Creation"), Is.EqualTo(1420035930));
                Assert.That(root.GetValue("Reserved"), Is.EqualTo("True"));

                using (var subkey = root.OpenSubKey("Phone"))
                {
                    Assert.That(subkey.GetValue("Type"), Is.EqualTo("Mobile"));
                    Assert.That(subkey.GetValue("Data"), Is.EqualTo("080-9876-5432"));
                }

                using (var subkey = root.OpenSubKey("Email"))
                {
                    Assert.That(subkey.GetValue("Type"), Is.EqualTo("Unknown"));
                    Assert.That(subkey.GetValue("Data"), Is.EqualTo("Unknown data"));
                }
            }
        }

        #region Internal resources

        /* ----------------------------------------------------------------- */
        ///
        /// Person
        /// 
        /// <summary>
        /// テスト用のデータクラスです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DataContract]
        internal class Person
        {
            #region Properties

            [DataMember(Name = "ID")]
            public int Identification
            {
                get { return _id; }
                set { _id = value; }
            }

            [DataMember]
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            [DataMember]
            public int Age
            {
                get { return _age; }
                set { _age = value; }
            }

            [DataMember]
            public DateTime Creation
            {
                get { return _creation; }
                set { _creation = value; }
            }

            [DataMember]
            public Address Phone
            {
                get { return _phone; }
                set { _phone = value; }
            }

            [DataMember]
            public Address Email
            {
                get { return _email; }
                set { _email = value; }
            }

            [DataMember]
            public bool Reserved
            {
                get { return _reserved; }
                set { _reserved = value; }
            }

            public string Secret
            {
                get { return _secret; }
                set { _secret = value; }
            }

            #endregion

            #region Fields
            private int _id = -1;
            private string _name = "Personal name";
            private int _age = -1;
            private DateTime _creation = DateTime.MinValue;
            private Address _phone = new Address();
            private Address _email = new Address();
            private bool _reserved = false;
            private string _secret = "secret message";
            #endregion
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Address
        /// 
        /// <summary>
        /// テスト用のデータクラスです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DataContract]
        internal class Address
        {
            #region Properties

            [DataMember]
            public string Type
            {
                get { return _type; }
                set { _type = value; }
            }

            [DataMember]
            public string Data
            {
                get { return _data; }
                set { _data = value; }
            }

            #endregion

            #region Fields
            private string _type = "Unknown";
            private string _data = "Unknown data";
            #endregion
        }

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
                _target.SetValue("Age", 11);
                _target.SetValue("Creation", 0x550640ba);
                _target.SetValue("Reserved", true);

                using (var child = _target.CreateSubKey("Phone"))
                {
                    child.SetValue("Type", "Mobile");
                    child.SetValue("Data", "090-1234-5678");
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
