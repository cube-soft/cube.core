/* ------------------------------------------------------------------------- */
///
/// DeviceAwareForm.cs
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
using System.Windows.Forms;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.DeviceAwareForm
    /// 
    /// <summary>
    /// デバイスの追加等に反応するフォームクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class DeviceAwareForm : NtsForm
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// DeviceAwareForm
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DeviceAwareForm() : base() { }

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// DeviceConnected
        ///
        /// <summary>
        /// 新しいデバイスが接続された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler DeviceConnected;

        /* ----------------------------------------------------------------- */
        ///
        /// DeviceRemoved
        ///
        /// <summary>
        /// デバイスが取り外された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler DeviceRemoved;

        #endregion

        #region Virtual methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnDeviceConnected
        ///
        /// <summary>
        /// 新しいデバイスが接続された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnDeviceConnected(EventArgs e)
        {
            if (DeviceConnected != null) DeviceConnected(this, e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnDeviceRemoved
        ///
        /// <summary>
        /// デバイスが取り外された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnDeviceRemoved(EventArgs e)
        {
            if (DeviceRemoved != null) DeviceRemoved(this, e);
        }

        #endregion

        #region Override methods

        /* ----------------------------------------------------------------- */
        ///
        /// WndProc
        ///
        /// <summary>
        /// ウィンドウメッセージを処理するためのハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0219: // WM_DEVICECHANGE
                    var param = m.WParam.ToInt32();
                    if (param == 0x8000 /* DBT_DEVICEARRIVAL */) OnDeviceConnected(new EventArgs());
                    else if (param == 0x8004 /* DBT_DEVICEREMOVECOMPLETE */) OnDeviceRemoved(new EventArgs());
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);
        }

        #endregion
    }
}
