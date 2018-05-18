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
using System;
using System.Runtime.InteropServices;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// IDocHostShowUI
    ///
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/aa753269.aspx
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [ComImport,
     Guid("C4D244B0-D43E-11CF-893B-00AA00BDCE1A"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDocHostShowUI
    {
        /* ----------------------------------------------------------------- */
        ///
        /// ShowMessage
        ///
        /// <summary>
        /// メッセージを表示します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [return: MarshalAs(UnmanagedType.U4)]
        [PreserveSig]
        int ShowMessage(IntPtr hwnd,
            [MarshalAs(UnmanagedType.LPWStr)] string lpstrText,
            [MarshalAs(UnmanagedType.LPWStr)] string lpstrCaption,
            int dwType,
            [MarshalAs(UnmanagedType.LPWStr)] string lpstrHelpFile,
            int dwHelpContext,
            out int lpResult
        );

        /* ----------------------------------------------------------------- */
        ///
        /// ShowHelp
        ///
        /// <summary>
        /// ヘルプを表示します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [return: MarshalAs(UnmanagedType.U4)]
        [PreserveSig]
        int ShowHelp(
            IntPtr hwnd,
            [MarshalAs(UnmanagedType.LPWStr)] string pszHelpFile,
            int uCommand,
            int dwData,
            IntPtr ptMouse, // POINT
            [MarshalAs(UnmanagedType.IDispatch)] object pDispatchObjectHit
        );
    }
}
