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
using System;
using System.ComponentModel;
using System.Timers;
using System.Threading.Tasks;
using System.Reflection;
using Cube.Log;

namespace Cube.Settings
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
    /// このクラスでは、各種設定をレジストリで保持する事を想定しています。
    /// </remarks>
    /// 
    /* --------------------------------------------------------------------- */
    public class SettingsFolder<TValue> : IDisposable
        where TValue : INotifyPropertyChanged
    {
        #region Constructors and destructors

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
        /// <param name="assembly">
        /// 設定対象となる <c>Assembly</c> オブジェクト
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public SettingsFolder(Assembly assembly)
        {
            var reader = new AssemblyReader(assembly);
            Initialize(assembly, reader.Company, reader.Product);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SettingsFolder
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        /// 
        /// <param name="company">会社名</param>
        /// <param name="product">製品名</param>
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
        /// <param name="assembly">
        /// 設定対象となる <c>Assembly</c> オブジェクト
        /// </param>
        /// 
        /// <param name="company">会社名</param>
        /// <param name="product">製品名</param>
        /// 
        /// <remarks>
        /// SettingsFolder は HKCU\Software\(company)\(product) の
        /// レジストリ・サブキー下から各種設定を読み込みます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public SettingsFolder(Assembly assembly, string company, string product)
        {
            Initialize(assembly, company, product);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ~SettingsFolder
        ///
        /// <summary>
        /// オブジェクトを破棄します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        ~SettingsFolder()
        {
            Dispose(false);
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
        public Assembly Assembly { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Version
        ///
        /// <summary>
        /// バージョン情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SoftwareVersion Version { get; private set; }

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
        public string Company { get; private set; } = string.Empty;

        /* ----------------------------------------------------------------- */
        ///
        /// Product
        ///
        /// <summary>
        /// アプリケーション名を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Product { get; private set; } = string.Empty;

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

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// AutoSaved
        ///
        /// <summary>
        /// 自動保存機能が実行された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler AutoSaved;

        /* ----------------------------------------------------------------- */
        ///
        /// OnAutoSaved
        ///
        /// <summary>
        /// AutoSaved イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnAutoSaved(EventArgs e)
            => AutoSaved?.Invoke(this, e);

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
        public void Save() => OnSave();

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
        public void Load() => OnLoad();

        #region IDisposable

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        /// 
        /// <summary>
        /// オブジェクトを破棄します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        /// 
        /// <summary>
        /// オブジェクトを破棄します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            _disposed = true;

            if (disposing) _autosaver.Dispose();
        }

        #endregion

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
            if (User != null) User.PropertyChanged -= AutoSaver_Restart;

            var root = Microsoft.Win32.Registry.CurrentUser;

            using (var subkey = root.OpenSubKey(SubKeyName, false))
            {
                var result = subkey.Load<TValue>();
                if (result == null) return;

                User = result;
                User.PropertyChanged += AutoSaver_Restart;
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
            using (var subkey = root.CreateSubKey(SubKeyName)) subkey.Save(User);
            Startup.Save();
        }

        #endregion

        #region Implementations

        #region Initialize methods

        /* ----------------------------------------------------------------- */
        ///
        /// Initialize
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Initialize(Assembly assembly, string company, string product)
        {
            Assembly = assembly;
            Company = company;
            Product = product;
            Version = new SoftwareVersion(Assembly);

            _autosaver.AutoReset = false;
            _autosaver.Interval = 1000;
            _autosaver.Elapsed += AutoSaver_Elapsed;
        }

        #endregion

        #region Event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// AutoSaver_Restart
        ///
        /// <summary>
        /// ユーザ設定が変更された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void AutoSaver_Restart(object sender, PropertyChangedEventArgs e)
        {
            _autosaver.Stop();
            if (AutoSave) _autosaver.Start();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AutoSaver_Elapsed
        ///
        /// <summary>
        /// Start() 実行後、一度だけ実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private async void AutoSaver_Elapsed(object sender, ElapsedEventArgs e)
            => await Task.Run(()
            => this.LogException(() =>
        {
            Save();
            OnAutoSaved(EventArgs.Empty);
        }));

        #endregion

        #region Fields
        private bool _disposed = false;
        private Timer _autosaver = new Timer();
        #endregion

        #endregion
    }
}
