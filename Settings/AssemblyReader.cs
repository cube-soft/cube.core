/* ------------------------------------------------------------------------- */
///
/// AssemblyReader.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///  http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
///
/* ------------------------------------------------------------------------- */
using System;
using System.Diagnostics;
using System.Drawing;
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
        /* ----------------------------------------------------------------- */
        public AssemblyReader(Assembly assembly)
        {
            Assembly = assembly;
        }

        #endregion

        #region Properties

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
        /// Title
        ///
        /// <summary>
        /// タイトル情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Title
        {
            get
            {
                if (Assembly == null) return string.Empty;
                var obj = Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyTitleAttribute)) as AssemblyTitleAttribute;
                return (obj != null) ? obj.Title : string.Empty;
            }
        }

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
        {
            get
            {
                if (Assembly == null) return string.Empty;
                var obj = Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyDescriptionAttribute)) as AssemblyDescriptionAttribute;
                return (obj != null) ? obj.Description : string.Empty;
            }
        }

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
        {
            get
            {
                if (Assembly == null) return string.Empty;
                var obj = Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyConfigurationAttribute)) as AssemblyConfigurationAttribute;
                return (obj != null) ? obj.Configuration : string.Empty;
            }
        }

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
        {
            get
            {
                if (Assembly == null) return string.Empty;
                var obj = Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyCompanyAttribute)) as AssemblyCompanyAttribute;
                return (obj != null) ? obj.Company : string.Empty;
            }
        }

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
        {
            get
            {
                if (Assembly == null) return string.Empty;
                var obj = Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyProductAttribute)) as AssemblyProductAttribute;
                return (obj != null) ? obj.Product : string.Empty;
            }
        }

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
        {
            get
            {
                if (Assembly == null) return string.Empty;
                var obj = Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyCopyrightAttribute)) as AssemblyCopyrightAttribute;
                return (obj != null) ? obj.Copyright : string.Empty;
            }
        }

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
        {
            get
            {
                if (Assembly == null) return string.Empty;
                var obj = Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyTrademarkAttribute)) as AssemblyTrademarkAttribute;
                return (obj != null) ? obj.Trademark : string.Empty;
            }
        }

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
        {
            get
            {
                if (Assembly == null) return string.Empty;
                var obj = Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyCultureAttribute)) as AssemblyCultureAttribute;
                return (obj != null) ? obj.Culture : string.Empty;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Version
        ///
        /// <summary>
        /// バージョン情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Version Version
        {
            get { return (Assembly != null) ? Assembly.GetName().Version : new Version(); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// FileVersion
        ///
        /// <summary>
        /// ファイルバージョン情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Version FileVersion
        {
            get
            {
                return Assembly != null ?
                       new Version(FileVersionInfo.GetVersionInfo(Assembly.Location).FileVersion) :
                       new Version();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Icon
        ///
        /// <summary>
        /// アイコンを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Icon Icon
        {
            get { return (Assembly != null) ? Icon.ExtractAssociatedIcon(Assembly.Location) : null; }
        }

        #endregion
    }
}
