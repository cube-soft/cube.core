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
using Microsoft.Win32;
using Cube.Log;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// TimerState
    /// 
    /// <summary>
    /// 各種 Timer オブジェクトの状態を表すための列挙型です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum TimerState : int
    {
        Run     =  0,
        Stop    =  1,
        Suspend =  2,
        Unknown = -1
    }

    /* --------------------------------------------------------------------- */
    ///
    /// WakeableTimer
    /// 
    /// <summary>
    /// 端末の電源状態に応じて一時停止、再起動を行うタイマーです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class WakeableTimer : IDisposable
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// WakeableTimer
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public WakeableTimer() : this(TimeSpan.FromSeconds(1)) { }

        /* ----------------------------------------------------------------- */
        ///
        /// WakeableTimer
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        /// 
        /// <param name="interval">実行周期</param>
        ///
        /* ----------------------------------------------------------------- */
        public WakeableTimer(TimeSpan interval)
        {
            Interval = interval;
            _core.Elapsed += (s, e) => OnExecute(e);
            SystemEvents.PowerModeChanged += (s, e) => OnPowerModeChanged(e);
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
        /// LastExecuted
        /// 
        /// <summary>
        /// 操作が最後に実行された日時を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime LastExecuted { get; private set; } = DateTime.MinValue;

        /* ----------------------------------------------------------------- */
        ///
        /// State
        /// 
        /// <summary>
        /// オブジェクトの状態を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TimerState State { get; private set; } = TimerState.Stop;

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

        #endregion

        #region Events

        #region Execute

        /* ----------------------------------------------------------------- */
        ///
        /// Execute
        /// 
        /// <summary>
        /// 操作実行時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler Execute;

        /* ----------------------------------------------------------------- */
        ///
        /// OnExecute
        /// 
        /// <summary>
        /// Execute イベントを発生させます。
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

        #endregion

        #region PowerModeChanged

        /* ----------------------------------------------------------------- */
        ///
        /// PowerModeChanged
        /// 
        /// <summary>
        /// 電源状態が変更された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event PowerModeChangedEventHandler PowerModeChanged;

        /* ----------------------------------------------------------------- */
        ///
        /// OnPowerModeChanged
        /// 
        /// <summary>
        /// PowerModeChanged イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnPowerModeChanged(PowerModeChangedEventArgs e)
        {
            if (e.Mode != PowerModes.StatusChange) PowerMode = e.Mode;
            UpdateState(e.Mode);
            PowerModeChanged?.Invoke(this, e);
        }

        #endregion

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Start
        /// 
        /// <summary>
        /// タイマーを開始します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Start() => Start(TimeSpan.Zero);

        /* ----------------------------------------------------------------- */
        ///
        /// Start
        /// 
        /// <summary>
        /// タイマーを開始します。
        /// </summary>
        ///
        /// <param name="delay">初期遅延時間</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Start(TimeSpan delay)
        {
            if (State != TimerState.Stop) return;

            State = TimerState.Run;
            if (delay > TimeSpan.Zero) _core.Interval = delay.TotalMilliseconds;
            else
            {
                OnExecute(EventArgs.Empty);
                _core.Interval = Interval.TotalMilliseconds;
            }
            _core.Start();

            this.LogDebug($"Start\tInterval:{Interval}\tInitialDelay:{delay}");
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
            if (State == TimerState.Stop) return;

            if (_core.Enabled) _core.Stop();
            State = TimerState.Stop;

            this.LogDebug($"Stop\tLastExecuted:{LastExecuted}");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Reset
        /// 
        /// <summary>
        /// 内部状態をリセットします。
        /// </summary>
        /// 
        /// <remarks>
        /// 派生クラスでリセット処理が必要な場合、OnReset メソッドを
        /// オーバーライドして下さい。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public void Reset() => OnReset();

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
            _core.Interval = Interval.TotalMilliseconds;
        }

        #region IDisposable

        /* ----------------------------------------------------------------- */
        ///
        /// ~WakeableTimer
        /// 
        /// <summary>
        /// オブジェクトを破棄します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        ~WakeableTimer()
        {
            Dispose(false);
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

        #region Protected

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
            if (State != TimerState.Run) return;

            this.LogDebug($"RaiseExecute");
            OnExecute(EventArgs.Empty);
            _core.Interval = Interval.TotalMilliseconds;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Suspend
        /// 
        /// <summary>
        /// タイマーを一時停止します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected void Suspend()
        {
            if (State != TimerState.Run) return;

            _core.Stop();
            State = TimerState.Suspend;
            this.LogDebug($"Suspend\tLastExecuted:{LastExecuted}");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Resume
        /// 
        /// <summary>
        /// 一時停止していたタイマーを再開します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected void Resume()
        {
            if (State != TimerState.Suspend) return;

            var passed   = DateTime.Now - LastExecuted;
            var interval = Interval > passed ?
                           Interval - passed :
                           TimeSpan.FromMilliseconds(1);

            State = TimerState.Run;
            _core.Interval = interval.TotalMilliseconds;
            _core.Start();

            this.LogDebug($"Resume\tLastExecuted:{LastExecuted}\tInterval:{interval}");
        }

        #endregion

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateState
        /// 
        /// <summary>
        /// PowerMode に応じて State を変更します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateState(PowerModes mode)
        {
            var previous = State;

            switch (mode)
            {
                case PowerModes.Resume:
                    if (State == TimerState.Suspend) Resume();
                    break;
                case PowerModes.StatusChange:
                    break;
                case PowerModes.Suspend:
                    if (State == TimerState.Run) Suspend();
                    break;
                default:
                    break;
            }

            this.LogDebug($"PowerMode:{mode}\tState:{previous}->{State}");
        }

        #region Fields
        private bool _disposed = false;
        private System.Timers.Timer _core = new System.Timers.Timer();
        #endregion

        #endregion
    }
}
