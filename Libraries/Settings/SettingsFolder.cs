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
using Microsoft.Win32;
using Cube.Tasks;

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
    public class SettingsFolder<TValue> : IDisposable, INotifyPropertyChanged
        where TValue : INotifyPropertyChanged, new()
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
        /// Value
        ///
        /// <summary>
        /// 設定内容を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TValue Value { get; private set; }

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
        /// ユーザ毎の設定を自動的に保存するかどうかを示す値を取得または
        /// 設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool AutoSave { get; set; } = false;

        /* ----------------------------------------------------------------- */
        ///
        /// AutoSaveDelay
        ///
        /// <summary>
        /// 自動的保存の実行遅延時間を取得または設定します。
        /// </summary>
        /// 
        /// <remarks>
        /// AutoSave モードの場合、短時間に大量の保存処理が実行される
        /// 可能性があります。SettingsFolder では、直前のプロパティの
        /// 変更から一定時間保存を保留する事で、これらの問題を回避します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public TimeSpan AutoSaveDelay { get; set; } = TimeSpan.FromSeconds(1);

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

        #region PropertyChanged

        /* ----------------------------------------------------------------- */
        ///
        /// PropertyChanged
        ///
        /// <summary>
        /// プロパティの内容が変更された時に発生するイベントです。
        /// </summary>
        /// 
        /// <remarks>
        /// この PropertyChanged イベントは Value.PropertyChanged
        /// イベントを補足して中継するために使用されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public event PropertyChangedEventHandler PropertyChanged;

        /* ----------------------------------------------------------------- */
        ///
        /// OnPropertyChanged
        ///
        /// <summary>
        /// PropertyChangd イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
            => PropertyChanged?.Invoke(this, e);

        #endregion

        #region Loaded

        /* ----------------------------------------------------------------- */
        ///
        /// Loaded
        ///
        /// <summary>
        /// 読み込み時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event ValueChangedEventHandler<TValue> Loaded;

        /* ----------------------------------------------------------------- */
        ///
        /// OnLoaded
        ///
        /// <summary>
        /// Loaded イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnLoaded(ValueChangedEventArgs<TValue> e)
        {
            if (e.OldValue != null) e.OldValue.PropertyChanged -= WhenChanged;
            if (e.NewValue != null) e.NewValue.PropertyChanged += WhenChanged;

            Value = e.NewValue;
            Startup.Load();

            Loaded?.Invoke(this, e);
        }

        #endregion

        #region Saved

        /* ----------------------------------------------------------------- */
        ///
        /// Saved
        ///
        /// <summary>
        /// 自動保存機能が実行された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler Saved;

        /* ----------------------------------------------------------------- */
        ///
        /// OnSaved
        ///
        /// <summary>
        /// Saved イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnSaved(EventArgs e)
        {
            using (var key = Registry.CurrentUser.CreateSubKey(SubKeyName))
            {
                key.Save(Value);
            }

            Startup.Save();
            Saved?.Invoke(this, e);
        }

        #endregion

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
        public void Save() => OnSaved(EventArgs.Empty);

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
        public void Load()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(SubKeyName, false))
            {
                var dest = key.Load<TValue>();
                if (dest == null) return;
                OnLoaded(new ValueChangedEventArgs<TValue>(Value, dest));
            }
        }

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

        #region Implementations

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
            Company  = company;
            Product  = product;
            Version  = new SoftwareVersion(Assembly);
            Value    = new TValue();

            Value.PropertyChanged += WhenChanged;

            _autosaver.AutoReset = false;
            _autosaver.Elapsed  += WhenElapsed;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenChanged
        ///
        /// <summary>
        /// Value.PropertyChanged イベントが発生した時に実行される
        /// ハンドラです。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void WhenChanged(object sener, PropertyChangedEventArgs e)
        {
            _autosaver.Stop();
            if (AutoSave && AutoSaveDelay > TimeSpan.Zero)
            {
                _autosaver.Interval = AutoSaveDelay.TotalMilliseconds;
                _autosaver.Start();
            }

            PropertyChanged?.Invoke(this, e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenElapsed
        ///
        /// <summary>
        /// Start() 実行後、一度だけ実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenElapsed(object sender, ElapsedEventArgs e)
            => TaskEx.Run(() => Save()).Forget();

        #region Fields
        private bool _disposed = false;
        private Timer _autosaver = new Timer();
        #endregion

        #endregion
    }
}
