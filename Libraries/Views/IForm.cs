/* ------------------------------------------------------------------------- */
//
// Copyright (c) 2010 CubeSoft, Inc.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
/* ------------------------------------------------------------------------- */
using System;

namespace Cube.Forms
{
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
        /// フォームを表示します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        void Show();

        /* ----------------------------------------------------------------- */
        ///
        /// Close
        /// 
        /// <summary>
        /// フォームを閉じます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        void Close();

        /* ----------------------------------------------------------------- */
        ///
        /// Activate
        /// 
        /// <summary>
        /// フォームをアクティブ化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        void Activate();

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        /// 
        /// <summary>
        /// オブジェクトがロードされた時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        event EventHandler Load;

        /* ----------------------------------------------------------------- */
        ///
        /// Activated
        /// 
        /// <summary>
        /// フォームがアクティブ化した時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        event EventHandler Activated;

        /* ----------------------------------------------------------------- */
        ///
        /// Deactivate
        /// 
        /// <summary>
        /// フォームが非アクティブ化した時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        event EventHandler Deactivate;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// IBorderlessForm
    /// 
    /// <summary>
    /// 枠線やタイトルバーのないフォームを表すインターフェースです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public interface IBorderlessForm : IForm
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Caption
        /// 
        /// <summary>
        /// キャプション（タイトルバー）を表すコントロールを取得または
        /// 設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        CaptionControl Caption { get; set; }
    }
}
