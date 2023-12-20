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
namespace Cube.Logging.NLog;

using Cube.Text.Extensions;
using System;

/* ------------------------------------------------------------------------- */
///
/// LoggerSource
///
/// <summary>
/// Provides the ILoggerSource implementation by using the NLog package.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public sealed class LoggerSource : ILoggerSource
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Log
    ///
    /// <summary>
    /// Writes a log entry.
    /// </summary>
    ///
    /// <param name="path">Source file path.</param>
    /// <param name="number">Source line number.</param>
    /// <param name="level">Log level.</param>
    /// <param name="message">Logging message.</param>
    ///
    /* --------------------------------------------------------------------- */
    public void Log(string path, int number, LogLevel level, string message)
    {
        var e = global::NLog.LogManager.GetLogger(GetLoggerName(path));
        var m = $"({number}) {message}";

        switch (level)
        {
            case LogLevel.Trace:       e.Trace(m); break;
            case LogLevel.Debug:       e.Debug(m); break;
            case LogLevel.Information: e.Info(m);  break;
            case LogLevel.Warning:     e.Warn(m);  break;
            case LogLevel.Error:       e.Error(m); break;
        }
    }

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// GetLoggerName
    ///
    /// <summary>
    /// Gets the logger name with the specified path.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static string GetLoggerName(string path)
    {
        if (!path.HasValue()) return "None";

        var p0 = Math.Min(path.LastIndexOfAny(new[] { '/', '\\' }) + 1, path.Length - 1);
        var p1 = path.LastIndexOf('.');
        return p1 > p0 ? path.Substring(p0, p1 - p0) : path.Substring(p0);
    }

    #endregion
}
