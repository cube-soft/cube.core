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

using System;
using System.Threading.Tasks;
using Cube.DataContract;
using Cube.Tests;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// SettingFolderTest
///
/// <summary>
/// Tests the SettingFolder class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
sealed class SettingFolderTest : RegistryFixture
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Load
    ///
    /// <summary>
    /// Executes the test for loading from registry.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Load()
    {
        var fmt  = Format.Registry;
        var name = GetKeyName();

        using var src = new SettingFolder<Dummy>(fmt, name, new()) { AutoSave = false };
        src.Load();
        Assert.That(src.Format,   Is.EqualTo(fmt));
        Assert.That(src.Location, Is.EqualTo(name));
        Assert.That(src.Value,    Is.Not.Null);
        Assert.That(src.Version.ToString(), Is.EqualTo("1.0.0"));

        var dest = src.Value;
        Assert.That(dest.Name,            Is.EqualTo("山田太郎"));
        Assert.That(dest.Age,             Is.EqualTo(15));
        Assert.That(dest.Sex,             Is.EqualTo(Sex.Male));
        Assert.That(dest.Reserved,        Is.EqualTo(true));
        Assert.That(dest.Creation,        Is.EqualTo(new DateTime(2014, 12, 31, 23, 25, 30, DateTimeKind.Utc).ToLocalTime()));
        Assert.That(dest.Number,          Is.EqualTo(123));
        Assert.That(dest.Secret,          Is.EqualTo("secret message"));
        Assert.That(dest.Contact.Type,    Is.EqualTo("Phone"));
        Assert.That(dest.Contact.Value,   Is.EqualTo("080-9876-5432"));
        Assert.That(dest.Others.Count,    Is.EqualTo(2));
        Assert.That(dest.Others[0].Type,  Is.EqualTo("PC"));
        Assert.That(dest.Others[0].Value, Is.EqualTo("pc@example.com"));
        Assert.That(dest.Others[1].Type,  Is.EqualTo("Mobile"));
        Assert.That(dest.Others[1].Value, Is.EqualTo("mobile@example.com"));
        Assert.That(dest.Messages.Length, Is.EqualTo(3));
        Assert.That(dest.Messages[0],     Is.EqualTo("1st message"));
        Assert.That(dest.Messages[1],     Is.EqualTo("2nd message"));
        Assert.That(dest.Messages[2],     Is.EqualTo("3rd message"));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Load_Throws
    ///
    /// <summary>
    /// Tests the behavior when the specified file does not exist.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(Format.Json)]
    [TestCase(Format.Xml)]
    public void Load_NotFound(Format format)
    {
        var src = new SettingFolder<Dummy>(format, GetType().Assembly);
        src.Load();
        Assert.That(src.Value, Is.Not.Null);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// ReLoad
    ///
    /// <summary>
    /// Executes the test for loading twice.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void ReLoad()
    {
        var name = GetKeyName();

        using var src = new SettingFolder<Dummy>(Format.Registry, name, new());
        src.AutoSave = false;
        src.Load();
        src.Value.Name = "Before ReLoad";
        src.Load();

        Assert.That(src.Value.Name, Is.EqualTo("山田太郎"));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// AutoSave
    ///
    /// <summary>
    /// Executes the test for automatically saving the settings.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void AutoSave()
    {
        var key  = nameof(AutoSave);
        var name = GetKeyName(key);
        var ts   = TimeSpan.FromMilliseconds(100);

        using (var src = new SettingFolder<Dummy>(Format.Registry, name, new()))
        {
            src.AutoSave       = true;
            src.AutoSaveDelay  = ts;
            src.Value.Name     = "AutoSave";
            src.Value.Age      = 77;
            src.Value.Sex      = Sex.Female;
            src.Value.Secret   = "SecretChanged";
            src.Value.Reserved = true;
            src.Value.Reserved = false;
            src.Value.Reserved = false;

            Task.Delay(TimeSpan.FromTicks(ts.Ticks * 2)).Wait();
        }

        using var dest = OpenKey(key);
        Assert.That(dest.GetValue("Name"),     Is.EqualTo("AutoSave"));
        Assert.That(dest.GetValue("Age"),      Is.EqualTo(77));
        Assert.That(dest.GetValue("Sex"),      Is.EqualTo(1));
        Assert.That(dest.GetValue("Reserved"), Is.EqualTo(0));
        Assert.That(dest.GetValue("Secret"),   Is.Null);
    }

    #endregion

    #region Others

    /* --------------------------------------------------------------------- */
    ///
    /// Setup
    ///
    /// <summary>
    /// Invokes the setup operation.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [SetUp]
    public void Setup() => Format.Registry.Serialize(GetKeyName(), DummyFactory.Create());

    #endregion
}
