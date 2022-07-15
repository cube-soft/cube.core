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
using System.Threading;
using System.Threading.Tasks;
using Cube.Mixin.Assembly;
using Cube.Mixin.Collections;

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
    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Separator
    ///
    /// <summary>
    /// Gets or sets values to separate words.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static string Separator { get; set; } = "\t";

    #endregion

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
    /// LogDebug
    ///
    /// <summary>
    /// Outputs log as DEBUG level.
    /// </summary>
    ///
    /// <param name="type">Target type information.</param>
    /// <param name="values">User messages.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void LogDebug(this Type type, params string[] values) =>
        _source.Log(LogLevel.Debug, type, GetMessage(values));

    #endregion

    #region Info

    /* --------------------------------------------------------------------- */
    ///
    /// LogInfo
    ///
    /// <summary>
    /// Outputs log as INFO level.
    /// </summary>
    ///
    /// <param name="type">Target type information.</param>
    /// <param name="values">User messages.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void LogInfo(this Type type, params string[] values) =>
        _source.Log(LogLevel.Information, type, GetMessage(values));

    /* --------------------------------------------------------------------- */
    ///
    /// LogInfo
    ///
    /// <summary>
    /// Outputs system information as INFO level.
    /// </summary>
    ///
    /// <param name="type">Target type information.</param>
    /// <param name="assembly">Assembly object.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void LogInfo(this Type type, System.Reflection.Assembly assembly)
    {
        LogInfo(type, $"{assembly.GetProduct()} {assembly.GetVersionString(4, true)}");
        LogInfo(type, $"CLR {Environment.Version} ({Environment.OSVersion})");
        LogInfo(type, $"{Environment.UserName}@{Environment.MachineName} ({CultureInfo.CurrentCulture})");
    }

    #endregion

    #region Warn

    /* --------------------------------------------------------------------- */
    ///
    /// LogWarn
    ///
    /// <summary>
    /// Outputs log as WARN level.
    /// </summary>
    ///
    /// <param name="type">Target type information.</param>
    /// <param name="values">User messages.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void LogWarn(this Type type, params string[] values) =>
        _source.Log(LogLevel.Warning, type, GetMessage(values));

    /* --------------------------------------------------------------------- */
    ///
    /// LogWarn
    ///
    /// <summary>
    /// Outputs log as WARN level.
    /// </summary>
    ///
    /// <param name="type">Target type information.</param>
    /// <param name="error">Exception object.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void LogWarn(this Type type, Exception error) =>
        _source.Log(LogLevel.Warning, type, GetErrorMessage(error));

    /* --------------------------------------------------------------------- */
    ///
    /// LogWarn
    ///
    /// <summary>
    /// Outputs log as WARN level when an exception occurs.
    /// </summary>
    ///
    /// <param name="type">Target type information.</param>
    /// <param name="action">Function to monitor.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void LogWarn(this Type type, Action action) =>
        Invoke(action, e => LogWarn(type, e));

    #endregion

    #region Error

    /* --------------------------------------------------------------------- */
    ///
    /// LogError
    ///
    /// <summary>
    /// Outputs log as ERROR level.
    /// </summary>
    ///
    /// <param name="type">Target type information.</param>
    /// <param name="values">User messages.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void LogError(this Type type, params string[] values) =>
        _source.Log(LogLevel.Error, type, GetMessage(values));

    /* --------------------------------------------------------------------- */
    ///
    /// LogError
    ///
    /// <summary>
    /// Outputs log as ERROR level.
    /// </summary>
    ///
    /// <param name="type">Target type information.</param>
    /// <param name="error">Exception object.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void LogError(this Type type, Exception error) =>
        _source.Log(LogLevel.Error, type, GetErrorMessage(error));

    /* --------------------------------------------------------------------- */
    ///
    /// LogError
    ///
    /// <summary>
    /// Outputs log as ERROR level when an exception occurs.
    /// </summary>
    ///
    /// <param name="type">Target type information.</param>
    /// <param name="action">Function to monitor.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void LogError(this Type type, Action action) =>
        Invoke(action, e => LogError(type, e));

    #endregion

    #region Others

    /* --------------------------------------------------------------------- */
    ///
    /// ObserveTaskException
    ///
    /// <summary>
    /// Observes UnobservedTaskException exceptions and outputs to the
    /// log file.
    /// </summary>
    ///
    /// <returns>Disposable object to stop to monitor.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static IDisposable ObserveTaskException()
    {
        TaskScheduler.UnobservedTaskException -= WhenTaskError;
        TaskScheduler.UnobservedTaskException += WhenTaskError;
        return Disposable.Create(() => TaskScheduler.UnobservedTaskException -= WhenTaskError);
    }

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// GetMessage
    ///
    /// <summary>
    /// Gets the message from the specified arguments.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static string GetMessage(string[] values) =>
        values.Length == 1 ? values[0] :
        values.Length  > 1 ? values.Join(Separator) : string.Empty;

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

    /* --------------------------------------------------------------------- */
    ///
    /// WhenTaskError
    ///
    /// <summary>
    /// Occurs when the UnobservedTaskException is raised.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void WhenTaskError(object s, UnobservedTaskExceptionEventArgs e) =>
        typeof(TaskScheduler).LogError(e.Exception);

    #endregion

    #region Fields
    private static ILoggerSource _source = new NullLoggerSource();
    #endregion
}
