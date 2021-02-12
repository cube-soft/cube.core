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
using System;
using Cube.Mixin.Assembly;
using NUnit.Framework;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// SoftwareVersionTest
    ///
    /// <summary>
    /// Tests for the SoftwareVersion and related classes.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class SoftwareVersionTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Digit
        ///
        /// <summary>
        /// Executes the test for setting the digit.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(1, ExpectedResult = 2)]
        [TestCase(2, ExpectedResult = 2)]
        [TestCase(3, ExpectedResult = 3)]
        [TestCase(4, ExpectedResult = 4)]
        [TestCase(5, ExpectedResult = 4)]
        public int Digit(int src)
        {
            var asm = typeof(SoftwareVersionTest).Assembly;
            return new SoftwareVersion(asm) { Digit = src }.Digit;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetString
        ///
        /// <summary>
        /// Executes the test for getting the version string.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetString()
        {
            var src   = typeof(SoftwareVersionTest).Assembly;
            var major = src.GetVersion().Major;
            var minor = src.GetVersion().Minor;
            var pf    = src.GetArchitecture();
            var dest  = new SoftwareVersion(src)
            {
                Digit  = 2,
                Prefix = "begin-",
                Suffix = "-end"
            };

            Assert.That(dest.Architecture,    Is.EqualTo(pf));
            Assert.That(dest.ToString(true),  Is.EqualTo($"begin-{major}.{minor}-end ({pf})"));
            Assert.That(dest.ToString(false), Is.EqualTo($"begin-{major}.{minor}-end"));
            Assert.That(dest.ToString(),      Is.EqualTo(dest.ToString(false)));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Parse
        ///
        /// <summary>
        /// Tests to create a new instance of the SoftwareVersion class
        /// with the specified string.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("1.0",                   ExpectedResult = true )]
        [TestCase("1.0.0",                 ExpectedResult = true )]
        [TestCase("1.0.0.0",               ExpectedResult = true )]
        [TestCase("1.0.0.0-suffix",        ExpectedResult = true )]
        [TestCase("v1.0.0.0-suffix",       ExpectedResult = true )]
        [TestCase("v1.0.0.0-p21",          ExpectedResult = true )]
        [TestCase("p21-v1.0.0.0-suffix",   ExpectedResult = true )]
        [TestCase("1",                     ExpectedResult = false)]
        [TestCase("InvalidVersionNumber",  ExpectedResult = false)]
        [TestCase(null,                    ExpectedResult = false)]
        public bool Parse(string src) => new SoftwareVersion(src).ToString() == src;

        /* ----------------------------------------------------------------- */
        ///
        /// Compare
        ///
        /// <summary>
        /// Tests the CompareTo method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("1.0.0",       "1.0.0",      ExpectedResult =  0)]
        [TestCase("1.0.0",       "1.0",        ExpectedResult =  0)]
        [TestCase("1.0.0",       "2.0.0",      ExpectedResult = -1)]
        [TestCase("1.0.0",       "1.1.0",      ExpectedResult = -1)]
        [TestCase("1.0.0",       "1.0.1",      ExpectedResult = -1)]
        [TestCase("1.0.0",       "0.9.9",      ExpectedResult =  1)]
        [TestCase("3.1.4",       "2.2.2",      ExpectedResult =  1)]
        [TestCase("3.1.4",       "4.3.2",      ExpectedResult = -1)]
        [TestCase("v1.0.0",      "1.0.0",      ExpectedResult =  1)]
        [TestCase("v1.0.0",      "V1.0.0",     ExpectedResult =  0)]
        [TestCase("0.0.1-alpha", "0.0.1-beta", ExpectedResult = -1)]
        [TestCase("0.1.0-beta",  "0.1.0-BETA", ExpectedResult =  0)]
        public int Compare(string src, string cmp)
        {
            var v1 = new SoftwareVersion(src);
            var v2 = new SoftwareVersion(cmp);
            return new SoftwareVersionComparer().Compare(v1, v2);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Equals_GetHashCode
        ///
        /// <summary>
        /// Tests the Equals and GetHashCode methods.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("1.0.0",       "1.0.0",      true )]
        [TestCase("1.0.0",       "1.0",        true )]
        [TestCase("1.0.0",       "2.0.0",      false)]
        [TestCase("v1.0.0",      "1.0.0",      false)]
        [TestCase("v1.0.0",      "V1.0.0",     true )]
        [TestCase("0.0.1-alpha", "0.0.1-beta", false)]
        [TestCase("0.1.0-beta",  "0.1.0-BETA", true )]
        public void Equals_GetHashCode(string src, string cmp, bool expected)
        {
            var engine = new SoftwareVersionComparer();

            var v1  = new SoftwareVersion(src);
            var v2  = new SoftwareVersion(cmp);
            Assert.That(engine.Equals(v1, v2), Is.EqualTo(expected), nameof(engine.Equals));

            var h1 = engine.GetHashCode(v1);
            var h2 = engine.GetHashCode(v2);
            Assert.That(h1.Equals(h2), Is.EqualTo(expected), nameof(engine.GetHashCode));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Compare_Object
        ///
        /// <summary>
        /// Tests the CompareTo method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Compare_Object()
        {
            var src = new SoftwareVersionComparer();

            Assert.That(src.Compare(0, 0),    Is.EqualTo(0));
            Assert.That(src.Compare(0, 1),    Is.EqualTo(-1));
            Assert.That(src.Compare(1, 0),    Is.EqualTo(1));
            Assert.That(src.Compare(null, 0), Is.EqualTo(-1));
            Assert.That(src.Compare(0, null), Is.EqualTo(1));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Equals_Object
        ///
        /// <summary>
        /// Tests the CompareTo method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Equals_Object()
        {
            var src = new SoftwareVersionComparer();

            Assert.That(src.Equals(0, 0),    Is.True);
            Assert.That(src.Equals(0, 1),    Is.False);
            Assert.That(src.Equals(1, 0),    Is.False);
            Assert.That(src.Equals(null, 0), Is.False);
            Assert.That(src.Equals(0, null), Is.False);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetHashCode_Object
        ///
        /// <summary>
        /// Tests the CompareTo method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetHashCode_Object()
        {
            var src = new SoftwareVersionComparer();

            Assert.That(src.GetHashCode(0), Is.EqualTo(0));
            Assert.That(src.GetHashCode(1), Is.Not.EqualTo(0));
            Assert.That(() => src.GetHashCode(null), Throws.TypeOf<NullReferenceException>());
        }

        #endregion
    }
}
