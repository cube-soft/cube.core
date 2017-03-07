/* ------------------------------------------------------------------------- */
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
    public class CaptionControl : ControlBase, INotifyPropertyChanged
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
            get { return _maximizeBox; }
            set { SetProperty(ref _maximizeBox, value); }
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
            get { return _minimizeBox; }
            set { SetProperty(ref _minimizeBox, value); }
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
            get { return _closeBox; }
            set { SetProperty(ref _closeBox, value); }
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

        /* --------------------------------------------------------------------- */
        ///
        /// MaximizeControl
        /// 
        /// <summary>
        /// 最大化ボタンを表すコントロールを取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected System.Windows.Forms.Control MaximizeControl
        {
            get { return _maximizeControl; }
            set
            {
                if (_maximizeControl == value) return;
                if (_maximizeControl != null) _maximizeControl.Click -= WhenMaximize;

                _maximizeControl = value;
                if (_maximizeControl == null) return;

                _maximizeControl.Click  += WhenMaximize;
                _maximizeControl.Visible = MaximizeBox;
            }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// MinimizeControl
        /// 
        /// <summary>
        /// 最小化ボタンを表すコントロールを取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected System.Windows.Forms.Control MinimizeControl
        {
            get { return _minimizeControl; }
            set
            {
                if (_minimizeControl == value) return;
                if (_minimizeControl != null) _minimizeControl.Click -= WhenMinimize;

                _minimizeControl = value;
                if (_minimizeControl == null) return;

                _minimizeControl.Click  += WhenMinimize;
                _minimizeControl.Visible = MinimizeBox;
            }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// CloseControl
        /// 
        /// <summary>
        /// 閉じるボタンを表すコントロールを取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected System.Windows.Forms.Control CloseControl
        {
            get { return _closeControl; }
            set
            {
                if (_closeControl == value) return;
                if (_closeControl != null) _closeControl.Click -= WhenClose;

                _closeControl = value;
                if (_closeControl == null) return;

                _closeControl.Click  += WhenClose;
                _closeControl.Visible = CloseBox;
            }
        }

        #endregion

        #region Events

        #region Maximize

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
        /// OnMaximize
        /// 
        /// <summary>
        /// Maximize イベントを発生させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnMaximize(EventArgs e) => Maximize?.Invoke(this, e);

        #endregion

        #region Minimize

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
        /// OnMinimize
        /// 
        /// <summary>
        /// Minimize イベントを発生させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnMinimize(EventArgs e) => Minimize?.Invoke(this, e);

        #endregion

        #region Close

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
        /// OnClose
        /// 
        /// <summary>
        /// Close イベントを発生させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnClose(EventArgs e) => Close?.Invoke(this, e);

        #endregion

        #region PropertyChanged

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
        {
            switch (e.PropertyName)
            {
                case nameof(MaximizeBox):
                    MaximizeControl.Visible = MaximizeBox;
                    break;
                case nameof(MinimizeBox):
                    MinimizeControl.Visible = MinimizeBox;
                    break;
                case nameof(CloseBox):
                    CloseControl.Visible = CloseBox;
                    break;
                default:
                    break;
            }

            PropertyChanged?.Invoke(this, e);
        }

        #endregion

        #endregion

        #region SetProperty

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

        #region Event handlers
        private void WhenMaximize(object s, EventArgs e) => OnMaximize(e);
        private void WhenMinimize(object s, EventArgs e) => OnMinimize(e);
        private void WhenClose(object s, EventArgs e) => OnClose(e);
        #endregion

        #region Fields
        private bool _maximizeBox = true;
        private bool _minimizeBox = true;
        private bool _closeBox = true;
        private bool _active = true;
        private System.Windows.Forms.FormWindowState _state = System.Windows.Forms.FormWindowState.Normal;
        private System.Windows.Forms.Control _maximizeControl;
        private System.Windows.Forms.Control _minimizeControl;
        private System.Windows.Forms.Control _closeControl;
        #endregion
    }
}
