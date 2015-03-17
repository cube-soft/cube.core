/* ------------------------------------------------------------------------- */
///
/// FormBase.cs
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
    /// Cube.Forms.Demo.FormBase
    /// 
    /// <summary>
    /// デモ用プロジェクトで使用する各種フォームの基底となるクラスです。
    /// </summary>
    /// 
    /// <remarks>
    /// 新たなフォームが必要になった場合、FormBase クラスを継承した
    /// フォームクラスを作成します。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public partial class FormBase : Cube.Forms.WidgetForm
    {
        /* ----------------------------------------------------------------- */
        ///
        /// FormBase
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public FormBase()
        {
            InitializeComponent();
            CloseButton.Click += (s, e) => Close();
        }
    }
}
