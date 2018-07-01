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
using System.Diagnostics;
using System.Threading;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// OnceAction
    ///
    /// <summary>
    /// 登録された内容を一度だけ実行するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class OnceAction
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// OnceAction
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="action">一度だけ実行する内容</param>
        ///
        /* ----------------------------------------------------------------- */
        public OnceAction(Action action)
        {
            Debug.Assert(action != null);
            _action = action;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// IgnoreTwice
        ///
        /// <summary>
        /// 2 回目以降の実行を無視するかどうかを示す値を取得または
        /// 設定します。
        /// </summary>
        ///
        /// <remarks>
        /// false に設定した場合、2 回目以降の実行時に TwiceException が
        /// 送出されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public bool IgnoreTwice { get; set; } = true;

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Action を実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Invoke()
        {
            var dest = Interlocked.Exchange(ref _action, null);
            if (dest != null) dest();
            else if (!IgnoreTwice) throw new TwiceException();
        }

        #endregion

        #region Fields
        private Action _action;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// OnceAction(T)
    ///
    /// <summary>
    /// 登録された内容を一度だけ実行するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class OnceAction<T>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// OnceAction
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="action">一度だけ実行する内容</param>
        ///
        /* ----------------------------------------------------------------- */
        public OnceAction(Action<T> action)
        {
            Debug.Assert(action != null);
            _action = action;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// IgnoreTwice
        ///
        /// <summary>
        /// 2 回目以降の実行を無視するかどうかを示す値を取得または
        /// 設定します。
        /// </summary>
        ///
        /// <remarks>
        /// false に設定した場合、2 回目以降の実行時に TwiceException が
        /// 送出されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public bool IgnoreTwice { get; set; } = true;

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Action(T) を実行します。
        /// </summary>
        ///
        /// <param name="obj">引数</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Invoke(T obj)
        {
            var dest = Interlocked.Exchange(ref _action, null);
            if (dest != null) dest(obj);
            else if (!IgnoreTwice) throw new TwiceException();
        }

        #endregion

        #region Fields
        private Action<T> _action;
        #endregion
    }
}
