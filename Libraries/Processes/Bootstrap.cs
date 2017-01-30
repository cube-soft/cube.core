/* ------------------------------------------------------------------------- */
///
/// Bootstrap.cs
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

namespace Cube.Processes
{
    /* --------------------------------------------------------------------- */
    ///
    /// Bootstrap
    ///
    /// <summary>
    /// プロセス間通信 (IPC: Inter-Process Communication) によって
    /// プロセスの起動およびアクティブ化を行うためのクラスです。
    /// </summary>
    /// 
    /// <remarks>
    /// 二重起動を抑止したい時に、二重起動する代わりに既に起動している
    /// 同名プロセスをアクティブ化します。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class Bootstrap : Messenger
    {
        #region Constructors and the destructor

        /* ----------------------------------------------------------------- */
        ///
        /// Bootstrap
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Bootstrap(string name) : base(name, "activate") { }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Exists
        /// 
        /// <summary>
        /// 同じ名前を持つプロセスが既に存在するかどうかを判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Exists => !IsServer;

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// Activated
        /// 
        /// <summary>
        /// アクティブ化された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler Activated;

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Activate
        /// 
        /// <summary>
        /// 既に起動しているプロセスをアクティブ化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Activate() => Send(null);

        #endregion

        #region Virtual methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnActivated
        /// 
        /// <summary>
        /// Activated イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnActivated(EventArgs e)
            => Activated?.Invoke(this, e);

        #endregion

        #region Override methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnReceived
        /// 
        /// <summary>
        /// Received または Activated イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnReceived(ValueEventArgs<object> e)
        {
            if (e.Value == null) OnActivated(EventArgs.Empty);
            else base.OnReceived(e);
        }

        #endregion
    }
}
