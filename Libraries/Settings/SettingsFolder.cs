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
using System;
using System.ComponentModel;
using System.Timers;
using System.Threading.Tasks;
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
        public SettingsFolder() : this(SettingsType.Registry) { }

        /* ----------------------------------------------------------------- */
        ///
        /// SettingsFolder
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        /// 
        /// <param name="type">設定情報の保存方法</param>
        ///
        /* ----------------------------------------------------------------- */
        public SettingsFolder(SettingsType type)
        {
            Initialize(AssemblyReader.Default, type);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SettingsFolder
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="type">設定情報の保存方法</param>
        /// <param name="path">設定情報の保存場所</param>
        /// 
        /* ----------------------------------------------------------------- */
        public SettingsFolder(SettingsType type, string path)
        {
            Initialize(AssemblyReader.Default, type, path);
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
        /// Version
        ///
        /// <summary>
        /// バージョン情報を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SoftwareVersion Version { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Company
        ///
        /// <summary>
        /// アプリケーションの発行者を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Company { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Product
        ///
        /// <summary>
        /// アプリケーション名を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Product { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Type
        ///
        /// <summary>
        /// 設定情報の保存方法を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SettingsType Type { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Path
        ///
        /// <summary>
        /// 設定情報の保存場所を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Path { get; set; }

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
        public event KeyValueEventHandler<SettingsType, string> Saved;

        /* ----------------------------------------------------------------- */
        ///
        /// OnSaved
        ///
        /// <summary>
        /// Saved イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnSaved(KeyValueEventArgs<SettingsType, string> e)
        {
            e.Key.Save(e.Value, Value);
            Startup.Save();
            Saved?.Invoke(this, e);
        }

        #endregion

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// ユーザ設定を読み込みます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Load()
            => OnLoaded(ValueChangedEventArgs.Create(Value, Type.Load<TValue>(Path)));

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// ユーザ設定をレジストリへ保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Save()
            => OnSaved(KeyValueEventArgs.Create(Type, Path));

        #region IDisposable

        /* ----------------------------------------------------------------- */
        ///
        /// ~SettingsFolder
        ///
        /// <summary>
        /// オブジェクトを破棄します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        ~SettingsFolder() { _dispose.Invoke(false); }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        /// 
        /// <summary>
        /// リソースを破棄します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Dispose()
        {
            _dispose.Invoke(true);
            GC.SuppressFinalize(this);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        /// 
        /// <summary>
        /// リソースを破棄します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void Dispose(bool disposing)
        {
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
        private void Initialize(AssemblyReader reader, SettingsType type, string path)
        {
            Type     = type;
            Path     = path;
            Version  = new SoftwareVersion(reader.Assembly);
            Company  = reader.Company;
            Product  = reader.Product;
            Value    = new TValue();

            Value.PropertyChanged += WhenChanged;

            _autosaver.AutoReset = false;
            _autosaver.Elapsed += WhenElapsed;
            _dispose = new OnceAction<bool>(Dispose);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Initialize
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Initialize(AssemblyReader reader, SettingsType type)
        {
            var root = type == SettingsType.Registry ?
                       "Software" :
                       Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var path = System.IO.Path.Combine(root, $@"{reader.Company}\{reader.Product}");

            Initialize(reader, type, path);
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

            OnPropertyChanged(e);
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
            => Task.Run(() => Save()).Forget();

        #region Fields
        private OnceAction<bool> _dispose;
        private Timer _autosaver = new Timer();
        #endregion

        #endregion
    }
}
