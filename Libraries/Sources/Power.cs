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

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// Power
    ///
    /// <summary>
    /// 電源状況を検証するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Power
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Power
        ///
        /// <summary>
        /// 静的オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        static Power()
        {
            _context = new PowerModeContext(PowerModes.Resume);
            _context.PropertyChanged += WhenChanged;

            Microsoft.Win32.SystemEvents.PowerModeChanged +=
                (s, e) => _context.Mode = (PowerModes)((int)e.Mode);
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Mode
        ///
        /// <summary>
        /// 電源状態を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static PowerModes Mode => _context.Mode;

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// ModeChanged
        ///
        /// <summary>
        /// 電源状態が変化した時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static event EventHandler ModeChanged;

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Configure
        ///
        /// <summary>
        /// PowerModeContext オブジェクトを差し換えます。
        /// </summary>
        ///
        /// <remarks>
        /// プログラム上で独自に Power.Mode の状態を更新する必要がある
        /// 場合などに利用します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static void Configure(PowerModeContext context)
        {
            System.Diagnostics.Debug.Assert(context != null);
            lock (_lock)
            {
                _context.PropertyChanged -= WhenChanged;
                _context = context;
                _context.PropertyChanged -= WhenChanged;
                _context.PropertyChanged += WhenChanged;
            }
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// WhenChanged
        ///
        /// <summary>
        /// PowerModeContext.PropertyChanged イベント発生時に実行される
        /// ハンドラです。必要に応じて ModeChanged イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void WhenChanged(object s, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_context.Mode))
            {
                ModeChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        #region Fields
        private static PowerModeContext _context;
        private static readonly object _lock = new object();
        #endregion

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// PowerModeContext
    ///
    /// <summary>
    /// 電源の状態を保持するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class PowerModeContext : ObservableProperty
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// PowerModeContext
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="mode">電源状態</param>
        ///
        /* ----------------------------------------------------------------- */
        public PowerModeContext(PowerModes mode)
        {
            Mode = mode;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Mode
        ///
        /// <summary>
        /// 電源状態を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public PowerModes Mode
        {
            get => _mode;
            set
            {
                if (IgnoreStatusChanged && value == PowerModes.StatusChange) return;
                SetProperty(ref _mode, value);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IgnoreStatusChanged
        ///
        /// <summary>
        /// PowerModes.StatusChanged を無視するかどうかを示す値を取得
        /// または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IgnoreStatusChanged
        {
            get => _ignore;
            set => SetProperty(ref _ignore, value);
        }

        #endregion

        #region Fields
        private PowerModes _mode;
        private bool _ignore = true;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// PowerModes
    ///
    /// <summary>
    /// 電源状態を表す列挙型です。
    /// </summary>
    ///
    /// <remarks>
    /// .NET Standard の場合とソースコードを共有するために
    /// Microsoft.Win32.PowerModes と同様のものを定義します。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public enum PowerModes
    {
        /// <summary>復帰状態</summary>
        Resume = Microsoft.Win32.PowerModes.Resume,
        /// <summary>電源状態が変化した事を示す</summary>
        StatusChange = Microsoft.Win32.PowerModes.StatusChange,
        /// <summary>一時停止中</summary>
        Suspend = Microsoft.Win32.PowerModes.Suspend,
    }
}
