/* ------------------------------------------------------------------------- */
///
/// DemoNotify.cs
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

namespace Cube.Forms.Demo
{
    /* --------------------------------------------------------------------- */
    ///
    /// DemoDeviceAware
    /// 
    /// <summary>
    /// DeviceAwareForm のデモ用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class DemoDeviceAware : DeviceAwareForm
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// DemoDeviceAware
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DemoDeviceAware()
        {
            InitializeComponent();
        }

        #endregion

        #region Override methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnAttached
        ///
        /// <summary>
        /// デバイスが追加された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnAttached(DeviceEventArgs e)
        {
            Log($"{e.Letter}: ({e.Type}) is attached.");
            base.OnAttached(e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnDetached
        ///
        /// <summary>
        /// デバイスが取り外された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnDetached(DeviceEventArgs e)
        {
            Log($"{e.Letter}: ({e.Type}) is detached.");
            base.OnDetached(e);
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Log
        ///
        /// <summary>
        /// ログを出力します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Log(string message)
        {
            var builder = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(LogTextBox.Text)) builder.AppendLine(LogTextBox.Text);
            builder.Append($"{DateTime.Now} {message}");
            LogTextBox.Text = builder.ToString();
        }

        #endregion
    }
}
