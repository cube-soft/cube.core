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
using Cube.Generics;
using Cube.Log;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Reflection;

namespace Cube.Xui
{
    /* --------------------------------------------------------------------- */
    ///
    /// MessengerViewModel
    ///
    /// <summary>
    /// Messenger プロパティを保持する ViewModel です。
    /// </summary>
    ///
    /// <remarks>
    /// ViewModel の Messenger を Binding する事を前提とした Behavior を
    /// 数多く定義しているため、Messenger オブジェクトを外部からアクセス
    /// 可能にしています。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class MessengerViewModel : ViewModelBase
    {
        #region Constructors

        /* --------------------------------------------------------------------- */
        ///
        /// MessengerViewModel
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected MessengerViewModel(Assembly assembly) :
            this(assembly, GalaSoft.MvvmLight.Messaging.Messenger.Default) { }

        /* --------------------------------------------------------------------- */
        ///
        /// MessengerViewModel
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="assembly">アセンブリ情報</param>
        /// <param name="messenger">メッセージ伝搬用オブジェクト</param>
        ///
        /* --------------------------------------------------------------------- */
        protected MessengerViewModel(Assembly assembly, IMessenger messenger) : base(messenger)
        {
            Assembly = assembly;
        }

        #endregion

        #region Properties

        /* --------------------------------------------------------------------- */
        ///
        /// Messenger
        ///
        /// <summary>
        /// Messenger オブジェクトを取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public IMessenger Messenger => MessengerInstance;

        /* --------------------------------------------------------------------- */
        ///
        /// Assembly
        ///
        /// <summary>
        /// アセンブリ情報を取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected Assembly Assembly { get; }

        #endregion

        #region Methods

        /* --------------------------------------------------------------------- */
        ///
        /// Send(T)
        ///
        /// <summary>
        /// 空のメッセージを送信します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected void Send<T>() where T : new() => Messenger.Send(new T());

        /* --------------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// 表示用メッセージを送信します。
        /// </summary>
        ///
        /// <param name="src">表示用メッセージ</param>
        ///
        /* --------------------------------------------------------------------- */
        protected void Send(DialogMessage src) => Messenger.Send(src);

        /* --------------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// エラーメッセージを送信します。
        /// </summary>
        ///
        /// <param name="err">例外オブジェクト</param>
        ///
        /* --------------------------------------------------------------------- */
        protected void Send(Exception err) => Send(err, string.Empty);

        /* ----------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// 例外発生時にエラーメッセージを表示します。
        /// </summary>
        ///
        /// <param name="action">実行内容</param>
        ///
        /* ----------------------------------------------------------------- */
        protected void Send(Action action) => Send(action, string.Empty);

        /* ----------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// 例外発生時にエラーメッセージを表示します。
        /// </summary>
        ///
        /// <param name="action">実行内容</param>
        /// <param name="message">例外発生時のメッセージ</param>
        ///
        /* ----------------------------------------------------------------- */
        protected void Send(Action action, string message)
        {
            try { action(); }
            catch (Exception err) { Send(err, message); }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// エラーメッセージを送信します。
        /// </summary>
        ///
        /// <param name="err">例外オブジェクト</param>
        /// <param name="message">エラーメッセージ</param>
        ///
        /* --------------------------------------------------------------------- */
        protected void Send(Exception err, string message)
        {
            var ss = new System.Text.StringBuilder();
            if (message.HasValue())
            {
                ss.AppendLine(message);
                this.LogError(message);
            }
            this.LogError(err.ToString(), err);

            ss.Append($"{err.Message} ({err.GetType().Name})");
            Send(new DialogMessage(ss.ToString(), Assembly.GetReader().Title)
            {
                Button = System.Windows.MessageBoxButton.OK,
                Image  = System.Windows.MessageBoxImage.Error,
                Result = true,
            });
        }

        #endregion
    }
}
