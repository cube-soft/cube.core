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
namespace Cube.Xui.Tests;

using System;
using Cube.Globalization;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// BindableElementTest
///
/// <summary>
/// Represents tests of the BindableElement(T) class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class BindableElementTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Create
    ///
    /// <summary>
    /// Tests the constructor and confirms values of properties.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("Hello, world!", 10)]
    public void Create(string text, int n)
    {
        using var src = new BindableElement<int>(() => text, () => n, Dispatcher.Vanilla);
        Assert.That(src.Text,    Is.EqualTo(text));
        Assert.That(src.Value,   Is.EqualTo(n));
        Assert.That(src.Command, Is.Null);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Set
    ///
    /// <summary>
    /// Tests the setter method of the Value property.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Set()
    {
        var value = 0;
        var count = 0;
        using (var src = new BindableElement<int>(
            () => "Set",
            () => value,
            e  => value = e,
            Dispatcher.Vanilla
        )) {
            src.PropertyChanged += (s, e) => ++count;
            src.Value = 10;
            src.Value = 10;
            src.Value++;
            src.Value *= 2;
        }

        Assert.That(value, Is.EqualTo(22));
        Assert.That(count, Is.EqualTo(3));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Set_Throws
    ///
    /// <summary>
    /// Confirms the behavior when setting value without any setter
    /// functions.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Set_InvalidOperationException()
    {
        using var src = new BindableElement<string>(() => "Text", () => "Get", Dispatcher.Vanilla);
        Assert.That(src.Text, Is.EqualTo("Text"));
        Assert.That(src.Value, Is.EqualTo("Get"));
        Assert.That(() => src.Value = "Dummy", Throws.TypeOf<InvalidOperationException>());
        Assert.That(src.Command, Is.Null);
        src.Command = new DelegateCommand(() => { });
        Assert.That(src.Command, Is.Not.Null);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// SetLanguage
    ///
    /// <summary>
    /// Executes the test to change the language settings.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void SetLanguage()
    {
        var count = 0;
        using (var src = new BindableElement<int>(() => "Language", () => count, Dispatcher.Vanilla))
        {
            src.PropertyChanged += (s, e) => ++count;
            Locale.Reset(Language.French);
            Locale.Reset(Language.Russian);
            Locale.Reset(Language.Russian);
        }
        Assert.That(count, Is.EqualTo(4));
    }

    #endregion
}
