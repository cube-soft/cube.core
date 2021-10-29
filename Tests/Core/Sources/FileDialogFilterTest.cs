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
using System.Collections.Generic;
using System.Linq;
using Cube.Mixin.Generics;
using Cube.Mixin.String;
using NUnit.Framework;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// FileDialogFilterTest
    ///
    /// <summary>
    /// Tests the FileDialogFilter class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class FileDialogFilterTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// GetString
        ///
        /// <summary>
        /// Tests to convert to a filter string.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public string GetString(string descr, bool ignore, string[] exts)
        {
            var src = new FileDialogFilter(descr, ignore, exts);
            Assert.That(!src.Targets.Any(e => !e.HasValue()), "Empty");
            return src.ToString();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetString_Null
        ///
        /// <summary>
        /// Tests to initialize a new instance with null parameters.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetString_Null() => Assert.That(
            () => new FileDialogFilter(null, null).ToString(),
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
            Assert.That(dest, Does.EndWith("Customized files (*.cAb, *.1Ab)|*.cAb;*.1Ab"));
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
            () => new List<FileDialogFilter>().GetFilter(),
            Throws.TypeOf<InvalidOperationException>()
        );

        /* ----------------------------------------------------------------- */
        ///
        /// GetFilterIndex
        ///
        /// <summary>
        /// Tests the GetFilterIndex extended method.
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
        public static IEnumerable<TestCaseData> TestCases { get
        {
            yield return new TestCaseData("All files", true,
                new[] {".*" }
            ).Returns("All files (*.*)|*.*");

            yield return new TestCaseData("All files", false,
                new[] { "*" }
            ).Returns("All files (*.*)|*.*");

            yield return new TestCaseData("Text files", true,
                new[] { ".txt" }
            ).Returns("Text files (*.txt)|*.txt;*.TXT;*.Txt");

            yield return new TestCaseData("Text files", false,
                new[] { "*.txt" }
            ).Returns("Text files (*.txt)|*.txt");

            yield return new TestCaseData("Image files", true,
                new[] { ".Jpg", ".Jpeg", ".Png" }
            ).Returns("Image files (*.Jpg, *.Jpeg, *.Png)|*.Jpg;*.jpg;*.JPG;*.Jpeg;*.jpeg;*.JPEG;*.Png;*.png;*.PNG");

            yield return new TestCaseData("Image files", false,
                new[] { "Jpg", "Jpeg", "Png" }
            ).Returns("Image files (*.Jpg, *.Jpeg, *.Png)|*.Jpg;*.Jpeg;*.Png");

            yield return new TestCaseData("Text or customized files", true,
                new[] { ".txt", ".7z" }
            ).Returns("Text or customized files (*.txt, *.7z)|*.txt;*.TXT;*.Txt;*.7z;*.7Z");

            yield return new TestCaseData("Text or customized files", false,
                new[] { ".txt", ".7z", "" }
            ).Returns("Text or customized files (*.txt, *.7z)|*.txt;*.7z");

            yield return new TestCaseData("Customized files", true,
                new[] { ".cAb", ".1Ab" }
            ).Returns("Customized files (*.cAb, *.1Ab)|*.cAb;*.cab;*.CAB;*.Cab;*.1Ab;*.1ab;*.1AB");

            yield return new TestCaseData("Customized files", false,
                new[] { ".cAb", ".1Ab", null }
            ).Returns("Customized files (*.cAb, *.1Ab)|*.cAb;*.1Ab");
        }}

        /* ----------------------------------------------------------------- */
        ///
        /// TestCollection
        ///
        /// <summary>
        /// Gets a list of DisplayFilter objects.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static IEnumerable<FileDialogFilter> TestCollection =>
            TestCases.Select(e => new FileDialogFilter(
                e.Arguments[0] as string,
                e.Arguments[1].TryCast<bool>(),
                e.Arguments[2] as string[]
            ));

        #endregion
    }
}
