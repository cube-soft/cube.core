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
namespace Cube.Shell32;

using System;
using System.Runtime.InteropServices;

/* ------------------------------------------------------------------------- */
///
/// Shell32.NativeMethods
///
/// <summary>
/// Provides native methods defined in the shell32.dll.
/// </summary>
///
/* ------------------------------------------------------------------------- */
internal static class NativeMethods
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// SHGetFileInfo
    ///
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/bb762179.aspx
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [DllImport(LibName, CharSet = CharSet.Unicode)]
    public static extern IntPtr SHGetFileInfo(
        string pszPath,
        uint dwFileAttributes,
        ref ShFileInfo psfi,
        uint cbFileInfo,
        uint uFlags
    );

    /* --------------------------------------------------------------------- */
    ///
    /// SHGetStockIconInfo
    ///
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/bb762205.aspx
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [DllImport(LibName, CharSet = CharSet.Unicode)]
    public static extern void SHGetStockIconInfo(
        uint siid,
        uint uFlags,
        ref ShStockIconInfo sii
    );

    /* --------------------------------------------------------------------- */
    ///
    /// SHGetImageList
    ///
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/bb762185.aspx
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [DllImport(LibName, CharSet = CharSet.Unicode)]
    public static extern int SHGetImageList(
        uint iImageList,
        [MarshalAs(UnmanagedType.LPStruct)] Guid riid,
        out IImageList ppv
    );

    #endregion

    #region Fields
    private const string LibName = "shell32";
    #endregion
}
