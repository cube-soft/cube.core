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
namespace Cube.Xui.Drawing.Extensions;

using System;
using System.IO;
using System.Windows.Media.Imaging;
using Source = System.Drawing.Image;

/* ------------------------------------------------------------------------- */
///
/// Methods
///
/// <summary>
/// Provides the extended method of the Image class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class Methods
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// ToBitmapImage
    ///
    /// <summary>
    /// Converts to the BitmapImage object.
    /// </summary>
    ///
    /// <param name="src">Image object.</param>
    ///
    /// <returns>BitmapImage object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static BitmapImage ToBitmapImage(this Source src) =>
        src.ToBitmapImage(false);

    /* --------------------------------------------------------------------- */
    ///
    /// ToBitmapImage
    ///
    /// <summary>
    /// Converts to the BitmapImage object.
    /// </summary>
    ///
    /// <param name="src">Image object.</param>
    /// <param name="dispose">Whether disposing the source.</param>
    ///
    /// <returns>BitmapImage object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static BitmapImage ToBitmapImage(this Source src, bool dispose)
    {
        if (src is null) return default;
        try
        {
            if (TryGetObject(src, true,  out var e0)) return e0;
            if (TryGetObject(src, false, out var e1)) return e1;
            return default;
        }
        finally { if (dispose) src.Dispose(); }
    }

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// TryGetObject
    ///
    /// <summary>
    /// Tries to get a new BitmapImage object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static bool TryGetObject(Source src, bool icp, out BitmapImage dest)
    {
        try
        {
            using var ss = new StreamProxy(new MemoryStream());
            src.Save(ss, System.Drawing.Imaging.ImageFormat.Png);

            var opts = icp ?
                       BitmapCreateOptions.IgnoreColorProfile :
                       BitmapCreateOptions.None;

            dest = new BitmapImage();
            dest.BeginInit();
            dest.CacheOption   = BitmapCacheOption.OnLoad;
            dest.CreateOptions = opts;
            dest.StreamSource  = ss;
            dest.EndInit();

            if (dest.CanFreeze) dest.Freeze();
            return true;
        }
        catch (Exception e)
        {
            Logger.Warn($"{e.Message} (IgnoreColorProfile:{icp})");
            dest = default;
            return false;
        }
    }

    #endregion
}
