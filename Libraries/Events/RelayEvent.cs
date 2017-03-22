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
using System.Collections.Generic;

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
        /// Publish
        /// 
        /// <summary>
        /// 購読者にイベントを配信します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Publish()
        {
            foreach (var action in _subscriptions) action();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        /// 
        /// <summary>
        /// イベント発生時に実行する Action オブジェクトを登録します。
        /// </summary>
        /// 
        /// <param name="action">登録する Action オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Subscribe(Action action)
            => _subscriptions.Add(action);

        /* ----------------------------------------------------------------- */
        ///
        /// Unsubscribe
        /// 
        /// <summary>
        /// 登録されている Action オブジェクトを削除します。
        /// </summary>
        /// 
        /// <param name="action">削除する Action オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Unsubscribe(Action action)
        {
            if (!_subscriptions.Contains(action)) return;
            _subscriptions.Remove(action);
        }

        #endregion

        #region Fields
        private ICollection<Action> _subscriptions = new List<Action>();
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// RelayEvent(TPayload)
    /// 
    /// <summary>
    /// イベントを中継するためのクラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public class RelayEvent<TPayload>
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Publish
        /// 
        /// <summary>
        /// 購読者にイベントを配信します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Publish(TPayload payload)
        {
            foreach (var action in _subscriptions) action(payload);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        /// 
        /// <summary>
        /// イベント発生時に実行する Action オブジェクトを登録します。
        /// </summary>
        /// 
        /// <param name="action">登録する Action オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Subscribe(Action<TPayload> action)
            => _subscriptions.Add(action);

        /* ----------------------------------------------------------------- */
        ///
        /// Unsubscribe
        /// 
        /// <summary>
        /// 登録されている Action オブジェクトを削除します。
        /// </summary>
        /// 
        /// <param name="action">削除する Action オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public void UnSubscribe(Action<TPayload> action)
        {
            if (!_subscriptions.Contains(action)) return;
            _subscriptions.Remove(action);
        }

        #endregion

        #region Fields
        private ICollection<Action<TPayload>> _subscriptions = new List<Action<TPayload>>();
        #endregion
    }
}
