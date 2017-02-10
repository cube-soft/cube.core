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
using System.Drawing;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// NotifyLevel
    /// 
    /// <summary>
    /// 通知した項目の重要度を示す値を定義した列挙体です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum NotifyLevel : int
    {
        None        = 0,
        Debug       = 1,
        Information = 2,
        Important   = 3,
        Warning     = 4,
        Error       = 5,
    }

    /* --------------------------------------------------------------------- */
    ///
    /// NotifyItem
    /// 
    /// <summary>
    /// 通知内容を保持するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class NotifyItem
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Level
        /// 
        /// <summary>
        /// 通知内容の重要度を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public NotifyLevel Level { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Title
        /// 
        /// <summary>
        /// 通知内容のタイトルを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Title { get; set; }


        /* ----------------------------------------------------------------- */
        ///
        /// Description
        /// 
        /// <summary>
        /// 通知内容の本文を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Description { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Image
        /// 
        /// <summary>
        /// 通知内容を表すイメージを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Image Image { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// DisplayTime
        /// 
        /// <summary>
        /// 通知内容の表示時間を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TimeSpan DisplayTime { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// InitialDelay
        /// 
        /// <summary>
        /// 通知内容の表示を遅延させる時間を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TimeSpan InitialDelay { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Data
        /// 
        /// <summary>
        /// ユーザデータを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public object Data { get; set; }

        #endregion
    }
}
