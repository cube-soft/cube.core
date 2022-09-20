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

using System;
using System.Linq;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// DialogMessageTest
///
/// <summary>
/// Tests the DialogMessage class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class DialogMessageTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Init
    ///
    /// <summary>
    /// Tests the constructor and confirms values of some properties.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Init()
    {
        var dest = new DialogMessage("");
        Assert.That(dest.Text,    Is.Empty);
        Assert.That(dest.Title,   Is.Not.Null.And.Not.Empty);
        Assert.That(dest.Icon,    Is.EqualTo(DialogIcon.Error));
        Assert.That(dest.Buttons, Is.EqualTo(DialogButtons.Ok));
        Assert.That(dest.Value,   Is.EqualTo(DialogStatus.Ok));
        Assert.That(dest.Cancel,  Is.False);

        var cc = dest.CancelCandidates;
        Assert.That(cc.Count(),   Is.EqualTo(1));
        Assert.That(cc.First(),   Is.EqualTo(DialogStatus.Cancel));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Init_WithException
    ///
    /// <summary>
    /// Tests the From method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Init_WithException()
    {
        var dest = DialogMessage.From(new ArgumentException("TEST"));
        Assert.That(dest.Text,    Is.EqualTo("TEST (ArgumentException)"));
        Assert.That(dest.Title,   Is.Not.Null.And.Not.Empty);
        Assert.That(dest.Icon,    Is.EqualTo(DialogIcon.Error));
        Assert.That(dest.Buttons, Is.EqualTo(DialogButtons.Ok));
        Assert.That(dest.Value,   Is.EqualTo(DialogStatus.Ok));
        Assert.That(dest.Cancel,  Is.False);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Any
    ///
    /// <summary>
    /// Tests the Any extended method of the DialogStatus class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Any()
    {
        Assert.That(DialogStatus.Ok.Any(DialogStatus.Ok, DialogStatus.Cancel), Is.True);
        Assert.That(DialogStatus.Ok.Any(DialogStatus.No, DialogStatus.Cancel), Is.False);
        Assert.That(DialogStatus.Empty.Any(DialogStatus.Empty), Is.True);
    }

    #endregion
}
