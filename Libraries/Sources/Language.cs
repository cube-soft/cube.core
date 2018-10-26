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
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// Language
    ///
    /// <summary>
    /// Specifies kinds of language.
    /// </summary>
    ///
    /// <seealso href="https://msdn.microsoft.com/ja-jp/library/cc392381.aspx" />
    ///
    /* --------------------------------------------------------------------- */
    public enum Language
    {
        /// <summary>Same as the system locale</summary>
        Auto = 0x0000,
        /// <summary>English</summary>
        English = 0x0409,
        /// <summary>Japanese</summary>
        Japanese = 0x0411,
        /// <summary>German</summary>
        German = 0x0407,
        /// <summary>Spanish</summary>
        Spanish = 0x040A,
        /// <summary>French</summary>
        French = 0x040C,
        /// <summary>Russian</summary>
        Russian = 0x0419,
        /// <summary>Portuguese</summary>
        Portuguese = 0x0816,
    }

    /* --------------------------------------------------------------------- */
    ///
    /// LanguageExtension
    ///
    /// <summary>
    /// Provides extended methods for the Language enum.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class LanguageExtension
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// LanguageExtension
        ///
        /// <summary>
        /// Initializes static fields.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        static LanguageExtension()
        {
            _auto = Thread.CurrentThread.CurrentUICulture.Name;
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// ToCode
        ///
        /// <summary>
        /// Gets the language code from the specified value.
        /// </summary>
        ///
        /// <param name="src">Language value.</param>
        ///
        /// <returns>Language code.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string ToCode(this Language src)
        {
            Debug.Assert(src == Language.Auto || _codes.ContainsKey(src));
            return (src == Language.Auto) ? _auto : _codes[src];
        }

        #endregion

        #region Fields
        private static readonly string _auto;
        private static readonly IDictionary<Language, string> _codes = new Dictionary<Language, string>
        {
            { Language.English,    "en" },
            { Language.Japanese,   "ja" },
            { Language.German,     "de" },
            { Language.Spanish,    "es" },
            { Language.French,     "fr" },
            { Language.Russian,    "ru" },
            { Language.Portuguese, "pt" },
        };
        #endregion
    }
}
