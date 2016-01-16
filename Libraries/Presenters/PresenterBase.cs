/* ------------------------------------------------------------------------- */
///
/// VersionForm.cs
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
using System.Threading;
using log4net;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.PresenterBase
    ///
    /// <summary>
    /// Model と View が 1 対 1 対応している Presenter の基底となる
    /// クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class PresenterBase<ViewType, ModelType>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// PresenterBase
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected PresenterBase(ViewType view, ModelType model)
        {
            SynchronizationContext = SynchronizationContext.Current;
            Logger = LogManager.GetLogger(GetType());

            View  = view;
            Model = model;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Model
        /// 
        /// <summary>
        /// Model オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ModelType Model { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// View
        /// 
        /// <summary>
        /// View オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ViewType View { get; private set; }

        /* --------------------------------------------------------------------- */
        ///
        /// SynchronizationContext
        /// 
        /// <summary>
        /// オブジェクト初期化時のコンテキストを取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public SynchronizationContext SynchronizationContext { get; }

        /* --------------------------------------------------------------------- */
        ///
        /// Logger
        /// 
        /// <summary>
        /// ログ出力用オブジェクトを取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected ILog Logger { get; private set; }

        #endregion

        #region Methods

        /* --------------------------------------------------------------------- */
        ///
        /// Post
        /// 
        /// <summary>
        /// オブジェクト初期化時のスレッド上で各種操作を実行します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public void Post(Action action)
        {
            SynchronizationContext.Post(_ => action(), null);
        }

        /* --------------------------------------------------------------------- */
        ///
        /// Send
        /// 
        /// <summary>
        /// オブジェクト初期化時のスレッド上で各種操作を実行し、
        /// 実行が完了するまで待機します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public void Send(Action action)
        {
            SynchronizationContext.Send(_ => action(), null);
        }

        #endregion
    }
}
