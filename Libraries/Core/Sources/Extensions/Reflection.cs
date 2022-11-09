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
namespace Cube.Reflection.Extensions;

using System;
using System.Reflection;
using Cube.FileSystem;
using Cube.Text.Extensions;

/* ------------------------------------------------------------------------- */
///
/// Methods
///
/// <summary>
/// Provides extended methods of the Assembly class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class Methods
{
    #region Methods

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static Version GetVersion(this Assembly src) => src.GetName().Version;

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static SoftwareVersion GetSoftwareVersion(this Assembly src) => new(src);

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static string GetVersionString(this Assembly src, int digit, bool architecture) =>
        src.GetSoftwareVersion().ToString(digit, architecture);

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static string GetNameString(this Assembly src) => GetOrEmpty(src.GetName().Name);

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static string GetLocation(this Assembly src) => GetOrEmpty(src.Location);

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static string GetFileName(this Assembly src)
    {
        var path = src.GetLocation();
        return path.HasValue() ? Io.GetFileName(path) : string.Empty;
    }

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static string GetDirectoryName(this Assembly src)
    {
        var path = src.GetLocation();
        return path.HasValue() ? Io.GetDirectoryName(path) : string.Empty;
    }

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static string GetTitle(this Assembly src) =>
        GetOrEmpty(src.Get<AssemblyTitleAttribute>()?.Title);

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static string GetDescription(this Assembly src) =>
        GetOrEmpty(src.Get<AssemblyDescriptionAttribute>()?.Description);

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static string GetCompany(this Assembly src) =>
        GetOrEmpty(src.Get<AssemblyCompanyAttribute>()?.Company);

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static string GetProduct(this Assembly src) =>
        GetOrEmpty(src.Get<AssemblyProductAttribute>()?.Product);

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static string GetCopyright(this Assembly src) =>
        GetOrEmpty(src.Get<AssemblyCopyrightAttribute>()?.Copyright);

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static string GetTrademark(this Assembly src) =>
        GetOrEmpty(src.Get<AssemblyTrademarkAttribute>()?.Trademark);

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static string GetConfiguration(this Assembly src) =>
        GetOrEmpty(src.Get<AssemblyConfigurationAttribute>()?.Configuration);

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static string GetCulture(this Assembly src) =>
        GetOrEmpty(src.Get<AssemblyCultureAttribute>()?.Culture);

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static string GetArchitecture(this Assembly src) => src.GetName().ProcessorArchitecture switch
    {
        ProcessorArchitecture.X86   => "x86",
        ProcessorArchitecture.Amd64 => "x64",
        ProcessorArchitecture.IA64  => "IA64",
        ProcessorArchitecture.Arm   => "ARM",
        ProcessorArchitecture.MSIL  => IntPtr.Size == 4 ? "MSIL/32bit" : "MSIL/64bit",
        _ => IntPtr.Size == 4 ? "32bit" : "64bit",
    };

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// Get
    ///
    /// <summary>
    /// Gets the object by using the GetCustomAttribute method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static T Get<T>(this Assembly src) where T : class =>
        src is not null ?
        Attribute.GetCustomAttribute(src, typeof(T)) as T :
        default;

    /* --------------------------------------------------------------------- */
    ///
    /// GetOrEmpty
    ///
    /// <summary>
    /// Returns the specified value itself. If the specified value is null,
    /// the method will return an empty string.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static string GetOrEmpty(string src) => src ?? string.Empty;

    #endregion
}
