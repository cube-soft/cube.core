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
using Cube.Xml;
using NUnit.Framework;
using System.Linq;
using System.Xml.Linq;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// XmlTest
    ///
    /// <summary>
    /// XmlOperator のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class XmlTest : FileHelper
    {
        #region Tests

        #region GetElements

        /* ----------------------------------------------------------------- */
        ///
        /// GetElements_Rss
        ///
        /// <summary>
        /// Sample.rss に対して GetElements のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetElements_Rss() => Assert.That(
            Create("Sample.rss")
                .GetElement("channel")
                .GetElement("items")
                .GetElement("rdf", "Seq")
                .GetElements("rdf", "li")
                .Count(),
            Is.EqualTo(2)
        );

        /* ----------------------------------------------------------------- */
        ///
        /// GetElements_Opml
        ///
        /// <summary>
        /// Sample.opml に対して GetElements のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetElements_Opml() => Assert.That(
            Create("Sample.opml")
                .GetElement("body")
                .GetElements("outline")
                .Count(),
            Is.EqualTo(1)
        );

        /* ----------------------------------------------------------------- */
        ///
        /// GetElements_WrongNamespace
        ///
        /// <summary>
        /// 無効な名前空間を指定した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetElements_WrongNamespace() => Assert.That(
            Create("Sample.rss")
                .GetElement("channel")
                .GetElement("items")
                .GetElement("rdf", "Seq")
                .GetElements("dummy", "li")
                .Count(),
            Is.EqualTo(0)
        );

        #endregion

        #region GetDecendants

        /* ----------------------------------------------------------------- */
        ///
        /// GetDescendants_Rss
        ///
        /// <summary>
        /// Sample.rss に対して GetDescendants のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetDescendants_Rss() => Assert.That(
            Create("Sample.rss").GetDescendants("rdf", "li").Count(),
            Is.EqualTo(2)
        );

        /* ----------------------------------------------------------------- */
        ///
        /// GetDescendants_Opml
        ///
        /// <summary>
        /// Sample.opml に対して GetDescendants のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetDescendants_Opml() => Assert.That(
            Create("Sample.opml").GetDescendants("outline").Count(),
            Is.EqualTo(8)
        );

        /* ----------------------------------------------------------------- */
        ///
        /// GetDescendants_WrongNamespace
        ///
        /// <summary>
        /// 無効な名前空間を指定した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetDescendants_WrongNamespace() => Assert.That(
            Create("Sample.rss").GetDescendants("dummy", "li").Count(),
            Is.EqualTo(0)
        );

        #endregion

        #region GetValueOrAttribute

        /* ----------------------------------------------------------------- */
        ///
        /// GetValueOrAttribute_Value
        ///
        /// <summary>
        /// GetValueOrAttribute の結果 Value が取得できるケースの
        /// テストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetValueOrAttribute_Value() => Assert.That(
            Create("Sample.rss")
                .GetElement("channel")
                .GetValueOrAttribute("link", "href"),
            Is.EqualTo("http://xml.com/pub")
        );

        /* ----------------------------------------------------------------- */
        ///
        /// GetValueOrAttribute_Attribute
        ///
        /// <summary>
        /// GetValueOrAttribute の結果 Attribute が取得できるケースの
        /// テストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetValueOrAttribute_Attribute() => Assert.That(
            Create("Sample.rss")
                .GetElement("channel")
                .GetElement("items")
                .GetElement("rdf", "Seq")
                .GetValueOrAttribute("rdf", "li", "resource"),
            Is.EqualTo("http://xml.com/pub/2000/08/09/xslt/xslt.html")
        );

        /* ----------------------------------------------------------------- */
        ///
        /// GetValueOrAttribute_NotFound
        ///
        /// <summary>
        /// GetValueOrAttribute の結果どちらも見つからないケースの
        /// テストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetValueOrAttribute_NotFound() => Assert.That(
            Create("Sample.rss")
                .GetElement("channel")
                .GetElement("items")
                .GetElement("rdf", "Seq")
                .GetValueOrAttribute("rdf", "li", "dummy"),
            Is.Empty
        );

        /* ----------------------------------------------------------------- */
        ///
        /// GetValueOrAttribute_Empty
        ///
        /// <summary>
        /// GetValueOrAttribute の属性に空文字を指定した時の挙動を
        /// 確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetValueOrAttribute_Empty() => Assert.That(
            Create("Sample.rss")
                .GetElement("channel")
                .GetElement("items")
                .GetElement("rdf", "Seq")
                .GetValueOrAttribute("rdf", "li", string.Empty),
            Is.Empty
        );

        /* ----------------------------------------------------------------- */
        ///
        /// GetValueOrAttribute_WrongNamespace
        ///
        /// <summary>
        /// GetValueOrAttribute に無効な名前空間を指定した時の挙動を
        /// 確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetValueOrAttribute_WrongNamespace() => Assert.That(
            Create("Sample.rss")
                .GetElement("channel")
                .GetElement("items")
                .GetElement("rdf", "Seq")
                .GetValueOrAttribute("dummy", "li", "resource"),
            Is.Empty
        );

        #endregion

        #endregion

        #region Helper methods

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// テスト用の XElement オブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private XElement Create(string e) => XDocument.Load(Example(e)).Root;

        #endregion
    }
}
