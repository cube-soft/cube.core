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
using System.Runtime.InteropServices;
using System.Text;

namespace Cube.Forms.UrlMon
{
    /* --------------------------------------------------------------------- */
    ///
    /// UrlMon.NativeMethods
    ///
    /// <summary>
    /// Provides functions defined in the urlmon.dll.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal static class NativeMethods
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// UrlMkSetSessionOption
        ///
        /// <summary>
        /// https://msdn.microsoft.com/ja-jp/library/ms775125.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern int UrlMkSetSessionOption(int dwOption, string pBuffer,
            int dwBufferLength, int dwReserved);

        /* ----------------------------------------------------------------- */
        ///
        /// UrlMkGetSessionOption
        ///
        /// <summary>
        /// https://msdn.microsoft.com/ja-jp/library/ms775124.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern int UrlMkGetSessionOption(int dwOption, StringBuilder pBuffer,
            int dwBufferLength, ref int pdwBufferLength, int dwReserved);

        /* ----------------------------------------------------------------- */
        ///
        /// CoInternetIsFeatureEnabled
        ///
        /// <summary>
        /// https://msdn.microsoft.com/ja-jp/library/ms537164.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName)]
        public static extern int CoInternetIsFeatureEnabled(int featureEntry, int dwFlags);

        /* ----------------------------------------------------------------- */
        ///
        /// CoInternetSetFeatureEnabled
        ///
        /// <summary>
        /// https://msdn.microsoft.com/ja-jp/library/ms537168.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName)]
        public static extern int CoInternetSetFeatureEnabled(int FeatureEntry, int dwFlags, bool fEnable);

        #endregion

        #region Fields
        const string LibName = "urlmon.dll";
        #endregion
    }
}
