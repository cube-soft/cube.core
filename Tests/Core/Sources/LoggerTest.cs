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
namespace Cube.Tests;

using System;
using System.ComponentModel;
using System.Threading.Tasks;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// LoggerTest
///
/// <summary>
/// Represents tests for the Logger class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class LoggerTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Debug
    ///
    /// <summary>
    /// Executes the test of the LogDebug extended methods.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Debug()
    {
        Logger.Separator = ",";
        var src = GetType();
        src.LogDebug();
        src.LogDebug("Debug");
        src.LogDebug("Debug", "multiple", "messages");
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Info
    ///
    /// <summary>
    /// Executes the test of the LogInfo extended methods.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Info()
    {
        Logger.Separator = "\t";
        var src = GetType();
        src.LogInfo();
        src.LogInfo("Info");
        src.LogInfo("Info", "multiple", "messages");
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Warn
    ///
    /// <summary>
    /// Executes the test of the LogWarn extended methods.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Warn()
    {
        Logger.Separator = ":";
        var src = GetType();
        src.LogWarn();
        src.LogWarn("Warn");
        src.LogWarn("Warn", "multiple", "messages");

        var error = new ArgumentException("Warn (throw)");
        src.LogWarn(() => throw error);

        try { throw error; }
        catch (ArgumentException err) { src.LogWarn(err); }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Error
    ///
    /// <summary>
    /// Executes the test of the LogError extended methods.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Error()
    {
        var src = GetType();
        src.LogError();
        src.LogError("Error");
        src.LogError("Error", "multiple", "messages");

        var error = new Win32Exception(0);
        src.LogError(() => throw error);

        try { throw error; }
        catch (Win32Exception err) { src.LogError(err); }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// ObserveTaskException
    ///
    /// <summary>
    /// Executes the test of the ObserveTaskException method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void ObserveTaskException()
    {
        using (Logger.ObserveTaskException())
        {
            // Assert.DoesNotThrow
            _ = Task.Run(() => throw new ArgumentException("Test for ObserveTaskException"));
            Task.Delay(100).Wait();
        }
    }

    #endregion

    #region Others

    /* --------------------------------------------------------------------- */
    ///
    /// TearDown
    ///
    /// <summary>
    /// Invokes when each test terminates.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TearDown]
    public void TearDown() => Logger.Separator = "\t";

    #endregion
}
