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
namespace Cube.Tests.Icons;

using System.Drawing;
using System.Drawing.Imaging;
using Cube.Icons;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// StockIconTest
///
/// <summary>
/// Tests the StockIcon class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class StockIconTest : FileFixture
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Get
    ///
    /// <summary>
    /// Tests of the GetImage extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(StockIcon.Information,     IconSize.ExtraLarge, ExpectedResult =  48)]
    [TestCase(StockIcon.Warning,         IconSize.ExtraLarge, ExpectedResult =  48)]
    [TestCase(StockIcon.Error,           IconSize.ExtraLarge, ExpectedResult =  48)]
    [TestCase(StockIcon.Shield,          IconSize.Large,      ExpectedResult =  32)]
    [TestCase(StockIcon.World,           IconSize.Small,      ExpectedResult =  16)]
    [TestCase(StockIcon.Internet,        IconSize.Small,      ExpectedResult =  16)]
    [TestCase(StockIcon.EnhancedCdMedia, IconSize.Jumbo,      ExpectedResult = 256)]
    [TestCase(StockIcon.CdRMedia,        IconSize.Jumbo,      ExpectedResult = 256)]
    public int Get(StockIcon src, IconSize size)
    {
        var dest = Get($"{src}-{size}.png");
        using (var e = src.GetImage(size)) e.Save(dest, ImageFormat.Png);
        using (var e = Image.FromFile(dest)) return e.Width;
    }

    #endregion
}
