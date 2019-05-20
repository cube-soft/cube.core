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
    [Obsolete("The class and inherited classes will be removed in Cube.Forms 1.17.0")]
    public abstract class PresenterBase<TView> : PresentableBase
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
        protected PresenterBase(TView view, Aggregator ea) :
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
        /// <param name="aggregator">イベント集約オブジェクト</param>
        /// <param name="context">同期コンテキスト</param>
        ///
        /* ----------------------------------------------------------------- */
        protected PresenterBase(TView view, Aggregator aggregator, SynchronizationContext context) :
            base (aggregator, context) { View = view; }

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

        #endregion

        #region Methods

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
        protected PresenterBase(TView view, TModel model, Aggregator ea) : base(view, ea)
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
        protected PresenterBase(TView view, TModel model, Aggregator ea, SynchronizationContext context) :
            base(view, ea, context) { Model = model; }

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
        protected PresenterBase(TView view, TModel model, TSettings settings) :
            base(view, model) { Settings = settings; }

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
        protected PresenterBase(TView view, TModel model, TSettings settings, Aggregator ea) :
            base(view, model, ea) { Settings = settings; }

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
            Aggregator ea, SynchronizationContext context) :
            base(view, model, ea, context) { Settings = settings; }

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
