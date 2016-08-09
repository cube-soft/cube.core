/* ------------------------------------------------------------------------- */
///
/// NavigatingErrorEventArgs.cs
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
namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// NavigateErrorEventArgs
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
        public NavigatingErrorEventArgs(string url, string frame, int code)
            : base(url, frame)
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
        public int StatusCode { get; }

        #endregion
    }
}
