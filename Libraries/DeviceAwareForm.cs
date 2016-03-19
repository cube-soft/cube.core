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
using System.Runtime.InteropServices;
using Cube.Log;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// DeviceAwareForm
    /// 
    /// <summary>
    /// デバイスの追加等に反応するフォームクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class DeviceAwareForm : Form
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
        /// Attached
        ///
        /// <summary>
        /// ドライブまたはメディアが接続された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler<DeviceEventArgs> Attached;

        /* ----------------------------------------------------------------- */
        ///
        /// Detached
        ///
        /// <summary>
        /// ドライブまたはメディアが取り外された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler<DeviceEventArgs> Detached;

        #endregion

        #region Virtual methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnAttached
        ///
        /// <summary>
        /// ドライブまたはメディアが接続された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnAttached(DeviceEventArgs e)
        {
            if (Attached != null) Attached(this, e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnDetached
        ///
        /// <summary>
        /// ドライブまたはメディアが取り外された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnDetached(DeviceEventArgs e)
        {
            if (Detached != null) Detached(this, e);
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
                    RaiseDeviceChangeEvent(m);
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// RaiseDeviceChangeEvent
        ///
        /// <summary>
        /// デバイスの着脱を通知するためのイベントを発生させます。
        /// </summary>
        /// 
        /// <remarks>
        /// TODO: デバイスの着脱通知は、DBT_DEVTYP_VOLUME 以外にも
        /// DBT_DEVTYP_OEM (0x0000), DBT_DEVTYP_DEVNODE (0x0001),
        /// DBT_DEVTYP_PORT (0x0003), DBT_DEVTYP_NET (0x0004),
        /// DBT_DEVTYP_DEVICEINTERFACE (0x0005), DBT_DEVTYP_HANDLE (0x0006)
        /// の全 6 種類が存在する模様。これらの処理について検討する。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private void RaiseDeviceChangeEvent(Message m)
            => this.LogException(() =>
        {
            const int DBT_DEVICEARRIVAL = 0x8000;
            const int DBT_DEVICEREMOVECOMPLETE = 0x8004;

            var action = m.WParam.ToInt32();
            if (action != DBT_DEVICEARRIVAL && action != DBT_DEVICEREMOVECOMPLETE) return;

            var checker = (DEV_BROADCAST_HDR)Marshal.PtrToStructure(m.LParam, typeof(DEV_BROADCAST_HDR));
            if (checker.dbcv_devicetype != 0x0002 /* DBT_DEVTYP_VOLUME */) return;

            var device = (DEV_BROADCAST_VOLUME)Marshal.PtrToStructure(m.LParam, typeof(DEV_BROADCAST_VOLUME));
            var letter = ToDriveLetter(device.dbcv_unitmask);
            var type = (DeviceType)device.dbcv_flags;
            var args = new DeviceEventArgs(letter, type);

            if (action == DBT_DEVICEARRIVAL) OnAttached(args);
            else if (action == DBT_DEVICEREMOVECOMPLETE) OnDetached(args);
        });

        /* ----------------------------------------------------------------- */
        ///
        /// ToDriveLetter
        ///
        /// <summary>
        /// ビットマスクをドライブレターに変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private char ToDriveLetter(uint unitmask)
        {
            for (char offset = (char)0; offset < 26; ++offset)
            {
                if ((unitmask & 0x1) != 0) return (char)('A' + offset);
                unitmask = unitmask >> 1;
            }
            return 'A';
        }

        #endregion

        #region Structures

        /* ----------------------------------------------------------------- */
        ///
        /// DEV_BROADCAST_HDR
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/aa363246.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [StructLayout(LayoutKind.Sequential)]
        internal struct DEV_BROADCAST_HDR
        {
            public uint dbcv_size;
            public uint dbcv_devicetype;
            public uint dbcv_reserved;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DEV_BROADCAST_VOLUME
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/aa363249.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [StructLayout(LayoutKind.Sequential)]
        internal struct DEV_BROADCAST_VOLUME
        {
            public uint dbcv_size;
            public uint dbcv_devicetype;
            public uint dbcv_reserved;
            public uint dbcv_unitmask;
            public char dbcv_flags;
        }

        #endregion
    }
}
