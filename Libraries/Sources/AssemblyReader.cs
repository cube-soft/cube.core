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
using System.Reflection;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// AssemblyReader
    ///
    /// <summary>
    /// Assembly オブジェクトの各種情報を取得するクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class AssemblyReader
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// AssemblyReader
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="assembly">アセンブリオブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public AssemblyReader(Assembly assembly)
        {
            Assembly = assembly;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Platform
        ///
        /// <summary>
        /// ソフトウェアのプラットフォームを示す文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string Platform { get; } = (IntPtr.Size == 4) ? "x86" : "x64";

        /* ----------------------------------------------------------------- */
        ///
        /// Assembly
        ///
        /// <summary>
        /// 対象とする Assembly オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Assembly Assembly { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Location
        ///
        /// <summary>
        /// アセンブリの存在するパスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Location => Unify(Assembly?.Location);

        /* ----------------------------------------------------------------- */
        ///
        /// DirectoryName
        ///
        /// <summary>
        /// Gets the directory name that the assembly is located.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string DirectoryName
        {
            get
            {
                var path = Location;
                return path.HasValue() ? System.IO.Path.GetDirectoryName(path) : path;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Title
        ///
        /// <summary>
        /// タイトル情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Title => Unify(Get<AssemblyTitleAttribute>()?.Title);

        /* ----------------------------------------------------------------- */
        ///
        /// Description
        ///
        /// <summary>
        /// アセンブリの説明を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Description => Unify(Get<AssemblyDescriptionAttribute>()?.Description);

        /* ----------------------------------------------------------------- */
        ///
        /// Configuration
        ///
        /// <summary>
        /// Configuration 情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Configuration => Unify(Get<AssemblyConfigurationAttribute>()?.Configuration);

        /* ----------------------------------------------------------------- */
        ///
        /// Company
        ///
        /// <summary>
        /// 会社情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Company => Unify(Get<AssemblyCompanyAttribute>()?.Company);

        /* ----------------------------------------------------------------- */
        ///
        /// Product
        ///
        /// <summary>
        /// 製品情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Product => Unify(Get<AssemblyProductAttribute>()?.Product);

        /* ----------------------------------------------------------------- */
        ///
        /// Copyright
        ///
        /// <summary>
        /// 著作権情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Copyright => Unify(Get<AssemblyCopyrightAttribute>()?.Copyright);

        /* ----------------------------------------------------------------- */
        ///
        /// Trademark
        ///
        /// <summary>
        /// 商標情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Trademark => Unify(Get<AssemblyTrademarkAttribute>()?.Trademark);

        /* ----------------------------------------------------------------- */
        ///
        /// Culture
        ///
        /// <summary>
        /// Culture 情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Culture => Unify(Get<AssemblyCultureAttribute>()?.Culture);

        /* ----------------------------------------------------------------- */
        ///
        /// Version
        ///
        /// <summary>
        /// バージョン情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Version Version => Assembly?.GetName().Version ?? new Version();

        /* ----------------------------------------------------------------- */
        ///
        /// FileVersion
        ///
        /// <summary>
        /// ファイルバージョン情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Version FileVersion =>
            Assembly != null ?
            new Version(FileVersionInfo.GetVersionInfo(Assembly.Location).FileVersion) :
            new Version();

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Get
        ///
        /// <summary>
        /// CustomAttribute を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private T Get<T>() where T : class =>
            Assembly != null ?
            Attribute.GetCustomAttribute(Assembly, typeof(T)) as T :
            default(T);

        /* ----------------------------------------------------------------- */
        ///
        /// Unify
        ///
        /// <summary>
        /// Converts a null or empty string to the empty one.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private string Unify(string src) => src.Unify();

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// AssemblyReaderExtension
    ///
    /// <summary>
    /// AssemblyReader の拡張用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class AssemblyReaderExtension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// GetReader
        ///
        /// <summary>
        /// AssemblyReader オブジェクトを取得します。
        /// </summary>
        ///
        /// <param name="src">Assembly</param>
        ///
        /// <returns>AssemblyReader</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static AssemblyReader GetReader(this Assembly src) =>
            new AssemblyReader(src);

        #endregion
    }
}
