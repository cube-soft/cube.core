/* ------------------------------------------------------------------------- */
///
/// ToggleButton.cs
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

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.ToggleButton
    /// 
    /// <summary>
    /// トグルボタンを作成するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ToggleButton : System.Windows.Forms.CheckBox
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ToggleButton
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ToggleButton() : base() { _painter = new ToggleButtonPainter(this); }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Surface
        ///
        /// <summary>
        /// ボタンの基本となる外観を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [Category("Surface")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Surface Surface
        {
            get { return _painter.Surface; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CheckedSurface
        ///
        /// <summary>
        /// ボタンがチェック状態時の外観を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [Category("Surface")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Surface CheckedSurface
        {
            get { return _painter.CheckedSurface; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MouseDownSurface
        /// 
        /// <summary>
        /// マウスがクリック状態時の外観を定義したオブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [Category("Surface")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Surface MouseDownSurface
        {
            get { return _painter.MouseDownSurface; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MouseOverSurface
        /// 
        /// <summary>
        /// マウスポインタがボタンの境界範囲内に存在する時の外観を定義した
        /// オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [Category("Surface")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Surface MouseOverSurface
        {
            get { return _painter.MouseOverSurface; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ShowFocusCues
        /// 
        /// <summary>
        /// フォーカス時に枠線を描画するかどうかを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override bool ShowFocusCues
        {
            get { return false; }
        }

        #region Hiding properties for ButtonBase

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new System.Drawing.Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new System.Drawing.Image BackgroundImage
        {
            get { return base.BackgroundImage; }
            set { base.BackgroundImage = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new System.Windows.Forms.FlatButtonAppearance FlatAppearance
        {
            get { return base.FlatAppearance; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new System.Windows.Forms.FlatStyle FlatStyle
        {
            get { return base.FlatStyle; }
            set { base.FlatStyle = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new System.Drawing.Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new System.Drawing.Image Image
        {
            get { return base.Image; }
            set { base.Image = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new int ImageIndex
        {
            get { return base.ImageIndex; }
            set { base.ImageIndex = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new string ImageKey
        {
            get { return base.ImageKey; }
            set { base.ImageKey = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new System.Windows.Forms.ImageList ImageList
        {
            get { return base.ImageList; }
            set { base.ImageList = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new System.Windows.Forms.TextImageRelation TextImageRelation
        {
            get { return base.TextImageRelation; }
            set { base.TextImageRelation = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new bool UseVisualStyleBackColor
        {
            get { return base.UseVisualStyleBackColor; }
            set { base.UseVisualStyleBackColor = value; }
        }

        #endregion

        #region Hiding properties for CheckBox

        [Browsable(false)]
        public new System.Windows.Forms.Appearance Appearance
        {
            get { return base.Appearance; }
            set { base.Appearance = value; }
        }

        [Browsable(false)]
        public new System.Drawing.ContentAlignment CheckAlign
        {
            get { return base.CheckAlign; }
            set { base.CheckAlign = value; }
        }

        #endregion

        #endregion

        #region Fields
        private FlatButtonPainter _painter = null;
        #endregion
    }
}
