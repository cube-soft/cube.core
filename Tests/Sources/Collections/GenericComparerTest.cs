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
using Cube.Collections;
using NUnit.Framework;
using System.Collections.Generic;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// GenericComparerTest
    ///
    /// <summary>
    /// GenericComparer(T) および GenericEqualityComparer(T) のテスト用
    /// クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class GenericComparerTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// GenericComparer
        ///
        /// <summary>
        /// Executes the test to use an instance of the GenericComparer(T)
        /// class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GenericComparer()
        {
            var x0 = new Person { Name = "Mike",  Age = 30, Sex = Sex.Male   };
            var x1 = new Person { Name = "Jenny", Age = 15, Sex = Sex.Female };
            var x2 = new Person { Name = "Tom",   Age = 12, Sex = Sex.Male   };
            var x3 = new Person { Name = "Mike",  Age = 25, Sex = Sex.Male   };

            var src = new List<Person> { x0, x1, x2, x3 };
            src.Sort(new GenericComparer<Person>((x, y) =>
            {
                var r0 = x.Name.CompareTo(y.Name);
                if (r0 != 0) return r0;
                var r1 = x.Age.CompareTo(y.Age);
                if (r1 != 0) return r1;
                return x.Sex.CompareTo(y.Sex);
            }));

            Assert.That(src[0], Is.EqualTo(x1));
            Assert.That(src[1], Is.EqualTo(x3));
            Assert.That(src[2], Is.EqualTo(x0));
            Assert.That(src[3], Is.EqualTo(x2));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GenericEqualityComparer
        ///
        /// <summary>
        /// Executes the test to use an instance of the
        /// GenericEqualityComparer(T) class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GenericEqualityComparer()
        {
            var cmp = new GenericEqualityComparer<Person>((x, y) =>
                x.Name == y.Name &&
                x.Age  == y.Age  &&
                x.Sex  == y.Sex
            );

            var dic = new Dictionary<Person, object>(cmp);
            var x0  = new Person { Name = "Mike", Age = 30, Sex = Sex.Male };
            var x1  = new Person { Name = "Jack", Age = 30, Sex = Sex.Male };
            var x2  = new Person { Name = "Mike", Age = 30, Sex = Sex.Male };

            dic.Add(x0, new object());
            dic.Add(x1, new object());
            Assert.That(dic.Count, Is.EqualTo(2));
            Assert.That(() => dic.Add(x2, new object()), Throws.TypeOf<System.ArgumentException>());
        }

        #endregion
    }
}
