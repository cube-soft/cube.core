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
using System.Runtime.Serialization;
using System.Threading;
using Cube.Log;

namespace Cube.Processes
{
    /* --------------------------------------------------------------------- */
    ///
    /// IMessenger(TValue)
    /// 
    /// <summary>
    /// 相互通信を表すインターフェースです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public interface IMessenger<TValue> : IDisposable
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Publish
        /// 
        /// <summary>
        /// データを送信します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        void Publish(TValue value);

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        ///
        /// <summary>
        /// 相手からのデータ送信時に実行される処理を登録します。
        /// </summary>
        /// 
        /// <param name="action">処理を表すオブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        void Subscribe(Action<TValue> action);

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
        void Unsubscribe(Action<TValue> action);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Messenger(TValue)
    /// 
    /// <summary>
    /// プロセス間等で通信を実現するためのクラスです。
    /// 最初に初期化されたオブジェクトがサーバ、それ以降のオブジェクト
    /// がクライアントとして機能します。
    /// </summary>
    /// 
    /// <remarks>
    /// サーバが既に存在しているかどうかを Mutex を用いて判別しているため、
    /// 同じスレッドで Messenger オブジェクトを複数作成するとエラーが
    /// 発生する場合があります。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class Messenger<TValue> : IMessenger<TValue> where TValue : class
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Messenger
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        /// 
        /// <param name="id">識別子</param>
        /// 
        /* ----------------------------------------------------------------- */
        public Messenger(string id)
        {
            _mutex = new Mutex(false, id);
            IsServer = _mutex.WaitOne(0, false);

            if (IsServer) _core = new MessengerServer<TValue>(id);
            else _core = new MessengerClient<TValue>(id);
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// IsServer
        /// 
        /// <summary>
        /// サーバとして動作しているかどうかを示す値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IsServer { get; private set; } = false;

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Publish
        /// 
        /// <summary>
        /// データを送信します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Publish(TValue value) => _core?.Publish(value);

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        ///
        /// <summary>
        /// 相手からのデータ送信時に実行される処理を登録します。
        /// </summary>
        /// 
        /// <param name="action">処理を表すオブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Subscribe(Action<TValue> action) => _core?.Subscribe(action);

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
        public void Unsubscribe(Action<TValue> action) => _core?.Unsubscribe(action);

        #region IDisposable

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        /// 
        /// <summary>
        /// オブジェクトを破棄します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        /// 
        /// <summary>
        /// オブジェクトを破棄します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _core?.Dispose();
                _mutex?.Close();
            }

            _disposed = true;
        }

        #endregion

        #endregion

        #region Fields
        private bool _disposed = false;
        private Mutex _mutex = null;
        private IMessenger<TValue> _core;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// MessengerServer(TValue)
    /// 
    /// <summary>
    /// プロセス間等で通信を実現するためのサーバクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class MessengerServer<TValue> : IMessenger<TValue> where TValue : class
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// MessengerServer
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        /// 
        /// <param name="id">識別子</param>
        /// 
        /* ----------------------------------------------------------------- */
        public MessengerServer(string id)
        {
            var address = new Uri($"net.pipe://localhost/{id}");
            var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            _service = new MessengerService<TValue>();

            _host = new ServiceHost(_service);
            _host.AddServiceEndpoint(typeof(IMessengerService), binding, address);
            _host.Open();
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Publish
        /// 
        /// <summary>
        /// クライアントにデータを送信します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Publish(TValue value)
        {
            using (var ms = new MemoryStream())
            {
                var json = new DataContractSerializer(typeof(TValue));
                json.WriteObject(ms, value);
                _service.SendToClient(ms.ToArray());
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        ///
        /// <summary>
        /// クライアントからのデータ送信時に実行される処理を登録します。
        /// </summary>
        /// 
        /// <param name="action">処理を表すオブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Subscribe(Action<TValue> action)
            => _service.Subscribe(action);

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
            => _service.Unsubscribe(action);

        #region IDisposable

        /* ----------------------------------------------------------------- */
        ///
        /// ~MessengerServer
        /// 
        /// <summary>
        /// オブジェクトを破棄します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        ~MessengerServer()
        {
            Dispose(false);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        /// 
        /// <summary>
        /// リソースを解放します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        /// 
        /// <summary>
        /// リソースを解放します。
        /// </summary>
        /// 
        /// <param name="disposing">
        /// マネージリソースを解放するかどうかを示す値
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing) _host?.Abort();

            _disposed = true;
        }

        #endregion

        #endregion

        #region Fields
        private bool _disposed = false;
        private MessengerService<TValue> _service;
        private ServiceHost _host;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// MessengerClient(TValue)
    /// 
    /// <summary>
    /// プロセス間等で通信を実現するためのクライアントクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class MessengerClient<TValue> : IMessenger<TValue> where TValue : class
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// MessengerClient
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        /// 
        /// <param name="id">識別子</param>
        /// 
        /* ----------------------------------------------------------------- */
        public MessengerClient(string id)
        {
            var uri     = new Uri($"net.pipe://localhost/{id}");
            var address = new EndpointAddress(uri);
            var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);

            _callback = new MessengerServiceCallback<TValue>();
            _context  = new InstanceContext(_callback);
            _factory  = new DuplexChannelFactory<IMessengerService>(_callback, binding, address);

            Recreate();
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Publish
        /// 
        /// <summary>
        /// クライアントにデータを送信します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Publish(TValue value)
        {
            var channel = _service as IClientChannel;
            if (channel.State != CommunicationState.Opened) Recreate();

            using (var ms = new MemoryStream())
            {
                var json = new DataContractSerializer(typeof(TValue));
                json.WriteObject(ms, value);
                _service.Send(ms.ToArray());
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        ///
        /// <summary>
        /// サーバからのデータ送信時に実行される処理を登録します。
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

        #region IDisposable

        /* ----------------------------------------------------------------- */
        ///
        /// ~MessengerServer
        /// 
        /// <summary>
        /// オブジェクトを破棄します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        ~MessengerClient()
        {
            Dispose(false);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        /// 
        /// <summary>
        /// リソースを解放します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        /// 
        /// <summary>
        /// リソースを解放します。
        /// </summary>
        /// 
        /// <param name="disposing">
        /// マネージリソースを解放するかどうかを示す値
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                (_service as IClientChannel)?.Abort();
                _service = null;
                _context?.Abort();
                _factory?.Abort();
            }

            _disposed = true;
        }

        #endregion

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// WhenRecreate
        ///
        /// <summary>
        /// チャンネル再生成時に実行されるハンドラです。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void WhenRecreate(object s, EventArgs e) => Recreate();

        /* ----------------------------------------------------------------- */
        ///
        /// Recreate
        ///
        /// <summary>
        /// チャンネルを再生成します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void Recreate()
        {
            try
            {
                if (_service != null)
                {
                    var ch0 = _service as IClientChannel;
                    ch0.Faulted -= WhenRecreate;
                    ch0.Closed  -= WhenRecreate;
                    ch0.Abort();
                }

                _service = _factory.CreateChannel();

                var ch1 = _service as IClientChannel;
                ch1.Faulted += WhenRecreate;
                ch1.Closed  += WhenRecreate;

                _service.Connect();
            }
            catch (Exception err) { this.LogWarn(err.ToString()); }
        }

        #region Fields
        private bool _disposed = false;
        private ChannelFactory<IMessengerService> _factory;
        private IMessengerService _service;
        private InstanceContext _context;
        private MessengerServiceCallback<TValue> _callback;
        #endregion

        #endregion
    }
}
