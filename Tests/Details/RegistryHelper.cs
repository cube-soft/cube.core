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
using Microsoft.Win32;
using NUnit.Framework;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// RegistryHelper
    ///
    /// <summary>
    /// テストでレジストリを使用するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    class RegistryHelper : FileHelper
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// RegistryHelper
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected RegistryHelper()
        {
            RootSubKeyName = $@"Software\CubeSoft\{GetType().Name}";
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// RootSubKeyName
        ///
        /// <summary>
        /// ルートとなるサブキー名を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected string RootSubKeyName { get; }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// GetSubKeyName
        ///
        /// <summary>
        /// サブキー名を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected string GetSubKeyName(string subkey) => $@"{RootSubKeyName}\{subkey}";

        /* ----------------------------------------------------------------- */
        ///
        /// GetSubKeyName
        ///
        /// <summary>
        /// デフォルト・サブキー名を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected string GetSubKeyName() => GetSubKeyName(_default);

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
            Formatter.RootKey.CreateSubKey(GetSubKeyName(subkey));

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
            Formatter.RootKey.OpenSubKey(GetSubKeyName(subkey), false);

        /* ----------------------------------------------------------------- */
        ///
        /// OpenSaveKey
        ///
        /// <summary>
        /// レジストリ・サブキーを読み取り専用で開きます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected RegistryKey OpenSubKey() => OpenSubKey(_default);

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
            using (var key = CreateSubKey(_default))
            {
                key.SetValue("ID", 1357);
                key.SetValue(nameof(Person.Name), "山田太郎");
                key.SetValue(nameof(Person.Sex), 0);
                key.SetValue(nameof(Person.Age), 52);
                key.SetValue(nameof(Person.Creation), "2015/03/16 02:32:26");
                key.SetValue(nameof(Person.Reserved), 1);

                using (var sk = key.CreateSubKey(nameof(Person.Contact)))
                {
                    sk.SetValue(nameof(Address.Type), "Phone");
                    sk.SetValue(nameof(Address.Value), "090-1234-5678");
                }

                using (var sk = key.CreateSubKey(nameof(Person.Others)))
                {
                    using (var ssk = sk.CreateSubKey("0")) SetAddress(ssk, "PC", "pc@example.com");
                    using (var ssk = sk.CreateSubKey("1")) SetAddress(ssk, "Mobile", "mobile@example.com");
                }

                using (var sk = key.CreateSubKey(nameof(Person.Messages)))
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
        protected virtual void Teardown() => Formatter.RootKey.DeleteSubKeyTree(RootSubKeyName, false);

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
            src.SetValue(nameof(Address.Type), type);
            src.SetValue(nameof(Address.Value), value);
        }

        #endregion

        #region Fields
        private readonly string _default = "Default";
        #endregion
    }
}
