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
using Cube.Log;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

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
    public class WakeableTimer : IDisposable
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
        /// Gets or sets the execution interval.
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
        /// Gets the time of latest Publishing.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime? LastPublished { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Next
        ///
        /// <summary>
        /// Gets or sets the time to be published.
        /// </summary>
        ///
        /// <remarks>
        /// 主にスリープモード復帰時に利用します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected DateTime? Next { get; set; } = DateTime.Now;

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
        /// Subscriptions
        ///
        /// <summary>
        /// Ges the collection of subscriptions.
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
            UpdateState(Power.Mode);
            PowerModeChanged?.Invoke(this, e);
        }

        #endregion

        #region Methods

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
                var time = Math.Max(delay.TotalMilliseconds, 1);
                State = TimerState.Run;
                Next  = DateTime.Now + TimeSpan.FromMilliseconds(time);
                _core.Interval = time;
                _core.Start();
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
            if (_core.Enabled) _core.Stop();
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
            _core.Stop();
            State = TimerState.Suspend;
            this.LogDebug($"Suspend\tInterval:{Interval}");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SubscribeAsync
        ///
        /// <summary>
        /// Sets the specified action that runs as an asynchronous
        /// operation to the timer.
        /// </summary>
        ///
        /// <param name="action">Asynchronous user action.</param>
        ///
        /// <returns>Disposable object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public IDisposable SubscribeAsync(Func<Task> action)
        {
            Subscriptions.Add(action);
            return Disposable.Create(() => Subscriptions.Remove(action));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        ///
        /// <summary>
        /// Sets the specified action to the timer.
        /// </summary>
        ///
        /// <param name="action">User action.</param>
        ///
        /// <returns>Disposable object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public IDisposable Subscribe(Action action) => SubscribeAsync(() =>
        {
            action();
            return Task.FromResult(0);
        });

        /* ----------------------------------------------------------------- */
        ///
        /// Reset
        ///
        /// <summary>
        /// Resets some condition.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public virtual void Reset() => OnReset();

        #region IDisposable

        /* ----------------------------------------------------------------- */
        ///
        /// ~WakeableTimer
        ///
        /// <summary>
        /// Finalizes the WakeableTimer.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        ~WakeableTimer() { _dispose.Invoke(false); }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases all resources used by the WakeableTimer.
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
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                State = TimerState.Stop;
                _core.Dispose();
            }
        }

        #endregion

        #region Protected

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
            _core.Interval = Interval.TotalMilliseconds;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PublishAsync
        ///
        /// <summary>
        /// Publishes an event as an asynchronous operation to each
        /// subscription.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual async Task PublishAsync()
        {
            foreach (var action in Subscriptions)
            {
                if (State != TimerState.Run) return;
                await action().ConfigureAwait(false);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Resume
        ///
        /// <summary>
        /// Resumes the timer.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected void Resume(TimeSpan delay)
        {
            if (State != TimerState.Suspend) return;

            var now   = DateTime.Now;
            var delta = Next - now;
            var time  = delta > delay ? delta : delay;

            State = TimerState.Run;
            Next  = now + time;

            this.LogDebug(string.Format("Resume\tLast:{0}\tNext:{1}\tInterval:{2}",
                LastPublished, Next, Interval));

            _core.Interval = Math.Max(time.Value.TotalMilliseconds, 1);
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
        /// Updates the timer state corresponding to the specified power
        /// mode.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateState(PowerModes mode)
        {
            switch (mode)
            {
                case PowerModes.Resume:
                    Resume(TimeSpan.FromMilliseconds(100));
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
        /// Occurs at regular intervals.
        /// </summary>
        ///
        /// <remarks>
        /// 原則としてユーザの設定したインターバルで Publish を発行します。
        /// ただし、Publish の処理時間がユーザの設定したインターバルを
        /// 超える場合、最低でもその 1/10 秒ほどの間隔をあけて次の
        /// Publish を発行します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private async void WhenPublished(object s, ElapsedEventArgs e)
        {
            if (State != TimerState.Run) return;

            try
            {
                LastPublished = e.SignalTime;
                await PublishAsync().ConfigureAwait(false);
            }
            finally
            {
                var user = Interval.TotalMilliseconds;
                var diff = (DateTime.Now - LastPublished).Value.TotalMilliseconds;
                var time = Math.Max(Math.Max(user - diff, user / 10.0), 1.0); // see remarks

                Next = DateTime.Now + TimeSpan.FromMilliseconds(time);

                if (State == TimerState.Run)
                {
                    _core.Interval = time;
                    _core.Start();
                }
            }
        }

        #endregion

        #region Fields
        private readonly OnceAction<bool> _dispose;
        private readonly Timer _core = new Timer();
        private TimeSpan _interval;
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
