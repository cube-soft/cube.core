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
namespace Cube.Tests.Messages;

using System.Collections.Generic;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// MessageTest
///
/// <summary>
/// Tests the message classes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class MessageTest
{
    /* --------------------------------------------------------------------- */
    ///
    /// Test
    ///
    /// <summary>
    /// Confirms the message text with the ToString method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCaseSource(nameof(TestCases))]
    public void Test(MessageBase src, string expected) =>
        Assert.That(src.ToString(), Is.EqualTo(expected));

    /* --------------------------------------------------------------------- */
    ///
    /// TestCases
    ///
    /// <summary>
    /// Gets test cases of the Test method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    static IEnumerable<TestCaseData> TestCases()
    {
        yield return new(new Message<object>(), "Object Message");
        yield return new(new CancelMessage<object>(), "Object CancelMessage");
        yield return new(new QueryMessage<string, object>("Source"), "String and Object QueryMessage");
        yield return new(new DialogMessage("Dialog message sample"), "Dialog message sample");
        yield return new(new OpenDirectoryMessage(), nameof(OpenDirectoryMessage));
        yield return new(new OpenFileMessage(), nameof(OpenFileMessage));
        yield return new(new SaveFileMessage(), nameof(SaveFileMessage));
        yield return new(new CloseMessage(), nameof(CloseMessage));
        yield return new(new ActivateMessage(), nameof(ActivateMessage));
        yield return new(new ApplyMessage(), nameof(ApplyMessage));
        yield return new(new ProcessMessage("https://example.com/"), nameof(ProcessMessage));
    }
}
