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
using Cube.Generics;
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
    /// AssemblyReader のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class AssemblyReaderTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Assembly_Properties
        ///
        /// <summary>
        /// 各種プロパティの内容を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Assembly_Properties(Assembly src, Result expected) {
            var dest = new AssemblyReader(src);
            Assert.That(dest.Location.HasValue(), Is.EqualTo(expected.Location));
            Assert.That(dest.Assembly,            Is.EqualTo(expected.Assembly));
            Assert.That(dest.Title,               Is.EqualTo(expected.Title));
            Assert.That(dest.Description,         Is.EqualTo(expected.Description));
            Assert.That(dest.Company,             Is.EqualTo(expected.Company));
            Assert.That(dest.Product,             Is.EqualTo(expected.Product));
            Assert.That(dest.Copyright,           Is.EqualTo(expected.Copyright));
            Assert.That(dest.Trademark,           Is.EqualTo(expected.Trademark));
            Assert.That(dest.Configuration,       Is.EqualTo(expected.Configuration));
            Assert.That(dest.Culture,             Is.EqualTo(expected.Culture));
            Assert.That(dest.Version,             Is.EqualTo(expected.Version));
            Assert.That(dest.FileVersion,         Is.EqualTo(expected.FileVersion));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestCases
        ///
        /// <summary>
        /// テストケースを取得します。
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
                    Location      = true,
                    Title         = "Cube.Core testing project",
                    Description   = "NUnit framework を用いて Cube.Core プロジェクトをテストします。",
                    Company       = "CubeSoft",
                    Product       = "Cube.Core.Tests",
                    Copyright     = "Copyright © 2010 CubeSoft, Inc.",
                    Trademark     = "CubeSoft, Inc.",
                    Configuration = string.Empty,
                    Culture       = string.Empty,
                    Version       = new Version(1, 12, 0, 0),
                    FileVersion   = new Version(1, 12, 0, 0)
                });

                yield return new TestCaseData(Assembly.GetAssembly(typeof(AssemblyReader)), new Result
                {
                    Assembly      = Assembly.GetAssembly(typeof(AssemblyReader)),
                    Location      = true,
                    Title         = "Cube.Core",
                    Description   = "Common library for CubeSoft applications.",
                    Company       = "CubeSoft",
                    Product       = "Cube.Core",
                    Copyright     = "Copyright © 2010 CubeSoft, Inc.",
                    Trademark     = string.Empty,
                    Configuration = string.Empty,
                    Culture       = string.Empty,
                    Version       = new Version(1, 12, 0, 0),
                    FileVersion   = new Version(1, 12, 0, 0)
                });

                yield return new TestCaseData(null, new Result
                {
                    Assembly      = null,
                    Location      = false,
                    Title         = string.Empty,
                    Description   = string.Empty,
                    Company       = string.Empty,
                    Product       = string.Empty,
                    Copyright     = string.Empty,
                    Trademark     = string.Empty,
                    Configuration = string.Empty,
                    Culture       = string.Empty,
                    Version       = new Version(),
                    FileVersion   = new Version()
                });
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Result
        ///
        /// <summary>
        /// 期待される結果を保持するための構造体です。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public class Result
        {
            public Assembly Assembly { get; set; }
            public bool Location { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Company { get; set; }
            public string Product { get; set; }
            public string Copyright { get; set; }
            public string Trademark { get; set; }
            public string Configuration { get; set; }
            public string Culture { get; set; }
            public Version Version { get; set; }
            public Version FileVersion { get; set; }
        }
    }
}
