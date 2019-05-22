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
using Cube.Mixin.Assembly;
using NUnit.Framework;
using System.Reflection;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// SoftwareVersionTest
    ///
    /// <summary>
    /// Tests for the SoftwareVersion class.
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
            var asm = Assembly.GetExecutingAssembly();
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
            var src   = Assembly.GetExecutingAssembly();
            var major = src.GetVersion().Major;
            var minor = src.GetVersion().Minor;
            var pf    = src.GetPlatform();
            var dest  = new SoftwareVersion(src)
            {
                Digit  = 2,
                Prefix = "begin-",
                Suffix = "-end"
            };

            Assert.That(dest.Platform,        Is.EqualTo(pf));
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
        public bool Parse(string src)
        {
            var dest = new SoftwareVersion(src);
            var cmp  = new SoftwareVersion(src);

            Assert.That(dest,               Is.EqualTo(cmp));
            Assert.That(dest,               Is.Not.EqualTo(src));
            Assert.That(dest,               Is.Not.EqualTo(default(SoftwareVersion)));
            Assert.That(dest.GetHashCode(), Is.EqualTo(cmp.GetHashCode()));

            return dest.ToString() == src;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Compare
        ///
        /// <summary>
        /// Tests the Equals method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Compare()
        {
            var src = new SoftwareVersion("1.0.0.0");
            Assert.That(src.Equals(null), Is.False);
            Assert.That(src.Equals(new object()), Is.False);
            var c0  = new SoftwareVersion("1.0.0.0");
            Assert.That(src.Equals((object)c0), Is.True, c0.ToString());
            var c1 = new SoftwareVersion("1.0");
            Assert.That(src.Equals((object)c1), Is.False, c1.ToString());
            var c2 = new SoftwareVersion("v1.0.0.0");
            Assert.That(src.Equals((object)c2), Is.False, c2.ToString());
            var c3 = new SoftwareVersion("1.0.0.0-p21");
            Assert.That(src.Equals((object)c3), Is.False, c3.ToString());
        }

        #endregion
    }
}
