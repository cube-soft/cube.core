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
    public class SoftwareVersion
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
            Number   = src.GetVersion();
            Platform = src.GetPlatform();
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
            if (TryParse(number, out var result))
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
        /// Platform
        ///
        /// <summary>
        /// Gets or sets the platform identification.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Platform { get; }

        #endregion

        #region Methods

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
            if (platform) Append(ss, $" ({Platform})");

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
        /// TryParse
        ///
        /// <summary>
        /// Tries to convert the specified string to a Version object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool TryParse(string src, out Version dest)
        {
            try
            {
                dest = new Version(src);
                return true;
            }
            catch
            {
                dest = null;
                return false;
            }
        }

        #endregion

        #region Fields
        private int _digit = 4;
        #endregion
    }
}
