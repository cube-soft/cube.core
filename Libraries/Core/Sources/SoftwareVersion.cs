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
namespace Cube;

using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Cube.Mixin.Assembly;
using Cube.Mixin.String;

/* ------------------------------------------------------------------------- */
///
/// SoftwareVersion
///
/// <summary>
/// Represents the software version.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class SoftwareVersion
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// SoftwareVersion
    ///
    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public SoftwareVersion() : this(new Version(1, 0, 0, 0)) { }

    /* --------------------------------------------------------------------- */
    ///
    /// SoftwareVersion
    ///
    /// <summary>
    /// Initializes a new instance of the class with the specified
    /// arguments.
    /// </summary>
    ///
    /// <param name="src">Version object.</param>
    ///
    /* --------------------------------------------------------------------- */
    public SoftwareVersion(Version src)
    {
        Number       = src;
        Architecture = GetType().Assembly.GetArchitecture();
    }

    /* --------------------------------------------------------------------- */
    ///
    /// SoftwareVersion
    ///
    /// <summary>
    /// Initializes a new instance of the class with the specified
    /// string.
    /// </summary>
    ///
    /// <param name="src">
    /// String value that represents the version.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    public SoftwareVersion(string src) : this()
    {
        if (!src.HasValue()) return;

        var match = Regex.Match(
            src,
            @"(?<prefix>.*?)(?<number>[0-9]+(\.[0-9]+){1,3})(?<suffix>.*)",
            RegexOptions.Singleline
        );

        Prefix = match.Groups["prefix"].Value;
        Suffix = match.Groups["suffix"].Value;

        var number = match.Groups["number"].Value;
        if (Version.TryParse(number, out var result)) Number = result;
    }

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Number
    ///
    /// <summary>
    /// Gets or sets the value that represents the version number.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public Version Number { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// Prefix
    ///
    /// <summary>
    /// Gets or sets the prefix of the version.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Prefix { get; set; } = string.Empty;

    /* --------------------------------------------------------------------- */
    ///
    /// Suffix
    ///
    /// <summary>
    /// Gets or sets the suffix of the version.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Suffix { get; set; } = string.Empty;

    /* --------------------------------------------------------------------- */
    ///
    /// Architecture
    ///
    /// <summary>
    /// Gets the architecture identification (32bit or 64bit).
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Architecture { get; }

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public override string ToString() => ToString(3, false);

    /* --------------------------------------------------------------------- */
    ///
    /// ToString
    ///
    /// <summary>
    /// Returns the string that represents the version.
    /// </summary>
    ///
    /// <param name="digit">Number of display digits</param>
    /// <param name="architecture">
    /// Indicates whether the architecture identification is displayed.
    /// </param>
    ///
    /// <returns>String for the version.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public string ToString(int digit, bool architecture)
    {
        var ss = new StringBuilder();

        if (Prefix.HasValue()) Append(ss, Prefix);
        AppendNumber(ss, Math.Max(Math.Min(digit, 4), 1));
        if (Suffix.HasValue()) Append(ss, Suffix);
        if (architecture) Append(ss, $" ({Architecture})");

        return ss.ToString();
    }

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// AppendNumber
    ///
    /// <summary>
    /// Appends the version number to the specified object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private void AppendNumber(StringBuilder dest, int digit)
    {
        Append(dest, $"{Number.Major}");
        if (digit <= 1) return;
        Append(dest, $".{Number.Minor}");
        if (digit <= 2) return;
        Append(dest, $".{Number.Build}");
        if (digit <= 3) return;
        Append(dest, $".{Number.Revision}");
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Append
    ///
    /// <summary>
    /// Appends the specified value.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private void Append(StringBuilder src, string value)
    {
        var check = src.Append(value);
        Debug.Assert(check == src);
    }

    #endregion
}
