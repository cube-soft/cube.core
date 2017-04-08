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
using System.IO;
using System.ServiceModel;
using System.Runtime.Serialization.Json;

namespace Cube.Processes
{
    /* --------------------------------------------------------------------- */
    ///
    /// IMessengerService
    ///
    /// <summary>
    /// プロセス間でメッセージをやり取りするサービスを表す
    /// インターフェースです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [ServiceContract(CallbackContract = typeof(IMessengerServiceCallback))]
    internal interface IMessengerService
    {
        /* ----------------------------------------------------------------- */
        ///
        /// SendToServer
        ///
        /// <summary>
        /// サーバにメッセージを送信します。
        /// </summary>
        /// 
        /// <param name="bytes">送信データ</param>
        ///
        /* ----------------------------------------------------------------- */
        [OperationContract(IsOneWay = true)]
        void SendToServer(byte[] bytes);

        /* ----------------------------------------------------------------- */
        ///
        /// SendToClient
        ///
        /// <summary>
        /// クライアントにメッセージを送信します。
        /// </summary>
        /// 
        /// <param name="bytes">送信データ</param>
        ///
        /* ----------------------------------------------------------------- */
        [OperationContract(IsOneWay = true)]
        void SendToClient(byte[] bytes);

        /* ----------------------------------------------------------------- */
        ///
        /// Connect
        ///
        /// <summary>
        /// サーバに接続します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [OperationContract(IsOneWay = true)]
        void Connect();

        /* ----------------------------------------------------------------- */
        ///
        /// Disconnect
        ///
        /// <summary>
        /// サーバとの接続を解除します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [OperationContract(IsOneWay = true)]
        void Disconnect();
    }

    /* --------------------------------------------------------------------- */
    ///
    /// MessengerService(TValue)
    ///
    /// <summary>
    /// プロセス間でメッセージをやり取りするサービスを表すクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    internal class MessengerService<TValue> : IMessengerService where TValue : class
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
        public MessengerService(Action<TValue> callback)
        {
            _callback = callback;
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// SendToServer
        ///
        /// <summary>
        /// サーバにメッセージを送信します。
        /// </summary>
        /// 
        /// <param name="bytes">送信データ</param>
        ///
        /* ----------------------------------------------------------------- */
        public void SendToServer(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                var json = new DataContractJsonSerializer(typeof(TValue));
                if (json.ReadObject(ms) is TValue value) _callback(value);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SendToClient
        ///
        /// <summary>
        /// クライアントにメッセージを送信します。
        /// </summary>
        /// 
        /// <param name="bytes">送信データ</param>
        ///
        /* ----------------------------------------------------------------- */
        public void SendToClient(byte[] bytes)
        {
            var aborts = new List<IMessengerServiceCallback>();
            foreach (var x in _subscriptions)
            {
                try { x.SendCallback(bytes); }
                catch (CommunicationObjectAbortedException /* err */) { aborts.Add(x); }
                catch (Exception err) { System.Diagnostics.Trace.WriteLine(err.ToString()); }
            }

            foreach (var c in aborts) _subscriptions.Remove(c);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Connect
        ///
        /// <summary>
        /// サーバに接続します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Connect()
        {
            var channel = OperationContext.Current.GetCallbackChannel<IMessengerServiceCallback>();
            if (channel == null || _subscriptions.Contains(channel)) return;
            _subscriptions.Add(channel);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Disconnect
        ///
        /// <summary>
        /// 接続を解除します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Disconnect()
        {
            var channel = OperationContext.Current.GetCallbackChannel<IMessengerServiceCallback>();
            if (channel == null) return;
            _subscriptions.Remove(channel);
        }

        #endregion

        #region Fields
        private Action<TValue> _callback;
        private List<IMessengerServiceCallback> _subscriptions = new List<IMessengerServiceCallback>();
        #endregion
    }
}
