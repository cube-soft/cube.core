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
using Cube.Forms.Controls;
using System;
using System.ComponentModel;
using System.Drawing;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Button
    ///
    /// <summary>
    /// ボタンを作成するためのクラスです。
    /// </summary>
    ///
    /// <remarks>
    /// Button クラスは、System.Windows.Forms.Button クラスにおける
    /// いくつかの表示上の問題を解決するために定義されたクラスです。
    /// さらに柔軟な外観を定義する場合は、FlatButton クラスを利用して下さい。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class Button : System.Windows.Forms.Button, IControl
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Aggregator
        ///
        /// <summary>
        /// イベント集約用オブジェクトを取得または設定します。
        /// </summary>
        ///
        /// <remarks>
        /// Controls に登録されている IControl オブジェクトに対して、
        /// 再帰的に設定します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IAggregator Aggregator
        {
            get => _aggregator;
            set
            {
                if (_aggregator == value) return;
                _aggregator = value;
                foreach (var obj in Controls)
                {
                    if (obj is IControl c) c.Aggregator = value;
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Dpi
        ///
        /// <summary>
        /// 現在の Dpi の値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double Dpi
        {
            get => _dpi;
            set
            {
                if (_dpi == value) return;
                var old = _dpi;
                _dpi = value;
                OnDpiChanged(ValueChangedEventArgs.Create(old, value));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ShowFocusCues
        ///
        /// <summary>
        /// フォーカス時に枠線を表示するかどうかを示す値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override bool ShowFocusCues => false;

        #endregion

        #region Events

        #region DpiChanged

        /* ----------------------------------------------------------------- */
        ///
        /// DpiChanged
        ///
        /// <summary>
        /// DPI の値が変化した時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event ValueChangedEventHandler<double> DpiChanged;

        /* ----------------------------------------------------------------- */
        ///
        /// OnDpiChanged
        ///
        /// <summary>
        /// DpiChanged イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnDpiChanged(ValueChangedEventArgs<double> e)
        {
            this.UpdateDpi(e.OldValue, e.NewValue);
            DpiChanged?.Invoke(this, e);
        }

        #endregion

        #region NcHitTest

        /* ----------------------------------------------------------------- */
        ///
        /// NcHitTest
        ///
        /// <summary>
        /// マウスのヒットテスト時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event QueryEventHandler<Point, Position> NcHitTest;

        /* ----------------------------------------------------------------- */
        ///
        /// OnNcHitTest
        ///
        /// <summary>
        /// NcHitTest イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnNcHitTest(QueryEventArgs<Point, Position> e) =>
            NcHitTest?.Invoke(this, e);

        #endregion

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// ShowFocusCues
        ///
        /// <summary>
        /// フォーカス時に枠線を表示するかどうかを示す値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnEnabledChanged(EventArgs e)
        {
            try
            {
                if (Enabled == _previous) return;

                if (Enabled) SetEnabledColor();
                else SetDisabledColor();
                _previous = Enabled;
            }
            finally { base.OnEnabledChanged(e); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetEnabledColor
        ///
        /// <summary>
        /// ボタンが有効状態の時の色を設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void SetEnabledColor()
        {
            BackColor = _background;
            ForeColor = _foreground;
            FlatAppearance.BorderColor = _border;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetDisabledColor
        ///
        /// <summary>
        /// ボタンが無効状態の時の色を設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void SetDisabledColor()
        {
            _background = BackColor;
            _foreground = ForeColor;
            _border = FlatAppearance.BorderColor;

            BackColor = Color.FromArgb(204, 204, 204);
            ForeColor = SystemColors.GrayText;
            FlatAppearance.BorderColor = Color.FromArgb(191, 191, 191);
        }

        #endregion

        #region Fields
        private bool _previous = true;
        private Color _background = Color.Empty;
        private Color _foreground = Color.Empty;
        private Color _border = Color.Empty;
        private IAggregator _aggregator;
        private double _dpi = StandardForm.BaseDpi;
        #endregion
    }
}
