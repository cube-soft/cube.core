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
using Cube.Mixin.Generics;
using NUnit.Framework;
using System;
using System.ComponentModel;

namespace Cube.Tests.Mixin
{
    /* --------------------------------------------------------------------- */
    ///
    /// GenericsTest
    ///
    /// <summary>
    /// Tests the extended methods of generic classes.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class GenericsTest
    {
        #region Tests

        #region TryCast

        /* ----------------------------------------------------------------- */
        ///
        /// TryCast
        ///
        /// <summary>
        /// Tests the TryCast extended method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TryCast()
        {
            var c0 = new Person { Identification = 1 };
            Assert.That(c0, Is.Not.Null);
            var c1 = c0.TryCast<INotifyPropertyChanged>();
            Assert.That(c1, Is.Not.Null);
            var c2 = c1.TryCast<SerializableBase>();
            Assert.That(c2, Is.Not.Null);
            var c3 = c0.TryCast<Person>();
            Assert.That(c3, Is.Not.Null);
            var c4 = c3.TryCast<GenericsTest>();
            Assert.That(c4, Is.Null);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TryCast_Integer
        ///
        /// <summary>
        /// Tests the TryCast extended method with the int value.
        /// </summary>
        ///
        /// <remarks>
        /// int から double など継承関係にない型への TryCast は常に失敗します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TryCast_Integer()
        {
            Assert.That(10.TryCast(-1L),     Is.EqualTo(-1L));
            Assert.That(10.TryCast(-1.0),    Is.EqualTo(-1.0));
            Assert.That(10.TryCast("error"), Is.EqualTo("error"));
        }

        #endregion

        #region Copy

        /* ----------------------------------------------------------------- */
        ///
        /// Copy_Serializable
        ///
        /// <summary>
        /// Tests the Copy extended method with the serializable object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Copy_Serializable()
        {
            var src = new SerializableData
            {
                Identification = 10,
                Name           = "Copy Serializable",
                Sex            = Sex.Male,
                Creation       = DateTime.Now,
                Reserved       = true
            };
            var copy = src.Copy();
            Assert.That(copy.Identification, Is.EqualTo(src.Identification));
            Assert.That(copy.Name,           Is.EqualTo(src.Name));
            Assert.That(copy.Sex,            Is.EqualTo(src.Sex));
            Assert.That(copy.Creation,       Is.EqualTo(src.Creation));
            Assert.That(copy.Reserved,       Is.EqualTo(src.Reserved));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Copy_NonSerializable
        ///
        /// <summary>
        /// Tests the Copy extended method with the non-serializable object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Copy_NonSerializable()
        {
            var src = new Person
            {
                Identification = 10,
                Name           = "Copy Serializable",
                Sex            = Sex.Male,
                Age            = 25,
                Creation       = DateTime.Now,
                Reserved       = true
            };
            src.Contact.Value = "01-2345-6789";
            src.Secret        = "Non-DataMember";

            var copy = src.Copy();
            Assert.That(copy.Identification, Is.EqualTo(src.Identification));
            Assert.That(copy.Name,           Is.EqualTo(src.Name));
            Assert.That(copy.Sex,            Is.EqualTo(src.Sex));
            Assert.That(copy.Age,            Is.EqualTo(src.Age));
            Assert.That(copy.Creation,       Is.EqualTo(src.Creation));
            Assert.That(copy.Reserved,       Is.EqualTo(src.Reserved));
            Assert.That(copy.Contact.Value,  Is.EqualTo(src.Contact.Value));
            Assert.That(copy.Secret,         Is.EqualTo(src.Secret));
            Assert.That(copy.Guid,           Is.Not.EqualTo(src.Guid));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Copy_Modify
        ///
        /// <summary>
        /// Confirms the result when creating a copied object and
        /// modifying properties of the source object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Copy_Modify()
        {
            var src = new Person
            {
                Identification = 10,
                Name           = "Copy Serializable",
                Contact        = new Address
                {
                    Type  = "Phone",
                    Value = "01-2345-6789"
                },
            };

            var dest = src.Copy();

            src.Identification = 20;
            src.Name           = "Re-assign";
            src.Contact.Value  = "98-7654-3210";

            Assert.That(dest.Identification, Is.Not.EqualTo(src.Identification));
            Assert.That(dest.Name,           Is.Not.EqualTo(src.Name));
            Assert.That(dest.Contact.Value,  Is.EqualTo(src.Contact.Value));
        }

        #endregion

        #endregion
    }
}
