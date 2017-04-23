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
using System.Collections.Generic;
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
            _core.Elapsed += (s, e) => Publish();
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

        /* ----------------------------------------------------------------- */
        ///
        /// Subscriptions
        /// 
        /// <summary>
        /// 購読者一覧を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected IList<Action> Subscriptions { get; } = new List<Action>();

        #endregion

        #region Events

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
                Publish();
                _core.Interval = Interval.TotalMilliseconds;
            }
            _core.Start();
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
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        ///
        /// <summary>
        /// 一定間隔毎に実行される処理を登録します。
        /// </summary>
        /// 
        /// <param name="action">処理を表すオブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Subscribe(Action action)
            => Subscriptions.Add(action);

        /* ----------------------------------------------------------------- */
        ///
        /// Reset
        /// 
        /// <summary>
        /// 内部状態をリセットします。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public virtual void Reset()
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
        /// Publish
        /// 
        /// <summary>
        /// イベントを発行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void Publish()
        {
            LastExecuted = DateTime.Now;
            var delta = Math.Abs(_core.Interval - Interval.TotalMilliseconds);
            if (delta > 1.0) _core.Interval = Interval.TotalMilliseconds;
            foreach (var action in Subscriptions) action();
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
            this.LogDebug($"Suspend\tInterval:{Interval}");
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

            var now  = DateTime.Now;
            var pass = now - LastExecuted;
            var time = Interval > pass ?
                       Interval - pass :
                       TimeSpan.FromMilliseconds(1);

            this.LogDebug(string.Format("Resume\tLast:{0}\tNext:{1}\tInterval:{2}",
                LastExecuted, now + time, Interval));

            State = TimerState.Run;
            _core.Interval = time.TotalMilliseconds;
            _core.Start();
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
        }

        #region Fields
        private bool _disposed = false;
        private System.Timers.Timer _core = new System.Timers.Timer();
        #endregion

        #endregion
    }
}
