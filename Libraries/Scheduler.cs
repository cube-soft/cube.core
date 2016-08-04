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
using Microsoft.Win32;
using Cube.Log;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// SchedulerState
    /// 
    /// <summary>
    /// Scheduler オブジェクトの状態を表すための列挙型です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum SchedulerState : int
    {
        Run     =  0,
        Stop    =  1,
        Suspend =  2,
        Unknown = -1
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Scheduler
    /// 
    /// <summary>
    /// 特定の操作を定期的に実行するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Scheduler : IDisposable
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
            _core.Elapsed += (s, e) => OnExecute(e);
            SystemEvents.PowerModeChanged += (s, e) => OnPowerModeChanged(e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ~Scheduler
        /// 
        /// <summary>
        /// オブジェクトを破棄します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        ~Scheduler()
        {
            Dispose(false);
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
        public TimeSpan Interval { get; set; } = TimeSpan.FromSeconds(1);

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
        public TimeSpan InitialDelay { get; set; } = TimeSpan.Zero;

        /* ----------------------------------------------------------------- */
        ///
        /// LastExecuted
        /// 
        /// <summary>
        /// 操作が最後に実行された日時を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime LastExecuted { get; private set; } = DateTime.Now;

        /* ----------------------------------------------------------------- */
        ///
        /// State
        /// 
        /// <summary>
        /// オブジェクトの状態を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SchedulerState State { get; private set; } = SchedulerState.Stop;

        /* ----------------------------------------------------------------- */
        ///
        /// PowerMode
        /// 
        /// <summary>
        /// 電源の状態を取得します。
        /// </summary>
        /// 
        /// <remarks>
        /// このプロパティは Resume または Suspend どちらかの値を示します。
        /// そのため、PowerModeChanged イベントの Mode プロパティの値とは
        /// 必ずしも一致しません。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public PowerModes PowerMode { get; private set; } = PowerModes.Resume;

        /* ----------------------------------------------------------------- */
        ///
        /// PowerModeAware
        /// 
        /// <summary>
        /// 電源の状態に反応してスケジュール機能を停止、再開するかどうかを
        /// 示す値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool PowerModeAware { get; set; } = true;

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

        /* ----------------------------------------------------------------- */
        ///
        /// PowerModeChanged
        /// 
        /// <summary>
        /// 電源状態が変更された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler<PowerModeChangedEventArgs> PowerModeChanged;

        #endregion

        #region Methods

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
        public void Reset() => OnReset();

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

            State = SchedulerState.Run;
            if (InitialDelay > TimeSpan.Zero) _core.Interval = InitialDelay.TotalMilliseconds;
            else
            {
                OnExecute(EventArgs.Empty);
                _core.Interval = Interval.TotalMilliseconds;
            }
            _core.Start();

            this.LogDebug($"Start\tInterval:{Interval}\tInitialDelay:{InitialDelay}");
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

            if (_core.Enabled) _core.Stop();
            State = SchedulerState.Stop;

            this.LogDebug($"Stop\tLastExecuted:{LastExecuted}");
            LastExecuted = DateTime.Now;
        }

        #endregion

        #region Methods for IDisposable

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
            if (disposing) _core.Dispose();
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
        /* ----------------------------------------------------------------- */
        protected virtual void OnReset()
        {
            LastExecuted = DateTime.Now;
            _core.Interval = Interval.TotalMilliseconds;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnExecute
        /// 
        /// <summary>
        /// 操作を実行するタイミングになった時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnExecute(EventArgs e)
        {
            LastExecuted = DateTime.Now;
            var delta = Math.Abs(_core.Interval - Interval.TotalMilliseconds);
            if (delta > 1.0) _core.Interval = Interval.TotalMilliseconds;
            Execute?.Invoke(this, e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnPowerModeChanged
        /// 
        /// <summary>
        /// 電源の状態が変化した時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnPowerModeChanged(PowerModeChangedEventArgs e)
        {
            if (e.Mode != PowerModes.StatusChange) PowerMode = e.Mode;
            ChangeState(e.Mode);
            PowerModeChanged?.Invoke(this, e);
        }

        #endregion

        #region Non-virtual methods

        /* ----------------------------------------------------------------- */
        ///
        /// RaiseExecute
        /// 
        /// <summary>
        /// 直ちに Execute イベントを発生させます。
        /// </summary>
        /// 
        /// <remarks>
        /// State が Run 以外の場合、RaiseExecute は無視されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected void RaiseExecute()
        {
            if (State != SchedulerState.Run) return;

            this.LogDebug($"RaiseExecute");
            OnExecute(EventArgs.Empty);
            _core.Interval = Interval.TotalMilliseconds;
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
        protected void Suspend()
        {
            if (State != SchedulerState.Run) return;

            _core.Stop();
            State = SchedulerState.Suspend;
            this.LogDebug($"Suspend\tLastExecuted:{LastExecuted}");
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
        protected void Resume()
        {
            if (State != SchedulerState.Suspend) return;

            var passed   = DateTime.Now - LastExecuted;
            var interval = Interval > passed ?
                           Interval - passed :
                           TimeSpan.FromMilliseconds(1);

            State = SchedulerState.Run;
            _core.Interval = interval.TotalMilliseconds;
            _core.Start();

            this.LogDebug($"Resume\tLastExecuted:{LastExecuted}\tInterval:{interval}");
        }

        #endregion

        #region Other private methods

        /* ----------------------------------------------------------------- */
        ///
        /// ChangeState
        /// 
        /// <summary>
        /// PowerMode に応じて State を変更します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ChangeState(PowerModes mode)
        {
            if (!PowerModeAware) return;

            var previous = State;

            switch (mode)
            {
                case PowerModes.Resume:
                    if (State == SchedulerState.Suspend) Resume();
                    break;
                case PowerModes.StatusChange:
                    break;
                case PowerModes.Suspend:
                    if (State == SchedulerState.Run) Suspend();
                    break;
                default:
                    break;
            }

            this.LogDebug($"PowerMode:{mode}\tState:{previous}->{State}");
        }

        #endregion

        #region Fields
        private bool _disposed = false;
        private System.Timers.Timer _core = new System.Timers.Timer();
        #endregion
    }
}
