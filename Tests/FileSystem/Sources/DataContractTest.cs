﻿/* ------------------------------------------------------------------------- */
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cube.FileSystem.DataContract;
using Cube.Mixin.Iteration;
using Cube.Mixin.Registry;
using Microsoft.Win32;
using NUnit.Framework;

namespace Cube.FileSystem.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// DataContractTest
    ///
    /// <summary>
    /// Tests methods of the DataContract related classes.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class DataContractTest : RegistryFixture
    {
        #region Tests

        #region Serialize

        /* ----------------------------------------------------------------- */
        ///
        /// Serialize_File
        ///
        /// <summary>
        /// Tests the Serialize methods with files.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(Format.Xml,  "Person.xml")]
        [TestCase(Format.Json, "Person.json")]
        public void Serialize_File(Format format, string filename)
        {
            var dest = Get(filename);
            format.Serialize(dest, CreateDummy());
            Assert.That(new FileInfo(dest).Length, Is.AtLeast(1));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Serialize_Registry
        ///
        /// <summary>
        /// Tests the Serialize method with the registry subkey.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Serialize_Registry()
        {
            var name = GetKeyName(nameof(Serialize_Registry));

            Format.Registry.Serialize(name, CreateDummy());
            Format.Registry.Serialize(name, default(Person)); // ignore

            using var sk = OpenSubKey(nameof(Serialize_Registry));
            var time = new DateTime(2014, 12, 31, 23, 25, 30).ToUniversalTime();

            Assert.That(sk.GetValue("Name"), Is.EqualTo("山田花子"));
            Assert.That(sk.GetValue("Age"), Is.EqualTo(15));
            Assert.That(sk.GetValue("Sex"), Is.EqualTo(1));
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

        /* ----------------------------------------------------------------- */
        ///
        /// Serialize_Registry_Remove
        ///
        /// <summary>
        /// Tests the Serialize method with objects that have a collection.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Serialize_Registry_Remove()
        {
            var name = GetKeyName(nameof(Serialize_Registry_Remove));
            var src  = CreateDummy();

            Format.Registry.Serialize(name, src);
            src.Others.RemoveAt(0);
            Format.Registry.Serialize(name, src);

            var dest = Format.Registry.Deserialize<Person>(name);
            Assert.That(dest.Others.Count, Is.EqualTo(1));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Serialize_Registry_Add
        ///
        /// <summary>
        /// Tests the Serialize method with objects that have a collection.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Serialize_Registry_Add()
        {
            var name = GetKeyName(nameof(Serialize_Registry_Add));
            var src  = CreateDummy();

            Format.Registry.Serialize(name, src);
            src.Messages = 10.Make(i => $"{i}th message").ToArray();
            Format.Registry.Serialize(name, src);

            var dest = Format.Registry.Deserialize<Person>(name);
            Assert.That(dest.Messages.Length, Is.EqualTo(10));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Serialize_Registry_Null
        ///
        /// <summary>
        /// Confirms the behavior when serializing to the invalid registry
        /// subkey.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Serialize_Registry_Null()
        {
            var src  = default(RegistryKey);
            var data = CreateDummy();
            Assert.DoesNotThrow(() => src.Serialize(data));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Serialize_Throws
        ///
        /// <summary>
        /// Tests the Serialize method with Registry format and stream.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Serialize_Throws()
        {
            var dest = Get("Person.reg");
            using var file = File.Create(dest);
            var src = Format.Registry;
            var data = CreateDummy();
            Assert.That(() => src.Serialize(file, data), Throws.ArgumentException);
        }

        #endregion

        #region Deserialize

        /* ----------------------------------------------------------------- */
        ///
        /// Deserialize_File
        ///
        /// <summary>
        /// Tests the Deserialize methods with files.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(Format.Xml,  "Settings.xml",     ExpectedResult = "John Lennon")]
        [TestCase(Format.Json, "Settings.json",    ExpectedResult = "Mike Davis")]
        [TestCase(Format.Xml,  "Settings.ja.xml",  ExpectedResult = "鈴木一朗")]
        [TestCase(Format.Json, "Settings.ja.json", ExpectedResult = "山田太郎")]
        public string Deserialize_File(Format format, string filename)
        {
            var dest = format.Deserialize<Person>(GetSource(filename));
            Assert.That(dest.Dispatcher, Is.EqualTo(Dispatcher.Vanilla));
            dest.Refresh(nameof(dest.Identification), nameof(dest.Name));
            return dest.Name;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Deserialize_Registry_Null
        ///
        /// <summary>
        /// Confirms the behavior when deserializing from the invalid
        /// registry subkey.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Deserialize_Registry_Null()
        {
            var dest     = default(RegistryKey).Deserialize<Person>();
            var expected = new Person();

            Assert.That(dest.Dispatcher,     Is.EqualTo(Dispatcher.Vanilla));
            Assert.That(dest.Identification, Is.EqualTo(expected.Identification));
            Assert.That(dest.Name,           Is.EqualTo(expected.Name));
            Assert.That(dest.Age,            Is.EqualTo(expected.Age));
            Assert.That(dest.Sex,            Is.EqualTo(expected.Sex));
            Assert.That(dest.Reserved,       Is.EqualTo(expected.Reserved));
            Assert.That(dest.Creation,       Is.EqualTo(expected.Creation));
            Assert.That(dest.Secret,         Is.EqualTo(expected.Secret));
            Assert.That(dest.Contact.Type,   Is.EqualTo(expected.Contact.Type));
            Assert.That(dest.Contact.Value,  Is.EqualTo(expected.Contact.Value));
            Assert.That(dest.Others.Count,   Is.EqualTo(0));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Deserialize_Throws
        ///
        /// <summary>
        /// Tests the Deserialize method with the Registry format and
        /// stream.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Deserialize_Throws()
        {
            var src = GetSource("Settings.xml");
            using var e = File.OpenRead(src);
            var dest = Format.Registry;
            Assert.That(() => dest.Deserialize<Person>(e), Throws.ArgumentException);
        }

        #endregion

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// CreateDummy
        ///
        /// <summary>
        /// Creates dummy data.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Person CreateDummy() => new()
        {
            Identification = 123,
            Name           = "山田花子",
            Sex            = Sex.Female,
            Age            = 15,
            Creation       = new DateTime(2014, 12, 31, 23, 25, 30),
            Contact        = new Address { Type = "Phone", Value = "080-9876-5432" },
            Reserved       = true,
            Secret         = "dummy data",
            Others         = new List<Address>
            {
                new Address { Type = "PC",     Value = "pc@example.com" },
                new Address { Type = "Mobile", Value = "mobile@example.com" }
            },
            Messages       = new[]
            {
                "1st message",
                "2nd message",
                "3rd message",
            }
        };

        #endregion
    }
}