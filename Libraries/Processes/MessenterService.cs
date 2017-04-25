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
using System.ServiceModel;

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

        /* ----------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// サーバにデータを送信します。
        /// </summary>
        /// 
        /// <param name="bytes">送信データ</param>
        ///
        /* ----------------------------------------------------------------- */
        [OperationContract(IsOneWay = true)]
        void Send(byte[] bytes);

        /* ----------------------------------------------------------------- */
        ///
        /// SendToClient
        ///
        /// <summary>
        /// クライアントにデータを送信します。
        /// </summary>
        /// 
        /// <param name="bytes">送信データ</param>
        ///
        /* ----------------------------------------------------------------- */
        [OperationContract(IsOneWay = true)]
        void SendToClient(byte[] bytes);
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
        #region Methods

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
            if (channel == null || _clients.Contains(channel)) return;
            _clients.Add(channel);
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
            _clients.Remove(channel);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// サーバにデータを送信します。
        /// </summary>
        /// 
        /// <param name="bytes">送信データ</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Send(byte[] bytes) => _callback.SendCallback(bytes);

        /* ----------------------------------------------------------------- */
        ///
        /// SendToClient
        ///
        /// <summary>
        /// クライアントにデータを送信します。
        /// </summary>
        /// 
        /// <param name="bytes">送信データ</param>
        ///
        /* ----------------------------------------------------------------- */
        public void SendToClient(byte[] bytes)
        {
            var aborts = new List<IMessengerServiceCallback>();
            foreach (var x in _clients)
            {
                try { x.SendCallback(bytes); }
                catch (CommunicationObjectAbortedException /* err */) { aborts.Add(x); }
                catch (Exception err) { System.Diagnostics.Trace.WriteLine(err.ToString()); }
            }

            foreach (var c in aborts) _clients.Remove(c);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        ///
        /// <summary>
        /// Send で実行される処理を登録します。
        /// </summary>
        /// 
        /// <param name="action">処理を表すオブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Subscribe(Action<TValue> action)
            => _callback.Subscribe(action);

        /* ----------------------------------------------------------------- */
        ///
        /// Unsubscribe
        ///
        /// <summary>
        /// 登録した処理を解除します。
        /// </summary>
        /// 
        /// <param name="action">処理を表すオブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Unsubscribe(Action<TValue> action)
            => _callback.Unsubscribe(action);

        #endregion

        #region Fields
        private MessengerServiceCallback<TValue> _callback = new MessengerServiceCallback<TValue>();
        private List<IMessengerServiceCallback> _clients = new List<IMessengerServiceCallback>();
        #endregion
    }
}
