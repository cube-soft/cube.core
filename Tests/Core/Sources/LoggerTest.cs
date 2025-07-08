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
    /// Test
    ///
    /// <summary>
    /// Tests the Logger basic methods.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Test()
    {
        Assert.DoesNotThrow(() => Logger.Trace("Message"));
        Assert.DoesNotThrow(() => Logger.Debug("Message"));
        Assert.DoesNotThrow(() => Logger.Info("Message"));
        Assert.DoesNotThrow(() => Logger.Warn("Message"));
        Assert.DoesNotThrow(() => Logger.Error("Message"));

        Assert.DoesNotThrow(() => Logger.Info(GetType().Assembly));
        Assert.That(Logger.Try(() => throw new Exception("Test"), 2), Is.False);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// TestTask
    ///
    /// <summary>
    /// Tests for logging UnobservedTaskException exceptions.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void TestTask()
    {
        _ = Task.Run(() => throw new Exception("Test for ObserveTaskException"));
        Task.Delay(100).Wait();
    }

    /* --------------------------------------------------------------------- */
    ///
    /// TestName
    ///
    /// <summary>
    /// Tests the Debug method with various paths as arguments.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("C:\\Win\\Path\\To\\Filename1.cs")]
    [TestCase("Win\\Path\\To\\Filename2.cs")]
    [TestCase("Win\\Path\\To\\.cs")]
    [TestCase("Win\\Dir\\Only\\")]
    [TestCase("/Unix/Path/To/Filename3.cs")]
    [TestCase("Unix/Path/To/Filename4.cs")]
    [TestCase("Unix/Path/To/.cs")]
    [TestCase("Unix/Dir/Only/")]
    [TestCase("FilenameOnly.cs")]
    [TestCase("BasenameOnly")]
    [TestCase(".cs")]
    [TestCase("")]
    [TestCase(null)]
    public void TestName(string name) =>  Assert.DoesNotThrow(() => Logger.Debug("Message", name));

    #endregion
}
