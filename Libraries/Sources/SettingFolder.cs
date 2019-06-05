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
using Cube.Mixin.Assembly;
using Cube.Mixin.Environment;
using Cube.Mixin.IO;
using Cube.Mixin.Logging;
using Cube.Mixin.Tasks;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;

namespace Cube.FileSystem
{
    /* --------------------------------------------------------------------- */
    ///
    /// SettingFolder(T)
    ///
    /// <summary>
    /// Provides functionality to load and save user settings.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class SettingFolder<T> : ObservableBase where T : INotifyPropertyChanged, new()
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// SettingFolder(T)
        ///
        /// <summary>
        /// Initializes a new instance of the SettingsFolder class with
        /// the specified arguments.
        /// </summary>
        ///
        /// <param name="assembly">Assembly information.</param>
        /// <param name="format">Serialization format.</param>
        ///
        /* ----------------------------------------------------------------- */
        public SettingFolder(Assembly assembly, Format format) :
            this(assembly, format, new IO()) { }

        /* ----------------------------------------------------------------- */
        ///
        /// SettingFolder(T)
        ///
        /// <summary>
        /// Initializes a new instance of the SettingsFolder class with
        /// the specified arguments.
        /// </summary>
        ///
        /// <param name="assembly">Assembly information.</param>
        /// <param name="format">Serialization format.</param>
        /// <param name="io">I/O handler.</param>
        ///
        /* ----------------------------------------------------------------- */
        public SettingFolder(Assembly assembly, Format format, IO io) :
            this(assembly, format, GetLocation(assembly, format, io), io) { }

        /* ----------------------------------------------------------------- */
        ///
        /// SettingFolder(T)
        ///
        /// <summary>
        /// Initializes a new instance of the SettingsFolder class with
        /// the specified arguments.
        /// </summary>
        ///
        /// <param name="assembly">Assembly information.</param>
        /// <param name="format">Serialization format.</param>
        /// <param name="location">Saved data location.</param>
        ///
        /* ----------------------------------------------------------------- */
        public SettingFolder(Assembly assembly, Format format, string location) :
            this(assembly, format, location, new IO()) { }

        /* ----------------------------------------------------------------- */
        ///
        /// SettingFolder(T)
        ///
        /// <summary>
        /// Initializes a new instance of the SettingsFolder class with
        /// the specified arguments.
        /// </summary>
        ///
        /// <param name="assembly">Assembly information.</param>
        /// <param name="format">Serialization format.</param>
        /// <param name="location">Saved data location.</param>
        /// <param name="io">I/O handler.</param>
        ///
        /* ----------------------------------------------------------------- */
        public SettingFolder(Assembly assembly, Format format, string location, IO io)
        {
            _autosaver.AutoReset = false;
            _autosaver.Elapsed += (s, e) => Task.Run(() => Save()).Forget();

            Assembly = assembly;
            IO       = io;
            Format   = format;
            Location = location;
            Version  = new SoftwareVersion(assembly);
            Value    = new T();

            Value.PropertyChanged += WhenChanged;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Value
        ///
        /// <summary>
        /// Gets a value that represents user settings.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public T Value { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Assembly
        ///
        /// <summary>
        /// Gets the assembly information.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Assembly Assembly { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Version
        ///
        /// <summary>
        /// Gets the software version.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SoftwareVersion Version { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Format
        ///
        /// <summary>
        /// Gets or sets the serialization format.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Format Format { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Location
        ///
        /// <summary>
        /// Gets or sets the location that the serialized data is saved in.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Location { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// IO
        ///
        /// <summary>
        /// Gets the I/O handler.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IO IO { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// AutoSave
        ///
        /// <summary>
        /// Gets or sets the value indicating whether saving automatically
        /// when user settings are changed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool AutoSave { get; set; } = false;

        /* ----------------------------------------------------------------- */
        ///
        /// AutoSaveDelay
        ///
        /// <summary>
        /// Gets or sets the delay between detecting changed in user
        /// settings and saving them.
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

        #region Loaded

        /* ----------------------------------------------------------------- */
        ///
        /// Loaded
        ///
        /// <summary>
        /// Occurs when user settings is loaded from the provided location.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event ValueChangedEventHandler<T> Loaded;

        /* ----------------------------------------------------------------- */
        ///
        /// OnLoaded
        ///
        /// <summary>
        /// Raises the Loaded event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnLoaded(ValueChangedEventArgs<T> e)
        {
            if (e.OldValue != null) e.OldValue.PropertyChanged -= WhenChanged;
            if (e.NewValue != null) e.NewValue.PropertyChanged += WhenChanged;
            Value = e.NewValue;
            if (Loaded != null) Dispatcher.Invoke(() => Loaded(this, e));
        }

        #endregion

        #region Saved

        /* ----------------------------------------------------------------- */
        ///
        /// Saved
        ///
        /// <summary>
        /// Occurs when user settings is saved to the provided location.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event KeyValueEventHandler<Format, string> Saved;

        /* ----------------------------------------------------------------- */
        ///
        /// OnSaved
        ///
        /// <summary>
        /// Raises the Saved event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnSaved(KeyValueEventArgs<Format, string> e)
        {
            if (e.Key == Format.Registry) e.Key.Serialize(e.Value, Value);
            else IO.Save(e.Value, ss => e.Key.Serialize(ss, Value));
            if (Saved != null) Dispatcher.Invoke(() => Saved(this, e));
        }

        #endregion

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// Loads user settings.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Load() => OnLoaded(ValueEventArgs.Create(Value, LoadCore()));

        /* ----------------------------------------------------------------- */
        ///
        /// TryLoad
        ///
        /// <summary>
        /// Tries to load user settings.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool TryLoad()
        {
            try { Load(); return true; }
            catch (Exception err) { this.LogWarn(err); }
            return false;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// Saves user settings.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Save() => OnSaved(KeyValueEventArgs.Create(Format, Location));

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases the unmanaged resources used by the SettingsFolder
        /// and optionally releases the managed resources.
        /// </summary>
        ///
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources;
        /// false to release only unmanaged resources.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Dispose(bool disposing)
        {
            if (disposing) _autosaver.Dispose();
            if (AutoSave) this.LogWarn(() => Save());
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetLocation
        ///
        /// <summary>
        /// Gets the location of serialization data.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static string GetLocation(Assembly asm, Format fmt, IO io)
        {
            var root = fmt != Format.Registry ?
                       Environment.SpecialFolder.LocalApplicationData.GetName() :
                       string.Empty;
            return io.Combine(root, asm.GetCompany(), asm.GetProduct());
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LoadCore
        ///
        /// <summary>
        /// Loads user settings.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private T LoadCore() =>
            Format == Format.Registry ?
            Format.Deserialize<T>(Location) :
            IO.Load(Location, e => Format.Deserialize<T>(e));

        /* ----------------------------------------------------------------- */
        ///
        /// WhenChanged
        ///
        /// <summary>
        /// Occurs when the PropertyChanged event of the Value property
        /// is fired.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenChanged(object sener, PropertyChangedEventArgs e)
        {
            try
            {
                _autosaver.Stop();

                if (!AutoSave) return;
                if (AutoSaveDelay > TimeSpan.Zero)
                {
                    _autosaver.Interval = AutoSaveDelay.TotalMilliseconds;
                    _autosaver.Start();
                }
                else this.LogWarn(() => Save());
            }
            finally { OnPropertyChanged(e); }
        }

        #endregion

        #region Fields
        private readonly Timer _autosaver = new Timer();
        #endregion
    }
}
