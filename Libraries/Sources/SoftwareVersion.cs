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
using Cube.Generics;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

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
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// SoftwareVersion
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SoftwareVersion() { }

        /* ----------------------------------------------------------------- */
        ///
        /// SoftwareVersion
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="assembly">アセンブリオブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public SoftwareVersion(Assembly assembly)
        {
            var number = assembly?.GetName().Version;
            if (number != null) Number = number;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SoftwareVersion
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="version">バージョンを表す文字列</param>
        ///
        /* ----------------------------------------------------------------- */
        public SoftwareVersion(string version)
        {
            Parse(version);
        }

        #endregion

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
        /// Digit
        ///
        /// <summary>
        /// Number プロパティの有効桁数を取得または設定します。
        /// </summary>
        ///
        /// <remarks>
        /// 指定可能な値は 2, 3, 4 の 3 種類です。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public int Digit
        {
            get => _digit;
            set
            {
                if (_digit == value) return;
                _digit = Math.Min(Math.Max(value, 2), 4);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Prefix
        ///
        /// <summary>
        /// バージョン番号の先頭に付与する文字列を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Prefix { get; set; } = string.Empty;

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

        /* ----------------------------------------------------------------- */
        ///
        /// Platform
        ///
        /// <summary>
        /// ソフトウェアのプラットフォームを示す文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Platform => AssemblyReader.Platform;

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
        /// <returns>バージョンを表す文字列</returns>
        ///
        /* ----------------------------------------------------------------- */
        public override string ToString() => ToString(false);

        /* ----------------------------------------------------------------- */
        ///
        /// ToString
        ///
        /// <summary>
        /// バージョンを表す文字列を取得します。
        /// </summary>
        ///
        /// <param name="platform">
        /// Platform を付与するかどうかを示す値
        /// </param>
        ///
        /// <returns>バージョンを表す文字列</returns>
        ///
        /* ----------------------------------------------------------------- */
        public string ToString(bool platform)
        {
            var ss = new StringBuilder();

            if (Prefix.HasValue()) ss.Append(Prefix);
            AppendNumber(ss);
            if (Suffix.HasValue()) ss.Append(Suffix);
            if (platform) ss.Append($" ({Platform})");

            return ss.ToString();
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// AppendNumber
        ///
        /// <summary>
        /// バージョン番号を追加します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void AppendNumber(StringBuilder ss)
        {
            ss.Append($"{Number.Major}.{Number.Minor}");
            if (Digit <= 2) return;
            ss.Append($".{Number.Build}");
            if (Digit <= 3) return;
            ss.Append($".{Number.Revision}");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Parse
        ///
        /// <summary>
        /// バージョンを表す文字列を解析します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Parse(string str)
        {
            var match = Regex.Match(
                str,
                @"(?<prefix>.*?)(?<number>[0-9]+(\.[0-9]+){1,3})(?<suffix>.*)",
                RegexOptions.Singleline
            );

            Prefix = match.Groups["prefix"].Value;
            Suffix = match.Groups["suffix"].Value;

            var number = match.Groups["number"].Value;
            if (Version.TryParse(number, out Version result))
            {
                Number = result;
                Digit  = number.Count(c => c == '.') + 1;
            }
        }

        #endregion

        #region Fields
        private int _digit = 4;
        #endregion
    }
}
