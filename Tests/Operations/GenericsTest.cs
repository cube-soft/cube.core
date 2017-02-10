/* ------------------------------------------------------------------------- */
///
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///  http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
///
/* ------------------------------------------------------------------------- */
using System;
using NUnit.Framework;
using Cube.Generics;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// GenericsTest
    /// 
    /// <summary>
    /// Cube.Generics.Operations のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Parallelizable]
    [TestFixture]
    class GenericsTest
    {
        #region Tests

        #region Copy

        /* ----------------------------------------------------------------- */
        ///
        /// Copy_Serializable
        ///
        /// <summary>
        /// Serializable 属性が付与されているオブジェクトのコピーをテストします。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        [Test]
        public void Copy_Serializable()
        {
            var src = new SerializableData();
            src.Identification = 10;
            src.Name = "Copy Serializable";
            src.Sex = Sex.Male;
            src.Creation = DateTime.Now;
            src.Reserved = true;

            var copy = src.Copy();
            Assert.That(copy.Identification, Is.EqualTo(src.Identification));
            Assert.That(copy.Name, Is.EqualTo(src.Name));
            Assert.That(copy.Sex, Is.EqualTo(src.Sex));
            Assert.That(copy.Creation, Is.EqualTo(src.Creation));
            Assert.That(copy.Reserved, Is.EqualTo(src.Reserved));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Copy_NonSerializable
        ///
        /// <summary>
        /// Serializable 属性が付与されていないオブジェクトのコピーをテストします。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        [Test]
        public void Copy_NonSerializable()
        {
            var src = new Person();
            src.Identification = 10;
            src.Name = "Copy Serializable";
            src.Sex = Sex.Male;
            src.Age = 25;
            src.Creation = DateTime.Now;
            src.Reserved = true;
            src.Phone.Value = "01-2345-6789";
            src.Secret = "Non-DataMember";

            var copy = src.Copy();
            Assert.That(copy.Identification, Is.EqualTo(src.Identification));
            Assert.That(copy.Name, Is.EqualTo(src.Name));
            Assert.That(copy.Sex, Is.EqualTo(src.Sex));
            Assert.That(copy.Age, Is.EqualTo(src.Age));
            Assert.That(copy.Creation, Is.EqualTo(src.Creation));
            Assert.That(copy.Reserved, Is.EqualTo(src.Reserved));
            Assert.That(copy.Phone.Value, Is.EqualTo(src.Phone.Value));
            Assert.That(copy.Secret, Is.EqualTo(src.Secret));
        }

        #endregion

        #region Modify after copying

        /* ----------------------------------------------------------------- */
        ///
        /// Modify_Integer_NotEqual
        ///
        /// <summary>
        /// コピー後、コピー元オブジェクトの int の値を変更するテストを行います。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        [Test]
        public void Modify_Integer_NotEqual()
        {
            var src = new Person();
            src.Identification = 10;
            var copy = src.Copy();
            src.Identification = 20;

            Assert.That(
                copy.Identification,
                Is.Not.EqualTo(src.Identification)
            );
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Modify_String_NotEqual
        ///
        /// <summary>
        /// コピー後、コピー元オブジェクトの string の値を変更するテストを行います。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        [Test]
        public void Modify_String_NotEqual()
        {
            var src = new Person();
            src.Name = "Copy Serializable";
            var copy = src.Copy();
            src.Name = "Re-assign";

            Assert.That(
                copy.Name,
                Is.Not.EqualTo(src.Name)
            );
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Modify_Reference_Equal
        ///
        /// <summary>
        /// コピー後、コピー元オブジェクトのクラスの値を変更するテストを行います。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        [Test]
        public void Modify_Reference_Equal()
        {
            var src = new Person();
            src.Phone.Value = "01-2345-6789";
            var copy = src.Copy();
            src.Phone.Value = "98-7654-3210";

            Assert.That(
                copy.Phone.Value,
                Is.EqualTo(src.Phone.Value)
            );
        }

        #endregion

        #endregion
    }
}
