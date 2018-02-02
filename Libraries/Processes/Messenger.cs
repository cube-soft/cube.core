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
using System.IO;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Threading;
using Cube.Log;

namespace Cube.Processes
{
    /* --------------------------------------------------------------------- */
    ///
    /// IMessenger(T)
    ///
    /// <summary>
    /// 相互通信を表すインターフェースです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public interface IMessenger<T> : IDisposable
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
        void Publish(T value);

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
        /// <returns>登録解除用オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        IDisposable Subscribe(Action<T> action);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Messenger(T)
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
    public class Messenger<T> : IMessenger<T> where T : class
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Messenger(T)
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
            _dispose = new OnceAction<bool>(Dispose);
            _mutex = new Mutex(false, id);
            IsServer = _mutex.WaitOne(0, false);

            if (IsServer) _core = new MessengerServer<T>(id);
            else _core = new MessengerClient<T>(id);
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
        public void Publish(T value) => _core.Publish(value);

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
        /// <returns>登録解除用オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public IDisposable Subscribe(Action<T> action) => _core.Subscribe(action);

        #region IDisposable

        /* ----------------------------------------------------------------- */
        ///
        /// ~Messenger
        ///
        /// <summary>
        /// オブジェクトを破棄します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        ~Messenger() { _dispose.Invoke(false); }

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
            _dispose.Invoke(true);
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
        /* ----------------------------------------------------------------- */
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _core.Dispose();
                _mutex.Close();
            }
        }

        #endregion

        #endregion

        #region Fields
        private OnceAction<bool> _dispose;
        private Mutex _mutex = null;
        private IMessenger<T> _core;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// MessengerServer(T)
    ///
    /// <summary>
    /// プロセス間等で通信を実現するためのサーバクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class MessengerServer<T> : IMessenger<T> where T : class
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// MessengerServer(T)
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
            _dispose = new OnceAction<bool>(Dispose);

            var address = new Uri($"net.pipe://localhost/{id}");
            var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            _service = new MessengerService<T>();

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
        public void Publish(T value)
        {
            using (var ms = new MemoryStream())
            {
                var dc = new DataContractSerializer(typeof(T));
                dc.WriteObject(ms, value);
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
        /// <returns>登録解除用オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public IDisposable Subscribe(Action<T> action) => _service.Subscribe(action);

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
        ~MessengerServer() { _dispose.Invoke(false); }

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
            _dispose.Invoke(true);
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
            if (disposing) _host.Abort();
        }

        #endregion

        #endregion

        #region Fields
        private OnceAction<bool> _dispose;
        private MessengerService<T> _service;
        private ServiceHost _host;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// MessengerClient(T)
    ///
    /// <summary>
    /// プロセス間等で通信を実現するためのクライアントクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class MessengerClient<T> : IMessenger<T> where T : class
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// MessengerClient(T)
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

            _dispose  = new OnceAction<bool>(Dispose);
            _callback = new MessengerServiceCallback<T>();
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
        public void Publish(T value)
        {
            var channel = _service as IClientChannel;
            if (channel.State != CommunicationState.Opened) Recreate();

            using (var ms = new MemoryStream())
            {
                var dc = new DataContractSerializer(typeof(T));
                dc.WriteObject(ms, value);
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
        /// <returns>登録解除用オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public IDisposable Subscribe(Action<T> action) => _callback.Subscribe(action);

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
        ~MessengerClient() { _dispose.Invoke(false); }

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
            _dispose.Invoke(true);
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
            if (disposing)
            {
                (_service as IClientChannel)?.Abort();
                _service = null;
                _context.Abort();
                _factory.Abort();
            }
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

        #endregion

        #region Fields
        private OnceAction<bool> _dispose;
        private ChannelFactory<IMessengerService> _factory;
        private IMessengerService _service;
        private InstanceContext _context;
        private MessengerServiceCallback<T> _callback;
        #endregion
    }
}
