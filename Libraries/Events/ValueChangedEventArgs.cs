/* ------------------------------------------------------------------------- */
///
/// ValueChangedEventArgs.cs
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
    /// ValueChangedEventArgs(TValue)
    /// 
    /// <summary>
    /// 値の変更に関連するイベントに使用するクラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public class ValueChangedEventArgs<TValue> : EventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ValueChangedEventArgs
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ValueChangedEventArgs(TValue before, TValue after)
        {
            OldValue = before;
            NewValue = after;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// OldValue
        /// 
        /// <summary>
        /// 変更前のオブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TValue OldValue { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// NewValue
        /// 
        /// <summary>
        /// 変更後のオブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TValue NewValue { get; }

        #endregion
    }
}
