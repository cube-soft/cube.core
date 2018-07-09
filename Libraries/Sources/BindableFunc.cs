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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Cube.Xui
{
    /* --------------------------------------------------------------------- */
    ///
    /// BindableFunc(T)
    ///
    /// <summary>
    /// 関数オブジェクトの結果を Binding 可能にするためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class BindableFunc<T> : INotifyPropertyChanged
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// BindableFunc
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="func">関数オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public BindableFunc(Func<T> func) : this(func, SynchronizationContext.Current) { }

        /* ----------------------------------------------------------------- */
        ///
        /// BindableFunc
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="func">関数オブジェクト</param>
        /// <param name="context">同期用コンテキスト</param>
        ///
        /* ----------------------------------------------------------------- */
        public BindableFunc(Func<T> func, SynchronizationContext context)
        {
            Func = func;
            _context = context;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Value
        ///
        /// <summary>
        /// 値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public T Value => Func();

        /* ----------------------------------------------------------------- */
        ///
        /// HasValue
        ///
        /// <summary>
        /// 有効な値が設定されているかどうかを示す値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool HasValue => Func != null;

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
        /// Func
        ///
        /// <summary>
        /// Value の内容を決定するための関数オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected Func<T> Func { get; }

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// PropertyChanged
        ///
        /// <summary>
        /// プロパティの内容変更時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event PropertyChangedEventHandler PropertyChanged;

        /* ----------------------------------------------------------------- */
        ///
        /// OnPropertyChanged
        ///
        /// <summary>
        /// PropertyChanged イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) =>
            PropertyChanged?.Invoke(this, e);

        /* ----------------------------------------------------------------- */
        ///
        /// RaisePropertyChanged
        ///
        /// <summary>
        /// PropertyChanged イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected void RaisePropertyChanged([CallerMemberName] string name = null)
        {
            var e = new PropertyChangedEventArgs(name);
            if (_context != null)
            {
                if (IsSynchronous) _context.Send(_ => OnPropertyChanged(e), null);
                else _context.Post(_ => OnPropertyChanged(e), null);
            }
            else OnPropertyChanged(e);
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// RaiseValueChanged
        ///
        /// <summary>
        /// Value の内容が変化した事を表すイベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void RaiseValueChanged() => RaisePropertyChanged(nameof(Value));

        #endregion

        #region Fields
        private readonly SynchronizationContext _context;
        #endregion
    }
}
