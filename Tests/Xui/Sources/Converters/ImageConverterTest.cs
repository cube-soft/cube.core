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
namespace Cube.Xui.Tests.Converters;

using System.Windows.Media.Imaging;
using Cube.Xui.Converters;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// ImageConverterTest
///
/// <summary>
/// Tests the ImageConverter class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class ImageConverterTest : ConvertHelper
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// ImageConverter
    ///
    /// <summary>
    /// Tests the Convert method with a Bitmap object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void ImageConverter() => Assert.That(
        Convert<BitmapImage>(new ImageConverter(), Properties.Resources.Logo),
        Is.Not.Null
    );

    /* --------------------------------------------------------------------- */
    ///
    /// ImageConverter_Icon
    ///
    /// <summary>
    /// Tests the Convert method with an Icon object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void ImageConverter_Icon() => Assert.That(
        Convert<BitmapImage>(new ImageConverter(), Properties.Resources.App),
        Is.Not.Null
    );

    /* --------------------------------------------------------------------- */
    ///
    /// ImageConverter_Null
    ///
    /// <summary>
    /// Tests the Convert method with null.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void ImageConverter_Null() => Assert.That(
        Convert<BitmapImage>(new ImageConverter(), null),
        Is.Null
    );

    #endregion
}
