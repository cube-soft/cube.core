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
namespace Cube.Xui;

using System.Diagnostics;

/* ------------------------------------------------------------------------- */
///
/// BindingLogger
///
/// <summary>
/// Provides functionality to output the binding errors.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public sealed class BindingLogger : TraceListener
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// BindingLogger
    ///
    /// <summary>
    /// Initializes a new instance of the BindingLogger class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private BindingLogger() { }

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Setup
    ///
    /// <summary>
    /// Configures the log settings.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static void Setup() => _once.Invoke(_core);

    /* --------------------------------------------------------------------- */
    ///
    /// WriteLine
    ///
    /// <summary>
    /// Writes a message to the debugging logger.
    /// </summary>
    ///
    /// <param name="message">Message to write.</param>
    ///
    /* --------------------------------------------------------------------- */
    public override void WriteLine(string message) => Logger.Debug(message);

    /* --------------------------------------------------------------------- */
    ///
    /// Write
    ///
    /// <summary>
    /// Writes a message to the debugging logger.
    /// </summary>
    ///
    /// <param name="message">Message to write.</param>
    ///
    /* --------------------------------------------------------------------- */
    public override void Write(string message) { }

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// Register
    ///
    /// <summary>
    /// Registers the binding logger source.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void Register(BindingLogger src) =>
        PresentationTraceSources.DataBindingSource.Listeners.Add(src);

    #endregion

    #region Fields
    private static readonly BindingLogger _core = new();
    private static readonly OnceAction<BindingLogger> _once = new(Register);
    #endregion
}
