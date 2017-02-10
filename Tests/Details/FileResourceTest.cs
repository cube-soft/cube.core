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
using NUnit.Framework;
using IoEx = System.IO;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// FileResourceTest
    /// 
    /// <summary>
    /// FileResource をテストするためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Parallelizable]
    [TestFixture]
    class FileResourceTest : FileResource
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Examples_Exists
        /// 
        /// <summary>
        /// Examples フォルダが存在するかどうかをテストします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Examples_Exists()
        {
            Assert.That(
                IoEx.Directory.Exists(Examples),
                Is.True
            );
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Results_Exists
        /// 
        /// <summary>
        /// Results フォルダが存在するかどうかをテストします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Results_Exists()
        {
            Assert.That(
                IoEx.Directory.Exists(Results),
                Is.True
            );
        }

        #endregion
    }
}
