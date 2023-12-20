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
namespace Cube.FileSystem;

using System;
using System.Runtime.InteropServices;
using Cube.Text.Extensions;

/* ------------------------------------------------------------------------- */
///
/// Shell
///
/// <summary>
/// Provides utility methods for Shell32 DLL.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class Shell
{
    /* --------------------------------------------------------------------- */
    ///
    /// GetTypeName
    ///
    /// <summary>
    /// Gets a value that represents type of the specified file.
    /// </summary>
    ///
    /// <param name="src">Path of the source file.</param>
    ///
    /// <returns>Type name of the file.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static string GetTypeName(string src)
    {
        if (!src.HasValue()) return string.Empty;

        var dest   = new ShFileInfo();
        var status = Shell32.NativeMethods.SHGetFileInfo(
            src,
            0x0080, // FILE_ATTRIBUTE_NORMAL
            ref dest,
            (uint)Marshal.SizeOf(dest),
            0x0410 // SHGFI_TYPENAME | SHGFI_USEFILEATTRIBUTES
        );

        return status != IntPtr.Zero ? dest.szTypeName : string.Empty;
    }
}
