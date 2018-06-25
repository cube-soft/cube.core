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
using Cube.Generics;
using NUnit.Framework;
using System;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// GenericExtensionTest
    ///
    /// <summary>
    /// GenericExtension のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class GenericExtensionTest
    {
        #region Tests

        #region Copy

        /* ----------------------------------------------------------------- */
        ///
        /// Copy_Serializable
        ///
        /// <summary>
        /// Serializable 属性が付与されているオブジェクトのコピーの
        /// テストを実行します。
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
        /// Serializable 属性が付与されていないオブジェクトのコピーの
        /// テストを実行します。
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
            src.Secret      = "Non-DataMember";

            var copy = src.Copy();
            Assert.That(copy.Identification, Is.EqualTo(src.Identification));
            Assert.That(copy.Name,           Is.EqualTo(src.Name));
            Assert.That(copy.Sex,            Is.EqualTo(src.Sex));
            Assert.That(copy.Age,            Is.EqualTo(src.Age));
            Assert.That(copy.Creation,       Is.EqualTo(src.Creation));
            Assert.That(copy.Reserved,       Is.EqualTo(src.Reserved));
            Assert.That(copy.Contact.Value,    Is.EqualTo(src.Contact.Value));
            Assert.That(copy.Secret,         Is.EqualTo(src.Secret));
            Assert.That(copy.Guid,           Is.Not.EqualTo(src.Guid));
        }

        #endregion

        #region Modify after copying

        /* ----------------------------------------------------------------- */
        ///
        /// Copy_Modify_Integer
        ///
        /// <summary>
        /// コピー後、コピー元オブジェクトの int の値を変更するテストを
        /// 実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Copy_Modify_Integer()
        {
            var src = new Person();
            src.Identification = 10;
            var copy = src.Copy();
            src.Identification = 20;

            Assert.That(copy.Identification, Is.Not.EqualTo(src.Identification));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Copy_Modify_String
        ///
        /// <summary>
        /// コピー後、コピー元オブジェクトの string の値を変更するテストを
        /// 実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Copy_Modify_String()
        {
            var src = new Person();
            src.Name = "Copy Serializable";
            var copy = src.Copy();
            src.Name = "Re-assign";

            Assert.That(copy.Name, Is.Not.EqualTo(src.Name));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Copy_Modify_Reference
        ///
        /// <summary>
        /// コピー後、コピー元オブジェクトのクラスの値を変更するテストを
        /// 実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Copy_Modify_Reference()
        {
            var src = new Person();
            src.Contact.Value = "01-2345-6789";
            var copy = src.Copy();
            src.Contact.Value = "98-7654-3210";

            Assert.That(copy.Contact.Value, Is.EqualTo(src.Contact.Value));
        }

        #endregion

        #region TryCast

        /* ----------------------------------------------------------------- */
        ///
        /// TryCast_Failed
        ///
        /// <summary>
        /// TryCast に失敗した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TryCast_Failed() =>
            Assert.That(10.TryCast("failed"), Is.EqualTo("failed"));

        #endregion

        #endregion
    }
}
