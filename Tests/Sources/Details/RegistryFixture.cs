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
using Cube.DataContract;
using Cube.FileSystem.TestService;
using Microsoft.Win32;
using NUnit.Framework;
using System.Reflection;

namespace Cube.FileSystem.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// RegistryFixture
    ///
    /// <summary>
    /// テストでレジストリを使用するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    class RegistryFixture : FileFixture
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Assembly
        ///
        /// <summary>
        /// アセンブリ情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected Assembly Assembly { get; } = Assembly.GetExecutingAssembly();

        /* ----------------------------------------------------------------- */
        ///
        /// Shared
        ///
        /// <summary>
        /// 共通するサブキー名を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected string Shared => $@"CubeSoft\{GetType().Name}";

        /* ----------------------------------------------------------------- */
        ///
        /// Default
        ///
        /// <summary>
        /// デフォルトのサブキー名を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected string Default => nameof(Default);

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// GetKeyName
        ///
        /// <summary>
        /// サブキー名を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected string GetKeyName(string subkey) => $@"{Shared}\{subkey}";

        /* ----------------------------------------------------------------- */
        ///
        /// CreateSubKey
        ///
        /// <summary>
        /// レジストリ・サブキーを作成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected RegistryKey CreateSubKey(string subkey) =>
            Formatter.DefaultKey.CreateSubKey(GetKeyName(subkey));

        /* ----------------------------------------------------------------- */
        ///
        /// OpenSubKey
        ///
        /// <summary>
        /// レジストリ・サブキーを読み取り専用で開きます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected RegistryKey OpenSubKey(string subkey) =>
            Formatter.DefaultKey.OpenSubKey(GetKeyName(subkey), false);

        #region Setup

        /* ----------------------------------------------------------------- */
        ///
        /// Setup
        ///
        /// <summary>
        /// 各テスト前に実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [SetUp]
        protected virtual void Setup()
        {
            using (var k = CreateSubKey(Default))
            {
                k.SetValue("ID", 1357);
                k.SetValue(nameof(Person.Name), "山田太郎");
                k.SetValue(nameof(Person.Sex), 0);
                k.SetValue(nameof(Person.Age), 52);
                k.SetValue(nameof(Person.Creation), "2015/03/16 02:32:26");
                k.SetValue(nameof(Person.Reserved), 1);

                using (var sk = k.CreateSubKey(nameof(Person.Contact)))
                {
                    sk.SetValue(nameof(Address.Type), "Phone");
                    sk.SetValue(nameof(Address.Value), "090-1234-5678");
                }

                using (var sk = k.CreateSubKey(nameof(Person.Others)))
                {
                    using (var ssk = sk.CreateSubKey("0")) SetAddress(ssk, "PC", "pc@example.com");
                    using (var ssk = sk.CreateSubKey("1")) SetAddress(ssk, "Mobile", "mobile@example.com");
                }

                using (var sk = k.CreateSubKey(nameof(Person.Messages)))
                {
                    using (var ssk = sk.CreateSubKey("0")) ssk.SetValue("", "1st message");
                    using (var ssk = sk.CreateSubKey("1")) ssk.SetValue("", "2nd message");
                    using (var ssk = sk.CreateSubKey("2")) ssk.SetValue("", "3rd message");
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Teardown
        ///
        /// <summary>
        /// 各テスト後に実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TearDown]
        protected virtual void Teardown() => Formatter.DefaultKey.DeleteSubKeyTree(Shared, false);

        #endregion

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// SetAddress
        ///
        /// <summary>
        /// レジストリに Address オブジェクトに対応する値を設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void SetAddress(RegistryKey src, string type, string value)
        {
            src.SetValue(nameof(Address.Type),  type );
            src.SetValue(nameof(Address.Value), value);
        }

        #endregion
    }
}
