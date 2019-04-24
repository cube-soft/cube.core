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
using Cube.Mixin.Generics;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cube.FileSystem.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// ExtensionFilterTest
    ///
    /// <summary>
    /// Provides a test fixture for the DisplayFilter class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class ExtensionFilterTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// ToString
        ///
        /// <summary>
        /// Tests to convert to a filter string.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public string ToString(string descr, bool ignore, string[] exts) =>
            new ExtensionFilter(descr, ignore, exts).ToString();

        /* ----------------------------------------------------------------- */
        ///
        /// ToString_Null
        ///
        /// <summary>
        /// Tests to initialize a new instance with null parameters.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void ToString_Null() => Assert.That(
            () => new ExtensionFilter(null, null).ToString(),
            Throws.TypeOf<ArgumentNullException>()
        );

        /* ----------------------------------------------------------------- */
        ///
        /// GetFilter
        ///
        /// <summary>
        /// Tests to get the filter string from a DisplayFilter collection.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetFilter()
        {
            var dest = TestCollection.GetFilter();
            Assert.That(dest, Does.StartWith("All files (*.*)|*.*"));
            Assert.That(dest, Does.EndWith("Text or customized files (*.txt, *.1)|*.txt;*.1"));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetFilter_Throws
        ///
        /// <summary>
        /// Tests to get the filter string from an empty collection.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetFilter_Throws() => Assert.That(
            () => new List<ExtensionFilter>().GetFilter(),
            Throws.TypeOf<InvalidOperationException>()
        );

        /* ----------------------------------------------------------------- */
        ///
        /// GetFilter_Throws
        ///
        /// <summary>
        /// Tests to get the filter index.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("Test.txt", ExpectedResult = 3)]
        [TestCase("Test.PNG", ExpectedResult = 5)]
        [TestCase("Test.pdf", ExpectedResult = 0)]
        [TestCase("Test",     ExpectedResult = 0)]
        [TestCase("",         ExpectedResult = 0)]
        public int GetFilterIndex(string path) => TestCollection.GetFilterIndex(path);

        #endregion

        #region TestCases

        /* ----------------------------------------------------------------- */
        ///
        /// TestCases
        ///
        /// <summary>
        /// Gets a list of test cases.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                yield return new TestCaseData("All files", true,
                    new[] {".*" }
                ).Returns("All files (*.*)|*.*");

                yield return new TestCaseData("All files", false,
                    new[] { ".*" }
                ).Returns("All files (*.*)|*.*");

                yield return new TestCaseData("Text files", true,
                    new[] { ".txt" }
                ).Returns("Text files (*.txt)|*.txt;*.TXT");

                yield return new TestCaseData("Text files", false,
                    new[] { ".txt" }
                ).Returns("Text files (*.txt)|*.txt");

                yield return new TestCaseData("Image files", true,
                    new[] { ".Jpg", ".Jpeg", ".Png",}
                ).Returns("Image files (*.Jpg, *.Jpeg, *.Png)|*.jpg;*.JPG;*.jpeg;*.JPEG;*.png;*.PNG");

                yield return new TestCaseData("Image files", false,
                    new[] { ".Jpg", ".Jpeg", ".Png",}
                ).Returns("Image files (*.Jpg, *.Jpeg, *.Png)|*.Jpg;*.Jpeg;*.Png");

                yield return new TestCaseData("Text or customized files", true,
                    new[] { ".txt", ".1" }
                ).Returns("Text or customized files (*.txt, *.1)|*.txt;*.TXT;*.1");

                yield return new TestCaseData("Text or customized files", false,
                    new[] { ".txt", ".1" }
                ).Returns("Text or customized files (*.txt, *.1)|*.txt;*.1");
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestCollection
        ///
        /// <summary>
        /// Gets a list of DisplayFilter objects.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static IEnumerable<ExtensionFilter> TestCollection =>
            TestCases.Select(e => new ExtensionFilter(
                e.Arguments[0] as string,
                e.Arguments[1].TryCast<bool>(),
                e.Arguments[2] as string[]
            ));

        #endregion
    }
}
