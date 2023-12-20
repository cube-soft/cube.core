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

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Cube.Collections.Extensions;
using Cube.Text.Extensions;

/* ------------------------------------------------------------------------- */
///
/// FileDialogFilter
///
/// <summary>
/// Provides functionality to create a filter description for the
/// OpenFileDialog or SaveFileDialog.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class FileDialogFilter
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// FileDialogFilter
    ///
    /// <summary>
    /// Initializes a new instance of the FileDialogFilter class
    /// with the specified parameters
    /// </summary>
    ///
    /// <param name="description">Description for the filter.</param>
    /// <param name="extensions">
    /// List of target extensions (e.g., ".txt").
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    public FileDialogFilter(string description, params string[] extensions) :
        this(description, true, extensions) { }

    /* --------------------------------------------------------------------- */
    ///
    /// FileDialogFilter
    ///
    /// <summary>
    /// Initializes a new instance of the FileDialogFilter class
    /// with the specified parameters
    /// </summary>
    ///
    /// <param name="text">Description for the filter.</param>
    /// <param name="ignoreCase">Ignores case or not.</param>
    /// <param name="extensions">
    /// List of target extensions (e.g., ".txt").
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    public FileDialogFilter(string text, bool ignoreCase, params string[] extensions)
    {
        Text       = text;
        IgnoreCase = ignoreCase;
        Targets    = Normalize(extensions).Where(e => e.HasValue()).ToList();
    }

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Text
    ///
    /// <summary>
    /// Gets a description for the filter.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Text { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// Targets
    ///
    /// <summary>
    /// Gets a list of target extensions.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public IEnumerable<string> Targets { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// IgnoreCase
    ///
    /// <summary>
    /// Gets a value indicating whether letter cases of the specified
    /// extensions are ignored.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public bool IgnoreCase { get; }

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// ToString
    ///
    /// <summary>
    /// Converts to a string representing the filter.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public override string ToString()
    {
        var src = Targets.Select(e => $"*{e}");
        var cvt = src as string[] ?? src.ToArray();
        var s0  = cvt.Join(", ");
        var s1  = cvt.Join(";", Format);

        return $"{Text} ({s0})|{s1}";
    }

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// Normalize
    ///
    /// <summary>
    /// Normalizes the specified extensions.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private IEnumerable<string> Normalize(IEnumerable<string> src) => src.Select(e =>
    {
        if (!e.HasValue() || e.StartsWith(".")) return e;
        if (e.StartsWith("*.")) return e.Substring(1);
        return $".{e}";
    });

    /* --------------------------------------------------------------------- */
    ///
    /// Format
    ///
    /// <summary>
    /// Converts an extension to a filter string according to the user
    /// settings.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private string Format(string src)
    {
        if (!IgnoreCase) return src;

        var cvt = CultureInfo.InvariantCulture.TextInfo;

        return new[] {
            src,
            cvt.ToLower(src),
            cvt.ToUpper(src),
            cvt.ToTitleCase(src),
        }.Distinct().Join(";");
    }

    #endregion
}
