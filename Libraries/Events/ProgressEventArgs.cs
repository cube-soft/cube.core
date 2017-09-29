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

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// ProgressEventArgs(TValue)
    ///
    /// <summary>
    /// 進捗状況を保持するためのクラスです。
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
        /// <param name="ratio">進捗状況（パーセンテージ等）</param>
        /// <param name="value">ユーザデータ</param>
        /// 
        /* ----------------------------------------------------------------- */
        public ProgressEventArgs(double ratio, TValue value) : base()
        {
            Ratio = ratio;
            Value = value;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Ratio
        /// 
        /// <summary>
        /// 進捗状況を表す値を取得します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public double Ratio { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Value
        /// 
        /// <summary>
        /// ユーザデータを取得します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public TValue Value { get; }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// ProgressEventArgs
    ///
    /// <summary>
    /// ProgressEventArgs(T) オブジェクトを生成するための補助クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class ProgressEventArgs
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Create
        /// 
        /// <summary>
        /// ProgressEventArgs(T) オブジェクトを生成します。
        /// </summary>
        /// 
        /// <param name="ratio">進捗状況（パーセンテージ等）</param>
        /// <param name="value">ユーザデータ</param>
        ///
        /* ----------------------------------------------------------------- */
        public static ProgressEventArgs<T> Create<T>(double ratio, T value)
            => new ProgressEventArgs<T>(ratio, value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// ProgressEventHandler(TValue)
    /// 
    /// <summary>
    /// イベントを処理するメソッドを表します。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public delegate void ProgressEventHandler<TValue>(object sender, ProgressEventArgs<TValue> e);
}
