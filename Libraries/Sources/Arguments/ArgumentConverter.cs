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
using Cube.Mixin.String;
using System.Collections.Generic;
using System.Linq;

namespace Cube.Collections
{
    #region IArgumentConverter

    /* --------------------------------------------------------------------- */
    ///
    /// IArgumentConverter
    ///
    /// <summary>
    /// Represents the interface to normalize option parameters.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal interface IArgumentConverter
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the normalization.
        /// </summary>
        ///
        /// <param name="src">Source arguments.</param>
        ///
        /// <returns>Normalized arguments.</returns>
        ///
        /* ----------------------------------------------------------------- */
        IEnumerable<string> Invoke(IEnumerable<string> src);
    }

    #endregion

    #region DosArgumentConverter

    /* --------------------------------------------------------------------- */
    ///
    /// DosArgumentConverter
    ///
    /// <summary>
    /// Provides functionality to normalize the DOS based argument options.
    /// </summary>
    ///
    /// <remarks>
    /// '/Foo' と言う形のみをオプションとして許容します。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    internal class DosArgumentConverter : IArgumentConverter
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the normalization.
        /// </summary>
        ///
        /// <param name="src">Source arguments.</param>
        ///
        /// <returns>Normalized arguments.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public IEnumerable<string> Invoke(IEnumerable<string> src) =>
            src.Select(e => e.Unify())
               .Select(e => Convert(e));

        /* ----------------------------------------------------------------- */
        ///
        /// Convert
        ///
        /// <summary>
        /// Converts the specified argument.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private string Convert(string src) =>
            src.StartsWith("/") ? $"{ArgumentFactory.Prefix}{src.Substring(1)}" : src;
    }

    #endregion

    #region WindowsArgumentConverter

    /* --------------------------------------------------------------------- */
    ///
    /// WindowsArgumentConverter
    ///
    /// <summary>
    /// Provides functionality to normalize the Windows based argument
    /// options.
    /// </summary>
    ///
    /// <remarks>
    /// '/Foo', '-Foo', '--Foo' の 3 種類を許容し、全て同じ単一のオプションと
    /// 見なします。すなわち、POSIX のように -a -b -c を -abc を省略する事は
    /// できません。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    internal class WindowsArgumentConverter : IArgumentConverter
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the normalization.
        /// </summary>
        ///
        /// <param name="src">Source arguments.</param>
        ///
        /// <returns>Normalized arguments.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public IEnumerable<string> Invoke(IEnumerable<string> src) =>
            src.Select(e => e.Unify())
               .Select(e => Convert(e));

        /* ----------------------------------------------------------------- */
        ///
        /// Convert
        ///
        /// <summary>
        /// Converts the specified argument.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private string Convert(string src) =>
            src.StartsWith("--") ? $"{ArgumentFactory.Prefix}{src.Substring(2)}" :
            src.StartsWith("-")  ? $"{ArgumentFactory.Prefix}{src.Substring(1)}" :
            src.StartsWith("/")  ? $"{ArgumentFactory.Prefix}{src.Substring(1)}" :
            src;
    }

    #endregion
}
