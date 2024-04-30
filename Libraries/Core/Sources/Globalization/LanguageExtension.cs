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

using System;
using System.Globalization;
using System.Linq;

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
    public static Language ToLanguage(this CultureInfo src)
    {
        var dest = Enum.GetValues(typeof(Language))
                       .Cast<Language>()
                       .FirstOrDefault(e => (int)e == src.LCID);
        return dest != Language.Auto ? dest : Language.Unknown;
    }

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
    public static CultureInfo ToCultureInfo(this Language src) => src switch
    {
        Language.Auto    => Locale.GetDefaultCultureInfo(),
        Language.Unknown => default,
        _ => new((int)src)
    };

    #endregion
}
