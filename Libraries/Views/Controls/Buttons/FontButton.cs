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

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// FontButton
    /// 
    /// <summary>
    /// フォントを設定するためのボタンクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class FontButton : Button
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// FontButton
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public FontButton() : base() { }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// AllowScriptChange
        ///
        /// <summary>
        /// [スクリプト] コンボボックスに指定されている文字セットを
        /// ユーザーが変更し、現在表示されている文字セットとは異なる
        /// 文字セットを表示できるかどうかを示す値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool AllowScriptChange { get; set; } = true;

        /* ----------------------------------------------------------------- */
        ///
        /// AllowSimulations
        ///
        /// <summary>
        /// ダイアログ ボックスで、 Graphics Device Interface (GDI) に
        /// おけるフォント表示をシミュレーションできるかどうかを示す値を
        /// 取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool AllowSimulations { get; set; } = true;

        /* ----------------------------------------------------------------- */
        ///
        /// AllowVectorFonts
        ///
        /// <summary>
        /// ダイアログボックスでベクターフォントを選択できるかどうかを
        /// 示す値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool AllowVectorFonts { get; set; } = true;

        /* ----------------------------------------------------------------- */
        ///
        /// AllowVerticalFonts
        ///
        /// <summary>
        /// ダイアログボックスに縦書きと横書きのフォントを両方とも
        /// 表示するのか、横書きフォントだけを表示するのかを示す値を
        /// 取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool AllowVerticalFonts { get; set; } = true;

        /* ----------------------------------------------------------------- */
        ///
        /// FixedPitchOnly
        ///
        /// <summary>
        /// ダイアログボックスで選択できるフォントを固定幅フォントだけに
        /// 限定するかどうかを示す値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool FixedPitchOnly { get; set; } = false;

        /* ----------------------------------------------------------------- */
        ///
        /// FontMustExist
        ///
        /// <summary>
        /// 存在しないフォントやスタイルをユーザーが選択しようとした場合
        /// ダイアログボックスにエラーメッセージが表示されるかどうかを
        /// 示す値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(false)]
        public bool FontMustExist { get; set; } = false;

        /* ----------------------------------------------------------------- */
        ///
        /// ScriptsOnly
        ///
        /// <summary>
        /// ダイアログボックスで、非 OEM 文字セット、Symbol 文字セット、
        /// および ANSI 文字セットのすべてに対するフォントを選択できるか
        /// どうかを示す値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(false)]
        public bool ScriptsOnly { get; set; } = false;

        /* ----------------------------------------------------------------- */
        ///
        /// ShowApply
        ///
        /// <summary>
        /// ダイアログボックスに [適用] ボタンを表示するかどうかを示す値を
        /// 取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(false)]
        public bool ShowApply { get; set; } = false;

        /* ----------------------------------------------------------------- */
        ///
        /// ShowColor
        ///
        /// <summary>
        /// ダイアログボックスに色の選択肢を表示するかどうかを示す値を
        /// 取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(false)]
        public bool ShowColor { get; set; } = false;

        /* ----------------------------------------------------------------- */
        ///
        /// ShowEffects
        ///
        /// <summary>
        /// ダイアログボックスに、取り消し線、下線、テキストの色などの
        /// オプションをユーザーが指定するためのコントロールを表示するか
        /// どうかを示す値を設定または取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool ShowEffects { get; set; } = true;

        /* ----------------------------------------------------------------- */
        ///
        /// MaxSize
        ///
        /// <summary>
        /// 選択可能なポイントサイズの最大値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(0)]
        public int MaxSize { get; set; } = 0;

        /* ----------------------------------------------------------------- */
        ///
        /// MinSize
        ///
        /// <summary>
        /// 選択可能なポイントサイズの最小値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(0)]
        public int MinSize { get; set; } = 0;

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// Apply
        ///
        /// <summary>
        /// 適用ボタンが押下された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public EventHandler Apply;

        /* ----------------------------------------------------------------- */
        ///
        /// OnApply
        ///
        /// <summary>
        /// Apply イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnApply(EventArgs e)
            => Apply?.Invoke(this, e);

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// OnClick
        ///
        /// <summary>
        /// ボタンがクリックされた時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            var dialog = new System.Windows.Forms.FontDialog();
            dialog.AllowScriptChange = AllowScriptChange;
            dialog.AllowSimulations = AllowSimulations;
            dialog.AllowVectorFonts = AllowVectorFonts;
            dialog.AllowVerticalFonts = AllowVerticalFonts;
            dialog.FixedPitchOnly = FixedPitchOnly;
            dialog.FontMustExist = FontMustExist;
            dialog.MaxSize = MaxSize;
            dialog.MinSize = MinSize;
            dialog.ScriptsOnly = ScriptsOnly;
            dialog.ShowApply = ShowApply;
            dialog.ShowColor = ShowColor;
            dialog.ShowEffects = ShowEffects;
            dialog.Apply -= WhenApply;
            dialog.Apply += WhenApply;

            dialog.Color = ForeColor;
            dialog.Font = Font;

            var result = dialog.ShowDialog();
            dialog.Apply -= WhenApply;
            if (result == System.Windows.Forms.DialogResult.Cancel) return;

            ForeColor = dialog.Color;
            Font = dialog.Font;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenApply
        ///
        /// <summary>
        /// FontDialog の適用ボタンが押下された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenApply(object sender, EventArgs e) => OnApply(e);

        #endregion
    }
}
