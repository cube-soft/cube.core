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
using IoEx = System.IO;

namespace Cube.FileSystem
{
    /* --------------------------------------------------------------------- */
    ///
    /// FileHandler
    /// 
    /// <summary>
    /// ファイル操作を実行するクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class FileHandler
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// FileHandler
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public FileHandler() { }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Move
        ///
        /// <summary>
        /// ファイルを移動します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public void Move(string src, string dest, bool overwrite = false)
            => Execute(nameof(Move), () =>
        {
            if (!overwrite || !IoEx.File.Exists(dest)) IoEx.File.Move(src, dest);
            else
            {
                IoEx.File.Copy(src, dest, true);
                IoEx.File.Delete(src);
            }
        });

        /* ----------------------------------------------------------------- */
        ///
        /// Copy
        ///
        /// <summary>
        /// ファイルをコピーします。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public void Copy(string src, string dest, bool overwrite = false)
            => Execute(nameof(Copy), () => IoEx.File.Copy(src, dest, overwrite));

        /* ----------------------------------------------------------------- */
        ///
        /// Delete
        ///
        /// <summary>
        /// ファイルを削除します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public void Delete(string src)
            => Execute(nameof(Delete), () => IoEx.File.Delete(src));

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// Failed
        ///
        /// <summary>
        /// 操作に失敗した時に発生するイベントです。
        /// </summary>
        /// 
        /// <remarks>
        /// Key には失敗したメソッド名、Value には失敗した時に送出された例外
        /// オブジェクトが設定されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler<KeyValueCancelEventArgs<string, Exception>> Failed;

        #endregion

        #region Virtual methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnFailed
        ///
        /// <summary>
        /// Failed イベントを発生させます。
        /// </summary>
        /// 
        /// <remarks>
        /// Failed イベントにハンドラが設定されていない場合、Cancel を true に
        /// 設定します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnFailed(KeyValueCancelEventArgs<string, Exception> e)
        {
            if (Failed != null) Failed(this, e);
            else e.Cancel = true;
        }

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// Execute
        ///
        /// <summary>
        /// 各種操作を実行します。
        /// </summary>
        /// 
        /// <remarks>
        /// 操作に失敗した場合、イベントハンドラで Cancel が設定されるまで実行
        /// し続けます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private void Execute(string name, Action action)
        {
            var complete = false;
            while (!complete)
            {
                try
                {
                    action();
                    complete = true;
                }
                catch (Exception err)
                {
                    var args = KeyValueEventArgs.Create(name, err, false);
                    OnFailed(args);
                    complete = args.Cancel;
                }
            }
        }

        #endregion
    }
}
