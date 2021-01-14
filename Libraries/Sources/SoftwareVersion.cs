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
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Cube.Mixin.Assembly;
using Cube.Mixin.String;

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
    public class SoftwareVersion : IComparable<SoftwareVersion>, IComparable
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// SoftwareVersion
        ///
        /// <summary>
        /// Initializes a new instance of the class with the specified
        /// assembly.
        /// </summary>
        ///
        /// <param name="src">Assembly object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public SoftwareVersion(Assembly src)
        {
            Number       = src.GetVersion();
            Architecture = src.GetArchitecture();
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
        /// <param name="version">
        /// String value that represents the version.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public SoftwareVersion(string version) : this(typeof(SoftwareVersion).Assembly)
        {
            if (!version.HasValue()) return;

            var match = Regex.Match(
                version,
                @"(?<prefix>.*?)(?<number>[0-9]+(\.[0-9]+){1,3})(?<suffix>.*)",
                RegexOptions.Singleline
            );

            Prefix = match.Groups["prefix"].Value;
            Suffix = match.Groups["suffix"].Value;

            var number = match.Groups["number"].Value;
            if (Version.TryParse(number, out var result))
            {
                Number = result;
                Digit = number.Count(c => c == '.') + 1;
            }
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
        public Version Number { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Digit
        ///
        /// <summary>
        /// Gets or sets the number of significant digits of version.
        /// </summary>
        ///
        /// <remarks>
        /// 2, 3, or 4 is available.
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
        /// Architecture
        ///
        /// <summary>
        /// Gets the architecture identification (32bit or 64bit).
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Architecture { get; }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// CompareTo
        ///
        /// <summary>
        /// Compares this instance with a specified object and indicates
        /// whether this instance precedes, follows, or appears in the same
        /// position in the sort order as the specified object.
        /// </summary>
        ///
        /// <param name="value">Compared value.</param>
        ///
        /// <returns>
        /// 32-bit signed integer that indicates whether this instance
        /// precedes, follows, or appears in the same position in the sort
        /// order as the value parameter.
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public int CompareTo(SoftwareVersion value)
        {
            var e0 = CompareValue(Prefix, value.Prefix);
            if (e0 != 0) return e0;

            var e1 = CompareValue(Zero(Number.Major), Zero(value.Number.Major));
            if (e1 != 0) return e1;

            var e2 = CompareValue(Zero(Number.Minor), Zero(value.Number.Minor));
            if (e2 != 0) return e2;

            var e3 = CompareValue(Zero(Number.Build), Zero(value.Number.Build));
            if (e3 != 0) return e3;

            var e4 = CompareValue(Zero(Number.Revision), Zero(value.Number.Revision));
            if (e4 != 0) return e4;

            return CompareValue(Suffix, value.Suffix);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CompareTo
        ///
        /// <summary>
        /// Compares this instance with a specified object and indicates
        /// whether this instance precedes, follows, or appears in the same
        /// position in the sort order as the specified object.
        /// </summary>
        ///
        /// <param name="value">
        /// Object that evaluates to a SoftwareVersion.
        /// </param>
        ///
        /// <returns>
        /// 32-bit signed integer that indicates whether this instance
        /// precedes, follows, or appears in the same position in the sort
        /// order as the value parameter.
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public int CompareTo(object value)
        {
            if (value is SoftwareVersion e) return CompareTo(e);
            else return 1;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToString
        ///
        /// <summary>
        /// Returns the string that represents the version without the
        /// platform identification.
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

            if (Prefix.HasValue()) Append(ss, Prefix);
            AppendNumber(ss);
            if (Suffix.HasValue()) Append(ss, Suffix);
            if (platform) Append(ss, $" ({Architecture})");

            return ss.ToString();
        }

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
            Append(dest, $"{Number.Major}.{Number.Minor}");
            if (Digit <= 2) return;
            Append(dest, $".{Number.Build}");
            if (Digit <= 3) return;
            Append(dest, $".{Number.Revision}");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Append
        ///
        /// <summary>
        /// Appends the specified value.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Append(StringBuilder src, string value)
        {
            var check = src.Append(value);
            Debug.Assert(check == src);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Zero
        ///
        /// <summary>
        /// Returns zero if the specified value is negative.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private int Zero(int src) => src > 0 ? src : 0;

        /* ----------------------------------------------------------------- */
        ///
        /// CompareValue
        ///
        /// <summary>
        /// Compares the specified values.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private int CompareValue(int src, int cmp)
        {
            var value = src - cmp;
            return value < 0 ? -1 :
                   value > 0 ?  1 : 0;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CompareValue
        ///
        /// <summary>
        /// Compares the specified values.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private int CompareValue(string src, string cmp) =>
            string.Compare(src, cmp, StringComparison.InvariantCultureIgnoreCase);

        #endregion

        #region Fields
        private int _digit = 4;
        #endregion
    }
}
