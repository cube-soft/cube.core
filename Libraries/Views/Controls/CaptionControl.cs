/* ------------------------------------------------------------------------- */
///
/// CaptionControl.cs
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
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// CaptionControl
    /// 
    /// <summary>
    /// 画面上部のキャプション（タイトルバー）を表すコントロールです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class CaptionControl : UserControl, INotifyPropertyChanged
    {
        #region Properties

        /* --------------------------------------------------------------------- */
        ///
        /// MaximizeBox
        /// 
        /// <summary>
        /// 最大化ボタンを表示するかどうかを示す値を取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public bool MaximizeBox
        {
            get { return _maximize; }
            set { SetProperty(ref _maximize, value); }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// MinimizeBox
        /// 
        /// <summary>
        /// 最小化ボタンを表示するかどうかを示す値を取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public bool MinimizeBox
        {
            get { return _minimize; }
            set { SetProperty(ref _minimize, value); }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// CloseBox
        /// 
        /// <summary>
        /// 閉じるボタンを表示するかどうかを示す値を取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public bool CloseBox
        {
            get { return _close; }
            set { SetProperty(ref _close, value); }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// Active
        /// 
        /// <summary>
        /// 関連付けられているフォームがアクティブかどうかを示す値を取得
        /// または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public bool Active
        {
            get { return _active; }
            set { SetProperty(ref _active, value); }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// WindowState
        /// 
        /// <summary>
        /// ウィンドウの状態を取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public System.Windows.Forms.FormWindowState WindowState
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }

        #endregion

        #region Events

        /* --------------------------------------------------------------------- */
        ///
        /// Maximize
        /// 
        /// <summary>
        /// 最大化が要求された時に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler Maximize;

        /* --------------------------------------------------------------------- */
        ///
        /// Minimize
        /// 
        /// <summary>
        /// 最小化が要求された時に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler Minimize;

        /* --------------------------------------------------------------------- */
        ///
        /// Close
        /// 
        /// <summary>
        /// 画面を閉じる操作が要求された時に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler Close;

        /* --------------------------------------------------------------------- */
        ///
        /// PropertyChanged
        /// 
        /// <summary>
        /// プロパティが変化した時に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Virtual methods

        /* --------------------------------------------------------------------- */
        ///
        /// OnMaximize
        /// 
        /// <summary>
        /// Maximize イベントを発生させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnMaximize(EventArgs e)
            => Maximize?.Invoke(this, e);

        /* --------------------------------------------------------------------- */
        ///
        /// OnMinimize
        /// 
        /// <summary>
        /// Minimize イベントを発生させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnMinimize(EventArgs e)
            => Minimize?.Invoke(this, e);

        /* --------------------------------------------------------------------- */
        ///
        /// OnClose
        /// 
        /// <summary>
        /// Close イベントを発生させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnClose(EventArgs e)
            => Close?.Invoke(this, e);

        /* --------------------------------------------------------------------- */
        ///
        /// OnPropertyChanged
        /// 
        /// <summary>
        /// PropertyChanged イベントを発生させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
            => PropertyChanged?.Invoke(this, e);

        #endregion

        #region Non-virtual protected methods

        /* ----------------------------------------------------------------- */
        ///
        /// SetProperty
        /// 
        /// <summary>
        /// プロパティに値を設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected bool SetProperty<T>(ref T field, T value,
            [CallerMemberName] string name = null) =>
            SetProperty(ref field, value, EqualityComparer<T>.Default, name);

        /* ----------------------------------------------------------------- */
        ///
        /// SetProperty
        /// 
        /// <summary>
        /// プロパティに値を設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected bool SetProperty<T>(ref T field, T value,
            IEqualityComparer<T> func, [CallerMemberName] string name = null)
        {
            if (func.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(new PropertyChangedEventArgs(name));
            return true;
        }

        #endregion

        #region Fields
        private bool _maximize = true;
        private bool _minimize = true;
        private bool _close = true;
        private bool _active = true;
        private System.Windows.Forms.FormWindowState _state = System.Windows.Forms.FormWindowState.Normal;
        #endregion
    }
}
