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
using System.Collections.Generic;

namespace Cube.Collections
{
    #region Argument

    /* --------------------------------------------------------------------- */
    ///
    /// ArgumentType
    ///
    /// <summary>
    /// Specifies the prefix type of optional parameters.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum ArgumentType
    {
        /// <summary>Allows '/', '-', and '--' prefix.</summary>
        Windows,
        /// <summary>Allows only the '/' prefix.</summary>
        Dos,
        /// <summary>Allows only the '-' prefix, and option names are all one character..</summary>
        Posix,
    }

    #endregion

    #region ArgumentExtension

    /* --------------------------------------------------------------------- */
    ///
    /// ArgumentFactory
    ///
    /// <summary>
    /// Provides extended methods of the ArgumentType enum.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal static class ArgumentFactory
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Prefix
        ///
        /// <summary>
        /// Gets the universal prefix character in the project.
        /// </summary>
        ///
        /// <remarks>
        /// IArgumentConverter による Prefix 正規化に使用します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static char Prefix { get; } = ' ';

        /* ----------------------------------------------------------------- */
        ///
        /// Map
        ///
        /// <summary>
        /// Gets the map of Argument and convert implementation.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static IDictionary<ArgumentType, IArgumentConverter> Map { get; } =
            new Dictionary<ArgumentType, IArgumentConverter>
            {
                { ArgumentType.Windows, new WindowsArgumentConverter() },
                { ArgumentType.Dos,     new DosArgumentConverter()     },
                { ArgumentType.Posix,   new PosixArgumentConverter()   },
            };

        /* ----------------------------------------------------------------- */
        ///
        /// Get
        ///
        /// <summary>
        /// Gets the converter from the specified prefix type.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static IArgumentConverter Get(this ArgumentType src) =>
            Map.TryGetValue(src, out var dest) ? dest : throw new ArgumentException();
    }

    #endregion
}
