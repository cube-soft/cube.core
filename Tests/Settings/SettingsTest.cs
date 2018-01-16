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
    class SettingsTest : FileHelper
    {
        #region Tests

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
            var settings = new SettingsFolder<Person>(SettingsType.Json);
            var root = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            Assert.That(settings.Type,           Is.EqualTo(SettingsType.Json));
            Assert.That(settings.Path,           Does.StartWith(root));
            Assert.That(settings.Path,           Does.EndWith(AssemblyReader.Default.Product));
            Assert.That(settings.Company,        Is.EqualTo(AssemblyReader.Default.Company));
            Assert.That(settings.Product,        Is.EqualTo(AssemblyReader.Default.Product));
            Assert.That(settings.Version.Number, Is.EqualTo(AssemblyReader.Default.Version));
            Assert.That(settings.Value,          Is.Not.Null);
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
        public string Load_File(SettingsType type, string filename)
            => type.Load<Person>(Example(filename)).Name;

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
        public void Load_File_Throws(SettingsType type, string filename)
            => Assert.That(
                () => type.Load<Person>(Example(filename)),
                Throws.TypeOf<ArgumentException>()
            );

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
            using (var reader = new StreamReader(Example(filename)))
            {
                return type.Load<Person>(reader.BaseStream).Name;
            }
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
        [TestCase(SettingsType.Unknown,  "Settings.xml")]
        public void Load_Stream_Throws(SettingsType type, string filename)
            => Assert.That(() =>
        {
            using (var reader = new StreamReader(Example(filename)))
            {
                type.Load<Person>(reader.BaseStream);
            }
        }, Throws.TypeOf<ArgumentException>());

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
            Assert.That(File.Exists(dest), Is.True);
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
        public void Save_File_Throws(SettingsType type, string filename)
            => Assert.That(
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
            using (var sw = new StreamWriter(dest))
            {
                type.Save(sw.BaseStream, CreatePerson());
            }
            Assert.That(File.Exists(dest), Is.True);
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
        [TestCase(SettingsType.Unknown,  "Person.txt")]
        public void Save_Stream_Throws(SettingsType type, string filename)
            => Assert.That(() =>
        {
            using (var sw = new StreamWriter(Result(filename)))
            {
                type.Save(sw.BaseStream, CreatePerson());
            }
        }, Throws.TypeOf<ArgumentException>());

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

        #endregion
    }
}
