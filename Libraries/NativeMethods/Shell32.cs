/* ------------------------------------------------------------------------- */
///
/// Shell32.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///  http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
///
/* ------------------------------------------------------------------------- */
using System;
using System.Runtime.InteropServices;

namespace Cube.Shell32
{
    /* --------------------------------------------------------------------- */
    ///
    /// Shell32
    /// 
    /// <summary>
    /// shell32.dll に定義された関数を宣言するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal static class NativeMethods
    {
        /* ----------------------------------------------------------------- */
        ///
        /// SHGetFileInfo
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/bb762179.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes,
            ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

        /* ----------------------------------------------------------------- */
        ///
        /// SHGetStockIconInfo
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/bb762205.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern void SHGetStockIconInfo(uint siid, uint uFlags, ref SHSTOCKICONINFO sii);

        /* ----------------------------------------------------------------- */
        ///
        /// SHGetImageList
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/bb762185.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern int SHGetImageList(uint iImageList, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, out IImageList ppv);

        #region Constant fields
        public static readonly uint SHGFI_USEFILEATTRIBUTES = 0x00000010;
        public static readonly uint SHGFI_TYPENAME          = 0x00000400;
        public static readonly uint SHGFI_SYSICONINDEX      = 0x00004000;
        public static readonly uint FILE_ATTRIBUTE_NORMAL   = 0x00000080;
        public static readonly uint ILD_NORMAL              = 0x00000000;
        public static readonly uint ILD_TRANSPARENT         = 0x00000001;
        public static readonly Guid IID_IImageList = new Guid("46EB5926-582E-4017-9FDF-E8998DAA0950");
        #endregion
    }
}
