/* ------------------------------------------------------------------------- */
///
/// NotifyForm.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
///
/* ------------------------------------------------------------------------- */
using System;
using System.ComponentModel;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.NotifyForm
    /// 
    /// <summary>
    /// 通知用フォームを作成するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class NotifyForm : WidgetForm
    {
        #region Constructors

        /* --------------------------------------------------------------------- */
        ///
        /// NotifyForm
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public NotifyForm()
        {
            InitializeComponent();
            CloseButton.Click += (s, e) => Close();
        }

        #endregion

        #region Properties

        /* --------------------------------------------------------------------- */
        ///
        /// Title
        /// 
        /// <summary>
        /// タイトルを取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(true)]
        public string Title
        {
            get { return TitleButton.Text; }
            set
            {
                TitleButton.Text = value;
                base.Text = value;
            }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// Image
        /// 
        /// <summary>
        /// イメージを取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(true)]
        public System.Drawing.Image Image
        {
            get { return ImageButton.Image; }
            set { ImageButton.Image = value; }
        }

        #region Hiding properties

        #endregion

        #endregion
    }
}
