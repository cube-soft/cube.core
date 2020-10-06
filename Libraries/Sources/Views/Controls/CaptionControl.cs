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

namespace Cube.Forms.Controls
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
    public class CaptionControl : ControlBase
    {
        #region Properties

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
            get => _active;
            set
            {
                if (_active == value) return;
                _active = value;

                if (value) OnActivated(EventArgs.Empty);
                else OnDeactivate(EventArgs.Empty);
            }
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
            get => _state;
            set
            {
                if (_state == value) return;
                _state = value;

                switch (value)
                {
                    case System.Windows.Forms.FormWindowState.Maximized:
                        OnMaximized(EventArgs.Empty);
                        break;
                    case System.Windows.Forms.FormWindowState.Minimized:
                        OnMinimized(EventArgs.Empty);
                        break;
                    case System.Windows.Forms.FormWindowState.Normal:
                        OnNormalized(EventArgs.Empty);
                        break;
                    default:
                        break;
                }
            }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// MaximizeControl
        ///
        /// <summary>
        /// 最大化ボタンを表すコントロールを取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Forms.Control MaximizeControl
        {
            get => _maximize;
            protected set
            {
                if (_maximize == value) return;
                if (_maximize != null) _maximize.Click -= WhenMaximizeRequested;
                _maximize = value;
                if (_maximize != null) _maximize.Click += WhenMaximizeRequested;
            }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// MinimizeControl
        ///
        /// <summary>
        /// 最小化ボタンを表すコントロールを取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Forms.Control MinimizeControl
        {
            get => _minimize;
            protected set
            {
                if (_minimize == value) return;
                if (_minimize != null) _minimize.Click -= WhenMinimizeRequested;
                _minimize = value;
                if (_minimize != null) _minimize.Click += WhenMinimizeRequested;
            }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// CloseControl
        ///
        /// <summary>
        /// 閉じるボタンを表すコントロールを取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Forms.Control CloseControl
        {
            get => _close;
            protected set
            {
                if (_close == value) return;
                if (_close != null) _close.Click -= WhenCloseRequested;
                _close = value;
                if (_close != null) _close.Click += WhenCloseRequested;
            }
        }

        #endregion

        #region Events

        #region CloseRequested

        /* --------------------------------------------------------------------- */
        ///
        /// CloseRequested
        ///
        /// <summary>
        /// 画面を閉じる操作が要求された時に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler CloseRequested;

        /* --------------------------------------------------------------------- */
        ///
        /// OnCloseRequested
        ///
        /// <summary>
        /// CloseRequested イベントを発生させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnCloseRequested(EventArgs e) =>
            CloseRequested?.Invoke(this, e);

        #endregion

        #region MaximizeRequested

        /* --------------------------------------------------------------------- */
        ///
        /// MaximizeRequested
        ///
        /// <summary>
        /// 最大化が要求された時に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler MaximizeRequested;

        /* --------------------------------------------------------------------- */
        ///
        /// OnMaximizeRequested
        ///
        /// <summary>
        /// MaximizeRequested イベントを発生させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnMaximizeRequested(EventArgs e) =>
            MaximizeRequested?.Invoke(this, e);

        #endregion

        #region Maximized

        /* --------------------------------------------------------------------- */
        ///
        /// Maximized
        ///
        /// <summary>
        /// 最大化時に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler Maximized;

        /* --------------------------------------------------------------------- */
        ///
        /// OnMaximized
        ///
        /// <summary>
        /// Maximized イベントを発生させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnMaximized(EventArgs e) => Maximized?.Invoke(this, e);

        #endregion

        #region MinimizeRequested

        /* --------------------------------------------------------------------- */
        ///
        /// MinimizeRequested
        ///
        /// <summary>
        /// 最小化が要求された時に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler MinimizeRequested;

        /* --------------------------------------------------------------------- */
        ///
        /// OnMinimizeRequested
        ///
        /// <summary>
        /// MinimizeRequested イベントを発生させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnMinmizeRequested(EventArgs e) =>
            MinimizeRequested?.Invoke(this, e);

        #endregion

        #region Minimized

        /* --------------------------------------------------------------------- */
        ///
        /// Minimized
        ///
        /// <summary>
        /// 最小化時に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler Minimized;

        /* --------------------------------------------------------------------- */
        ///
        /// OnMinimized
        ///
        /// <summary>
        /// Minimized イベントを発生させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnMinimized(EventArgs e) => Minimized?.Invoke(this, e);

        #endregion

        #region Normalized

        /* --------------------------------------------------------------------- */
        ///
        /// Normalized
        ///
        /// <summary>
        /// 元の大きさに戻った時に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler Normalized;

        /* --------------------------------------------------------------------- */
        ///
        /// OnNormalized
        ///
        /// <summary>
        /// Normalized イベントを発生させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnNormalized(EventArgs e) => Normalized?.Invoke(this, e);

        #endregion

        #region Activated

        /* --------------------------------------------------------------------- */
        ///
        /// Activated
        ///
        /// <summary>
        /// アクティブ化された時に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler Activated;

        /* --------------------------------------------------------------------- */
        ///
        /// OnActivated
        ///
        /// <summary>
        /// Activated イベントを発生させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnActivated(EventArgs e) => Activated?.Invoke(this, e);

        #endregion

        #region Deactivate

        /* --------------------------------------------------------------------- */
        ///
        /// Deactivate
        ///
        /// <summary>
        /// 非アクティブ化された時に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler Deactivate;

        /* --------------------------------------------------------------------- */
        ///
        /// OnDeactivate
        ///
        /// <summary>
        /// Deactivate イベントを発生させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnDeactivate(EventArgs e) => Deactivate?.Invoke(this, e);

        #endregion

        #endregion

        #region Handlers
        private void WhenMaximizeRequested(object s, EventArgs e) => OnMaximizeRequested(e);
        private void WhenMinimizeRequested(object s, EventArgs e) => OnMinmizeRequested(e);
        private void WhenCloseRequested(object s, EventArgs e) => OnCloseRequested(e);
        #endregion

        #region Fields
        private bool _active = true;
        private System.Windows.Forms.FormWindowState _state = System.Windows.Forms.FormWindowState.Normal;
        private System.Windows.Forms.Control _maximize;
        private System.Windows.Forms.Control _minimize;
        private System.Windows.Forms.Control _close;
        #endregion
    }
}
