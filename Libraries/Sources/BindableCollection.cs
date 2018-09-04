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
using Cube.Generics;
using Cube.Log;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;

namespace Cube.Xui
{
    /* --------------------------------------------------------------------- */
    ///
    /// BindableCollection(T)
    ///
    /// <summary>
    /// Provides functionality to binding a collection.
    /// </summary>
    ///
    /// <remarks>
    /// ObservableCollection(T) で発生する PropertyChanged および
    /// CollectionChanged イベントをコンストラクタで指定された同期
    /// コンテキストを用いて伝搬させます。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class BindableCollection<T> : ObservableCollection<T>, IDisposable
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// BindableCollection
        ///
        /// <summary>
        /// Initializes a new instance of the <c>BindableCollection</c>
        /// class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public BindableCollection() : this(new T[0]) { }

        /* ----------------------------------------------------------------- */
        ///
        /// BindableCollection
        ///
        /// <summary>
        /// Initializes a new instance of the <c>BindableCollection</c>
        /// class with the specified collection.
        /// </summary>
        ///
        /// <param name="collection">Collection to be copied.</param>
        ///
        /* ----------------------------------------------------------------- */
        public BindableCollection(IEnumerable<T> collection) : base(collection ?? new T[0])
        {
            Context  = SynchronizationContext.Current;
            _dispose = new OnceAction<bool>(Dispose);
            SetHandler(this);
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Context
        ///
        /// <summary>
        /// Gets or sets the synchronization context.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SynchronizationContext Context { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// IsSynchronous
        ///
        /// <summary>
        /// UI スレッドに対して同期的にイベントを発生させるかどうかを
        /// 示す値を取得または設定します。
        /// </summary>
        ///
        /// <remarks>
        /// true の場合は Send メソッド、false の場合は Post メソッドを
        /// 用いてイベントを伝搬します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public bool IsSynchronous { get; set; } = true;

        /* ----------------------------------------------------------------- */
        ///
        /// IsRedirected
        ///
        /// <summary>
        /// 要素のイベントをリダイレクトするかどうかを示す値を取得します。
        /// </summary>
        ///
        /// <remarks>
        /// true の場合、要素の PropertyChanged イベントが発生した時に該当
        /// 要素に対して NotifyCollectionChangedAction.Replace を設定した
        /// 状態で CollectionChanged イベントを発生させます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public bool IsRedirected { get; set; } = false;

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// ~BindableCollection
        ///
        /// <summary>
        /// オブジェクトを破棄します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        ~BindableCollection() { _dispose.Invoke(false); }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// リソースを開放します。
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
        /// リソースを開放します。
        /// </summary>
        ///
        /// <param name="disposing">
        /// マネージオブジェクトを開放するかどうか
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void Dispose(bool disposing)
        {
            if (disposing) UnsetHandler(this);
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// OnPropertyChanged
        ///
        /// <summary>
        /// PropertyChanged イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnPropertyChanged(PropertyChangedEventArgs e) =>
            Invoke(() => base.OnPropertyChanged(e));

        /* ----------------------------------------------------------------- */
        ///
        /// OnCollectionChanged
        ///
        /// <summary>
        /// CollectionChanged イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e) =>
            Invoke(() => OnCollectionChangedCore(e));

        /* ----------------------------------------------------------------- */
        ///
        /// OnCollectionChangedCore
        ///
        /// <summary>
        /// CollectionChanged イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void OnCollectionChangedCore(NotifyCollectionChangedEventArgs e) => this.LogWarn(() =>
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    SetHandler(e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    UnsetHandler(e.OldItems);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    UnsetHandler(e.OldItems);
                    SetHandler(e.NewItems);
                    break;
                default:
                    break;
            }
            base.OnCollectionChanged(e);
        });

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the action through the SynchronizationContext.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Invoke(Action action)
        {
            if (Context != null)
            {
                if (IsSynchronous) Context.Send(_ => action(), null);
                else Context.Post(_ => action(), null);
            }
            else action();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetHandler
        ///
        /// <summary>
        /// 各項目に対してハンドラを設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void SetHandler(IList items)
        {
            foreach (var item in items)
            {
                if (item is INotifyPropertyChanged e)
                {
                    e.PropertyChanged -= WhenMemberChanged;
                    e.PropertyChanged += WhenMemberChanged;
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UnsetHandler
        ///
        /// <summary>
        /// 各項目からハンドラの設定を解除します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UnsetHandler(IList items)
        {
            foreach (var item in items)
            {
                if (item is INotifyPropertyChanged e) e.PropertyChanged -= WhenMemberChanged;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenMemberChanged
        ///
        /// <summary>
        /// 要素のプロパティが変更された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenMemberChanged(object s, PropertyChangedEventArgs e)
        {
            if (!IsRedirected) return;
            var index = IndexOf(s.TryCast<T>());
            if (index < 0) return;

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Replace, s, s, index
            ));
        }

        #endregion

        #region Fields
        private readonly OnceAction<bool> _dispose;
        #endregion
    }
}
