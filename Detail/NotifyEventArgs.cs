/* ------------------------------------------------------------------------- */
///
/// NavigatingEventArgs.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
///
/* ------------------------------------------------------------------------- */
using System;
using System.Drawing;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// NotifyEventArgs
    /// 
    /// <summary>
    /// 通知フォームで表示された内容を保持するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class NotifyEventArgs : EventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// NotifyEventArgs
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public NotifyEventArgs(NotifyLevel level, string title, Image image, object data = null)
            : base()
        {
            Level = level;
            Title = title;
            Image = image;
            Data  = data;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Level
        /// 
        /// <summary>
        /// 重要度を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public NotifyLevel Level { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Title
        /// 
        /// <summary>
        /// タイトルを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Title { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Image
        /// 
        /// <summary>
        /// イメージを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Image Image { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Data
        /// 
        /// <summary>
        /// ユーザデータを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public object Data { get; private set; }

        #endregion
    }
}
