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
using System.IO;
using System.ServiceModel;
using System.Runtime.Serialization.Json;

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
    /// MessengerServiceCallback(TValue)
    ///
    /// <summary>
    /// IMessengerService からのメッセージの受信時に実行される
    /// コールバック関数を定義するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal class MessengerServiceCallback<TValue> : IMessengerServiceCallback where TValue : class
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// MessengerServiceCallback
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        /// 
        /// <param name="callback">コールバック時に実行される処理</param>
        ///
        /* ----------------------------------------------------------------- */
        public MessengerServiceCallback(Action<TValue> callback)
        {
            _callback = callback;
        }

        #endregion

        #region Methods

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
        public void SendCallback(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                var json = new DataContractJsonSerializer(typeof(TValue));
                if (json.ReadObject(ms) is TValue value) _callback(value);
            }
        }

        #endregion

        #region Fields
        private Action<TValue> _callback;
        #endregion
    }
}
