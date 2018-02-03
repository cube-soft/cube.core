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
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using Cube.Xml;

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

        #endregion

        #region GetDecendants

        /* ----------------------------------------------------------------- */
        ///
        /// GetDecendants_Rss
        ///
        /// <summary>
        /// Sample.rss に対して GetDecendants のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetDecendants_Rss() => Assert.That(
            Create("Sample.rss").GetDecendants("rdf", "li").Count(),
            Is.EqualTo(2)
        );

        /* ----------------------------------------------------------------- */
        ///
        /// GetDecendants_Opml
        ///
        /// <summary>
        /// Sample.opml に対して GetDecendants のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetDecendants_Opml() => Assert.That(
            Create("Sample.opml").GetDecendants("outline").Count(),
            Is.EqualTo(8)
        );

        #endregion

        #region GetValueOrAttribute

        /* ----------------------------------------------------------------- */
        ///
        /// GetValueOrAttribute
        ///
        /// <summary>
        /// GetValueOrAttribute のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetValueOrAttribute()
        {
            var src = Create("Sample.rss").GetElement("channel");

            Assert.That(
                src.GetValueOrAttribute("link", string.Empty),
                Is.EqualTo("http://xml.com/pub")
            );

            var seq = src.GetElement("items").GetElement("rdf", "Seq");

            Assert.That(
                seq.GetValueOrAttribute("rdf", "li", string.Empty),
                Is.Empty
            );

            Assert.That(
                seq.GetValueOrAttribute("rdf", "li", "resource"),
                Is.EqualTo("http://xml.com/pub/2000/08/09/xslt/xslt.html")
            );

            Assert.That(
                seq.GetValueOrAttribute("rdf", "li", "dummy"),
                Is.Empty
            );

            Assert.That(
                seq.GetValueOrAttribute("li", "resource"),
                Is.Empty
            );
        }

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
