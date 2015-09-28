/* ------------------------------------------------------------------------- */
///
/// Scheduler.cs
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
using log4net;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Scheduler
    /// 
    /// <summary>
    /// 特定の操作を定期的に実行するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Scheduler
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Scheduler
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Scheduler()
        {
            Interval = TimeSpan.FromSeconds(1);
            InitialDelay = TimeSpan.Zero;
            LastExecuted = DateTime.Now;
            State = SchedulerState.Stop;
            Logger = LogManager.GetLogger(GetType());
            _impl.Elapsed += (s, e) => OnExecute(e);
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Interval
        /// 
        /// <summary>
        /// 実行間隔を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TimeSpan Interval { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// InitialDelay
        /// 
        /// <summary>
        /// 最初の実行を遅延させる時間を取得または設定します。
        /// </summary>
        /// 
        /// <remarks>
        /// この値は、Start が実行されてから最初に操作が実行されるまでの
        /// 時間に適用されます。したがってStop and Start 操作を行う度に
        /// この値が適用されます。ただし、Suspend and Resume 操作では
        /// この値は適用されません。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public TimeSpan InitialDelay { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// LastExecuted
        /// 
        /// <summary>
        /// 操作が最後に実行された日時を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime LastExecuted { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// State
        /// 
        /// <summary>
        /// オブジェクトの状態を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SchedulerState State { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Logger
        /// 
        /// <summary>
        /// ログ出力用オブジェクトを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected ILog Logger { get; set; }

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// Execute
        /// 
        /// <summary>
        /// 操作を実行するタイミングになった時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler Execute;

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Start
        /// 
        /// <summary>
        /// スケジューリングを開始します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Start()
        {
            if (State != SchedulerState.Stop) return;
            System.Diagnostics.Debug.Assert(!_impl.Enabled);
            State = SchedulerState.Run;

            if (InitialDelay > TimeSpan.Zero) _impl.Interval = InitialDelay.TotalMilliseconds;
            else
            {
                OnExecute(new EventArgs());
                _impl.Interval = Interval.TotalMilliseconds;
            }

            _impl.Start();
            Logger.Debug("Start");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Reset
        /// 
        /// <summary>
        /// リセットします。
        /// </summary>
        /// 
        /// <remarks>
        /// 派生クラスでリセット処理が必要な場合、OnReset メソッドを
        /// オーバーライドして実装して下さい。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public void Reset()
        {
            OnReset(new EventArgs());
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Stop
        /// 
        /// <summary>
        /// スケジューリングを停止します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Stop()
        {
            if (State == SchedulerState.Stop) return;

            if (_impl.Enabled) _impl.Stop();
            Logger.DebugFormat("Stop\tLastExecuted:{0}", LastExecuted);

            State = SchedulerState.Stop;
            LastExecuted = DateTime.Now;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Suspend
        /// 
        /// <summary>
        /// スケジューリングを一時停止します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Suspend()
        {
            if (State != SchedulerState.Run) return;

            System.Diagnostics.Debug.Assert(_impl.Enabled);
            _impl.Stop();
            State = SchedulerState.Suspend;
            Logger.DebugFormat("Suspend\tLastExecuted:{0}", LastExecuted);

            var interval = Interval - (DateTime.Now - LastExecuted);
            if (interval < TimeSpan.Zero) interval = Interval;
            _impl.Interval = interval.TotalMilliseconds;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Resume
        /// 
        /// <summary>
        /// 一時停止していたスケジューリングを再開します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Resume()
        {
            if (State != SchedulerState.Suspend) return;

            System.Diagnostics.Debug.Assert(!_impl.Enabled);
            State = SchedulerState.Run;
            _impl.Start();
            Logger.DebugFormat("Resume\tLastExecuted:{0}\tInterval:{1:D}msec", LastExecuted, _impl.Interval);
        }

        #endregion

        #region Virtual methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnReset
        /// 
        /// <summary>
        /// リセット処理を実行します。
        /// </summary>
        /// 
        /// <remarks>
        /// TODO: クラス外部に OnReset に対応するイベントを公開すべきか
        /// どうか検討。公開する場合、イベント名を何にするかも要検討。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnReset(EventArgs e)
        {
            if (State != SchedulerState.Stop) Stop();
            LastExecuted = DateTime.Now;
            _impl.Interval = Interval.TotalMilliseconds;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnExecute
        /// 
        /// <summary>
        /// 操作を実行するタイミングになった時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnExecute(EventArgs e)
        {
            if (Execute != null) Execute(this, e);

            LastExecuted = DateTime.Now;
            if ((int)_impl.Interval != (int)Interval.TotalMilliseconds)
            {
                _impl.Interval = Interval.TotalMilliseconds;
            }
        }

        #endregion

        #region Fields
        private System.Timers.Timer _impl = new System.Timers.Timer();
        #endregion
    }
}
