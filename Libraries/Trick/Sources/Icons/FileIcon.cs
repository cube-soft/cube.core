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
namespace Cube.Icons;

using System;
using System.Drawing;
using System.Runtime.InteropServices;

/* ------------------------------------------------------------------------- */
///
/// FileIcon
///
/// <summary>
/// Provides functionality to get an icon associated by the provided
/// file or directory.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class FileIcon
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Get
    ///
    /// <summary>
    /// Gets a new instance of the Icon class with the specified
    /// arguments.
    /// </summary>
    ///
    /// <param name="src">File or directory path.</param>
    /// <param name="size">Icon size.</param>
    ///
    /// <returns>Icon object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static Icon Get(string src, IconSize size)
    {
        var s0 = new ShFileInfo();
        var f0 = 0x4010u; // SHGFI_SYSICONINDEX | SHGFI_USEFILEATTRIBUTES
        _ = Shell32.NativeMethods.SHGetFileInfo(src, 0, ref s0, (uint)Marshal.SizeOf(s0), f0);

        var s1 = new Guid("46EB5926-582E-4017-9FDF-E8998DAA0950"); // IID_IImageList
        var r1 = Shell32.NativeMethods.SHGetImageList((uint)size, s1, out var images);
        if (r1 != 0 || images is null) return default;

        var s2 = IntPtr.Zero;
        var f2 = 0x01; // ILD_TRANSPARENT
        var r2 = images.GetIcon(s0.iIcon, f2, ref s2);
        return (r2 == 0 && s2 != IntPtr.Zero) ? Icon.FromHandle(s2) : default;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetImage
    ///
    /// <summary>
    /// Gets a new instance of the Image instance with the specified
    /// arguments.
    /// </summary>
    ///
    /// <param name="src">File or directory path.</param>
    /// <param name="size">Icon size.</param>
    ///
    /// <returns>Image object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static Image GetImage(string src, IconSize size)
    {
        using var icon = Get(src, size);
        return icon?.ToBitmap();
    }

    #endregion
}
