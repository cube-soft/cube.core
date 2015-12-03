﻿/* ------------------------------------------------------------------------- */
///
/// ProgressEventArgs.cs
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

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.ProgressEventArgs
    ///
    /// <summary>
    /// 進捗情報を保持するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ProgressEventArgs : EventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ProgressEventArgs
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public ProgressEventArgs(int value, string message) : base()
        {
            Value = value;
            Message = message;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Value
        /// 
        /// <summary>
        /// 進捗状況を取得します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public int Value { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Message
        /// 
        /// <summary>
        /// 進捗に関するメッセージを取得します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public string Message { get; }

        #endregion
    }
}