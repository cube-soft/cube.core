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
namespace Cube.Tests.Extensions;

using System;
using System.Collections.Generic;
using System.Reflection;
using Cube.Reflection.Extensions;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// AssemblyTest
///
/// <summary>
/// Tests extended methods of the Assembly class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class AssemblyTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Invoke
    ///
    /// <summary>
    /// Confirms the results of extended methods.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCaseSource(nameof(TestCases))]
    public void Invoke(Assembly src, Expected expected) {
        Assert.That(src.GetNameString(),    Is.EqualTo(expected.Name));
        Assert.That(src.GetLocation(),      Does.EndWith(expected.FileName));
        Assert.That(src.GetFileName(),      Is.EqualTo(expected.FileName));
        Assert.That(src.GetDirectoryName(), Does.Contain(expected.DirectoryName));
        Assert.That(src.GetTitle(),         Is.EqualTo(expected.Title));
        Assert.That(src.GetDescription(),   Is.EqualTo(expected.Description));
        Assert.That(src.GetCompany(),       Is.EqualTo(expected.Company));
        Assert.That(src.GetProduct(),       Is.EqualTo(expected.Product));
        Assert.That(src.GetCopyright(),     Is.EqualTo(expected.Copyright));
        Assert.That(src.GetConfiguration(), Is.EqualTo(expected.Configuration));
        Assert.That(src.GetTrademark(),     Is.EqualTo(expected.Trademark));
        Assert.That(src.GetCulture(),       Is.EqualTo(expected.Culture));
        Assert.That(src.GetVersion(),       Is.EqualTo(expected.Version));
    }

    #endregion

    #region TestCases

    /* --------------------------------------------------------------------- */
    ///
    /// TestCases
    ///
    /// <summary>
    /// Gets the collection of test cases.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static IEnumerable<TestCaseData> TestCases
    {
        get
        {
            yield return new TestCaseData(typeof(AssemblyTest).Assembly, new Expected
            {
                Name          = "Cube.Core.Tests",
                FileName      = "Cube.Core.Tests.dll",
                DirectoryName = @"Tests\Core\bin",
                Title         = "Cube.Core UnitTest",
                Description   = "NUnit framework を用いて Cube.Core プロジェクトをテストします。",
                Company       = "CubeSoft, Inc.",
                Product       = "Cube.Tests",
                Copyright     = "Copyright © 2010 CubeSoft, Inc.",
                Configuration = Configuration,
                Trademark     = string.Empty,
                Culture       = string.Empty,
                Version       = new(9, 5, 0, 0),
            });

            yield return new TestCaseData(typeof(Methods).Assembly, new Expected
            {
                Name          = "Cube.Core",
                FileName      = "Cube.Core.dll",
                DirectoryName = @"Tests\Core\bin",
                Title         = "Cube.Core",
                Description   = "Basic components to practice the MVVM pattern in application development.",
                Company       = "CubeSoft",
                Product       = "Cube.Core",
                Copyright     = "Copyright © 2010 CubeSoft, Inc.",
                Configuration = Configuration,
                Trademark     = string.Empty,
                Culture       = string.Empty,
                Version       = new(9, 5, 0, 0),
            });
        }
    }

    #endregion

    #region Others

    /* --------------------------------------------------------------------- */
    ///
    /// Expected
    ///
    /// <summary>
    /// Represents information of expected results.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Expected
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        public string DirectoryName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public string Product { get; set; }
        public string Copyright { get; set; }
        public string Trademark { get; set; }
        public string Configuration { get; set; }
        public string Culture { get; set; }
        public Version Version { get; set; }
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
