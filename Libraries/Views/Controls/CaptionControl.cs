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
        bool MaximizeBox
        {
            get { return _maximize; }
            set
            {
                if (_maximize == value) return;
                _maximize = value;
                RaisePropertyChanged(nameof(MaximizeBox));
            }
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
        bool MinimizeBox
        {
            get { return _minimize; }
            set
            {
                if (_minimize == value) return;
                _minimize = value;
                RaisePropertyChanged(nameof(MinimizeBox));
            }
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
        bool CloseBox
        {
            get { return _close; }
            set
            {
                if (_close == value) return;
                _close = value;
                RaisePropertyChanged(nameof(CloseBox));
            }
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
        /// RaisePropertyChanged
        /// 
        /// <summary>
        /// PropertyChanged イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected void RaisePropertyChanged([CallerMemberName] string name = null)
            => OnPropertyChanged(new PropertyChangedEventArgs(name));

        #endregion

        #region Fields
        private bool _maximize = true;
        private bool _minimize = true;
        private bool _close = true;
        #endregion
    }
}
