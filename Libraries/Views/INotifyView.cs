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
using System;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// INotifyView
    /// 
    /// <summary>
    /// 通知フォームを表すインターフェースです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public interface INotifyView : IForm
    {
        /* --------------------------------------------------------------------- */
        ///
        /// IsBusy
        /// 
        /// <summary>
        /// 実行中かどうかを取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        bool IsBusy { get; }

        /* --------------------------------------------------------------------- */
        ///
        /// Value
        /// 
        /// <summary>
        /// 通知内容を取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        NotifyItem Value { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Show
        /// 
        /// <summary>
        /// フォームを表示します。
        /// </summary>
        /// 
        /// <param name="item">表示内容</param>
        ///
        /* ----------------------------------------------------------------- */
        void Show(NotifyItem item);

        /* ----------------------------------------------------------------- */
        ///
        /// Show
        /// 
        /// <summary>
        /// フォームを表示します。
        /// </summary>
        /// 
        /// <param name="item">表示内容</param>
        /// <param name="style">表示スタイル</param>
        ///
        /* ----------------------------------------------------------------- */
        void Show(NotifyItem item, NotifyStyle style);

        /* ----------------------------------------------------------------- */
        ///
        /// Selected
        /// 
        /// <summary>
        /// ユーザの選択時に発生します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        event ValueEventHandler<NotifyComponents> Selected;

        /* ----------------------------------------------------------------- */
        ///
        /// Completed
        /// 
        /// <summary>
        /// 通知完了時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        event EventHandler Completed;
    }
}
