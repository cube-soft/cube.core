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
using System.IO;
using System.Linq;
using Cube.DataContract;
using Cube.Registries.Extensions;
using Cube.Syntax.Extensions;
using Cube.Tests;
using Microsoft.Win32;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// DataContractTest
///
/// <summary>
/// Tests methods of the DataContract related classes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class DataContractTest : RegistryFixture
{
    #region Tests

    #region Serialize

    /* --------------------------------------------------------------------- */
    ///
    /// Serialize_File
    ///
    /// <summary>
    /// Tests the Serialize methods with files.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(Format.Xml,  "Person.xml")]
    [TestCase(Format.Json, "Person.json")]
    public void Serialize_File(Format format, string filename)
    {
        var dest = Get(filename);
        format.Serialize(dest, DummyFactory.Create());
        Assert.That(new Entity(dest).Length, Is.AtLeast(1));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Serialize_Registry
    ///
    /// <summary>
    /// Tests the Serialize method with the registry subkey.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Serialize_Registry()
    {
        var name = GetKeyName(nameof(Serialize_Registry));

        Format.Registry.Serialize(name, DummyFactory.Create());
        Format.Registry.Serialize(name, default(Dummy)); // ignore

        using var sk = OpenKey(nameof(Serialize_Registry));
        var time = new DateTime(2014, 12, 31, 23, 25, 30, DateTimeKind.Utc);

        Assert.That(sk.GetValue("Name"), Is.EqualTo("山田太郎"));
        Assert.That(sk.GetValue("Age"), Is.EqualTo(15));
        Assert.That(sk.GetValue("Sex"), Is.EqualTo(0));
        Assert.That(sk.GetValue("Reserved"), Is.EqualTo(1));
        Assert.That(sk.GetValue("Creation"), Is.EqualTo(time.ToString("o")));
        Assert.That(sk.GetValue("ID"), Is.EqualTo(123));
        Assert.That(sk.GetValue("Secret"), Is.Null);

        Assert.That(sk.GetValue<string>(@"Contact", "Type"), Is.EqualTo("Phone"));
        Assert.That(sk.GetValue<string>(@"Contact", "Value"), Is.EqualTo("080-9876-5432"));
        Assert.That(sk.GetValue<string>(@"Others\0", "Type"), Is.EqualTo("PC"));
        Assert.That(sk.GetValue<string>(@"Others\0", "Value"), Is.EqualTo("pc@example.com"));
        Assert.That(sk.GetValue<string>(@"Others\1", "Type"), Is.EqualTo("Mobile"));
        Assert.That(sk.GetValue<string>(@"Others\1", "Value"), Is.EqualTo("mobile@example.com"));
        Assert.That(sk.GetValue<string>(@"Messages\0", ""), Is.EqualTo("1st message"));
        Assert.That(sk.GetValue<string>(@"Messages\1", ""), Is.EqualTo("2nd message"));
        Assert.That(sk.GetValue<string>(@"Messages\2", ""), Is.EqualTo("3rd message"));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Serialize_Registry_Remove
    ///
    /// <summary>
    /// Tests the Serialize method with objects that have a collection.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Serialize_Registry_Remove()
    {
        var name = GetKeyName(nameof(Serialize_Registry_Remove));
        var src  = DummyFactory.Create();

        Format.Registry.Serialize(name, src);
        src.Others.RemoveAt(0);
        Format.Registry.Serialize(name, src);

        var dest = Format.Registry.Deserialize<Dummy>(name);
        Assert.That(dest.Others.Count, Is.EqualTo(1));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Serialize_Registry_Add
    ///
    /// <summary>
    /// Tests the Serialize method with objects that have a collection.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Serialize_Registry_Add()
    {
        var name = GetKeyName(nameof(Serialize_Registry_Add));
        var src  = DummyFactory.Create();

        Format.Registry.Serialize(name, src);
        src.Messages = 10.Make(i => $"{i}th message").ToArray();
        Format.Registry.Serialize(name, src);

        var dest = Format.Registry.Deserialize<Dummy>(name);
        Assert.That(dest.Messages.Length, Is.EqualTo(10));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Serialize_Registry_Null
    ///
    /// <summary>
    /// Confirms the behavior when serializing to the invalid registry
    /// subkey.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Serialize_Registry_Null()
    {
        var src  = default(RegistryKey);
        var data = DummyFactory.Create();
        src.Serialize(data); // Does not throw.
    }

    #endregion

    #region Deserialize

    /* --------------------------------------------------------------------- */
    ///
    /// Deserialize_File
    ///
    /// <summary>
    /// Tests the Deserialize methods with files.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(Format.Xml,  "Settings.xml",     ExpectedResult = "John Lennon")]
    [TestCase(Format.Json, "Settings.json",    ExpectedResult = "Mike Davis")]
    [TestCase(Format.Xml,  "Settings.ja.xml",  ExpectedResult = "鈴木一朗")]
    [TestCase(Format.Json, "Settings.ja.json", ExpectedResult = "山田太郎")]
    public string Deserialize_File(Format format, string filename)
    {
        var src = GetSource(filename);
        Assert.That(format.Exists(src), Is.True);

        var dest = format.Deserialize<Dummy>(src);
        dest.Refresh(nameof(dest.Number), nameof(dest.Name));
        return dest.Name;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Deserialize_File_Brackets
    ///
    /// <summary>
    /// Tests the Deserialize method with a file that contains only "{}".
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Deserialize_File_Brackets()
    {
        var src  = GetSource("Brackets.json");
        var dest = Format.Json.Deserialize<Dummy>(src);
        Assert.That(dest, Is.Not.Null);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Deserialize_File_Empty
    ///
    /// <summary>
    /// Tests the Deserialize methods with an empty file.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Deserialize_File_Empty() => Assert.That(() =>
    {
        var src = Get("Empty.json");
        IoEx.Touch(src);
        _ = Format.Json.Deserialize<Dummy>(src);
    }, Throws.TypeOf<System.Runtime.Serialization.SerializationException>());

    /* --------------------------------------------------------------------- */
    ///
    /// Deserialize_File_NotFound
    ///
    /// <summary>
    /// Tests the Deserialize methods with an inexistent file.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Deserialize_File_NotFound() => Assert.That(
        () => Format.Json.Deserialize<Dummy>(GetSource("not-found.json")),
        Throws.TypeOf<FileNotFoundException>()
    );

    /* --------------------------------------------------------------------- */
    ///
    /// Deserialize_Registry_Null
    ///
    /// <summary>
    /// Confirms the behavior when deserializing from the invalid
    /// registry subkey.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Deserialize_Registry_Null()
    {
        var src  = default(RegistryKey).Deserialize<Dummy>();
        var dest = new Dummy();

        Assert.That(src.Number, Is.EqualTo(dest.Number));
        Assert.That(src.Name,           Is.EqualTo(dest.Name));
        Assert.That(src.Age,            Is.EqualTo(dest.Age));
        Assert.That(src.Sex,            Is.EqualTo(dest.Sex));
        Assert.That(src.Reserved,       Is.EqualTo(dest.Reserved));
        Assert.That(src.Creation,       Is.EqualTo(dest.Creation));
        Assert.That(src.Secret,         Is.EqualTo(dest.Secret));
        Assert.That(src.Contact.Type,   Is.EqualTo(dest.Contact.Type));
        Assert.That(src.Contact.Value,  Is.EqualTo(dest.Contact.Value));
        Assert.That(src.Others.Count,   Is.EqualTo(0));
    }

    #endregion

    /* --------------------------------------------------------------------- */
    ///
    /// Delete_Registry
    ///
    /// <summary>
    /// Tests the Delete extended method for the registry.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Delete_Registry()
    {
        var fmt = Format.Registry;
        var src = GetKeyName(nameof(Delete_Registry));

        Assert.That(fmt.Exists(src), Is.False);
        fmt.Serialize(src, DummyFactory.Create());
        Assert.That(fmt.Exists(src), Is.True);
        fmt.Delete(src);
        fmt.Delete(src); // ignored
        Assert.That(fmt.Exists(src), Is.False);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Delete_Json
    ///
    /// <summary>
    /// Tests the Delete extended method for the JSON data.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Delete_Json()
    {
        var fmt = Format.Json;
        var src = Get("Settings.json");

        Assert.That(fmt.Exists(src), Is.False);
        Io.Copy(GetSource("Settings.json"), src, true);
        Assert.That(fmt.Exists(src), Is.True);
        fmt.Delete(src);
        fmt.Delete(src);
        Assert.That(fmt.Exists(src), Is.False);
    }

    #endregion
}
