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
namespace Cube.Collections;

using System.Collections.Generic;
using System.Linq;

#region IArgumentPreprocessor

/* ------------------------------------------------------------------------- */
///
/// IArgumentPreprocessor
///
/// <summary>
/// Represents interface to process the provided arguments before
/// parsing.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public interface IArgumentPreprocessor
{
    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    IEnumerable<ArgumentToken> Invoke(IEnumerable<string> src);
}

#endregion

#region PosixArgumentPreprocessor

/* ------------------------------------------------------------------------- */
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
/* ------------------------------------------------------------------------- */
public class PosixArgumentPreprocessor : IArgumentPreprocessor
{
    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public IEnumerable<ArgumentToken> Invoke(IEnumerable<string> src) =>
        src.SelectMany(Convert);

    /* --------------------------------------------------------------------- */
    ///
    /// Convert
    ///
    /// <summary>
    /// Converts the specified argument.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected virtual IEnumerable<ArgumentToken> Convert(string src) =>
        src.StartsWith("-") ?
        src.Skip(1).Select(c => new ArgumentToken(c.ToString(), "-")) :
        new[] { new ArgumentToken(src) };
}

#endregion

#region GnuArgumentPreprocessor

/* ------------------------------------------------------------------------- */
///
/// GnuArgumentPreprocessor
///
/// <summary>
/// Provides functionality to process the GNU based arguments.
/// </summary>
///
/// <seealso href="https://www.gnu.org/prep/standards/html_node/Command_002dLine-Interfaces.html" />
///
/* ------------------------------------------------------------------------- */
public class GnuArgumentPreprocessor : PosixArgumentPreprocessor
{
    /* --------------------------------------------------------------------- */
    ///
    /// Convert
    ///
    /// <summary>
    /// Converts the specified argument.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected override IEnumerable<ArgumentToken> Convert(string src) =>
        src.StartsWith("--") ?
        new[] { new ArgumentToken(src.Substring(2), "--") } :
        base.Convert(src);
}

#endregion

#region DosArgumentPreprocessor

/* ------------------------------------------------------------------------- */
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
/* ------------------------------------------------------------------------- */
public class DosArgumentPreprocessor : IArgumentPreprocessor
{
    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public IEnumerable<ArgumentToken> Invoke(IEnumerable<string> src) =>
        src.Select(Convert);

    /* --------------------------------------------------------------------- */
    ///
    /// Convert
    ///
    /// <summary>
    /// Converts the specified argument.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected virtual ArgumentToken Convert(string src) =>
        src.StartsWith("/") ? new(src.Substring(1), "/") : new(src);
}

#endregion

#region WindowsArgumentPreprocessor

/* ------------------------------------------------------------------------- */
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
/* ------------------------------------------------------------------------- */
public class WindowsArgumentPreprocessor : DosArgumentPreprocessor
{
    /* --------------------------------------------------------------------- */
    ///
    /// Convert
    ///
    /// <summary>
    /// Converts the specified argument.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected override ArgumentToken Convert(string src) =>
        src.StartsWith("--") ? new(src.Substring(2), "--") :
        src.StartsWith("-")  ? new(src.Substring(1), "-" ) :
        base.Convert(src);
}

#endregion
