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
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Cube.Xui
{
    /* --------------------------------------------------------------------- */
    ///
    /// BindableCommand(T)
    ///
    /// <summary>
    /// 特定のプロパティを関連付けられるコマンドです。
    /// Observe メソッドによって関連付けられたオブジェクトの PropertyChanged
    /// イベント発生時に CanExecuteChanged イベントを発生させます。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class BindableCommand<T> : RelayCommand<T>, IDisposable
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// BindableCommand(T)
        ///
        /// <summary>
        /// Initializes a new instance of the BindableCommand(T) class
        /// with the specified action.
        /// </summary>
        ///
        /// <param name="execute">Action to execute.</param>
        ///
        /* ----------------------------------------------------------------- */
        public BindableCommand(Action<T> execute) : this(execute, e => true) { }

        /* ----------------------------------------------------------------- */
        ///
        /// BindableCommand(T)
        ///
        /// <summary>
        /// Initializes a new instance of the BindableCommand(T) class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="execute">Action to execute.</param>
        /// <param name="canExecute">
        /// Function to determine whether the command can be executed.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public BindableCommand(Action<T> execute, Func<T, bool> canExecute) : base(execute, canExecute, true)
        {
            _dispose = new OnceAction<bool>(Dispose);
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Observe
        ///
        /// <summary>
        /// Observes the PropertyChanged event of the specified object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public BindableCommand<T> Observe(INotifyPropertyChanged src, params string[] names)
        {
            var set = new HashSet<string>(names);
            void changed(object s, PropertyChangedEventArgs e)
            {
                if (set.Count <= 0 || set.Contains(e.PropertyName)) RaiseCanExecuteChanged();
            }

            src.PropertyChanged += changed;
            _observer.Add(Disposable.Create(() => src.PropertyChanged -= changed));

            return this;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ~BindableCommand
        ///
        /// <summary>
        /// オブジェクトを破棄します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        ~BindableCommand() { _dispose.Invoke(false); }

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
            if (!disposing) return;
            foreach (var obj in _observer) obj.Dispose();
            _observer.Clear();
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// WhenChanged
        ///
        /// <summary>
        /// 関連付けられたオブジェクトのプロパティが変更差た時に実行
        /// されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenChanged(object s, PropertyChangedEventArgs e) =>
            RaiseCanExecuteChanged();

        #endregion

        #region Fields
        private readonly OnceAction<bool> _dispose;
        private readonly IList<IDisposable> _observer = new List<IDisposable>();
        #endregion
    }
}
