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
using System.Reflection;

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
    public class SettingsFolder<TValue>
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
        public SettingsFolder(Assembly assembly)
        {
            var reader = new AssemblyReader(assembly);
            Company = reader.Company;
            Product = reader.Product;
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
        {
            Company = company;
            Product = product;
        }

        #endregion

        #region Properties

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
        /// InstallDirectory
        ///
        /// <summary>
        /// アプリケーションがインストールされているディレクトリのパスを
        /// 取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string InstallDirectory
        {
            get { return GetInstallDirectory(); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Version
        ///
        /// <summary>
        /// バージョンを表す文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Version
        {
            get { return GetVersion(); }
        }

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
        public string SubKeyName
        {
            get { return string.Format(@"Software\{0}\{1}", Company, Product); }
        }

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
        public void Save() { ExecuteSave(); }

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
        public void Load() { ExecuteLoad(); }

        #endregion

        #region Virtual methods

        /* ----------------------------------------------------------------- */
        ///
        /// ExecuteLoad
        ///
        /// <summary>
        /// アプリケーション設定、およびユーザ設定をレジストリから
        /// 読み込みます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void ExecuteLoad()
        {
            LoadApplicationSettings();
            LoadUserSettings();
            Startup.Load();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ExecuteSave
        ///
        /// <summary>
        /// ユーザ設定をレジストリへ保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void ExecuteSave()
        {
            var root = Microsoft.Win32.Registry.CurrentUser;
            using (var subkey = root.CreateSubKey(SubKeyName))
            {
                Settings.Save<TValue>(User, subkey);
            }
            Startup.Save();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetInstallDirectory
        ///
        /// <summary>
        /// アプリケーションがインストールされているディレクトリのパスを
        /// 取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual string GetInstallDirectory()
        {
            return (_app != null) ? _app.InstallDirectory : string.Empty;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetVersion
        ///
        /// <summary>
        /// バージョンを表す文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual string GetVersion()
        {
            return (_app != null) ? _app.Version : string.Empty;
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// LoadApplicationSettings
        ///
        /// <summary>
        /// アプリケーション設定をレジストリから読み込みます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void LoadApplicationSettings()
        {
            var root = Microsoft.Win32.Registry.LocalMachine;
            using (var subkey = root.OpenSubKey(SubKeyName, false))
            {
                var result = Settings.Load<ApplicationSettingsValue>(subkey);
                if (result != null) _app = result;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LoadUserSettings
        ///
        /// <summary>
        /// ユーザ設定をレジストリから読み込みます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void LoadUserSettings()
        {
            var root = Microsoft.Win32.Registry.CurrentUser;
            using (var subkey = root.OpenSubKey(SubKeyName, false))
            {
                var result = Settings.Load<TValue>(subkey);
                if (result != null) User = result;
            }
        }

        #endregion

        #region Fields
        private ApplicationSettingsValue _app = null;
        #endregion
    }
}
