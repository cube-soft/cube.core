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
using System.Threading;

namespace Cube.Processes
{
    /* --------------------------------------------------------------------- */
    ///
    /// Messenger(TValue)
    /// 
    /// <summary>
    /// プロセス間通信を実現するためのクラスです。
    /// </summary>
    /// 
    /// <remarks>
    /// TValue には DataContract 属性が指定されている必要があります。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class Messenger<TValue> : IDisposable where TValue : class
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
        /// <param name="callback">データ受信時に実行される処理</param>
        /// 
        /* ----------------------------------------------------------------- */
        public Messenger(string id, Action<TValue> callback)
        {
            _callback = callback;
            _address  = new Uri($"net.pipe://localhost/{id}");
            _mutex    = new Mutex(false, id);
            IsServer  = _mutex.WaitOne(0, false);

            if (IsServer) CreateServer();
            else CreateClient();
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
        /// Send
        /// 
        /// <summary>
        /// 他のプロセスにメッセージを送信します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Send(TValue value)
        {
            using (var ms = new MemoryStream())
            {
                var json = new DataContractJsonSerializer(typeof(TValue));
                json.WriteObject(ms, value);

                var bytes = ms.ToArray();
                if (IsServer) _service.SendToClient(bytes);
                else _service.SendToServer(bytes);
            }
        }

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
                if (IsServer)
                {
                    if (_context is ServiceHost s) s.Close();
                }
                else
                {
                    _service?.Disconnect();
                    if (_context is InstanceContext c) c.Close();
                }
                _context = null;
                _mutex?.Close();
            }

            _disposed = true;
        }

        #endregion

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// CreateServer
        /// 
        /// <summary>
        /// サーバ用 IPC チャンネルを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void CreateServer()
        {
            var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            _service = new MessengerService<TValue>(_callback);

            var context = new ServiceHost(_service);
            context.AddServiceEndpoint(typeof(IMessengerService), binding, _address);
            context.Open();

            _context = context;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CreateClient
        /// 
        /// <summary>
        /// クライアント用 IPC チャンネルを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void CreateClient()
        {
            var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            var context = new InstanceContext(new MessengerServiceCallback<TValue>(_callback));

            _service = DuplexChannelFactory<IMessengerService>
                       .CreateChannel(context, binding, new EndpointAddress(_address));
            _service.Connect();
            _context = context;
        }

        #region Fields
        private bool _disposed = false;
        private Mutex _mutex = null;
        private object _context = null;
        private IMessengerService _service = null;
        private Action<TValue> _callback;
        private Uri _address = null;
        #endregion

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Bootstrap
    ///
    /// <summary>
    /// プロセス間通信によってプロセスの起動確認およびアクティブ化を
    /// 行うためのクラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public class Bootstrap : Messenger<string[]>
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Bootstrap
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        /// 
        /// <param name="id">
        /// プロセス通信を実行するための識別子
        /// </param>
        /// 
        /// <remarks>
        /// このクラスは "ipc://{name}/activate" と言う URI を基に
        /// プロセス間通信を実現します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public Bootstrap(string id, Action<string[]> callback)
            : base($"{id}/activate", callback) { }

        /* ----------------------------------------------------------------- */
        ///
        /// Exists
        /// 
        /// <summary>
        /// 同じ名前を持つプロセスが既に存在するかどうかを判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Exists => !IsServer;
    }
}
