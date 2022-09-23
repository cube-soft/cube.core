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
namespace Cube;

using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Cube.Reflection.Extensions;

/* ------------------------------------------------------------------------- */
///
/// Logger
///
/// <summary>
/// Provides settings and methods for logging.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class Logger
{
    #region Configure

    /* --------------------------------------------------------------------- */
    ///
    /// Configure
    ///
    /// <summary>
    /// Configures with the specified logger source object.
    /// </summary>
    ///
    /// <param name="src">Logger source.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Configure(ILoggerSource src) => Interlocked.Exchange(ref _source, src);

    #endregion

    #region Debug

    /* --------------------------------------------------------------------- */
    ///
    /// Debug
    ///
    /// <summary>
    /// Logs the specified message as Debug level.
    /// </summary>
    ///
    /// <param name="message">Message to be output as log.</param>
    /// <param name="path">Caller file path.</param>
    /// <param name="n">Caller line number.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Debug(string message,
        [CallerFilePath] string path = default, [CallerLineNumber] int n = 0) =>
        _source.Log(path, n, LogLevel.Debug, message);

    #endregion

    #region Info

    /* --------------------------------------------------------------------- */
    ///
    /// Info
    ///
    /// <summary>
    /// Logs the specified message as Information level.
    /// </summary>
    ///
    /// <param name="message">Message to be output as log.</param>
    /// <param name="path">Caller file path.</param>
    /// <param name="n">Caller line number.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Info(string message,
        [CallerFilePath] string path = default, [CallerLineNumber] int n = 0) =>
        _source.Log(path, n, LogLevel.Information, message);

    /* --------------------------------------------------------------------- */
    ///
    /// Info
    ///
    /// <summary>
    /// Logs the system information.
    /// </summary>
    ///
    /// <param name="src">Source assembly</param>
    /// <param name="path">Caller file path.</param>
    /// <param name="n">Caller line number.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Info(Assembly src, [CallerFilePath] string path = default, [CallerLineNumber] int n = 0)
    {
        var fw   = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;
        var os   = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
        var arch = System.Runtime.InteropServices.RuntimeInformation.OSArchitecture;
        Info($"{src.GetProduct()} {src.GetVersionString(4, true)}", path, n);
        Info($"{os} ({arch})", path, n);
        Info($"{fw}", path, n);
        Info($"{Environment.UserName}@{Environment.MachineName} ({CultureInfo.CurrentCulture})", path, n);
    }

    #endregion

    #region Warn

    /* --------------------------------------------------------------------- */
    ///
    /// Warn
    ///
    /// <summary>
    /// Logs the specified message as Warning level.
    /// </summary>
    ///
    /// <param name="message">Message to be output as log.</param>
    /// <param name="path">Caller file path.</param>
    /// <param name="n">Caller line number.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Warn(string message,
        [CallerFilePath] string path = default, [CallerLineNumber] int n = 0) =>
        _source.Log(path, n, LogLevel.Warning, message);

    /* --------------------------------------------------------------------- */
    ///
    /// Warn
    ///
    /// <summary>
    /// Logs the specified exception as Warning level.
    /// </summary>
    ///
    /// <param name="error">Exception object.</param>
    /// <param name="path">Caller file path.</param>
    /// <param name="n">Caller line number.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Warn(Exception error,
        [CallerFilePath] string path = default, [CallerLineNumber] int n = 0) =>
        Warn(GetErrorMessage(error), path, n);

    /* --------------------------------------------------------------------- */
    ///
    /// Warn
    ///
    /// <summary>
    /// Logs as Warning level when the specified action throws an exception.
    /// </summary>
    ///
    /// <param name="action">Action to monitor.</param>
    /// <param name="path">Caller file path.</param>
    /// <param name="n">Caller line number.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Warn(Action action,
        [CallerFilePath] string path = default, [CallerLineNumber] int n = 0) =>
        Invoke(action, e => Warn(e, path, n));

    #endregion

    #region Error

    /* --------------------------------------------------------------------- */
    ///
    /// Error
    ///
    /// <summary>
    /// Logs the specified message as Error level.
    /// </summary>
    ///
    /// <param name="message">Message to be output as log.</param>
    /// <param name="path">Caller file path.</param>
    /// <param name="n">Caller line number.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Error(string message,
        [CallerFilePath] string path = default, [CallerLineNumber] int n = 0) =>
        _source.Log(path, n, LogLevel.Error, message);

    /* --------------------------------------------------------------------- */
    ///
    /// Error
    ///
    /// <summary>
    /// Logs the specified exception as Error level.
    /// </summary>
    ///
    /// <param name="error">Exception object.</param>
    /// <param name="path">Caller file path.</param>
    /// <param name="n">Caller line number.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Error(Exception error,
        [CallerFilePath] string path = default, [CallerLineNumber] int n = 0) =>
        Error(GetErrorMessage(error), path, n);

    /* --------------------------------------------------------------------- */
    ///
    /// Error
    ///
    /// <summary>
    /// Logs as Error level when the specified action throws an exception.
    /// </summary>
    ///
    /// <param name="action">Action to monitor.</param>
    /// <param name="path">Caller file path.</param>
    /// <param name="n">Caller line number.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Error(Action action,
        [CallerFilePath] string path = default, [CallerLineNumber] int n = 0) =>
        Invoke(action, e => Error(e, path, n));

    #endregion

    #region Others

    /* --------------------------------------------------------------------- */
    ///
    /// ObserveTaskException
    ///
    /// <summary>
    /// Observes UnobservedTaskException exceptions and logs them.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static void ObserveTaskException()
    {
        static void f(object s, UnobservedTaskExceptionEventArgs e) => Error(e.Exception);
        TaskScheduler.UnobservedTaskException -= f;
        TaskScheduler.UnobservedTaskException += f;
        _disposable.Add(Disposable.Create(() => TaskScheduler.UnobservedTaskException -= f));
    }

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// GetErrorMessage
    ///
    /// <summary>
    /// Gets the error message from the specified exception.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static string GetErrorMessage(Exception src) =>
        src is Win32Exception we ?
        $"{we.Message} (0x{we.NativeErrorCode:X8}){Environment.NewLine}{we.StackTrace}" :
        src.ToString();

    /* --------------------------------------------------------------------- */
    ///
    /// Invoke
    ///
    /// <summary>
    /// Invokes the specified action.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void Invoke(Action action, Action<Exception> error)
    {
        try { action(); }
        catch (Exception err) { error(err); }
    }

    #endregion

    #region Fields
    private static ILoggerSource _source = new NullLoggerSource();
    private static readonly DisposableContainer _disposable = new();
    #endregion
}
