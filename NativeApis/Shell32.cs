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

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Shell32
    /// 
    /// <summary>
    /// shell32.dll に定義された関数を宣言するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal abstract class Shell32
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
        public static extern int SHGetFileInfo(string pszPath, int dwFileAttributes, ref SHFILEINFO psfi, int cbFileInfo, uint uFlags);
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern int SHGetFileInfo(IntPtr pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, int cbFileInfo, uint uFlags);

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
        public static extern void SHGetStockIconInfo(UInt32 siid, UInt32 uFlags, ref SHSTOCKICONINFO sii);

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
        public static extern void SHGetImageList(Int32 iImageList, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, out IImageList ppv);

        #region Constant fields
        public static readonly uint SHGI_SYSICONINDEX = 0x00004000;
        public static readonly uint ILD_NORMAL        = 0x00000000;
        public static readonly uint ILD_TRANSPARENT   = 0x00000001;
        public static readonly Guid IID_IImageList    = new Guid("46EB5926-582E-4017-9FDF-E8998DAA0950");
        #endregion
    }
}
