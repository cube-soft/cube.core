/* ------------------------------------------------------------------------- */
///
/// ResourceInitializer.cs
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
