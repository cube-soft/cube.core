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
        /// TestLoadFile
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
        public void TestLoadFile(Cube.Settings.FileType type, string filename, string expected)
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
                    Assert.That(data.Name, Is.EqualTo("Harry Potter"));
                    Assert.That(data.Age, Is.EqualTo(11));
                    Assert.That(data.Secret, Is.Null);
                }
            });
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
            [DataMember]
            public string Name { get; set; }

            [DataMember]
            public int Age { get; set; }

            public string Secret { get; set; }
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
                _target = _parent.CreateSubKey(_subkey);
                _target.SetValue("Name", "Harry Potter");
                _target.SetValue("Age", 11);
            }

            private void Delete()
            {
                _target.Close();
                _parent.DeleteSubKeyTree(_subkey);
                _parent.Close();
            }

            #endregion

            #region Fields

            private Microsoft.Win32.RegistryKey _parent = null;
            private Microsoft.Win32.RegistryKey _target = null;
            private string _subkey = "SettingsTester";
            private bool _disposed = false;

            #endregion
        }

        #endregion
    }
}
