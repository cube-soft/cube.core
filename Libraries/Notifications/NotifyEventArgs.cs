/* ------------------------------------------------------------------------- */
///
/// NotifyEventArgs.cs
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
    /// NotifyEventArgs
    /// 
    /// <summary>
    /// 通知フォームで表示された内容を保持するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class NotifyEventArgs : EventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// NotifyEventArgs
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public NotifyEventArgs(NotifyLevel level, string title, string description, Image image, object data = null)
            : base()
        {
            Level       = level;
            Title       = title;
            Description = description;
            Image       = image;
            Data        = data;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Level
        /// 
        /// <summary>
        /// 重要度を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public NotifyLevel Level { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Title
        /// 
        /// <summary>
        /// タイトルを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Title { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Description
        /// 
        /// <summary>
        /// 本文を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Description { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Image
        /// 
        /// <summary>
        /// イメージを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Image Image { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Data
        /// 
        /// <summary>
        /// ユーザデータを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public object Data { get; private set; }

        #endregion
    }
}
