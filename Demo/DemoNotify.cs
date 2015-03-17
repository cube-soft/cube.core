/* ------------------------------------------------------------------------- */
///
/// DemoNotify.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
///
/* ------------------------------------------------------------------------- */
using System;

namespace Cube.Forms.Demo
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.Demo.DemoNotify
    /// 
    /// <summary>
    /// 通知フォームのデモ用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class DemoNotify : FormBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// DemoNotify
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DemoNotify()
        {
            InitializeComponent();
        }

        #endregion

        #region Event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// ShowButton_Click
        ///
        /// <summary>
        /// Show ボタンが押下された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ShowButton_Click(object sender, EventArgs e)
        {
            var dialog = new Cube.Forms.NotifyForm();
            dialog.Title = TitleTextBox.Text;
            dialog.Show();
        }

        #endregion
    }
}
