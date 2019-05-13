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
using System.Linq;

namespace Cube.Collections
{
    #region IArgumentConverter

    /* --------------------------------------------------------------------- */
    ///
    /// IArgumentConverter
    ///
    /// <summary>
    /// Represents interface to normalize the provided arguments.
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
        IEnumerable<KeyValuePair<bool, string>> Invoke(IEnumerable<string> src);
    }

    #endregion

    #region PosixArgumentConverter

    /* --------------------------------------------------------------------- */
    ///
    /// PosixArgumentConverter
    ///
    /// <summary>
    /// Provides functionality to normalize the POSIX based arguments.
    /// </summary>
    ///
    /// <remarks>
    /// '-foption_argument' の形式はサポートせず、'-f', '-o', '-p' ... と
    /// 解釈する事とします。
    /// </remarks>
    ///
    /// <seealso href="http://pubs.opengroup.org/onlinepubs/009696899/basedefs/xbd_chap12.html" />
    ///
    /* --------------------------------------------------------------------- */
    internal class PosixArgumentConverter : IArgumentConverter
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
        public IEnumerable<KeyValuePair<bool, string>> Invoke(IEnumerable<string> src) =>
            src.SelectMany(e => Convert(e));

        /* ----------------------------------------------------------------- */
        ///
        /// Convert
        ///
        /// <summary>
        /// Converts the specified argument.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual IEnumerable<KeyValuePair<bool, string>> Convert(string src) =>
            src.StartsWith("-") ?
            src.Skip(1).Select(c => KeyValuePair.Create(true, c.ToString())) :
            new[] { KeyValuePair.Create(false, src) };
    }

    #endregion

    #region GnuArgumentConverter

    /* --------------------------------------------------------------------- */
    ///
    /// GnuArgumentConverter
    ///
    /// <summary>
    /// Provides functionality to normalize the GNU based arguments.
    /// </summary>
    ///
    /// <seealso href="https://www.gnu.org/prep/standards/html_node/Command_002dLine-Interfaces.html" />
    ///
    /* --------------------------------------------------------------------- */
    internal class GnuArgumentConverter : PosixArgumentConverter
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Convert
        ///
        /// <summary>
        /// Converts the specified argument.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override IEnumerable<KeyValuePair<bool, string>> Convert(string src) =>
            src.StartsWith("--") ?
            new[] { KeyValuePair.Create(true, src.Substring(2)) } :
            base.Convert(src);
    }

    #endregion

    #region DosArgumentConverter

    /* --------------------------------------------------------------------- */
    ///
    /// DosArgumentConverter
    ///
    /// <summary>
    /// Provides functionality to normalize the DOS based arguments.
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
        public IEnumerable<KeyValuePair<bool, string>> Invoke(IEnumerable<string> src) =>
            src.Select(e => Convert(e));

        /* ----------------------------------------------------------------- */
        ///
        /// Convert
        ///
        /// <summary>
        /// Converts the specified argument.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual KeyValuePair<bool, string> Convert(string src) =>
            src.StartsWith("/") ?
            KeyValuePair.Create(true, src.Substring(1)) :
            KeyValuePair.Create(false, src);
    }

    #endregion

    #region WindowsArgumentConverter

    /* --------------------------------------------------------------------- */
    ///
    /// WindowsArgumentConverter
    ///
    /// <summary>
    /// Provides functionality to normalize the Windows based arguments.
    /// </summary>
    ///
    /// <remarks>
    /// '/Foo', '-Foo', '--Foo' の 3 種類を許容し、全て同じ単一のオプションと
    /// 見なします。すなわち、POSIX のように -a -b -c を -abc と省略する事は
    /// できません。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    internal class WindowsArgumentConverter : DosArgumentConverter
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Convert
        ///
        /// <summary>
        /// Converts the specified argument.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override KeyValuePair<bool, string> Convert(string src) =>
            src.StartsWith("--") ? KeyValuePair.Create(true, src.Substring(2)) :
            src.StartsWith("-")  ? KeyValuePair.Create(true, src.Substring(1)) :
            base.Convert(src);
    }

    #endregion
}
