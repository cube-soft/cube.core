/* ------------------------------------------------------------------------- */
///
/// MainForm.cs
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
    /// Cube.Forms.Demo.MainForm
    /// 
    /// <summary>
    /// デモ用プロジェクトのメインフォームです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class MainForm : Cube.Forms.WidgetForm
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// MainForm
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public MainForm()
        {
            InitializeComponent();
            CloseButton.Click += (s, e) => Close();
        }

        #endregion

        #region Event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// ButtonsButton_Click
        ///
        /// <summary>
        /// 各種ボタンのデモ用フォームを表示します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ButtonsButton_Click(object sender, EventArgs e)
        {
            var dialog = new ButtonsForm();
            dialog.ShowDialog();
        }

        #endregion
    }
}
