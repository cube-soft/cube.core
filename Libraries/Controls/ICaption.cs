/* ------------------------------------------------------------------------- */
///
/// ICaption.cs
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

namespace Cube.Forms.Controls
{
    /* --------------------------------------------------------------------- */
    ///
    /// ICaption
    /// 
    /// <summary>
    /// 画面上部のキャプション（タイトルバー）を表すインターフェースです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public interface ICaption
    {
        #region Properties

        /* --------------------------------------------------------------------- */
        ///
        /// MaximizeBox
        /// 
        /// <summary>
        /// 最大化ボタンを表示するかどうかを示す値を取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        bool MaximizeBox { get; set; }

        /* --------------------------------------------------------------------- */
        ///
        /// MinimizeBox
        /// 
        /// <summary>
        /// 最小化ボタンを表示するかどうかを示す値を取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        bool MinimizeBox { get; set; }

        /* --------------------------------------------------------------------- */
        ///
        /// CloseBox
        /// 
        /// <summary>
        /// 閉じるボタンを表示するかどうかを示す値を取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        bool CloseBox { get; set; }

        #endregion

        #region Events

        /* --------------------------------------------------------------------- */
        ///
        /// Maximize
        /// 
        /// <summary>
        /// 最大化が要求された時に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        event EventHandler Maximize;

        /* --------------------------------------------------------------------- */
        ///
        /// Minimize
        /// 
        /// <summary>
        /// 最小化が要求された時に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        event EventHandler Minimize;

        /* --------------------------------------------------------------------- */
        ///
        /// Close
        /// 
        /// <summary>
        /// 画面を閉じる操作が要求された時に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        event EventHandler Close;

        #endregion
    }
}
