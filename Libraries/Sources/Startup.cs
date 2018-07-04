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
using Cube.Generics;
using Microsoft.Win32;

namespace Cube.FileSystem
{
    /* --------------------------------------------------------------------- */
    ///
    /// Startup
    ///
    /// <summary>
    /// スタートアップ設定を行うためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Startup : ObservableProperty
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Startup
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Startup() : this(string.Empty, string.Empty, false) { }

        /* ----------------------------------------------------------------- */
        ///
        /// Startup
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Startup(string name) : this(name, string.Empty, false) { }

        /* ----------------------------------------------------------------- */
        ///
        /// Startup
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Startup(string name, string command) : this(name, command, true) { }

        /* ----------------------------------------------------------------- */
        ///
        /// Startup
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Startup(string name, string command, bool enabled)
        {
            Name    = name;
            Command = command;
            Enabled = enabled;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// RegistryKeyName
        ///
        /// <summary>
        /// スタートアップ設定を保持するレジストリのルートに当たる
        /// サブキー名を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string RegistryKeyName =>
            @"Software\Microsoft\Windows\CurrentVersion\Run";

        /* ----------------------------------------------------------------- */
        ///
        /// Enabled
        ///
        /// <summary>
        /// スタートアップ設定が有効かどうかを示す値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Enabled
        {
            get => _enabled;
            set => SetProperty(ref _enabled, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Name
        ///
        /// <summary>
        /// スタートアップに登録する名前を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Command
        ///
        /// <summary>
        /// スタートアップ時に実行されるコマンドを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Command
        {
            get => _command;
            set => SetProperty(ref _command, value);
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// レジストリから設定をロードします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Load()
        {
            if (!Name.HasValue()) return;

            using (var subkey = Open(false))
            {
                Command = subkey.GetValue(Name, string.Empty) as string;
                Enabled = Command.HasValue();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// レジストリへ設定を保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Save()
        {
            if (!Name.HasValue()) return;

            using (var subkey = Open(true))
            {
                if (Enabled)
                {
                    if (Command.HasValue()) subkey.SetValue(Name, Command);
                }
                else subkey.DeleteValue(Name, false);
            }
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Open
        ///
        /// <summary>
        /// ルートキーを開きます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private RegistryKey Open(bool writable) =>
            Registry.CurrentUser.OpenSubKey(RegistryKeyName, writable);

        #endregion

        #region Fields
        private bool _enabled = false;
        private string _name = string.Empty;
        private string _command = string.Empty;
        #endregion
    }
}
