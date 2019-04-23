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
using Cube.Mixin.Xml;
using NUnit.Framework;
using System.Linq;
using System.Xml.Linq;

namespace Cube.Tests.Mixin
{
    /* --------------------------------------------------------------------- */
    ///
    /// XmlTest
    ///
    /// <summary>
    /// Tests the extended methods of the XML related classes.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class XmlTest : FileFixture
    {
        #region Tests

        #region GetElements

        /* ----------------------------------------------------------------- */
        ///
        /// GetElements_Rss
        ///
        /// <summary>
        /// Tests the GetElement and GetElements methods with the RSS data.
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
        /// Tests the GetElement and GetElements methods with the OPML data.
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
        /// Confirms the behavior when executing the GetElements method
        /// with the wrong namespace.
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
        /// Tests the GetDescendants method with the RSS data.
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
        /// Tests the GetDescendants method with the OPML data.
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
        /// Confirms the behavior when executing the GetDescendants method
        /// with the wrong namespace.
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
        /// Confirms that the GetValueOrAttribute method returns the value.
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
        /// Confirms that the GetValueOrAttribute method returns the
        /// attribute value.
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
        /// Confirms that the GetValueOrAttribute method returns the empty.
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
        /// Confirms that the GetValueOrAttribute method returns the empty.
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
        /// Confirms the behavior when executing the GetValueOrAttribute
        /// method with the wrong namespace.
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

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// Create a new instance of the XElement class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private XElement Create(string e) => XDocument.Load(GetExamplesWith(e)).Root;

        #endregion
    }
}
