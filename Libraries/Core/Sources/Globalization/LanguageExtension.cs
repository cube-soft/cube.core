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
namespace Cube.Globalization;

using System.Globalization;

/* ------------------------------------------------------------------------- */
///
/// LanguageExtension
///
/// <summary>
/// Provides extended methods related to the Language class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class LanguageExtension
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// ToLanguage
    ///
    /// <summary>
    /// Gets the Language value from the specified object.
    /// </summary>
    ///
    /// <param name="src">CultureInfo object.</param>
    ///
    /// <returns>Language value.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static Language ToLanguage(this CultureInfo src) => src.LCID switch
    {
        0x0407 => Language.German,
        0x0409 => Language.English,
        0x040A => Language.Spanish,
        0x040C => Language.French,
        0x0411 => Language.Japanese,
        0x0419 => Language.Russian,
        0x0816 => Language.Portuguese,
        _ => Language.Unknown,
    };

    /* --------------------------------------------------------------------- */
    ///
    /// ToCultureInfo
    ///
    /// <summary>
    /// Gets the CultureInfo object from the specified value.
    /// </summary>
    ///
    /// <param name="src">Language value.</param>
    ///
    /// <returns>CultureInfo object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static CultureInfo ToCultureInfo(this Language src) =>
        src == Language.Unknown ? default :
        src == Language.Auto    ? Locale.GetDefaultCultureInfo() : new((int)src);

    #endregion
}
