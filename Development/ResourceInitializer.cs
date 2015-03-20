/* ------------------------------------------------------------------------- */
///
/// ResourceInitializer.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
///
/* ------------------------------------------------------------------------- */
using System;

namespace Cube.Development
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Development.ResourceInitializer
    /// 
    /// <summary>
    /// ユニットテスト用クラスの初期化を補助するクラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public class ResourceInitializer
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ResourceInitializer
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected ResourceInitializer() { }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Root
        ///
        /// <summary>
        /// テスト用リソースの存在するルートディレクトリへのパスを
        /// 取得、または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Root
        {
            get { return _root; }
            set { _root = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Examples
        /// 
        /// <summary>
        /// テストを行うためのダミーファイル群の存在するディレクトリへの
        /// パスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Examples
        {
            get { return System.IO.Path.Combine(Root, "Examples"); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Results
        /// 
        /// <summary>
        /// テスト結果を格納するためのディレクトリへのパスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Results
        {
            get { return System.IO.Path.Combine(Root, "Results"); }
        }

        #endregion

        #region Fields
        private string _root = System.Environment.CurrentDirectory;
        #endregion
    }
}
