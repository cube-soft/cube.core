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
    /// IconFactory
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
            if (size == IconSize.Zero) return null;

            var info = new SHSTOCKICONINFO();
            info.cbSize = Marshal.SizeOf(info);
            Shell32.NativeMethods.SHGetStockIconInfo((uint)id,
                Shell32.NativeMethods.SHGFI_SYSICONINDEX, ref info);

            IImageList images;
            Shell32.NativeMethods.SHGetImageList((uint)size,
                Shell32.NativeMethods.IID_IImageList, out images);
            if (images == null) return null;

            var handle = IntPtr.Zero;
            images.GetIcon(info.iSysImageIndex,
                (int)Shell32.NativeMethods.ILD_TRANSPARENT, ref handle);
            return (handle != IntPtr.Zero) ? Icon.FromHandle(handle) : null;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// 指定されたファイルからアイコンを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static Icon Create(string path, IconSize size)
        {
            if (size == IconSize.Zero) return null;

            var info = new SHFILEINFO();
            Shell32.NativeMethods.SHGetFileInfo(path, 0, ref info, (uint)Marshal.SizeOf(info),
                Shell32.NativeMethods.SHGFI_SYSICONINDEX);

            IImageList images;
            Shell32.NativeMethods.SHGetImageList((uint)size,
                Shell32.NativeMethods.IID_IImageList, out images);
            if (images == null) return null;

            var handle = IntPtr.Zero;
            images.GetIcon(info.iIcon, (int)Shell32.NativeMethods.ILD_TRANSPARENT, ref handle);
            return (handle != IntPtr.Zero) ? Icon.FromHandle(handle) : null;
        }
    }
}
