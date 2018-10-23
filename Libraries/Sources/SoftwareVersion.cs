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
using System.Diagnostics;
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
    /// Represents the software version.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class SoftwareVersion : IEquatable<SoftwareVersion>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// SoftwareVersion
        ///
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SoftwareVersion() { }

        /* ----------------------------------------------------------------- */
        ///
        /// SoftwareVersion
        ///
        /// <summary>
        /// Initializes a new instance of the class with the specified
        /// assembly.
        /// </summary>
        ///
        /// <param name="assembly">Assembly object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public SoftwareVersion(Assembly assembly)
        {
            Debug.Assert(assembly != null);
            Number = assembly.GetName().Version;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SoftwareVersion
        ///
        /// <summary>
        /// Initializes a new instance of the class with the specified
        /// string.
        /// </summary>
        ///
        /// <param name="version">Represents the version.</param>
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
        /// Gets or sets the value that represents the version number.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Version Number { get; set; } = new Version(1, 0, 0, 0);

        /* ----------------------------------------------------------------- */
        ///
        /// Digit
        ///
        /// <summary>
        /// Gets or sets the number of significant digits of version.
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
        /// Gets or sets the prefix of the version.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Prefix { get; set; } = string.Empty;

        /* ----------------------------------------------------------------- */
        ///
        /// Suffix
        ///
        /// <summary>
        /// Gets or sets the suffix of the version.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Suffix { get; set; } = string.Empty;

        /* ----------------------------------------------------------------- */
        ///
        /// Platform
        ///
        /// <summary>
        /// Gets or sets the platform identification.
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
        /// Returns the string that represents the version without the
        /// platform idintification.
        /// </summary>
        ///
        /// <returns>String for the version.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public override string ToString() => ToString(false);

        /* ----------------------------------------------------------------- */
        ///
        /// ToString
        ///
        /// <summary>
        /// Returns the string that represents the version.
        /// </summary>
        ///
        /// <param name="platform">
        /// Indicates whether the platform identification is displayed.
        /// </param>
        ///
        /// <returns>String for the version.</returns>
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

        /* ----------------------------------------------------------------- */
        ///
        /// Equals
        ///
        /// <summary>
        /// Indicates whether the current object is equal to another object
        /// of the same type.
        /// </summary>
        ///
        /// <param name="other">Object to compare with this object.</param>
        ///
        /// <returns>
        /// true if the current object is equal to the other parameter;
        /// otherwise, false.
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public bool Equals(SoftwareVersion other) =>
            other != null &&
            Number == other.Number &&
            Prefix == other.Prefix &&
            Suffix == other.Suffix;

        /* ----------------------------------------------------------------- */
        ///
        /// Equals
        ///
        /// <summary>
        /// Indicates whether the current object is equal to another object.
        /// </summary>
        ///
        /// <param name="other">Object to compare with this object.</param>
        ///
        /// <returns>
        /// true if the current object is equal to the other parameter;
        /// otherwise, false.
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public override bool Equals(object other) =>
            other != null &&
            GetType() == other.GetType() &&
            Equals((SoftwareVersion)other);

        /* ----------------------------------------------------------------- */
        ///
        /// Equals
        ///
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        ///
        /// <returns>
        /// 32-bit signed integer hash code.
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public override int GetHashCode() =>
            Number.GetHashCode() ^ Prefix.GetHashCode() ^ Suffix.GetHashCode();

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// AppendNumber
        ///
        /// <summary>
        /// Appends the version number to the specified object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void AppendNumber(StringBuilder dest)
        {
            dest.Append($"{Number.Major}.{Number.Minor}");
            if (Digit <= 2) return;
            dest.Append($".{Number.Build}");
            if (Digit <= 3) return;
            dest.Append($".{Number.Revision}");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Parse
        ///
        /// <summary>
        /// Parses the string that represents the version.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Parse(string str)
        {
            if (!str.HasValue()) return;

            var match = Regex.Match(
                str,
                @"(?<prefix>.*?)(?<number>[0-9]+(\.[0-9]+){1,3})(?<suffix>.*)",
                RegexOptions.Singleline
            );

            Prefix = match.Groups["prefix"].Value;
            Suffix = match.Groups["suffix"].Value;

            var number = match.Groups["number"].Value;
            var result = Convert(number);
            if (result != null)
            {
                Number = result;
                Digit  = number.Count(c => c == '.') + 1;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Convert
        ///
        /// <summary>
        /// バージョンを表す文字列を Version オブジェクトに変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Version Convert(string src)
        {
            try { return new Version(src); }
            catch { return null; }
        }

        #endregion

        #region Fields
        private int _digit = 4;
        #endregion
    }
}
