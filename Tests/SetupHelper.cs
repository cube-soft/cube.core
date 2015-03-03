/* ------------------------------------------------------------------------- */
///
/// SetupHelper.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
///
/* ------------------------------------------------------------------------- */
using System;
using NUnit.Framework;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Tests.SetupHelper
    /// 
    /// <summary>
    /// 各種テストクラスの共通初期化用クラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    class SetupHelper
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// SetupHelper
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        /// 
        /// <remarks>
        /// このクラスを直接インスタンス化する事はできません。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected SetupHelper() { }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Examples
        /// 
        /// <summary>
        /// サンプルファイル群の存在するフォルダへのパスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Examples
        {
            get { return _examples; }
            private set { _examples = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Results
        /// 
        /// <summary>
        /// テスト結果を格納するためのフォルダへのパスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Results
        {
            get { return _results; }
            private set { _results = value; }
        }

        #endregion

        #region Public methods

        /* ----------------------------------------------------------------- */
        ///
        /// Setup
        /// 
        /// <summary>
        /// 初期処理を行います。
        /// </summary>
        /// 
        /// <remarks>
        /// テストに使用するサンプルファイル群は、テスト用プロジェクト
        /// フォルダ直下にある Examples と言うフォルダに存在します。
        /// テストを実行する際には、実行ファイルをテスト用プロジェクトに
        /// コピーしてから行う必要があります。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [SetUp]
        public void Setup()
        {
            var current = System.Environment.CurrentDirectory;
            Examples = System.IO.Path.Combine(current, "Examples");
            Results  = System.IO.Path.Combine(current, "Results");
            CustomSetup();
        }

        #endregion

        #region Protected methods

        /* ----------------------------------------------------------------- */
        ///
        /// ExtendedSetup
        /// 
        /// <summary>
        /// 追加的な初期処理を行います。
        /// </summary>
        /// 
        /// <remarks>
        /// SetupHelper を継承したクラスで独自の初期処理が必要な場合、
        /// このメソッドをオーバーライドして記述します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void CustomSetup() { }

        #endregion

        #region Fields
        private string _examples = string.Empty;
        private string _results  = string.Empty;
        #endregion
    }
}
