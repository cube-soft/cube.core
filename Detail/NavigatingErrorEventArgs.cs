/* ------------------------------------------------------------------------- */
///
/// NavigatingErrorEventArgs.cs
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
    /// Cube.Forms.NavigateErrorEventArgs
    /// 
    /// <summary>
    /// ウェブブラウザにおいて、移動中にエラーが発生した時の引数を保持する
    /// ためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class NavigatingErrorEventArgs : NavigatingEventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// NavigatingErrorEventArgs
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public NavigatingErrorEventArgs(string url, string frame, int code, bool cancel = false)
            : base(url, frame, cancel)
        {
            StatusCode = code;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// StatusCode
        /// 
        /// <summary>
        /// ステータスコードを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int StatusCode { get; private set; }

        #endregion
    }
}
