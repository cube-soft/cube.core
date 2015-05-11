/* ------------------------------------------------------------------------- */
///
/// StockIcons.cs
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

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.StockIcons
    /// 
    /// <summary>
    /// システムアイコンを取得するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class StockIcons
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// アイコンを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static Icon Create(Kind kind, Size size)
        {
            var sii = new Win32Api.SHSTOCKICONINFO();
            sii.cbSize = Marshal.SizeOf(sii);
            Win32Api.SHGetStockIconInfo((UInt32)kind, Win32Api.SHGSI_ICON | (UInt32)size, ref sii);
            if (sii.hIcon != IntPtr.Zero) return Icon.FromHandle(sii.hIcon);
            else return null;
        }

        #endregion

        #region Stock icons kind

        public enum Kind : uint
        {
            DocumentNotAssociated = 0,
            DocumentAssociated = 1,
            Application = 2,
            Folder = 3,
            FolderOpen = 4,
            Floppy525 = 5,
            Floppy35 = 6,
            RemovableDrive = 7,
            FixedDrive = 8,
            NetworkDrive = 9,
            NetworkDriveDisconnected = 10,
            CdDrive = 11,
            RamDrive = 12,
            World = 13,
            Server = 15,
            Printer = 16,
            Network = 17,
            Find = 22,
            Help = 23,
            Share = 28,
            Link = 29,
            SlowFile = 30,
            Recycle = 31,
            RecycleFull = 32,
            AudioCdMedia = 40,
            Lock = 47,
            AutoList = 49,
            NetworkPrinter = 50,
            ServerShare = 51,
            FaxPrinter = 52,
            NetworkFaxPrinter = 53,
            PrintToFile = 54,
            Stack = 55,
            SvcdMedia = 56,
            StuffedFolder = 57,
            UnknownDrive = 58,
            DvdDrive = 59,
            DvdMedia = 60,
            DvdRamMedia = 61,
            DvdRwMedia = 62,
            DvdRMedia = 63,
            DvdRomMedia = 64,
            CdPlusMedia = 65,
            CdRwMedia = 66,
            CdRMedia = 67,
            Burning = 68,
            BlankCdMedia = 69,
            CdRomMedia = 70,
            AudioFiles = 71,
            ImageFiles = 72,
            VideoFiles = 73,
            MixedFiles = 74,
            FolderBack = 75,
            FolderFront = 76,
            Shield = 77,
            Warning = 78,
            Info = 79,
            Error = 80,
            Key = 81,
            Software = 82,
            Rename = 83,
            Delete = 84,
            AudioDvdMedia = 85,
            MovieDvdMedia = 86,
            EnhancedCdMedia = 87,
            EnhancedDvdMedia = 88,
            HdDvdMedia = 89,
            BluRayMedia = 90,
            VcdMedia = 91,
            DvdPlusRMedia = 92,
            DvdPlusRwMedia = 93,
            Desktop = 94,
            Mobile = 95,
            Users = 96,
            SmartMedia = 97,
            CompactFlash = 98,
            CellPhone = 99,
            Camera = 100,
            VideoCamera = 101,
            AudioPlayer = 102,
            NetworkConnect = 103,
            Internet = 104,
            Zip = 105,
            Settings = 106,
            MaxIcons = 107
        }

        #endregion

        #region Stock icons size

        public enum Size : uint
        {
            Large      = 0, //  16 x  16 pixel
            Small      = 1, //  32 x  32 pixel
            ExtraLarge = 2, //  48 x  48 pixel
            Jumbo      = 4  // 256 x 256 pixel
        }

        #endregion

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
