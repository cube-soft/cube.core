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
    class SettingsTester : SetupHelper
    {
        /* ----------------------------------------------------------------- */
        ///
        /// SettingsTester
        ///
        /* ----------------------------------------------------------------- */
        public SettingsTester() : base() { }

        /* ----------------------------------------------------------------- */
        ///
        /// TestLoad
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("Settings.json", Cube.Settings.FileType.Json)]
        public void TestLoad(string path, Cube.Settings.FileType type)
        {
            Assert.DoesNotThrow(() => {
                var data = new TestData();
                Cube.Settings.Load<TestData>(path, data, type);
                Assert.That(data.Name, Is.EqualTo("Mike Davis"));
                Assert.That(data.Age, Is.EqualTo(20));
            });
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestLoad
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestLoad()
        {

        }

        #region Internal resources

        [DataContract]
        internal class TestData
        {
            [DataMember]
            public string Name { get; set; }

            [DataMember]
            public int Age { get; set; }
        }

        #endregion
    }
}
