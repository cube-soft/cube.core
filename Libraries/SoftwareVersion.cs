﻿/* ------------------------------------------------------------------------- */
///
/// SoftwareVersion.cs
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

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// SoftwareVersion
    /// 
    /// <summary>
    /// ソフトウェアのバージョンを表すクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class SoftwareVersion
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Number
        /// 
        /// <summary>
        /// バージョン番号を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Version Number { get; set; } = new Version(1, 0, 0, 0);

        /* ----------------------------------------------------------------- */
        ///
        /// Available
        /// 
        /// <summary>
        /// Number プロパティの有効桁数を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int Available { get; set; } = 3;

        /* ----------------------------------------------------------------- */
        ///
        /// Suffix
        /// 
        /// <summary>
        /// バージョン番号の末尾に付与する文字列を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Suffix { get; set; } = string.Empty;

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// ToString
        /// 
        /// <summary>
        /// バージョンを表す文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public override string ToString()
        {
            var dest = CreateVersion();
            if (!string.IsNullOrEmpty(Suffix)) dest += Suffix;
            return dest;
        }

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// バージョンを表す文字列を生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private string CreateVersion()
        {
            if (Number == null) return string.Empty;

            var ss = new System.Text.StringBuilder();

            ss.Append(Number.Major);
            if (Available <= 1) return ss.ToString();

            ss.Append($".{Number.Minor}");
            if (Available <= 2) return ss.ToString();

            ss.Append($".{Number.Build}");
            if (Available <= 3) return ss.ToString();

            ss.Append($".{Number.Revision}");
            return ss.ToString();
        }

        #endregion
    }
}
