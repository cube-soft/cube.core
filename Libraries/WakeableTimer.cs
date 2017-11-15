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
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
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
    public enum TimerState
    {
        /// <summary>実行中</summary>
        Run =  0,
        /// <summary>停止</summary>
        Stop =  1,
        /// <summary>一時停止</summary>
        Suspend =  2,
        /// <summary>不明</summary>
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
            _interval = interval;
            _dispose  = new OnceAction<bool>(Dispose);
            _core.AutoReset = false;
            _core.Elapsed += WhenPublished;
            Power.ModeChanged += (s, e) => OnPowerModeChanged(e);
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
        public TimeSpan Interval
        {
            get => _interval;
            set
            {
                if (_interval == value) return;
                _interval = value;
                Reset();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LastPublished
        /// 
        /// <summary>
        /// 最後に Publish が実行された日時を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime LastPublished { get; private set; } = DateTime.MinValue;

        /* ----------------------------------------------------------------- */
        ///
        /// Next
        /// 
        /// <summary>
        /// 次回の実行予定日時を取得または設定します。
        /// </summary>
        /// 
        /// <remarks>
        /// 主にスリープモード復帰時に利用します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected DateTime Next { get; set; } = DateTime.Now;

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
        /// Subscriptions
        /// 
        /// <summary>
        /// 購読者一覧を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected IList<Func<Task>> Subscriptions { get; } = new List<Func<Task>>();

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
            var time = delay > TimeSpan.Zero ? delay : TimeSpan.Zero;
            Next = DateTime.Now + time;
            _core.Interval = Math.Max(time.TotalMilliseconds, 1);
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
        /// 一定間隔で実行される非同期処理を登録します。
        /// </summary>
        /// 
        /// <param name="action">非同期処理を表すオブジェクト</param>
        /// 
        /// <returns>登録を解除するためのオブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public IDisposable Subscribe(Func<Task> action)
        {
            Subscriptions.Add(action);
            return Disposable.Create(() => Subscriptions.Remove(action));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        ///
        /// <summary>
        /// 一定間隔で実行される処理を登録します。
        /// </summary>
        /// 
        /// <param name="action">処理を表すオブジェクト</param>
        /// 
        /// <returns>登録を解除するためのオブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public IDisposable Subscribe(Action action)
            => Subscribe(() => Task.Run(() => action()));

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
            Next = DateTime.Now + Interval;
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
        ~WakeableTimer() { _dispose.Invoke(false); }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        /// 
        /// <summary>
        /// リソースを破棄します。
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
        /// オブジェクトを破棄します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void Dispose(bool disposing)
        {
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
        protected virtual async Task Publish()
        {
            LastPublished = DateTime.Now;

            var msec  = Interval.TotalMilliseconds;
            var delta = Math.Abs(_core.Interval - msec);
            if (delta > 1.0) _core.Interval = msec;

            Next = LastPublished + TimeSpan.FromMilliseconds(_core.Interval);

            foreach (var action in Subscriptions) await action();
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
            var time = now < Next ? Next - now : TimeSpan.FromMilliseconds(100);

            State = TimerState.Run;
            Next  = now + time;

            this.LogDebug(string.Format("Resume\tLast:{0}\tNext:{1}\tInterval:{2}",
                LastPublished, Next, Interval));

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
            switch (mode)
            {
                case PowerModes.Resume:
                    Resume();
                    break;
                case PowerModes.Suspend:
                    Suspend();
                    break;
                default:
                    break;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenPublished
        /// 
        /// <summary>
        /// 一定間隔で実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private async void WhenPublished(object sender, ElapsedEventArgs e)
        {
            try
            {
                LastPublished = e.SignalTime;
                await Publish();
            }
            finally
            {
                var msec  = Interval.TotalMilliseconds;
                var delta = Math.Abs(_core.Interval - msec);
                if (delta > 1.0) _core.Interval = msec;

                Next = DateTime.Now + TimeSpan.FromMilliseconds(_core.Interval);
                _core.Start();
            }
        }

        #region Fields
        private OnceAction<bool> _dispose;
        private TimeSpan _interval;
        private Timer _core = new Timer();
        #endregion

        #endregion
    }
}
