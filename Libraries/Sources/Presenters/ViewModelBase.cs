﻿/* ------------------------------------------------------------------------- */
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
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// ViewModelBase
    ///
    /// <summary>
    /// ViewModel の基底クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class ViewModelBase<TMessenger> :
        ObservableBase, IDisposable where TMessenger : IAggregator
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ViewModelBase
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="messenger">Messenger</param>
        ///
        /* ----------------------------------------------------------------- */
        protected ViewModelBase(TMessenger messenger) :
            this(messenger, SynchronizationContext.Current) { }

        /* ----------------------------------------------------------------- */
        ///
        /// ViewModelBase
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="messenger">Messenger</param>
        /// <param name="context">同期用コンテキスト</param>
        ///
        /// <remarks>
        /// _dispose オブジェクト初期化前に例外が発生しないように、base には
        /// Vanilla を指定します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected ViewModelBase(TMessenger messenger, SynchronizationContext context) :
            base(Cube.Dispatcher.Vanilla)
        {
            _dispose   = new OnceAction<bool>(Dispose);
            _post      = new Cube.Dispatcher(context, false);
            _send      = new Cube.Dispatcher(context, true);
            Dispatcher = _post;
            Messenger  = messenger;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Messenger
        ///
        /// <summary>
        /// Messenger オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TMessenger Messenger { get; }

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
        /// Post
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
        public void Post(Action action) => _post.Invoke(action);

        /* --------------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// オブジェクト初期化時のスレッド上で各種操作を実行し、
        /// 実行が完了するまで待機します。
        /// </summary>
        ///
        /// <param name="action">
        /// 同期コンテキスト上で実行する Action オブジェクト
        /// </param>
        ///
        /* --------------------------------------------------------------------- */
        public void Send(Action action) => _send.Invoke(action);

        /* --------------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// オブジェクト初期化時のスレッド上で各種操作を実行し、
        /// 実行が完了するまで待機します。
        /// </summary>
        ///
        /// <param name="func">
        /// 同期コンテキスト上で実行する Func(TResult) オブジェクト
        /// </param>
        ///
        /* --------------------------------------------------------------------- */
        public TResult Send<TResult>(Func<TResult> func)
        {
            var result = default(TResult);
            _send.Invoke(() => { result = func(); });
            return result;
        }

        #region IDisposable

        /* ----------------------------------------------------------------- */
        ///
        /// ~ViewModelBase
        ///
        /// <summary>
        /// オブジェクトを破棄します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        ~ViewModelBase() { _dispose.Invoke(false); }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// リソースを解放します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Dispose()
        {
            _dispose.Invoke(true);
            GC.SuppressFinalize(this);
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
        protected virtual void Dispose(bool disposing) { }

        #endregion

        #endregion

        #region Implementations

        /* --------------------------------------------------------------------- */
        ///
        /// OnPropertyChanged
        ///
        /// <summary>
        /// プロパティ変更時に実行されます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected override void OnPropertyChanged(PropertyChangedEventArgs e) =>
            Post(() => base.OnPropertyChanged(e));

        #endregion

        #region Fields
        private readonly OnceAction<bool> _dispose;
        private readonly IDispatcher _send;
        private readonly IDispatcher _post;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// ViewModelBase
    ///
    /// <summary>
    /// ViewModel の基底クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class ViewModelBase : ViewModelBase<Messenger>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ViewModelBase
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected ViewModelBase() : this(new Messenger()) { }

        /* ----------------------------------------------------------------- */
        ///
        /// ViewModelBase
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="messenger">Messenger</param>
        ///
        /* ----------------------------------------------------------------- */
        protected ViewModelBase(Messenger messenger) : base(messenger) { }

        /* ----------------------------------------------------------------- */
        ///
        /// ViewModelBase
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="messenger">Messenger</param>
        /// <param name="context">同期用コンテキスト</param>
        ///
        /* ----------------------------------------------------------------- */
        protected ViewModelBase(Messenger messenger, SynchronizationContext context) :
            base(messenger, context) { }

        #endregion
    }
}
