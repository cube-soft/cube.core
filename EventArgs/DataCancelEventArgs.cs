/* ------------------------------------------------------------------------- */
///
/// DataCancelEventArgs.cs
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
using System.ComponentModel;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.DataCancelEventArgs
    ///
    /// <summary>
    /// イベントハンドラに特定の型のデータを渡すためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class DataCancelEventArgs<TData> : CancelEventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// DataCancelEventArgs
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DataCancelEventArgs(TData value, bool cancel = false)
            : base(cancel)
        {
            Value = value;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Value
        /// 
        /// <summary>
        /// データを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TData Value { get; private set; }

        #endregion
    }
}
