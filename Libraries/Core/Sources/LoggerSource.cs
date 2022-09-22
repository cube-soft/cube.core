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

#region ILoggerSource

/* ------------------------------------------------------------------------- */
///
/// ILoggerSource
///
/// <summary>
/// Represents a type used to perform logging.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public interface ILoggerSource
{
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
    void Log(string path, int number, LogLevel level, string message);
}

#endregion

#region NullLoggerSource

/* ------------------------------------------------------------------------- */
///
/// NullLoggerSource
///
/// <summary>
/// Minimalistic ILoggerSource implementation that does nothing.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public sealed class NullLoggerSource : ILoggerSource
{
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
    public void Log(string path, int number, LogLevel level, string message) { }
}

#endregion