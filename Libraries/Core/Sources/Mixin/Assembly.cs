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
using System.IO;
using System.Reflection;
using Cube.Mixin.String;
using Source = System.Reflection.Assembly;

namespace Cube.Mixin.Assembly
{
    /* --------------------------------------------------------------------- */
    ///
    /// Extension
    ///
    /// <summary>
    /// Provides extended methods of the Assembly class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Extension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// GetVersion
        ///
        /// <summary>
        /// Gets the version information of the specified assembly.
        /// </summary>
        ///
        /// <param name="src">Assembly object.</param>
        ///
        /// <returns>Version object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static Version GetVersion(this Source src) => src.GetName().Version;

        /* ----------------------------------------------------------------- */
        ///
        /// GetSoftwareVersion
        ///
        /// <summary>
        /// Gets the version information of the specified assembly.
        /// </summary>
        ///
        /// <param name="src">Assembly object.</param>
        ///
        /// <returns>SoftwareVersion object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static SoftwareVersion GetSoftwareVersion(this Source src) =>
            new(src.GetVersion());

        /* ----------------------------------------------------------------- */
        ///
        /// GetVersionString
        ///
        /// <summary>
        /// Gets the version string of the specified assembly.
        /// </summary>
        ///
        /// <param name="src">Assembly object.</param>
        /// <param name="digit">Number of display digits</param>
        /// <param name="architecture">
        /// Indicates whether the architecture identification is displayed.
        /// </param>
        ///
        /// <returns>Version string.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetVersionString(this Source src, int digit, bool architecture) =>
            src.GetSoftwareVersion().ToString(digit, architecture);

        /* ----------------------------------------------------------------- */
        ///
        /// GetNameString
        ///
        /// <summary>
        /// Gets the name of the specified assembly.
        /// </summary>
        ///
        /// <param name="src">Assembly object.</param>
        ///
        /// <returns>String value of the assembly name.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetNameString(this Source src) => Unify(src.GetName().Name);

        /* ----------------------------------------------------------------- */
        ///
        /// GetLocation
        ///
        /// <summary>
        /// Gets the path that is the specified assembly is located.
        /// </summary>
        ///
        /// <param name="src">Assembly object.</param>
        ///
        /// <returns>Path of the location.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetLocation(this Source src) => Unify(src.Location);

        /* ----------------------------------------------------------------- */
        ///
        /// GetDirectoryName
        ///
        /// <summary>
        /// Gets the filename of the specified assembly.
        /// </summary>
        ///
        /// <param name="src">Assembly object.</param>
        ///
        /// <returns>Filename value.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetFileName(this Source src) => Path.GetFileName(src.GetLocation());

        /* ----------------------------------------------------------------- */
        ///
        /// GetDirectoryName
        ///
        /// <summary>
        /// Gets the directory path that the specified assembly is located.
        /// </summary>
        ///
        /// <param name="src">Assembly object.</param>
        ///
        /// <returns>Path of the located directory.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetDirectoryName(this Source src) => Path.GetDirectoryName(src.GetLocation());

        /* ----------------------------------------------------------------- */
        ///
        /// GetTitle
        ///
        /// <summary>
        /// Gets the title of the specified assembly.
        /// </summary>
        ///
        /// <param name="src">Assembly object.</param>
        ///
        /// <returns>Title value.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetTitle(this Source src) =>
            Unify(src.Get<AssemblyTitleAttribute>()?.Title);

        /* ----------------------------------------------------------------- */
        ///
        /// GetDescription
        ///
        /// <summary>
        /// Gets the description of the specified assembly.
        /// </summary>
        ///
        /// <param name="src">Assembly object.</param>
        ///
        /// <returns>Description value.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetDescription(this Source src) =>
            Unify(src.Get<AssemblyDescriptionAttribute>()?.Description);

        /* ----------------------------------------------------------------- */
        ///
        /// GetCompany
        ///
        /// <summary>
        /// Gets the company name of the specified assembly.
        /// </summary>
        ///
        /// <param name="src">Assembly object.</param>
        ///
        /// <returns>Company name.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetCompany(this Source src) =>
            Unify(src.Get<AssemblyCompanyAttribute>()?.Company);

        /* ----------------------------------------------------------------- */
        ///
        /// GetProduct
        ///
        /// <summary>
        /// Gets the product name of the specified assembly.
        /// </summary>
        ///
        /// <param name="src">Assembly object.</param>
        ///
        /// <returns>Product name.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetProduct(this Source src) =>
            Unify(src.Get<AssemblyProductAttribute>()?.Product);

        /* ----------------------------------------------------------------- */
        ///
        /// GetCopyright
        ///
        /// <summary>
        /// Gets the copyright description of the specified assembly.
        /// </summary>
        ///
        /// <param name="src">Assembly object.</param>
        ///
        /// <returns>Copyright description.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetCopyright(this Source src) =>
            Unify(src.Get<AssemblyCopyrightAttribute>()?.Copyright);

        /* ----------------------------------------------------------------- */
        ///
        /// GetTrademark
        ///
        /// <summary>
        /// Gets the trademark description of the specified assembly.
        /// </summary>
        ///
        /// <param name="src">Assembly object.</param>
        ///
        /// <returns>Trademark description.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetTrademark(this Source src) =>
            Unify(src.Get<AssemblyTrademarkAttribute>()?.Trademark);

        /* ----------------------------------------------------------------- */
        ///
        /// GetConfiguration
        ///
        /// <summary>
        /// Gets the configuration value of the specified assembly.
        /// </summary>
        ///
        /// <param name="src">Assembly object.</param>
        ///
        /// <returns>Configuration value.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetConfiguration(this Source src) =>
            Unify(src.Get<AssemblyConfigurationAttribute>()?.Configuration);

        /* ----------------------------------------------------------------- */
        ///
        /// GetCulture
        ///
        /// <summary>
        /// Gets the culture value of the specified assembly.
        /// </summary>
        ///
        /// <param name="src">Assembly object.</param>
        ///
        /// <returns>Culture value.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetCulture(this Source src) =>
            Unify(src.Get<AssemblyCultureAttribute>()?.Culture);

        /* ----------------------------------------------------------------- */
        ///
        /// GetArchitecture
        ///
        /// <summary>
        /// Gets the architecture value of the specified assembly.
        /// </summary>
        ///
        /// <param name="src">Assembly object.</param>
        ///
        /// <returns>32bit or 64bit</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetArchitecture(this Source src)
        {
            var ac = src.GetName().ProcessorArchitecture;
            return ac == ProcessorArchitecture.X86      ? "32bit" :
                   ac == ProcessorArchitecture.Amd64 ||
                   ac == ProcessorArchitecture.IA64     ? "64bit" :
                   IntPtr.Size == 4                     ? "32bit" : "64bit";
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Get
        ///
        /// <summary>
        /// Gets the object by using the GetCustomAttribute method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static T Get<T>(this Source src) where T : class =>
            src != null ?
            Attribute.GetCustomAttribute(src, typeof(T)) as T :
            default;

        /* ----------------------------------------------------------------- */
        ///
        /// Unify
        ///
        /// <summary>
        /// Converts a null or empty string to the empty one.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static string Unify(string src) => src.Unify();

        #endregion
    }
}
