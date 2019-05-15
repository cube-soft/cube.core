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
using Cube.Mixin.Registry;
using Cube.Net35;
using Microsoft.Win32;
using NUnit.Framework;

namespace Cube.Tests
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
                    sk.SetValue("0", "PC", "pc@example.com");
                    sk.SetValue("1", "Mobile", "mobile@example.com");
                }

                using (var sk = k.CreateSubKey(nameof(Person.Messages)))
                {
                    sk.SetValue("0", "", "1st message");
                    sk.SetValue("1", "", "2nd message");
                    sk.SetValue("2", "", "3rd message");
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
