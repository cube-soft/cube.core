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
        /// Default
        ///
        /// <summary>
        /// 既定の AssemblyReader オブジェクトを取得します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static AssemblyReader Default { get; }
            = new AssemblyReader(Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly());

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
        public string Location => Assembly?.Location;

        /* ----------------------------------------------------------------- */
        ///
        /// Title
        ///
        /// <summary>
        /// タイトル情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Title
            => Normalize(Get<AssemblyTitleAttribute>()?.Title);

        /* ----------------------------------------------------------------- */
        ///
        /// Description
        ///
        /// <summary>
        /// アセンブリの説明を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Description
            => Normalize(Get<AssemblyDescriptionAttribute>()?.Description);

        /* ----------------------------------------------------------------- */
        ///
        /// Configuration
        ///
        /// <summary>
        /// Configuration 情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Configuration
            => Normalize(Get<AssemblyConfigurationAttribute>()?.Configuration);

        /* ----------------------------------------------------------------- */
        ///
        /// Company
        ///
        /// <summary>
        /// 会社情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Company
            => Normalize(Get<AssemblyCompanyAttribute>()?.Company);

        /* ----------------------------------------------------------------- */
        ///
        /// Product
        ///
        /// <summary>
        /// 製品情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Product
            => Normalize(Get<AssemblyProductAttribute>()?.Product);

        /* ----------------------------------------------------------------- */
        ///
        /// Copyright
        ///
        /// <summary>
        /// 著作権情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Copyright
            => Normalize(Get<AssemblyCopyrightAttribute>()?.Copyright);

        /* ----------------------------------------------------------------- */
        ///
        /// Trademark
        ///
        /// <summary>
        /// 商標情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Trademark
            => Normalize(Get<AssemblyTrademarkAttribute>()?.Trademark);

        /* ----------------------------------------------------------------- */
        ///
        /// Culture
        ///
        /// <summary>
        /// Culture 情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Culture
            => Normalize(Get<AssemblyCultureAttribute>()?.Culture);

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
        private T Get<T>() where T : class
            => Assembly != null ?
               Attribute.GetCustomAttribute(Assembly, typeof(T)) as T :
               default(T);

        /* ----------------------------------------------------------------- */
        ///
        /// Normalize
        ///
        /// <summary>
        /// null を空文字に正規化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private string Normalize(string src)
            => !string.IsNullOrEmpty(src) ? src : string.Empty;

        #endregion
    }
}
