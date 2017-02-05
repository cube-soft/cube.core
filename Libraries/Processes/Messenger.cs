/* ------------------------------------------------------------------------- */
///
/// Messenger.cs
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
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting.Lifetime;

namespace Cube.Processes
{
    /* --------------------------------------------------------------------- */
    ///
    /// Messenger(TValue)
    /// 
    /// <summary>
    /// プロセス間通信 (IPC: Inter-Process Communication) を行うクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Messenger<TValue> : IDisposable
    {
        #region Constructors and destructors

        /* ----------------------------------------------------------------- */
        ///
        /// Messenger
        /// 
        /// <summary>
        /// 静的オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        static Messenger()
        {
            LifetimeServices.LeaseTime = TimeSpan.Zero;
            LifetimeServices.RenewOnCallTime = TimeSpan.Zero;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Messenger
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Messenger(string host, string path)
        {
            Host = host;
            Path = path;

            Register();
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Host
        /// 
        /// <summary>
        /// プロセス間通信の際のホスト名となる文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Host { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Path
        /// 
        /// <summary>
        /// プロセス間通信の際のパス名となる文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Path { get; }

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

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// Received
        /// 
        /// <summary>
        /// 他のプロセスからメッセージを受信した時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler<ValueEventArgs<TValue>> Received;

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
        public void Send(TValue args) => _core.Send(args);

        #endregion

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
            _disposed = true;

            if (disposing) _mutex.Close();
        }

        #endregion

        #region Virtual methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnReceived
        /// 
        /// <summary>
        /// Received イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnReceived(ValueEventArgs<TValue> e)
            => Received?.Invoke(this, e);

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// Register
        /// 
        /// <summary>
        /// IPC チャンネルに登録します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Register()
        {
            _mutex   = new System.Threading.Mutex(false, Host);
            IsServer = _mutex.WaitOne(0, false);
            _core    = IsServer ?
                       CreateServer() :
                       CreateServer();

            _core.Received += (s, e) => OnReceived(e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CreateServer
        /// 
        /// <summary>
        /// サーバ用 IPC チャンネルを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private IpcRemoteObject CreateServer()
        {
            var dest    = new IpcRemoteObject();
            var channel = new IpcServerChannel(Host);

            ChannelServices.RegisterChannel(channel, true);
            RemotingServices.Marshal(dest, Path, typeof(IpcRemoteObject));

            return dest;
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
        private IpcRemoteObject CreateClient()
        {
            var channel = new IpcClientChannel();
            var url     = $"ipc://{Host}/{Path}";

            ChannelServices.RegisterChannel(channel, true);

            return Activator.GetObject(typeof(IpcRemoteObject), url) as IpcRemoteObject;
        }

        #endregion

        #region Internal class

        public class IpcRemoteObject : MarshalByRefObject
        {
            public event EventHandler<ValueEventArgs<TValue>> Received;
            public void Send(TValue args) => Received?.Invoke(this, ValueEventArgs.Create(args));
            public override object InitializeLifetimeService() => null;
        }

        #endregion

        #region Fields
        private bool _disposed = false;
        private System.Threading.Mutex _mutex = null;
        private IpcRemoteObject _core = null;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Messenger
    /// 
    /// <summary>
    /// プロセス間通信 (IPC: Inter-Process Communication) を行うクラスです。
    /// </summary>
    /// 
    /// <remarks>
    /// Messenger(object) で特殊化したクラスです。Send で送信するオブジェクトの
    /// 型が一意に決定できない場合などに使用します。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class Messenger : Messenger<object>
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Messenger
        /// 
        /// <summary>
        /// 静的オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Messenger(string host, string path) : base(host, path) { }
    }
}
