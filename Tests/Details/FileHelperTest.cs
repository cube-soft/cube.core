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
using System.IO;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// FileHelperTest
    /// 
    /// <summary>
    /// FileHelper のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class FileHelperTest : FileHelper
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Examples_Exists
        /// 
        /// <summary>
        /// Examples フォルダが存在するかどうかのテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(ExpectedResult = true)]
        public bool Examples_Exists() => Directory.Exists(Examples);

        /* ----------------------------------------------------------------- */
        ///
        /// Results_Exists
        /// 
        /// <summary>
        /// Results フォルダが存在するかどうかのテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(ExpectedResult = true)]
        public bool Results_Exists() => Directory.Exists(Results);
    }
}
