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
using System.Threading;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// Disposable
    /// 
    /// <summary>
    /// IDisposable オブジェクトを生成するためのクラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public static class Disposable
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Create
        /// 
        /// <summary>
        /// IDisposable オブジェクトを生成します。
        /// </summary>
        /// 
        /// <param name="dispose">Dispose 時に実行する動作</param>
        /// 
        /// <returns>IDisposable オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IDisposable Create(Action dispose)
        {
            if (dispose == null) throw new ArgumentException(nameof(dispose));
            return new AnonymousDisposable(dispose);
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// AnonymousDisposable
    /// 
    /// <summary>
    /// Dispose 時に特定の動作を実行するためのクラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    internal sealed class AnonymousDisposable : IDisposable
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// AnonymousDisposable
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        /// 
        /// <param name="dispose">Dispose 時に実行する動作</param>
        ///
        /* ----------------------------------------------------------------- */
        public AnonymousDisposable(Action dispose)
        {
            System.Diagnostics.Debug.Assert(dispose != null);
            _dispose = dispose;
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        /// 
        /// <summary>
        /// 設定された動作を実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Dispose() => Interlocked.Exchange(ref _dispose, null)?.Invoke();

        #endregion

        #region Fields
        private Action _dispose;
        #endregion
    }
}
