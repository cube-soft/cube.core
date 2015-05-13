/* ------------------------------------------------------------------------- */
///
/// IconFactory.cs
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
using System.Drawing;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.IconFactory
    /// 
    /// <summary>
    /// Icon を生成するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class IconFactory
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// システムで用意されているアイコンを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static Icon Create(StockIcons id, IconSize size)
        {
            var sii = new Win32Api.SHSTOCKICONINFO();
            sii.cbSize = Marshal.SizeOf(sii);
            Win32Api.SHGetStockIconInfo((UInt32)id, Win32Api.SHGSI_ICON | (UInt32)size, ref sii);
            if (sii.hIcon != IntPtr.Zero) return Icon.FromHandle(sii.hIcon);
            else return null;
        }

        #region Win32 APIs

        internal class Win32Api
        {
            public const UInt32 SHGSI_ICON = 0x000000100;

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

            [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
            public static extern void SHGetStockIconInfo(UInt32 siid, UInt32 uFlags, ref SHSTOCKICONINFO sii);
        }

        #endregion
    }
}
