/* ------------------------------------------------------------------------- */
///
/// EventArgsFactory.cs
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
namespace Cube
{
    #region ValueEventArgs

    /* --------------------------------------------------------------------- */
    ///
    /// ValueEventArgs
    ///
    /// <summary>
    /// ValueEventArgs(T), ValueCancelEventArgs(T) オブジェクトを生成する
    /// ための補助クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class ValueEventArgs
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Create
        /// 
        /// <summary>
        /// ValueEventArgs(T) オブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static ValueEventArgs<T> Create<T>(T value)
            => new ValueEventArgs<T>(value);

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        /// 
        /// <summary>
        /// ValueCancelEventArgs(T) オブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static ValueCancelEventArgs<T> Create<T>(T value, bool cancel)
            => new ValueCancelEventArgs<T>(value, cancel);
    }

    #endregion

    #region KeyValueEventArgs

    /* --------------------------------------------------------------------- */
    ///
    /// KeyValueEventArgs
    ///
    /// <summary>
    /// KeyValueEventArgs(T, U), KeyValueCancelEventArgs(T, U)
    /// オブジェクトを生成するための補助クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class KeyValueEventArgs
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Create
        /// 
        /// <summary>
        /// ValueEventArgs(T) オブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static KeyValueEventArgs<T, U> Create<T, U>(T key, U value)
            => new KeyValueEventArgs<T, U>(key, value);

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        /// 
        /// <summary>
        /// ValueCancelEventArgs(T) オブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static KeyValueCancelEventArgs<T, U> Create<T, U>(T key, U value, bool cancel)
            => new KeyValueCancelEventArgs<T, U>(key, value, cancel);
    }

    #endregion

    #region ProcessEventArgs

    /* --------------------------------------------------------------------- */
    ///
    /// ProgressEventArgs
    ///
    /// <summary>
    /// ProgressEventArgs(T) オブジェクトを生成するための補助クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class ProgressEventArgs
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Create
        /// 
        /// <summary>
        /// ProgressEventArgs(T) オブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static ProgressEventArgs<T> Create<T>(double percentage, T value)
            => new ProgressEventArgs<T>(percentage, value);
    }

    #endregion
}
