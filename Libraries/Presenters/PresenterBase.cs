/* ------------------------------------------------------------------------- */
///
/// PresenterBase.cs
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
using System.Threading.Tasks;
using Cube.Log;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// PresenterBase
    ///
    /// <summary>
    /// View のみを保持する Presenter の基底となるクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class PresenterBase<TView> : IDisposable
    {
        #region Constructors and destructors

        /* ----------------------------------------------------------------- */
        ///
        /// PresenterBase
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected PresenterBase(TView view)
        {
            SynchronizationContext = SynchronizationContext.Current;

            View  = view;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ~PresenterBase
        /// 
        /// <summary>
        /// オブジェクトを破棄します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        ~PresenterBase()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// View
        /// 
        /// <summary>
        /// View オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TView View { get; private set; }

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

        #endregion

        #region Methods

        /* --------------------------------------------------------------------- */
        ///
        /// Async
        /// 
        /// <summary>
        /// 各種操作を非同期で実行します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public Task Async(Action action) => Task.Run(() => action());

        /* --------------------------------------------------------------------- */
        ///
        /// Async
        /// 
        /// <summary>
        /// 各種操作を非同期で実行します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public Task<TResult> Async<TResult>(Func<TResult> func) => Task.Run(() => func());

        /* --------------------------------------------------------------------- */
        ///
        /// Sync
        /// 
        /// <summary>
        /// オブジェクト初期化時のスレッド上で各種操作を実行します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public void Sync(Action action)
            => this.LogException(() => SynchronizationContext.Post(_ => action(), null));

        /* --------------------------------------------------------------------- */
        ///
        /// SyncWait
        /// 
        /// <summary>
        /// オブジェクト初期化時のスレッド上で各種操作を実行し、
        /// 実行が完了するまで待機します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public void SyncWait(Action action)
            => this.LogException(() => SynchronizationContext.Send(_ => action(), null));

        /* --------------------------------------------------------------------- */
        ///
        /// SyncWait
        /// 
        /// <summary>
        /// オブジェクト初期化時のスレッド上で各種操作を実行し、
        /// 実行が完了するまで待機します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public TResult SyncWait<TResult>(Func<TResult> func)
        {
            TResult result = default(TResult);
            try { SynchronizationContext.Send(_ => { result = func(); }, null); }
            catch (Exception err) { this.LogError(err.Message, err); }
            return result;
        }

        #endregion

        #region Methods for IDisposable

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        /// 
        /// <summary>
        /// オブジェクトを破棄する際に必要な終了処理を実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        /// 
        /// <summary>
        /// オブジェクトを破棄する際に必要な終了処理を実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void Dispose(bool disposing) { }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// PresenterBase
    ///
    /// <summary>
    /// View と Model が 1 対 1 対応する Presenter の基底となるクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class PresenterBase<TView, TModel> : PresenterBase<TView>
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
        public PresenterBase(TView view, TModel model) : base(view)
        {
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
        public TModel Model { get; private set; }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// PresenterBase
    ///
    /// <summary>
    /// View/Model/EventAggregator を持つ Presenter の基底となるクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class PresenterBase<TView, TModel, TEvents> : PresenterBase<TView, TModel>
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
        public PresenterBase(TView view, TModel model, TEvents events)
            : base(view, model)
        {
            Events = events;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Events
        /// 
        /// <summary>
        /// EventAggregator オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TEvents Events { get; private set; }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// PresenterBase
    ///
    /// <summary>
    /// View/Model/EventAggregator/Settings を持つ Presenter の基底となる
    /// クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class PresenterBase<TView, TModel, TEvents, TSettings>
        : PresenterBase<TView, TModel, TEvents>
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
        public PresenterBase(TView view, TModel model, TEvents events, TSettings settings)
            : base(view, model, events)
        {
            Settings = settings;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Settings
        /// 
        /// <summary>
        /// Settings オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TSettings Settings { get; private set; }

        #endregion
    }

}
