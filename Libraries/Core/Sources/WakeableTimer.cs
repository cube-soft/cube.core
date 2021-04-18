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
using System.Timers;
using Cube.Collections;
using Cube.Mixin.Logging;
using Microsoft.Win32;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// WakeableTimer
    ///
    /// <summary>
    /// Represents the timer that suspends/resumes corresponding to the
    /// power mode.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class WakeableTimer : DisposableBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// WakeableTimer
        ///
        /// <summary>
        /// Initializes a new instance of the WakeableTimer class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public WakeableTimer() : this(TimeSpan.FromSeconds(1)) { }

        /* ----------------------------------------------------------------- */
        ///
        /// WakeableTimer
        ///
        /// <summary>
        /// Initializes a new instance of the WakeableTimer class with the
        /// specified interval.
        /// </summary>
        ///
        /// <param name="interval">Execution interval.</param>
        ///
        /* ----------------------------------------------------------------- */
        public WakeableTimer(TimeSpan interval)
        {
            _span  = interval;
            _power = Power.Subscribe(() => OnPowerModeChanged(EventArgs.Empty));
            _inner = new() { AutoReset = false };
            _inner.Elapsed += DoConstant;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// State
        ///
        /// <summary>
        /// Gets the current timer state.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TimerState State { get; private set; } = TimerState.Stop;

        /* ----------------------------------------------------------------- */
        ///
        /// Interval
        ///
        /// <summary>
        /// Gets or sets the execution interval.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TimeSpan Interval
        {
            get => _span;
            set
            {
                if (_span == value) return;
                _span = value;
                Reset();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Last
        ///
        /// <summary>
        /// Gets the last time to invoke the actions registered with the
        /// Subscribe method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime? Last { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Next
        ///
        /// <summary>
        /// Gets or sets the time when the registered actions are invoked
        /// next time.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected DateTime Next { get; set; } = DateTime.Now;

        /* ----------------------------------------------------------------- */
        ///
        /// Subscriptions
        ///
        /// <summary>
        /// Gets the collection of subscriptions.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected Subscription<AsyncAction> Subscription { get; } = new();

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// PowerModeChanged
        ///
        /// <summary>
        /// Occurs when the power mode of the computer is changed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler PowerModeChanged;

        /* ----------------------------------------------------------------- */
        ///
        /// OnPowerModeChanged
        ///
        /// <summary>
        /// Raises the PowerModeChanged event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnPowerModeChanged(EventArgs e)
        {
            switch (Power.Mode)
            {
                case PowerModes.Resume:
                    Resume(TimeSpan.FromMilliseconds(100));
                    break;
                case PowerModes.Suspend:
                    Suspend();
                    break;
            }
            PowerModeChanged?.Invoke(this, e);
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Reset
        ///
        /// <summary>
        /// Resets some condition.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Reset() => OnReset();

        /* ----------------------------------------------------------------- */
        ///
        /// Start
        ///
        /// <summary>
        /// Starts or resumes the timer.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Start() => Start(TimeSpan.Zero);

        /* ----------------------------------------------------------------- */
        ///
        /// Start
        ///
        /// <summary>
        /// Starts or resumes the timer with the specified time.
        /// </summary>
        ///
        /// <param name="delay">Initial delay.</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Start(TimeSpan delay)
        {
            if (State == TimerState.Run) return;
            if (State == TimerState.Suspend) Resume(delay);
            else
            {
                State = TimerState.Run;
                Restart(Math.Max(delay.TotalMilliseconds, 1));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Stop
        ///
        /// <summary>
        /// Stops the timer.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Stop()
        {
            if (State == TimerState.Stop) return;
            if (_inner.Enabled) _inner.Stop();
            State = TimerState.Stop;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Suspend
        ///
        /// <summary>
        /// Suspends the timer.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Suspend()
        {
            if (State != TimerState.Run) return;
            _inner.Stop();
            State = TimerState.Suspend;
            this.LogDebug(nameof(Suspend), $"{nameof(Interval)}:{Interval}");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        ///
        /// <summary>
        /// Sets the specified asynchronous action to the timer.
        /// </summary>
        ///
        /// <param name="callback">Asynchronous user action.</param>
        ///
        /// <returns>Object to remove from the subscription.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public IDisposable Subscribe(AsyncAction callback) => Subscription.Subscribe(callback);

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases the unmanaged resources used by the WakeableTimer
        /// and optionally releases the managed resources.
        /// </summary>
        ///
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources;
        /// false to release only unmanaged resources.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Dispose(bool disposing)
        {
            if (!disposing) return;
            State = TimerState.Stop;
            _power?.Dispose();
            _inner?.Dispose();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnReset
        ///
        /// <summary>
        /// Resets inner fields.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnReset()
        {
            Next = DateTime.Now + Interval;
            _inner.Interval = Interval.TotalMilliseconds;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Resume
        ///
        /// <summary>
        /// Resumes the timer.
        /// </summary>
        ///
        /// <param name="min">Minimum delay.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected void Resume(TimeSpan min)
        {
            if (State != TimerState.Suspend) return;

            var delta = Next - DateTime.Now;
            var value = delta > min ? delta : min;

            State = TimerState.Run;
            Next  = DateTime.Now + value;

            this.LogDebug(nameof(Resume), $"{nameof(Interval)}:{Interval}",
                $"{nameof(Last)}:{Last}", $"{nameof(Next)}:{Next}");

            _inner.Interval = Math.Max(value.TotalMilliseconds, 1);
            _inner.Start();
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Restart
        ///
        /// <summary>
        /// Restarts the timer.
        /// </summary>
        ///
        /// <remarks>
        /// 原則としてユーザの設定したインターバルで実行を開始します。
        /// ただし、Subscribe で登録されているハンドラの総処理時間がユーザの
        /// 設定したインターバルを超える場合、最低でもその 1/10 秒ほど間隔を
        /// あけて次回の処理を実行します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private void Restart(DateTime time)
        {
            var delta = (DateTime.Now - time).TotalMilliseconds;
            var msec  = Interval.TotalMilliseconds;
            Restart(Math.Max(Math.Max(msec - delta, msec / 10.0), 1.0));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Restart
        ///
        /// <summary>
        /// Restarts the timer.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Restart(double msec)
        {
            Next = DateTime.Now + TimeSpan.FromMilliseconds(msec);
            if (State != TimerState.Run) return;
            _inner.Interval = msec;
            _inner.Start();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DoConstant
        ///
        /// <summary>
        /// Occurs at the provided intervals.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private async void DoConstant(object s, ElapsedEventArgs e)
        {
            if (State != TimerState.Run) return;
            try
            {
                Last = e.SignalTime;
                foreach (var cb in Subscription) await cb().ConfigureAwait(false);
            }
            finally { Restart(e.SignalTime); }
        }

        #endregion

        #region Fields
        private readonly IDisposable _power;
        private readonly Timer _inner;
        private TimeSpan _span;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// TimerState
    ///
    /// <summary>
    /// Specifies the timer state.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum TimerState
    {
        /// <summary>Run</summary>
        Run = 0,
        /// <summary>Stop</summary>
        Stop = 1,
        /// <summary>Suspend</summary>
        Suspend = 2,
        /// <summary>Unknown</summary>
        Unknown = -1
    }
}
