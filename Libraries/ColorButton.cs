/* ------------------------------------------------------------------------- */
///
/// ColorButton.cs
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
using System.Drawing;
using System.Linq;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// ColorButton
    /// 
    /// <summary>
    /// 色を設定するためのボタンクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ColorButton : Button
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ColorButton
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ColorButton() : base()
        {
            UseVisualStyleBackColor = false;
            BackColor = SystemColors.Control;
            ForeColor = BackColor;
            FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            FlatAppearance.BorderColor = SystemColors.ControlDark;
            FlatAppearance.BorderSize = 1;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// AnyColor
        ///
        /// <summary>
        /// 使用可能なすべての色を基本色セットとしてダイアログボックスに
        /// 表示するかどうかを示す値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool AnyColor { get; set; } = true;

        /* ----------------------------------------------------------------- */
        ///
        /// SolidColorOnly
        ///
        /// <summary>
        /// ダイアログボックスでユーザーが選択できる色を純色だけに
        /// 制限するかどうかを示す値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(false)]
        public bool SolidColorOnly { get; set; } = false;

        /* ----------------------------------------------------------------- */
        ///
        /// AllowFullOpen
        ///
        /// <summary>
        /// ユーザーがダイアログボックスを使用してカスタムカラーを定義
        /// できるかどうかを示す値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool AllowFullOpen { get; set; } = true;

        /* ----------------------------------------------------------------- */
        ///
        /// FullOpen
        ///
        /// <summary>
        /// ダイアログボックスが開かれたときに、カスタムカラーの作成用の
        /// コントロールを表示するかどうかを示す値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool FullOpen { get; set; } = true;

        /* ----------------------------------------------------------------- */
        ///
        /// CustomColors
        ///
        /// <summary>
        /// ダイアログボックスに表示されるカスタムカラーセットを取得または
        /// 設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<int> CustomColors { get; set; } = new List<int>();

        #endregion

        #region Override methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnClick
        ///
        /// <summary>
        /// ボタンが押下された時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            var dialog = new System.Windows.Forms.ColorDialog();
            dialog.Color = BackColor;
            dialog.AnyColor = AnyColor;
            dialog.SolidColorOnly = SolidColorOnly;
            dialog.AllowFullOpen = AllowFullOpen;
            dialog.FullOpen = FullOpen;
            if (CustomColors != null) dialog.CustomColors = CustomColors.ToArray();
            
            var result = dialog.ShowDialog();

            FullOpen = dialog.FullOpen;
            if (CustomColors != null)
            {
                CustomColors.Clear();
                foreach (var color in dialog.CustomColors) CustomColors.Add(color);
            }

            if (result == System.Windows.Forms.DialogResult.Cancel) return;

            BackColor = dialog.Color;
            ForeColor = BackColor;
        }

        #endregion
    }
}
