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
        /// オブジェクトを初期化します。
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
    }
}
