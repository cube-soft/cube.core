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
    /// Argument
    ///
    /// <summary>
    /// Specifies prefix kinds of optional parameters.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum Argument
    {
        /// <summary>Allows only the '-' prefix, and option names are all one character.</summary>
        Posix,
        /// <summary>Allows '-' and '--' prefix, the latter is known as long-named options.</summary>
        Gnu,
        /// <summary>Allows only the '/' prefix, and treated as long-named options.</summary>
        Dos,
        /// <summary>Allows '/', '-', and '--' prefix, and all of them are treated as long-named options.</summary>
        Windows,
    }

    #endregion

    #region ArgumentExtension

    /* --------------------------------------------------------------------- */
    ///
    /// ArgumentExtension
    ///
    /// <summary>
    /// Provides extended methods of the Argument enumeration.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal static class ArgumentExtension
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Map
        ///
        /// <summary>
        /// Gets the map of Argument values and preprocessors.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static Dictionary<Argument, IArgumentPreprocessor> Map { get; } = new()
        {
            { Argument.Posix,   new PosixArgumentPreprocessor()   },
            { Argument.Gnu,     new GnuArgumentPreprocessor()     },
            { Argument.Dos,     new DosArgumentPreprocessor()     },
            { Argument.Windows, new WindowsArgumentPreprocessor() },
        };

        /* ----------------------------------------------------------------- */
        ///
        /// Get
        ///
        /// <summary>
        /// Gets the preprocessor from the specified kind.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static IArgumentPreprocessor Get(this Argument src) =>
            Map.TryGetValue(src, out var dest) ? dest : throw new ArgumentException();
    }

    #endregion
}
