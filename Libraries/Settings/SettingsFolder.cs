/* ------------------------------------------------------------------------- */
///
/// SettingsFolder.cs
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
using System.ComponentModel;
using System.Reflection;
using Cube.Log;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// SettingsFolder
    /// 
    /// <summary>
    /// ユーザ設定を保持するためのクラスです。
    /// </summary>
    /// 
    /// <remarks>
    /// このクラスでは、原則として各種設定をレジストリで保持する事を想定
    /// しています。
    /// </remarks>
    /// 
    /* --------------------------------------------------------------------- */
    public class SettingsFolder<TValue> where TValue : INotifyPropertyChanged
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// SettingsFolder
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SettingsFolder() : this(Assembly.GetEntryAssembly()) { }

        /* ----------------------------------------------------------------- */
        ///
        /// SettingsFolder
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SettingsFolder(Assembly assembly)
        {
            var reader = new AssemblyReader(assembly);

            Assembly = assembly;
            Company  = reader.Company;
            Product  = reader.Product;
            Version  = new SoftwareVersion(Assembly);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SettingsFolder
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SettingsFolder(string company, string product)
            : this(Assembly.GetEntryAssembly(), company, product) { }

        /* ----------------------------------------------------------------- */
        ///
        /// SettingsFolder
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SettingsFolder(Assembly assembly, string company, string product)
        {
            Assembly = assembly;
            Company  = company;
            Product  = product;
            Version  = new SoftwareVersion(Assembly);
        }

        #endregion

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
        public Assembly Assembly { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Version
        ///
        /// <summary>
        /// バージョン情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SoftwareVersion Version { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// User
        ///
        /// <summary>
        /// ユーザ毎の設定を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TValue User { get; private set; } = default(TValue);

        /* ----------------------------------------------------------------- */
        ///
        /// Startup
        ///
        /// <summary>
        /// スタートアップ設定を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Startup Startup { get; } = new Startup();

        /* ----------------------------------------------------------------- */
        ///
        /// AutoSave
        ///
        /// <summary>
        /// ユーザ毎の設定を自動的に保存するかどうかを示す値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool AutoSave { get; set; } = false;

        /* ----------------------------------------------------------------- */
        ///
        /// Company
        ///
        /// <summary>
        /// アプリケーションの発行元を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Company { get; } = string.Empty;

        /* ----------------------------------------------------------------- */
        ///
        /// Product
        ///
        /// <summary>
        /// アプリケーション名を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Product { get; } = string.Empty;

        /* ----------------------------------------------------------------- */
        ///
        /// SubkeyName
        ///
        /// <summary>
        /// レジストリ上で各種設定が保存されているサブキー名を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string SubKeyName => $@"Software\{Company}\{Product}";

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// ユーザ設定をレジストリへ保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Save() { OnSave(); }

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// アプリケーション設定、およびユーザ設定をレジストリから
        /// 読み込みます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Load() { OnLoad(); }

        #endregion

        #region Virtual methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnLoad
        ///
        /// <summary>
        /// アプリケーション設定、およびユーザ設定をレジストリから
        /// 読み込みます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnLoad()
        {
            if (User != null) User.PropertyChanged -= User_Changed;

            var root = Microsoft.Win32.Registry.CurrentUser;
            using (var subkey = root.OpenSubKey(SubKeyName, false))
            {
                var result = Settings.Load<TValue>(subkey);
                if (result == null) return;
                User = result;
                User.PropertyChanged += User_Changed;
            }
            Startup.Load();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnSave
        ///
        /// <summary>
        /// ユーザ設定をレジストリへ保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnSave()
        {
            var root = Microsoft.Win32.Registry.CurrentUser;
            using (var subkey = root.CreateSubKey(SubKeyName))
            {
                Settings.Save(User, subkey);
            }
            Startup.Save();
        }

        #endregion

        #region Event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// User_Changed
        ///
        /// <summary>
        /// ユーザ設定が変更された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void User_Changed(object sender, PropertyChangedEventArgs e)
            => this.LogException(() =>
        {
            if (AutoSave) Save();
        });

        #endregion
    }
}
