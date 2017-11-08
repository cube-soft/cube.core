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
using System.Reflection;
using NUnit.Framework;

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
            var actual = new AssemblyReader(src);
            Assert.That(!string.IsNullOrEmpty(actual.Location), Is.EqualTo(expected.Location));
            Assert.That(actual.Assembly,      Is.EqualTo(expected.Assembly));
            Assert.That(actual.Title,         Is.EqualTo(expected.Title));
            Assert.That(actual.Description,   Is.EqualTo(expected.Description));
            Assert.That(actual.Company,       Is.EqualTo(expected.Company));
            Assert.That(actual.Product,       Is.EqualTo(expected.Product));
            Assert.That(actual.Copyright,     Is.EqualTo(expected.Copyright));
            Assert.That(actual.Trademark,     Is.EqualTo(expected.Trademark));
            Assert.That(actual.Configuration, Is.EqualTo(expected.Configuration));
            Assert.That(actual.Culture,       Is.EqualTo(expected.Culture));
            Assert.That(actual.Version,       Is.EqualTo(expected.Version));
            Assert.That(actual.FileVersion,   Is.EqualTo(expected.FileVersion));
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
                    Version       = new Version(1, 6, 0, 0),
                    FileVersion   = new Version(1, 6, 0, 0)
                });

                yield return new TestCaseData(Assembly.GetAssembly(typeof(AssemblyReader)), new Result
                {
                    Assembly      = Assembly.GetAssembly(typeof(AssemblyReader)),
                    Location      = true,
                    Title         = "Cube.Core",
                    Description   = "Common library for Cube.* projects.",
                    Company       = "CubeSoft",
                    Product       = "Cube.Core",
                    Copyright     = "Copyright © 2010 CubeSoft, Inc.",
                    Trademark     = string.Empty,
                    Configuration = string.Empty,
                    Culture       = string.Empty,
                    Version       = new Version(1, 6, 0, 0),
                    FileVersion   = new Version(1, 6, 0, 0)
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
        public struct Result
        {
            public Assembly Assembly;
            public bool Location;
            public string Title;
            public string Description;
            public string Company;
            public string Product;
            public string Copyright;
            public string Trademark;
            public string Configuration;
            public string Culture;
            public Version Version;
            public Version FileVersion;
        }
    }
}
