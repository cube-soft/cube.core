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
            SystemEvents.PowerModeChanged += RaiseModeChanged;
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
        /// このプロパティは Resume または Suspend どちらかの値を示します。
        /// そのため、PowerModeChanged イベントの Mode プロパティの値とは
        /// 必ずしも一致しません。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static PowerModes Mode { get; private set; } = PowerModes.Resume;

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
        public static PowerModeChangedEventHandler ModeChanged;

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Raise
        /// 
        /// <summary>
        /// ModeChanged イベントを手動で発生させます。
        /// </summary>
        /// 
        /// <param name="mode">電源状態</param>
        /// 
        /// <remarks>
        /// 主に動作検証やテスト等で使用されるメソッドです。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static void Raise(PowerModes mode)
            => RaiseModeChanged(null, new PowerModeChangedEventArgs(mode));

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// RaiseModeChanged
        /// 
        /// <summary>
        /// ModeChanged イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void RaiseModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode != PowerModes.StatusChange) Mode = e.Mode;
            ModeChanged?.Invoke(sender, e);
        }

        #endregion
    }
}
