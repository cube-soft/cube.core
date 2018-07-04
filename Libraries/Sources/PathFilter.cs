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
using Cube.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cube.FileSystem
{
    /* --------------------------------------------------------------------- */
    ///
    /// PathFilter
    ///
    /// <summary>
    /// パスのフィルタ用クラスです。
    /// </summary>
    ///
    /// <remarks>
    /// Windows で使用不可能な文字のエスケープ処理を行います。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class PathFilter
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// PathFilter
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="path">対象とするパス文字列</param>
        ///
        /* ----------------------------------------------------------------- */
        public PathFilter(string path) : this(path, new IO()) { }

        /* ----------------------------------------------------------------- */
        ///
        /// PathFilter
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="path">対象とするパス文字列</param>
        /// <param name="io">ファイル操作用オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public PathFilter(string path, IO io)
        {
            RawPath = path;
            _io = io;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// RawPath
        ///
        /// <summary>
        /// オリジナルのパスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string RawPath { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// EscapedResult
        ///
        /// <summary>
        /// エスケープ処理適用結果を示すオブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected EscapedObject EscapedResult => EscapeOnce();

        /* ----------------------------------------------------------------- */
        ///
        /// EscapedPath
        ///
        /// <summary>
        /// エスケープ処理適用後のパスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string EscapedPath => EscapedResult.Value;

        /* ----------------------------------------------------------------- */
        ///
        /// EscapeChar
        ///
        /// <summary>
        /// 使用不可能な文字を置き換える文字を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public char EscapeChar
        {
            get => _escapeChar;
            set => Set(ref _escapeChar, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AllowDriveLetter
        ///
        /// <summary>
        /// ドライブ文字を許容するかどうかを示す値を取得または設定します。
        /// </summary>
        ///
        /// <remarks>
        /// false に設定した場合、ドライブ文字に続く ":"（コロン）も
        /// エスケープ処理の対象となります。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public bool AllowDriveLetter
        {
            get => _allowDriveLetter;
            set => Set(ref _allowDriveLetter, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AllowCurrentDirectory
        ///
        /// <summary>
        /// カレントディレクトリを表す "." (single-dot) を許可するか
        /// どうかを示す値を取得または設定します。
        /// </summary>
        ///
        /// <remarks>
        /// false に設定した場合、"." 部分のディレクトリを取り除きます。
        /// 例えば、"foo\.\bar" は "foo\bar" となります。
        /// </remarks>
        ///
        /// <see cref="AllowInactivation"/>
        ///
        /* ----------------------------------------------------------------- */
        public bool AllowCurrentDirectory
        {
            get => _allowCurrentDirectory;
            set => Set(ref _allowCurrentDirectory, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AllowParentDirectory
        ///
        /// <summary>
        /// 一階層上のディレクトリを表す ".." (double-dot) を許可するか
        /// どうかを示す値を取得または設定します。
        /// </summary>
        ///
        /// <remarks>
        /// false に設定した場合、".." 部分のディレクトリを取り除きます。
        /// 例えば、"foo\..\bar" は "foo\bar" となります。
        /// </remarks>
        ///
        /// <see cref="AllowInactivation"/>
        ///
        /* ----------------------------------------------------------------- */
        public bool AllowParentDirectory
        {
            get => _allowParentDirectory;
            set => Set(ref _allowParentDirectory, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AllowInactivation
        ///
        /// <summary>
        /// サービス機能の不活性化を表す接頭辞 "\\?\" を許可するかどうかを
        /// 示す値を取得または設定します。
        /// </summary>
        ///
        /// <remarks>
        /// サービス機能の不活性化では "." および ".." は禁止されるため、
        /// true 設定時には AllowCurrentDirectory, AllowParentDirectory の
        /// 設定に関わらず、これらの文字列は除去されます。また、実装上の
        /// 都合で、true 設定時には AllowUnc の設定も無視されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public bool AllowInactivation
        {
            get => _allowInactivation;
            set => Set(ref _allowInactivation, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AllowUnc
        ///
        /// <summary>
        /// UNC パスを表す接頭辞 "\\" を許可するかどうかを示す値を取得
        /// または設定します。
        /// </summary>
        ///
        /// <see cref="AllowInactivation"/>
        ///
        /* ----------------------------------------------------------------- */
        public bool AllowUnc
        {
            get => _allowUnc;
            set => Set(ref _allowUnc, value);
        }

        #endregion

        #region Constants

        /* ----------------------------------------------------------------- */
        ///
        /// SeparatorChars
        ///
        /// <summary>
        /// パスの区切り文字を表す文字を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static char[] SeparatorChars { get; } = new[]
        {
            System.IO.Path.DirectorySeparatorChar,
            System.IO.Path.AltDirectorySeparatorChar,
        };

        /* ----------------------------------------------------------------- */
        ///
        /// CurrentDirectorySymbol
        ///
        /// <summary>
        /// カレントディレクトリを表す文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string CurrentDirectorySymbol { get; } = ".";

        /* ----------------------------------------------------------------- */
        ///
        /// ParentDirectorySymbol
        ///
        /// <summary>
        /// 親ディレクトリを表す文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string ParentDirectorySymbol { get; } = "..";

        /* ----------------------------------------------------------------- */
        ///
        /// UncSymbol
        ///
        /// <summary>
        /// UNC パスを表す接頭辞を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string UncSymbol { get; } = @"\\";

        /* ----------------------------------------------------------------- */
        ///
        /// InactivationSymbol
        ///
        /// <summary>
        /// サービス機能の不活性化を表す接頭辞を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string InactivationSymbol { get; } = @"\\?\";

        /* ----------------------------------------------------------------- */
        ///
        /// InvalidChars
        ///
        /// <summary>
        /// パスに使用不可能な記号一覧を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static char[] InvalidChars { get; } = System.IO.Path.GetInvalidFileNameChars();

        /* ----------------------------------------------------------------- */
        ///
        /// ReservedNames
        ///
        /// <summary>
        /// Windows で予約済みの名前一覧を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string[] ReservedNames { get; } = new[]
        {
            "CON",  "PRN",  "AUX",  "NUL",
            "COM0", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
            "LPT0", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9",
        };

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Match
        ///
        /// <summary>
        /// 指定されたファイル名またはディレクトリ名がパス中のどこかに
        /// 存在するかどうかを判別します。
        /// </summary>
        ///
        /// <param name="name">
        /// 判別するファイル名またはディレクトリ名
        /// </param>
        ///
        /// <returns>存在するかどうかを示す値</returns>
        ///
        /* ----------------------------------------------------------------- */
        public bool Match(string name) => Match(name, true);

        /* ----------------------------------------------------------------- */
        ///
        /// Match
        ///
        /// <summary>
        /// 指定されたファイル名またはディレクトリ名がパス中のどこかに
        /// 存在するかどうかを判別します。
        /// </summary>
        ///
        /// <param name="name">
        /// 判別するファイル名またはディレクトリ名
        /// </param>
        ///
        /// <param name="ignoreCase">
        /// 大文字・小文字を区別するかどうかを示す値
        /// </param>
        ///
        /// <returns>存在するかどうかを示す値</returns>
        ///
        /* ----------------------------------------------------------------- */
        public bool Match(string name, bool ignoreCase) =>
            EscapedResult.Parts.Any(s => string.Compare(s, name, ignoreCase) == 0);

        /* ----------------------------------------------------------------- */
        ///
        /// MatchAny
        ///
        /// <summary>
        /// 指定されたファイル名またはディレクトリ名のいずれか 1 つでも
        /// パス中のどこかに存在するかどうかを判別します。
        /// </summary>
        ///
        /// <param name="names">
        /// 判別するファイル名またはディレクトリ名一覧
        /// </param>
        ///
        /// <returns>存在するかどうかを示す値</returns>
        ///
        /* ----------------------------------------------------------------- */
        public bool MatchAny(IEnumerable<string> names) => MatchAny(names, true);

        /* ----------------------------------------------------------------- */
        ///
        /// MatchAny
        ///
        /// <summary>
        /// 指定されたファイル名またはディレクトリ名のいずれか 1 つでも
        /// パス中のどこかに存在するかどうかを判別します。
        /// </summary>
        ///
        /// <param name="names">
        /// 判別するファイル名またはディレクトリ名一覧
        /// </param>
        ///
        /// <param name="ignoreCase">
        /// 大文字・小文字を区別するかどうかを示す値
        /// </param>
        ///
        /// <returns>存在するかどうかを示す値</returns>
        ///
        /* ----------------------------------------------------------------- */
        public bool MatchAny(IEnumerable<string> names, bool ignoreCase)
        {
            foreach (var name in names)
            {
                if (Match(name, ignoreCase)) return true;
            }
            return false;
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Set
        ///
        /// <summary>
        /// プロパティに値を設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool Set<T>(ref T field, T value)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            _result = null;
            return true;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// EscapeOnce
        ///
        /// <summary>
        /// エスケープ処理を実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private EscapedObject EscapeOnce()
        {
            if (_result == null)
            {
                var k = string.IsNullOrEmpty(RawPath)          ? PathKind.Normal :
                        RawPath.StartsWith(InactivationSymbol) ? PathKind.Inactivation :
                        RawPath.StartsWith(UncSymbol)          ? PathKind.Unc :
                        PathKind.Normal;

                var v = string.IsNullOrEmpty(RawPath) ?
                        new string[0] :
                        RawPath.Split(SeparatorChars)
                               .SkipWhile(s => string.IsNullOrEmpty(s))
                               .Where((s, i) => !IsRemove(s, i))
                               .Select((s, i) => Escape(s, i))
                               .ToArray();

                _result = new EscapedObject(k, v, Combine(k, v));
            }
            return _result;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Escape
        ///
        /// <summary>
        /// エスケープ処理を実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private string Escape(string name, int index)
        {
            if (AllowDriveLetter && index == 0 && name.Length == 2 &&
                char.IsLetter(name[0]) && name[1] == ':') return name + '\\';

            var seq  = name.Select(c => InvalidChars.Contains(c) ? EscapeChar : c);
            var esc  = new string(seq.ToArray());
            var dot  = (esc == CurrentDirectorySymbol || esc == ParentDirectorySymbol);
            var dest = dot ? esc : esc.TrimEnd(new[] { ' ', '.' });

            return IsReserved(dest) ? $"{EscapeChar}{dest}" : dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Combine
        ///
        /// <summary>
        /// パスを結合します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private string Combine(PathKind kind, string[] parts)
        {
            var dest = _io.Combine(parts);
            var head = kind == PathKind.Inactivation && AllowInactivation ? InactivationSymbol :
                       kind == PathKind.Unc && AllowUncCore() ? UncSymbol :
                       string.Empty;
            return !string.IsNullOrEmpty(head) ? $"{head}{dest}" : dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IsRemove
        ///
        /// <summary>
        /// 除去する文字列かどうかを判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool IsRemove(string name, int index)
        {
            if (string.IsNullOrEmpty(name)) return true;
            if (index == 0 && name == "?") return true;
            if (name == CurrentDirectorySymbol && !AllowCurrentDirectoryCore()) return true;
            if (name == ParentDirectorySymbol && !AllowParentDirectoryCore()) return true;
            return false;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IsReserved
        ///
        /// <summary>
        /// 予約された文字列かどうかを判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool IsReserved(string src)
        {
            var index = src.IndexOf('.');
            var name  = index < 0 ? src : src.Substring(0, index);
            var cmp   = new GenericEqualityComparer<string>((x, y) => string.Compare(x, y, true) == 0);
            return ReservedNames.Contains(name, cmp);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AllowCurrentDirectoryCore
        ///
        /// <summary>
        /// カレントディレクトリを表す "." (single-dot) を許可するか
        /// どうかを示す値を取得します。
        /// </summary>
        ///
        /// <remarks>
        /// AllowInactivation 有効時は無効化されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private bool AllowCurrentDirectoryCore() =>
            !AllowInactivation && AllowCurrentDirectory;

        /* ----------------------------------------------------------------- */
        ///
        /// AllowParentDirectoryCore
        ///
        /// <summary>
        /// 一階層上のディレクトリを表す ".." (double-dot) を許可するか
        /// どうかを示す値を取得または設定します。
        /// </summary>
        ///
        /// <remarks>
        /// AllowInactivation 有効時は無効化されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private bool AllowParentDirectoryCore() =>
            !AllowInactivation && AllowParentDirectory;

        /* ----------------------------------------------------------------- */
        ///
        /// AllowUncCore
        ///
        /// <summary>
        /// UNC パスを表す接頭辞 "\\" を許可するかどうかを示す値を取得
        /// または設定します。
        /// </summary>
        ///
        /// <remarks>
        /// AllowInactivation 有効時は無効化されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private bool AllowUncCore() =>
            !AllowInactivation && AllowUnc;

        /* ----------------------------------------------------------------- */
        ///
        /// PathKind
        ///
        /// <summary>
        /// パスの種類を示す列挙型です。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected enum PathKind
        {
            /// <summary>通常のパス</summary>
            Normal,
            /// <summary>UNC パス</summary>
            Unc,
            /// <summary>サービス機能の不活性化されたパス</summary>
            Inactivation,
        }

        #endregion

        #region EscapedObject

        /* ----------------------------------------------------------------- */
        ///
        /// EscapedObject
        ///
        /// <summary>
        /// エスケープ処理適用後の状態を保持するためのクラスです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected class EscapedObject
        {
            /* ------------------------------------------------------------- */
            ///
            /// EscapedResult
            ///
            /// <summary>
            /// オブジェクトを初期化します。
            /// </summary>
            ///
            /// <param name="k">パスの種類</param>
            /// <param name="v">エスケープ処理の適用されたパス</param>
            /// <param name="s">最終結果</param>
            ///
            /// <see cref="Value"/>
            ///
            /* ------------------------------------------------------------- */
            public EscapedObject(PathKind k, string[] v, string s)
            {
                Kind  = k;
                Parts = v;
                Value = s;
            }

            /* ------------------------------------------------------------- */
            ///
            /// Kind
            ///
            /// <summary>
            /// パスの種類を取得します。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            public PathKind Kind  { get; }

            /* ------------------------------------------------------------- */
            ///
            /// Parts
            ///
            /// <summary>
            /// パスを分割後、エスケープ処理を適用した結果を取得します。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            public string[] Parts { get; }

            /* ------------------------------------------------------------- */
            ///
            /// Value
            ///
            /// <summary>
            /// 最終結果を取得します。
            /// </summary>
            ///
            /// <remarks>
            /// 最終結果は、Parts プロパティを単純に連結した結果と
            /// Kind に応じた接頭辞を結合したものになります。
            /// </remarks>
            ///
            /* ------------------------------------------------------------- */
            public string Value { get; }
        }

        #endregion

        #region Fields
        private readonly IO _io;
        private EscapedObject _result;
        private char _escapeChar = '_';
        private bool _allowDriveLetter = true;
        private bool _allowCurrentDirectory = true;
        private bool _allowParentDirectory = true;
        private bool _allowInactivation = false;
        private bool _allowUnc = true;
        #endregion
    }
}
