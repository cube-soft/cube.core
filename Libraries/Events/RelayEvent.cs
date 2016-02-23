/* ------------------------------------------------------------------------- */
///
/// RelayEvent.cs
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
    /// RelayEvent
    /// 
    /// <summary>
    /// イベントを中継するためのクラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public class RelayEvent
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Raise
        /// 
        /// <summary>
        /// イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Raise() => Raise(this);

        /* ----------------------------------------------------------------- */
        ///
        /// Raise
        /// 
        /// <summary>
        /// イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Raise(object sender) => Handle?.Invoke(sender, EventArgs.Empty);

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// Handle
        /// 
        /// <summary>
        /// Raise によって発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler Handle;

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// RelayEvent
    /// 
    /// <summary>
    /// イベントを中継するためのクラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public class RelayEvent<TEvent> where TEvent : EventArgs
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Raise
        /// 
        /// <summary>
        /// イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Raise(TEvent args)
            => Raise(this, args);

        /* ----------------------------------------------------------------- */
        ///
        /// Raise
        /// 
        /// <summary>
        /// イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Raise(object sender, TEvent args)
            => Handle?.Invoke(sender, args);

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// Handle
        /// 
        /// <summary>
        /// Raise によって発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler<TEvent> Handle;

        #endregion
    }
}
