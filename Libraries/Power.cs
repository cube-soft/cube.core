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
using Microsoft.Win32;

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
            SystemEvents.PowerModeChanged += WhenChanged;
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
        /// <remarks>
        /// 
        /// <para>
        /// Mode プロパティは通常、値の取得用途のみに使用されます。
        /// 値の設定は、動作検証やテスト等の時に使用して下さい。
        /// </para>
        /// 
        /// <para>
        /// このプロパティは通常 Resume または Suspend どちらかの値を
        /// 示します。そのため、PowerModeChanged イベントの
        /// Mode プロパティの値とは必ずしも一致しません。
        /// </para>
        /// 
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static PowerModes Mode
        {
            get { return _mode; }
            set
            {
                if (_mode == value) return;
                _mode = value;
                ModeChanged?.Invoke(null, new PowerModeChangedEventArgs(value));
            }
        }

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
        /// <remarks>
        /// SystemEvents.PowerModeChanged とは異なり、Mode プロパティに
        /// 明示的に設定しない限り、PowerModes.StatusChanged への変化時には
        /// イベントは発生しません。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static PowerModeChangedEventHandler ModeChanged;

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// WhenChanged
        /// 
        /// <summary>
        /// SystemEvents.PowerModeChanged イベント発生時に実行される
        /// ハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void WhenChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode != PowerModes.StatusChange) Mode = e.Mode;
        }

        #endregion

        #region Fields
        private static PowerModes _mode = PowerModes.Resume;
        #endregion
    }
}
