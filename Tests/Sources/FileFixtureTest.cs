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
using System.IO;
using NUnit.Framework;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// FileFixtureTest
    ///
    /// <summary>
    /// Tests the FileFixture class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class FileFixtureTest : FileFixture
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Examples_Exists
        ///
        /// <summary>
        /// Confirms that the path specified in the Examples property exists.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Examples_Exists() => Assert.That(Directory.Exists(Examples));

        /* ----------------------------------------------------------------- */
        ///
        /// Results_Exists
        ///
        /// <summary>
        /// Confirms that the path specified in the Results property exists.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Results_Exists() => Assert.That(Directory.Exists(Results));
    }
}
