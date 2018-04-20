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
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Cube.Processes
{
    /* --------------------------------------------------------------------- */
    ///
    /// IMessengerServiceCallback
    ///
    /// <summary>
    /// IMessengerService からのメッセージの受信時に実行される
    /// コールバック関数を定義するためのインターフェースです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [ServiceContract]
    internal interface IMessengerServiceCallback
    {
        /* ----------------------------------------------------------------- */
        ///
        /// SendCallback
        ///
        /// <summary>
        /// データ送信時に実行されるメソッドです。
        /// </summary>
        ///
        /// <param name="bytes">送信データ</param>
        ///
        /* ----------------------------------------------------------------- */
        [OperationContract(IsOneWay = true)]
        void SendCallback(byte[] bytes);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// MessengerServiceCallback(T)
    ///
    /// <summary>
    /// IMessengerService からのメッセージの受信時に実行される
    /// コールバック関数を定義するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal class MessengerServiceCallback<T> : IMessengerServiceCallback where T : class
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// SendCallback
        ///
        /// <summary>
        /// 相手（サーバ or クライアント）からデータが送信された時に
        /// 実行されます。
        /// </summary>
        ///
        /// <param name="bytes">送信データ</param>
        ///
        /* ----------------------------------------------------------------- */
        public void SendCallback(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                var json = new DataContractSerializer(typeof(T));
                if (json.ReadObject(ms) is T value)
                {
                    foreach (var f in _subscriptions) f(value);
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        ///
        /// <summary>
        /// SendCallback で実行される処理を登録します。
        /// </summary>
        ///
        /// <param name="action">処理を表すオブジェクト</param>
        ///
        /// <returns>登録解除用オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public IDisposable Subscribe(Action<T> action)
        {
            _subscriptions.Add(action);
            return Disposable.Create(() => _subscriptions.Remove(action));
        }

        #endregion

        #region Fields
        private readonly List<Action<T>> _subscriptions = new List<Action<T>>();
        #endregion
    }
}
