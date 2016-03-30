/* ------------------------------------------------------------------------- */
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
    /// ProgressEventArgs(TValue)
    ///
    /// <summary>
    /// 進捗情報を保持するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ProgressEventArgs<TValue> : EventArgs
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
        public ProgressEventArgs(double percentage, TValue value) : base()
        {
            Percentage = percentage;
            Value = value;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Percentage
        /// 
        /// <summary>
        /// 進捗状況を百分率で取得します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public double Percentage { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Value
        /// 
        /// <summary>
        /// ユーザから指定された値を取得します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public TValue Value { get; }

        #endregion
    }
}
