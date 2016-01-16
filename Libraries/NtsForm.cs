/* ------------------------------------------------------------------------- */
///
/// NtsForm.cs
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
using System.Drawing;
using log4net;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.NtsForm
    /// 
    /// <summary>
    /// 自動スケールモードを無効にした Form クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class NtsForm : System.Windows.Forms.Form
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// NtsForm
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public NtsForm()
            : base()
        {
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            DoubleBuffered = true;
            Font = FontFactory.Create(12, Font.Style, GraphicsUnit.Pixel);
            Logger = LogManager.GetLogger(GetType());
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Bootstrap
        /// 
        /// <summary>
        /// プロセス間通信を介した起動およびアクティブ化を制御するための
        /// オブジェクトを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Bootstrap Bootstrap
        {
            get { return _bootstrap; }
            set
            {
                if (_bootstrap != null) _bootstrap.Activated -= Bootstrap_Activated;

                _bootstrap = value;
                _bootstrap.Activated -= Bootstrap_Activated;
                _bootstrap.Activated += Bootstrap_Activated;
                _bootstrap.Register();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Logger
        ///
        /// <summary>
        /// ログ出力用オブジェクトを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected ILog Logger { get; private set; }

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// Received
        ///
        /// <summary>
        /// 他のプロセスからデータを受信した時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public EventHandler<DataEventArgs<object>> Received;

        #endregion

        #region Virtual methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnReceived
        ///
        /// <summary>
        /// 他のプロセスからデータを受信した時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnReceived(DataEventArgs<object> e)
        {
            if (Received != null) Received(this, e);
        }

        #endregion

        #region Event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// Bootstrap
        /// 
        /// <summary>
        /// 他プロセスからアクティブ化時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Bootstrap_Activated(object sender, DataEventArgs<object> e)
        {
            if (InvokeRequired) Invoke(new Action(() => Bootstrap_Activated(sender, e)));
            else
            {
                if (e.Value != null) OnReceived(e);

                Show();
                var tmp = TopMost;
                TopMost = true;
                TopMost = tmp;
            }
        }

        #endregion

        #region Fields
        private Bootstrap _bootstrap = null;
        #endregion
    }
}
