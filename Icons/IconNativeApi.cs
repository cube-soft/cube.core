/* ------------------------------------------------------------------------- */
///
/// IconNativeApi.cs
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
    /// Cube.IconNativeApi
    /// 
    /// <summary>
    /// Icon に関連する Win32 API 群を定義したクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal abstract class IconNativeApi
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// SHGetFileInfo
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/bb762179.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        public static extern int SHGetFileInfo(string pszPath, int dwFileAttributes, ref SHFILEINFO psfi, int cbFileInfo, uint uFlags);
        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
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

        #endregion

        #region Structures, classes, and interfaces

        /* ----------------------------------------------------------------- */
        ///
        /// SHSTOCKICONINFO
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/bb759805.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct SHSTOCKICONINFO
        {
            public Int32 cbSize;
            public IntPtr hIcon;
            public Int32 iSysImageIndex;
            public Int32 iIcon;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szPath;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SHFILEINFO
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/bb759792.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RECT
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/a5ch4fda.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// POINT
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/8kk2sy33.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IMAGELISTDRAWPARAMS
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/bb761395.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [StructLayout(LayoutKind.Sequential)]
        public struct IMAGELISTDRAWPARAMS
        {
            public int cbSize;
            public IntPtr himl;
            public int i;
            public IntPtr hdcDst;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public int xBitmap;    // x offest from the upperleft of bitmap
            public int yBitmap;    // y offset from the upperleft of bitmap
            public int rgbBk;
            public int rgbFg;
            public int fStyle;
            public int dwRop;
            public int fState;
            public int Frame;
            public int crEffect;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IMAGEINFO
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/bb761393.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [StructLayout(LayoutKind.Sequential)]
        public struct IMAGEINFO
        {
            public IntPtr hbmImage;
            public IntPtr hbmMask;
            public int Unused1;
            public int Unused2;
            public RECT rcImage;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IImageList
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/bb761490.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [ComImportAttribute()]
        [GuidAttribute("46EB5926-582E-4017-9FDF-E8998DAA0950")]
        [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IImageList
        {
            [PreserveSig]
            int Add(IntPtr hbmImage, IntPtr hbmMask, ref int pi);

            [PreserveSig]
            int ReplaceIcon(int i, IntPtr hicon, ref int pi);

            [PreserveSig]
            int SetOverlayImage(int iImage, int iOverlay);

            [PreserveSig]
            int Replace(int i, IntPtr hbmImage, IntPtr hbmMask);

            [PreserveSig]
            int AddMasked(IntPtr hbmImage, int crMask, ref int pi);

            [PreserveSig]
            int Draw(ref IMAGELISTDRAWPARAMS pimldp);

            [PreserveSig]
            int Remove(int i);

            [PreserveSig]
            int GetIcon(int i, int flags, ref IntPtr picon);

            [PreserveSig]
            int GetImageInfo(int i, ref IMAGEINFO pImageInfo);

            [PreserveSig]
            int Copy(int iDst, IImageList punkSrc, int iSrc, int uFlags);

            [PreserveSig]
            int Merge(int i1, IImageList punk2, int i2, int dx, int dy, ref Guid riid, ref IntPtr ppv);

            [PreserveSig]
            int Clone(ref Guid riid, ref IntPtr ppv);

            [PreserveSig]
            int GetImageRect(int i, ref RECT prc);

            [PreserveSig]
            int GetIconSize(ref int cx, ref int cy);

            [PreserveSig]
            int SetIconSize(int cx, int cy);

            [PreserveSig]
            int GetImageCount(ref int pi);

            [PreserveSig]
            int SetImageCount(int uNewCount);

            [PreserveSig]
            int SetBkColor(int clrBk, ref int pclr);

            [PreserveSig]
            int GetBkColor(ref int pclr);

            [PreserveSig]
            int BeginDrag(int iTrack, int dxHotspot, int dyHotspot);

            [PreserveSig]
            int EndDrag();

            [PreserveSig]
            int DragEnter(IntPtr hwndLock, int x, int y);

            [PreserveSig]
            int DragLeave(IntPtr hwndLock);

            [PreserveSig]
            int DragMove(int x, int y);

            [PreserveSig]
            int SetDragCursorImage(ref IImageList punk, int iDrag, int dxHotspot, int dyHotspot);

            [PreserveSig]
            int DragShowNolock(int fShow);

            [PreserveSig]
            int GetDragImage(ref POINT ppt, ref POINT pptHotspot, ref Guid riid, ref IntPtr ppv);

            [PreserveSig]
            int GetItemFlags(int i, ref int dwFlags);

            [PreserveSig]
            int GetOverlayImage(int iOverlay, ref int piIndex);
        }

        #endregion

        #region Constant fields
        public static readonly UInt32 SHGI_SYSICONINDEX = 0x00004000;
        public static readonly UInt32 ILD_NORMAL        = 0x00000000;
        public static readonly UInt32 ILD_TRANSPARENT   = 0x00000001;
        public static readonly Guid   IID_IImageList    = new Guid("46EB5926-582E-4017-9FDF-E8998DAA0950");
        #endregion
    }
}
