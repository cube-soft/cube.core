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

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// NavigatingEventArgs
    /// 
    /// <summary>
    /// Web ブラウザにおいて、画面遷移が発生した時の引数を保持するための
    /// クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class NavigatingEventArgs : System.ComponentModel.CancelEventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// NavigatingEventArgs
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public NavigatingEventArgs(string url, string frame, bool cancel = false)
            : base(cancel)
        {
            Url = url;
            Frame = frame;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Url
        /// 
        /// <summary>
        /// 遷移先の URL を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Url { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Frame
        /// 
        /// <summary>
        /// 遷移先のターゲットフレームを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Frame { get; private set; }

        #endregion
    }
}
