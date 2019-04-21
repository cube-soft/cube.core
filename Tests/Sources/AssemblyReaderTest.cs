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
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// AssemblyReaderTest
    ///
    /// <summary>
    /// Tests for the AssemblyReader class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class AssemblyReaderTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Assembly_Properties
        ///
        /// <summary>
        /// Confirms properties of the AssemblyReader object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Assembly_Properties(Assembly src, Result expected) {
            var dest = new AssemblyReader(src);
            Assert.That(dest.Location,      Does.EndWith(expected.Location));
            Assert.That(dest.DirectoryName, Does.Contain(expected.Directory));
            Assert.That(dest.Assembly,      Is.EqualTo(expected.Assembly));
            Assert.That(dest.Title,         Is.EqualTo(expected.Title));
            Assert.That(dest.Description,   Is.EqualTo(expected.Description));
            Assert.That(dest.Company,       Is.EqualTo(expected.Company));
            Assert.That(dest.Product,       Is.EqualTo(expected.Product));
            Assert.That(dest.Copyright,     Is.EqualTo(expected.Copyright));
            Assert.That(dest.Configuration, Is.EqualTo(expected.Configuration));
            Assert.That(dest.Trademark,     Is.EqualTo(expected.Trademark));
            Assert.That(dest.Culture,       Is.EqualTo(expected.Culture));
            Assert.That(dest.Version,       Is.EqualTo(expected.Version));
            Assert.That(dest.FileVersion,   Is.EqualTo(expected.FileVersion));
        }

        #endregion

        #region TestCases

        /* ----------------------------------------------------------------- */
        ///
        /// TestCases
        ///
        /// <summary>
        /// Gets the collection of test cases.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                yield return new TestCaseData(Assembly.GetExecutingAssembly(), new Result
                {
                    Assembly      = Assembly.GetExecutingAssembly(),
                    Location      = "Cube.Core.Tests.dll",
                    Directory     = @"Tests\bin",
                    Title         = "Cube.Core UnitTest",
                    Description   = "NUnit framework を用いて Cube.Core プロジェクトをテストします。",
                    Company       = "CubeSoft, Inc.",
                    Product       = "Cube.Tests",
                    Copyright     = "Copyright © 2010 CubeSoft, Inc.",
                    Configuration = Configuration,
                    Trademark     = string.Empty,
                    Culture       = string.Empty,
                    Version       = new Version(1, 16, 0, 0),
                    FileVersion   = new Version(1, 16, 0, 0),
                });

                yield return new TestCaseData(Assembly.GetAssembly(typeof(AssemblyReader)), new Result
                {
                    Assembly      = Assembly.GetAssembly(typeof(AssemblyReader)),
                    Location      = "Cube.Core.dll",
                    Directory     = @"Tests\bin",
                    Title         = "Cube.Core",
                    Description   = "Common library for CubeSoft libraries and applications.",
                    Company       = "CubeSoft",
                    Product       = "Cube.Core",
                    Copyright     = "Copyright © 2010 CubeSoft, Inc.",
                    Configuration = Configuration,
                    Trademark     = string.Empty,
                    Culture       = string.Empty,
                    Version       = new Version(1, 16, 0, 0),
                    FileVersion   = new Version(1, 16, 0, 0),
                });

                yield return new TestCaseData(null, new Result
                {
                    Assembly      = null,
                    Location      = string.Empty,
                    Directory     = string.Empty,
                    Title         = string.Empty,
                    Description   = string.Empty,
                    Company       = string.Empty,
                    Product       = string.Empty,
                    Copyright     = string.Empty,
                    Configuration = string.Empty,
                    Trademark     = string.Empty,
                    Culture       = string.Empty,
                    Version       = new Version(),
                    FileVersion   = new Version(),
                });
            }
        }

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// Result
        ///
        /// <summary>
        /// Represents information of expected results.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        internal class Result
        {
            public Assembly Assembly { get; set; }
            public string Location { get; set; }
            public string Directory { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Company { get; set; }
            public string Product { get; set; }
            public string Copyright { get; set; }
            public string Configuration { get; set; }
            public string Trademark { get; set; }
            public string Culture { get; set; }
            public Version Version { get; set; }
            public Version FileVersion { get; set; }
        }

        #endregion

        #region Fields
        #if DEBUG
        private static readonly string Configuration = "Debug";
        #else
        private static readonly string Configuration = "Release";
        #endif
        #endregion
    }
}
