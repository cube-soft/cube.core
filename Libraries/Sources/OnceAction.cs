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
    /// OnceAction
    ///
    /// <summary>
    /// Provides functionality to invoke the specified action once.
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
        /// Initializes a new instance of the OnceAction class with the
        /// specified action.
        /// </summary>
        ///
        /// <param name="action">Action that is invoked once.</param>
        ///
        /* ----------------------------------------------------------------- */
        public OnceAction(Action action)
        {
            _action = action ?? throw new ArgumentNullException();
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// IgnoreTwice
        ///
        /// <summary>
        /// Gets or sets a value indicating whether to ignore the second
        /// action.
        /// </summary>
        ///
        /// <remarks>
        /// false に設定した場合、2 回目以降の実行時に TwiceException が
        /// 送出されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public bool IgnoreTwice { get; set; } = true;

        /* ----------------------------------------------------------------- */
        ///
        /// Invoked
        ///
        /// <summary>
        /// Gets a value indicating whether the provided action has been
        /// already invoked.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Invoked => _action == null;

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the specified action.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Invoke()
        {
            if (!Invoked)
            {
                var dest = Interlocked.Exchange(ref _action, null);
                if (dest != null)
                {
                    dest();
                    return;
                }
            }

            if (!IgnoreTwice) throw new TwiceException();
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
    /// Initializes a new instance of the OnceAction class with the
    /// specified action.
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
        /// Initializes a new instance of the OnceAction class with the
        /// specified action.
        /// </summary>
        ///
        /// <param name="action">Action that is invoked once.</param>
        ///
        /* ----------------------------------------------------------------- */
        public OnceAction(Action<T> action)
        {
            _action = action ?? throw new ArgumentNullException();
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// IgnoreTwice
        ///
        /// <summary>
        /// Gets or sets a value indicating whether to ignore the second
        /// action.
        /// </summary>
        ///
        /// <remarks>
        /// false に設定した場合、2 回目以降の実行時に TwiceException が
        /// 送出されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public bool IgnoreTwice { get; set; } = true;

        /* ----------------------------------------------------------------- */
        ///
        /// Invoked
        ///
        /// <summary>
        /// Gets a value indicating whether the provided action has been
        /// already invoked.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Invoked => _action == null;

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the specified action with the specified arguments.
        /// </summary>
        ///
        /// <param name="obj">Arguments of the action.</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Invoke(T obj)
        {
            if (!Invoked)
            {
                var dest = Interlocked.Exchange(ref _action, null);
                if (dest != null)
                {
                    dest(obj);
                    return;
                }
            }

            if (!IgnoreTwice) throw new TwiceException();
        }

        #endregion

        #region Fields
        private Action<T> _action;
        #endregion
    }
}
