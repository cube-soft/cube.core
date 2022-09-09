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
namespace Cube.FileSystem;

using System.Collections.Generic;
using System.Linq;
using Cube.Collections;
using Cube.Collections.Extensions;
using Cube.String.Extensions;

/* ------------------------------------------------------------------------- */
///
/// SafePath
///
/// <summary>
/// Provides functionality to escape or remove the part of the provided
/// path according to the provided condition. The class also escapes
/// characters that cannot be used on Windows.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public sealed class SafePath
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// SafePath
    ///
    /// <summary>
    /// Initializes a new instance of the SafePath class with the
    /// specified path.
    /// </summary>
    ///
    /// <param name="src">Original path.</param>
    ///
    /* --------------------------------------------------------------------- */
    public SafePath(string src) => Source = src;

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Source
    ///
    /// <summary>
    /// Gets the original path.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Source { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// Value
    ///
    /// <summary>
    /// Gets the escaped path.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Value => EscapeOnce().Value;

    /* --------------------------------------------------------------------- */
    ///
    /// Parts
    ///
    /// <summary>
    /// Get a sequence of file or directory names separated by the
    /// path separator. Each file or directory name is escaped by
    /// the provided condition.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public IEnumerable<string> Parts => EscapeOnce().Parts;

    /* --------------------------------------------------------------------- */
    ///
    /// EscapeChar
    ///
    /// <summary>
    /// Gets the character used to replace invalid characters.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public char EscapeChar
    {
        get => _escapeChar;
        set => Set(ref _escapeChar, value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// AllowDriveLetter
    ///
    /// <summary>
    /// Gets or sets a value indicating whether the drive letter is
    /// allowed.
    /// </summary>
    ///
    /// <remarks>
    /// If set to false, the ":" (colon) following the drive letter will
    /// also be escaped.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public bool AllowDriveLetter
    {
        get => _allowDriveLetter;
        set => Set(ref _allowDriveLetter, value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// AllowCurrentDirectory
    ///
    /// <summary>
    /// Gets or sets a value indicating whether the character
    /// "." (single-dot), which indicates the current directory, is
    /// allowed.
    /// </summary>
    ///
    /// <remarks>
    /// If set to false, the "." character and the following path
    /// separator will simply be removed.
    /// For example, "foo\.\bar" would become "foo\bar".
    /// </remarks>
    ///
    /// <see cref="AllowInactivation"/>
    ///
    /* --------------------------------------------------------------------- */
    public bool AllowCurrentDirectory
    {
        get => _allowCurrentDirectory;
        set => Set(ref _allowCurrentDirectory, value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// AllowParentDirectory
    ///
    /// <summary>
    /// Gets or sets a value indicating whether the character
    /// ".." (double-dot), which indicates the parent directory, is
    /// allowed.
    /// </summary>
    ///
    /// <remarks>
    /// If set to false, the ".." character and the following path
    /// separator will simply be removed.
    /// For example, "foo\..\bar" would become "foo\bar".
    /// </remarks>
    ///
    /// <see cref="AllowInactivation"/>
    ///
    /* --------------------------------------------------------------------- */
    public bool AllowParentDirectory
    {
        get => _allowParentDirectory;
        set => Set(ref _allowParentDirectory, value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// AllowInactivation
    ///
    /// <summary>
    /// Gets or sets a value indicating whether the character "\\?\",
    /// which indicates the service inactivation, is allowed.
    /// </summary>
    ///
    /// <remarks>
    /// In deactivating a service function, ". and "..." are prohibited,
    /// so when set to true, these strings will be removed regardless of
    /// the AllowCurrentDirectory and AllowParentDirectory settings.
    /// Also, for implementation reasons, the AllowUnc setting is also
    /// ignored when set to true.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public bool AllowInactivation
    {
        get => _allowInactivation;
        set => Set(ref _allowInactivation, value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// AllowUnc
    ///
    /// <summary>
    /// Gets or sets a value indicating whether the character "\\",
    /// which indicates the UNC path, is allowed.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public bool AllowUnc
    {
        get => _allowUnc;
        set => Set(ref _allowUnc, value);
    }

    #endregion

    #region Constants

    /* --------------------------------------------------------------------- */
    ///
    /// SeparatorChar
    ///
    /// <summary>
    /// Gets the character that is used as the path separator.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static char SeparatorChar => SeparatorChars.First();

    /* --------------------------------------------------------------------- */
    ///
    /// SeparatorChars
    ///
    /// <summary>
    /// Gets the collection that may be used as the path separator.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static IEnumerable<char> SeparatorChars { get; } = new[]
    {
        System.IO.Path.DirectorySeparatorChar,
        System.IO.Path.AltDirectorySeparatorChar,
    };

    /* --------------------------------------------------------------------- */
    ///
    /// CurrentDirectorySymbol
    ///
    /// <summary>
    /// Gets the value that indicates the current directory.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static string CurrentDirectorySymbol { get; } = ".";

    /* --------------------------------------------------------------------- */
    ///
    /// ParentDirectorySymbol
    ///
    /// <summary>
    /// Gets the value that indicates the parent directory.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static string ParentDirectorySymbol { get; } = "..";

    /* --------------------------------------------------------------------- */
    ///
    /// UncSymbol
    ///
    /// <summary>
    /// Gets the value that indicates the UNC path.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static string UncSymbol { get; } = @"\\";

    /* --------------------------------------------------------------------- */
    ///
    /// InactivationSymbol
    ///
    /// <summary>
    /// Gets the value that indicates the service inactivation.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static string InactivationSymbol { get; } = @"\\?\";

    /* --------------------------------------------------------------------- */
    ///
    /// InvalidChars
    ///
    /// <summary>
    /// Gets the collection of characters that cannot be used as part
    /// of a path.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static IEnumerable<char> InvalidChars { get; } = System.IO.Path.GetInvalidFileNameChars();

    /* --------------------------------------------------------------------- */
    ///
    /// ReservedNames
    ///
    /// <summary>
    /// Gets the collection of names that is reserved by Windows.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static IEnumerable<string> ReservedNames { get; } = new[]
    {
        "CON",  "PRN",  "AUX",  "NUL",
        "COM0", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
        "LPT0", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9",
    };

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// Set
    ///
    /// <summary>
    /// Set the specified value to the specified field.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private bool Set<T>(ref T field, T value)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        _obj = null;
        return true;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// EscapeOnce
    ///
    /// <summary>
    /// Invokes the escape operation.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private EscapedObject EscapeOnce()
    {
        if (_obj == null)
        {
            var k = !Source.HasValue()                         ? PathKind.Normal :
                    Source.FuzzyStartsWith(InactivationSymbol) ? PathKind.Inactivation :
                    Source.FuzzyStartsWith(UncSymbol)          ? PathKind.Unc :
                    PathKind.Normal;

            var v = !Source.HasValue() ?
                    new string[0] :
                    Source.Split(SeparatorChars.ToArray())
                          .SkipWhile(s => !s.HasValue())
                          .Where((s, i) => !IsRemove(s, i))
                          .Select((s, i) => Escape(s, i))
                          .ToArray();

            _obj = new EscapedObject(k, v, Combine(k, v));
        }
        return _obj;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Escape
    ///
    /// <summary>
    /// Invokes the escape operation.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private string Escape(string name, int index)
    {
        if (AllowDriveLetter && index == 0 && name.Length == 2 &&
            char.IsLetter(name[0]) && name[1] == ':') return name;

        var seq  = name.Select(c => InvalidChars.Contains(c) ? EscapeChar : c);
        var esc  = new string(seq.ToArray());
        var dot  = esc == CurrentDirectorySymbol || esc == ParentDirectorySymbol;
        var dest = dot ? esc : esc.TrimEnd(new[] { ' ', '.' });

        return IsReserved(dest) ? $"{EscapeChar}{dest}" : dest;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Combine
    ///
    /// <summary>
    /// Combines the specified paths.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private string Combine(PathKind kind, string[] parts)
    {
        var dest = parts.Join(SeparatorChar.ToString());
        var head = kind == PathKind.Inactivation && AllowInactivation ? InactivationSymbol :
                   kind == PathKind.Unc && GetAllowUnc() ? UncSymbol :
                   string.Empty;
        return head.HasValue() ? $"{head}{dest}" : dest;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// IsRemove
    ///
    /// <summary>
    /// Determines whether the specified name will be removed.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private bool IsRemove(string name, int index)
    {
        if (!name.HasValue()) return true;
        if (index == 0 && name == "?") return true;
        if (name == CurrentDirectorySymbol && !GetAllowCurrentDirectory()) return true;
        if (name == ParentDirectorySymbol && !GetAllowParentDirectory()) return true;
        return false;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// IsReserved
    ///
    /// <summary>
    /// Determines whether the specified name is reserved.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private bool IsReserved(string src)
    {
        var index = src.IndexOf('.');
        var name  = index < 0 ? src : src.Substring(0, index);
        var cmp   = new LambdaEqualityComparer<string>((x, y) => string.Compare(x, y, true) == 0);
        return ReservedNames.Contains(name, cmp);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetAllowCurrentDirectory
    ///
    /// <summary>
    /// Gets a value indicating whether the character "." (single-dot)
    /// is allowed.
    /// </summary>
    ///
    /// <remarks>
    /// When AllowInactivation is enabled, it is disabled.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    private bool GetAllowCurrentDirectory() => !AllowInactivation && AllowCurrentDirectory;

    /* --------------------------------------------------------------------- */
    ///
    /// GetAllowParentDirectory
    ///
    /// <summary>
    /// Gets a value indicating whether the character ".." (double-dot)
    /// is allowed.
    /// </summary>
    ///
    /// <remarks>
    /// When AllowInactivation is enabled, it is disabled.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    private bool GetAllowParentDirectory() => !AllowInactivation && AllowParentDirectory;

    /* --------------------------------------------------------------------- */
    ///
    /// GetAllowUnc
    ///
    /// <summary>
    /// Gets a value indicating whether the UNC path  is allowed.
    /// </summary>
    ///
    /// <remarks>
    /// When AllowInactivation is enabled, it is disabled.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    private bool GetAllowUnc() => !AllowInactivation && AllowUnc;

    /* --------------------------------------------------------------------- */
    ///
    /// PathKind
    ///
    /// <summary>
    /// Specifies the path kind.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private enum PathKind
    {
        /// <summary>Normal path.</summary>
        Normal,
        /// <summary>UNC path.</summary>
        Unc,
        /// <summary>Inactivated path.</summary>
        Inactivation,
    }

    #endregion

    #region EscapedObject

    /* --------------------------------------------------------------------- */
    ///
    /// EscapedObject
    ///
    /// <summary>
    /// Represents the escaped object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private class EscapedObject
    {
        /* ----------------------------------------------------------------- */
        ///
        /// EscapedObject
        ///
        /// <summary>
        /// Initializes a new instance of the EscapedObject with the
        /// specified arguments.
        /// </summary>
        ///
        /// <param name="k">Path kind.</param>
        /// <param name="v">Collection of escaped names.</param>
        /// <param name="s">Escaped path.</param>
        ///
        /* ----------------------------------------------------------------- */
        public EscapedObject(PathKind k, string[] v, string s)
        {
            Kind  = k;
            Parts = v;
            Value = s;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Kind
        ///
        /// <summary>
        /// Gets the path kind.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public PathKind Kind  { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Parts
        ///
        /// <summary>
        /// Gets the collection of escaped file or directory names.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string[] Parts { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Value
        ///
        /// <summary>
        /// Gets the escaped path.
        /// </summary>
        ///
        /// <remarks>
        /// The value will be a simple concatenation of the Parts
        /// property and the prefix according to Kind.
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public string Value { get; }
    }

    #endregion

    #region Fields
    private EscapedObject _obj;
    private char _escapeChar = '_';
    private bool _allowDriveLetter = true;
    private bool _allowCurrentDirectory = true;
    private bool _allowParentDirectory = true;
    private bool _allowInactivation = false;
    private bool _allowUnc = true;
    #endregion
}
