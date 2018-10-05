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
using System.Threading;
using System.Threading.Tasks;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// PresenterBase
    ///
    /// <summary>
    /// View のみを保持する Presenter の基底クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class PresenterBase<TView> : DisposableBase
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
        /// <param name="view">View オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        protected PresenterBase(TView view) : this(view, null) { }

        /* ----------------------------------------------------------------- */
        ///
        /// PresenterBase
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="view">View オブジェクト</param>
        /// <param name="ea">イベント集約オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        protected PresenterBase(TView view, IAggregator ea) :
            this(view, ea, SynchronizationContext.Current) { }

        /* ----------------------------------------------------------------- */
        ///
        /// PresenterBase
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="view">View オブジェクト</param>
        /// <param name="ea">イベント集約オブジェクト</param>
        /// <param name="context">同期コンテキスト</param>
        ///
        /* ----------------------------------------------------------------- */
        protected PresenterBase(TView view, IAggregator ea, SynchronizationContext context)
        {
            View       = view;
            Aggregator = ea;
            Context    = context;
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
        public TView View { get; protected set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Aggregator
        ///
        /// <summary>
        /// イベント集約オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IAggregator Aggregator { get; protected set; }

        /* --------------------------------------------------------------------- */
        ///
        /// Context
        ///
        /// <summary>
        /// オブジェクト初期化時のコンテキストを取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public SynchronizationContext Context { get; }

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
        /// <param name="action">
        /// 非同期で実行する <c>Action</c> オブジェクト
        /// </param>
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
        /// <param name="func">
        /// 非同期で実行する <c>Func(TResult)</c> オブジェクト
        /// </param>
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
        /// <param name="action">
        /// 同期コンテキスト上で実行する <c>Action</c> オブジェクト
        /// </param>
        ///
        /* --------------------------------------------------------------------- */
        public void Sync(Action action)
        {
            if (Context != null) Context.Post(_ => action(), null);
            else action();
        }

        /* --------------------------------------------------------------------- */
        ///
        /// SyncWait
        ///
        /// <summary>
        /// オブジェクト初期化時のスレッド上で各種操作を実行し、
        /// 実行が完了するまで待機します。
        /// </summary>
        ///
        /// <param name="action">
        /// 同期コンテキスト上で実行する <c>Action</c> オブジェクト
        /// </param>
        ///
        /* --------------------------------------------------------------------- */
        public void SyncWait(Action action)
        {
            if (Context != null) Context.Send(_ => action(), null);
            else action();
        }

        /* --------------------------------------------------------------------- */
        ///
        /// SyncWait
        ///
        /// <summary>
        /// オブジェクト初期化時のスレッド上で各種操作を実行し、
        /// 実行が完了するまで待機します。
        /// </summary>
        ///
        /// <param name="func">
        /// 同期コンテキスト上で実行する <c>Func(TResult)</c> オブジェクト
        /// </param>
        ///
        /* --------------------------------------------------------------------- */
        public TResult SyncWait<TResult>(Func<TResult> func)
        {
            var result = default(TResult);
            if (Context != null) Context.Send(_ => { result = func(); }, null);
            else result = func();
            return result;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// リソースを解放します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Dispose(bool disposing) { }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// PresenterBase
    ///
    /// <summary>
    /// View および Model から構成される Presenter の基底クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class PresenterBase<TView, TModel> : PresenterBase<TView>
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
        /// <param name="view">View オブジェクト</param>
        /// <param name="model">Model オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        protected PresenterBase(TView view, TModel model) : base(view)
        {
            Model = model;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PresenterBase
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="view">View オブジェクト</param>
        /// <param name="model">Model オブジェクト</param>
        /// <param name="ea">イベント集約オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        protected PresenterBase(TView view, TModel model, IAggregator ea) : base(view, ea)
        {
            Model = model;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PresenterBase
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="view">View オブジェクト</param>
        /// <param name="model">Model オブジェクト</param>
        /// <param name="ea">イベント集約オブジェクト</param>
        /// <param name="context">同期コンテキスト</param>
        ///
        /* ----------------------------------------------------------------- */
        protected PresenterBase(TView view, TModel model, IAggregator ea,
            SynchronizationContext context) : base(view, ea, context)
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
        public TModel Model { get; protected set; }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// PresenterBase
    ///
    /// <summary>
    /// View, Model, Aggregator, Settings の 4 種類のオブジェクトから
    /// 構成される Presenter の基底クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class PresenterBase<TView, TModel, TSettings> : PresenterBase<TView, TModel>
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
        /// <param name="view">View オブジェクト</param>
        /// <param name="model">Model オブジェクト</param>
        /// <param name="settings">Settings オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        protected PresenterBase(TView view, TModel model, TSettings settings) : base(view, model)
        {
            Settings = settings;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PresenterBase
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="view">View オブジェクト</param>
        /// <param name="model">Model オブジェクト</param>
        /// <param name="settings">Settings オブジェクト</param>
        /// <param name="ea">イベント集約オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        protected PresenterBase(TView view, TModel model, TSettings settings, IAggregator ea) : base(view, model, ea)
        {
            Settings = settings;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PresenterBase
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="view">View オブジェクト</param>
        /// <param name="model">Model オブジェクト</param>
        /// <param name="settings">Settings オブジェクト</param>
        /// <param name="ea">イベント集約オブジェクト</param>
        /// <param name="context">同期コンテキスト</param>
        ///
        /* ----------------------------------------------------------------- */
        protected PresenterBase(TView view, TModel model, TSettings settings,
            IAggregator ea, SynchronizationContext context) : base(view, model, ea, context)
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
        public TSettings Settings { get; protected set; }

        #endregion
    }

}
