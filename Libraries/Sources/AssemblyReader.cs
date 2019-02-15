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
    /// Provides functionality to get information of an Assembly object.
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
        /// Initializes a new instance of the AssemblyReader class with
        /// the specified assembly.
        /// </summary>
        ///
        /// <param name="assembly">Assembly object.</param>
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
        /// Gets the value that represents the executing platform.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string Platform { get; } = (IntPtr.Size == 4) ? "x86" : "x64";

        /* ----------------------------------------------------------------- */
        ///
        /// Assembly
        ///
        /// <summary>
        /// Gets the target Assembly object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Assembly Assembly { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Location
        ///
        /// <summary>
        /// Gets the assembly location.
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
        /// Gets the title of the assembly.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Title => Unify(Get<AssemblyTitleAttribute>()?.Title);

        /* ----------------------------------------------------------------- */
        ///
        /// Description
        ///
        /// <summary>
        /// Gets the description of the assembly.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Description => Unify(Get<AssemblyDescriptionAttribute>()?.Description);

        /* ----------------------------------------------------------------- */
        ///
        /// Configuration
        ///
        /// <summary>
        /// Gets the configuration information of the assembly.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Configuration => Unify(Get<AssemblyConfigurationAttribute>()?.Configuration);

        /* ----------------------------------------------------------------- */
        ///
        /// Company
        ///
        /// <summary>
        /// Gets the company name of the assembly.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Company => Unify(Get<AssemblyCompanyAttribute>()?.Company);

        /* ----------------------------------------------------------------- */
        ///
        /// Product
        ///
        /// <summary>
        /// Gets the product name of the assembly.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Product => Unify(Get<AssemblyProductAttribute>()?.Product);

        /* ----------------------------------------------------------------- */
        ///
        /// Copyright
        ///
        /// <summary>
        /// Gets the copyright description of the assembly.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Copyright => Unify(Get<AssemblyCopyrightAttribute>()?.Copyright);

        /* ----------------------------------------------------------------- */
        ///
        /// Trademark
        ///
        /// <summary>
        /// Gets the trademark description of the assembly.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Trademark => Unify(Get<AssemblyTrademarkAttribute>()?.Trademark);

        /* ----------------------------------------------------------------- */
        ///
        /// Culture
        ///
        /// <summary>
        /// Gets the culture information of the assembly.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Culture => Unify(Get<AssemblyCultureAttribute>()?.Culture);

        /* ----------------------------------------------------------------- */
        ///
        /// Version
        ///
        /// <summary>
        /// Gets the version information of the assembly.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Version Version => Assembly?.GetName().Version ?? new Version();

        /* ----------------------------------------------------------------- */
        ///
        /// FileVersion
        ///
        /// <summary>
        /// Gets the file version information of the assembly.
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
        /// Gets the object by using the GetCustomAttribute method.
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
    /// Provides extended methods of the AssemblyReader class.
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
        /// Creates a new instance of the AssemblyReader class with the
        /// specified assembly.
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
