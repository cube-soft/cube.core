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
    /// DWebBrowserEvents2
    ///
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/aa768283.aspx
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [ComImport,
     Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D"),
     InterfaceType(ComInterfaceType.InterfaceIsIDispatch),
     TypeLibType(TypeLibTypeFlags.FHidden)]
    internal interface DWebBrowserEvents2
    {
        /* ----------------------------------------------------------------- */
        ///
        /// BeforeNavigate2
        ///
        /// <summary>
        /// ページ遷移の直前に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DispId(250)]
        void BeforeNavigate2(
            [In, MarshalAs(UnmanagedType.IDispatch)] object pDisp,
            [In] ref object URL,
            [In] ref object flags,
            [In] ref object targetFrameName,
            [In] ref object postData,
            [In] ref object headers,
            [In, Out] ref bool cancel
        );

        /* ----------------------------------------------------------------- */
        ///
        /// NewWindow3
        ///
        /// <summary>
        /// 新しいウィンドウが開く直前に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DispId(273)]
        void NewWindow3(
            [In, MarshalAs(UnmanagedType.IDispatch)] object pDisp,
            [In, Out] ref bool cancel,
            [In] ref object flags,
            [In] ref object URLContext,
            [In] ref object URL
        );

        /* ----------------------------------------------------------------- */
        ///
        /// NavigateError
        ///
        /// <summary>
        /// ページ遷移のエラー時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DispId(271)]
        void NavigateError(
            [In, MarshalAs(UnmanagedType.IDispatch)] object pDisp,
            [In] ref object URL,
            [In] ref object frame,
            [In] ref object statusCode,
            [In, Out] ref bool cancel
        );
    }
}
