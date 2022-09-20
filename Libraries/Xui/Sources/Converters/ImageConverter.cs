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
namespace Cube.Xui.Converters;

using Cube.Xui.Drawing.Extensions;

/* ------------------------------------------------------------------------- */
///
/// ImageConverter
///
/// <summary>
/// Provides functionality to convert from an Image object to
/// a BitmapImage object.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class ImageConverter : SimplexConverter
{
    /* --------------------------------------------------------------------- */
    ///
    /// ImageValueConverter
    ///
    /// <summary>
    /// Initializes a new instance of the ImageConverter class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public ImageConverter() : base(e =>
    {
        if (e is System.Drawing.Image i0) return i0.ToBitmapImage(false);
        if (e is System.Drawing.Icon  i1) return i1.ToBitmap().ToBitmapImage(true);
        return null;
    }) { }
}
