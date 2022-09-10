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
namespace Cube.FileSystem.Tests;

using Cube.Tests;
using Microsoft.Win32;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// StartupTest
///
/// <summary>
/// Tests the Startup class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class StartupTest : FileFixture
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Create
    ///
    /// <summary>
    /// Tests the constructor and properties.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Create()
    {
        var src = new Startup(nameof(StartupTest));
        Assert.That(src.Name,      Is.EqualTo(nameof(StartupTest)));
        Assert.That(src.Enabled,   Is.False);
        Assert.That(src.Source,    Is.EqualTo(string.Empty));
        Assert.That(src.Arguments, Is.Not.Null);
        Assert.That(src.Command,   Is.Empty);

        src.Source = "path";
        Assert.That(src.Command, Is.EqualTo("\"path\""));
        src.Arguments.Add("arg1");
        Assert.That(src.Command, Is.EqualTo("\"path\" \"arg1\""));
        src.Arguments.Add("arg2");
        Assert.That(src.Command, Is.EqualTo("\"path\" \"arg1\" \"arg2\""));
        src.Source = string.Empty;
        Assert.That(src.Command, Is.Empty);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Save
    ///
    /// <summary>
    /// Tests the Save method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Save()
    {
        var src = new Startup(nameof(StartupTest));
        Assert.That(src.Enabled,      Is.False);
        Assert.That(Exists(src.Name), Is.False, "Step1");

        src.Enabled = true;
        src.Save();
        Assert.That(Exists(src.Name), Is.True,  "Step2");

        src.Save(true);
        Assert.That(Exists(src.Name), Is.False, "Step3");

        src.Source = @"path\to\notfound.exe";
        src.Save(false);
        Assert.That(Exists(src.Name), Is.True,  "Step4");
        src.Save(true);
        Assert.That(Exists(src.Name), Is.False, "Step5");

        src.Save();
        Assert.That(Exists(src.Name), Is.True,  "Step6");
        src.Enabled = false;
        src.Save();
        Assert.That(Exists(src.Name), Is.False, "Step7");
    }

    #endregion

    #region Others

    /* --------------------------------------------------------------------- */
    ///
    /// Exists
    ///
    /// <summary>
    /// Determines whether the specified value name exists in the
    /// registry.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private bool Exists(string name) => Registry.GetValue(
        @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", name, null
    ) is not null;

    #endregion
}
