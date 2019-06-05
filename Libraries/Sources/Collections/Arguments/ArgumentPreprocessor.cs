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
    /// IArgumentPreprocessor
    ///
    /// <summary>
    /// Represents interface to process the provided arguments before
    /// parsing.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal interface IArgumentPreprocessor
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the processing.
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
    /// PosixArgumentPreprocessor
    ///
    /// <summary>
    /// Provides functionality to process the POSIX based arguments.
    /// </summary>
    ///
    /// <remarks>
    /// Treats a '-foption_argument' option as '-f', '-o', '-p', and more.
    /// </remarks>
    ///
    /// <seealso href="http://pubs.opengroup.org/onlinepubs/009696899/basedefs/xbd_chap12.html" />
    ///
    /* --------------------------------------------------------------------- */
    internal class PosixArgumentPreprocessor : IArgumentPreprocessor
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the processing.
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
            AsEnumerable(KeyValuePair.Create(false, src));

        /* ----------------------------------------------------------------- */
        ///
        /// AsEnumerable
        ///
        /// <summary>
        /// Treats the specified value as enumerable.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected IEnumerable<KeyValuePair<bool, string>> AsEnumerable(KeyValuePair<bool, string> src)
        {
            yield return src;
        }
    }

    #endregion

    #region GnuArgumentConverter

    /* --------------------------------------------------------------------- */
    ///
    /// GnuArgumentPreprocessor
    ///
    /// <summary>
    /// Provides functionality to process the GNU based arguments.
    /// </summary>
    ///
    /// <seealso href="https://www.gnu.org/prep/standards/html_node/Command_002dLine-Interfaces.html" />
    ///
    /* --------------------------------------------------------------------- */
    internal class GnuArgumentPreprocessor : PosixArgumentPreprocessor
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
            AsEnumerable(KeyValuePair.Create(true, src.Substring(2))) :
            base.Convert(src);
    }

    #endregion

    #region DosArgumentConverter

    /* --------------------------------------------------------------------- */
    ///
    /// DosArgumentPreprocessor
    ///
    /// <summary>
    /// Provides functionality to process the DOS based arguments.
    /// </summary>
    ///
    /// <remarks>
    /// Allows only '/Foo' format.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    internal class DosArgumentPreprocessor : IArgumentPreprocessor
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
    /// WindowsArgumentPreprocessor
    ///
    /// <summary>
    /// Provides functionality to process the Windows based arguments.
    /// </summary>
    ///
    /// <remarks>
    /// Allows '/Foo', '-Foo', '--Foo' formats and all of them are treated
    /// as 'Foo' option, it means that the class does not allow the short
    /// named options like POSIX.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    internal class WindowsArgumentPreprocessor : DosArgumentPreprocessor
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
