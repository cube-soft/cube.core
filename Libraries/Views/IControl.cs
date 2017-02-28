/* ------------------------------------------------------------------------- */
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
using System.Drawing;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// IControl
    /// 
    /// <summary>
    /// 各種コントロールのインターフェースです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public interface IControl
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Location
        /// 
        /// <summary>
        /// 表示位置を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        Point Location { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Size
        /// 
        /// <summary>
        /// 表示サイズを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        Size Size { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// EventAggregator
        /// 
        /// <summary>
        /// イベントを集約するためのオブジェクトを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        IEventAggregator EventAggregator { get; set; }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// IForm
    /// 
    /// <summary>
    /// 各種フォームのインターフェースです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public interface IForm : IControl
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Show
        /// 
        /// <summary>
        /// 画面を表示します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        void Show();

        /* ----------------------------------------------------------------- */
        ///
        /// Close
        /// 
        /// <summary>
        /// 画面を終了させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        void Close();
    }
}
