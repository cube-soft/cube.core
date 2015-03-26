/* ------------------------------------------------------------------------- */
///
/// DelayAction.cs
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
using System.ComponentModel;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.DelayAction
    /// 
    /// <summary>
    /// 遅延実行を実現するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class DelayAction
    {
        #region Constructors

        /* --------------------------------------------------------------------- */
        ///
        /// DelayAction
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public DelayAction()
        {
            PollingInterval = 100; // 100ms
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += Worker_DoWork;
        }

        #endregion

        #region Properties

        /* --------------------------------------------------------------------- */
        ///
        /// PollingInterval
        /// 
        /// <summary>
        /// ユーザによるキャンセル処理が発生したかどうかを確認する
        /// 間隔 (ミリ秒) を取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public int PollingInterval { get; set; }

        /* --------------------------------------------------------------------- */
        ///
        /// IsBusy
        /// 
        /// <summary>
        /// 遅延実行中かどうかを判別します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public bool IsBusy
        {
            get { return _worker.IsBusy; }
        }

        #endregion

        #region Methods

        /* --------------------------------------------------------------------- */
        ///
        /// RunAsync
        /// 
        /// <summary>
        /// action を　msec ミリ秒遅延させて実行します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public void RunAsync(int msec, Action action)
        {
            if (IsBusy) return;

            RunWorkerCompletedEventHandler handler = null;
            handler = (s, e) =>
            {
                var worker = s as BackgroundWorker;
                if (worker == null) return;
                worker.RunWorkerCompleted -= handler;
                if (e.Cancelled) return;
                action();
            };
            _worker.RunWorkerCompleted += handler;
            _worker.RunWorkerAsync(msec);

        }

        /* --------------------------------------------------------------------- */
        ///
        /// CancelAsync
        /// 
        /// <summary>
        /// 遅延実行を中止します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public void CancelAsync()
        {
            _worker.CancelAsync();
        }

        #endregion

        #region Implementations

        /* --------------------------------------------------------------------- */
        ///
        /// Worker_DoWork
        /// 
        /// <summary>
        /// BackgroundWorker のメイン処理を記述したハンドラです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            if (worker == null) return;

            var msec = (int)e.Argument;
            var expired = DateTime.Now + TimeSpan.FromMilliseconds(msec);
            while (DateTime.Now < expired)
            {
                if (worker.CancellationPending) break;
                var delta = expired - DateTime.Now;
                var sleep = (int)Math.Min(delta.TotalMilliseconds, 100);
                if (sleep > 0) System.Threading.Thread.Sleep(sleep);
            }
            if (worker.CancellationPending) e.Cancel = true;
        }

        #endregion

        #region Fields
        private BackgroundWorker _worker = new BackgroundWorker();
        #endregion
    }
}
