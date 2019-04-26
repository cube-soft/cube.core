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
using Cube.Collections;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// Power
    ///
    /// <summary>
    /// Provides functionality to observe power mode of the computer.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Power
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Power
        ///
        /// <summary>
        /// Initializes a static fields of the Power class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        static Power()
        {
            _context = new PowerModeContext(PowerModes.Resume);
            _context.PropertyChanged += WhenPropertyChanged;
            _initializer = new OnceInitializer(
                () => SystemEvents.PowerModeChanged += WhenModeChanged,
                () => SystemEvents.PowerModeChanged -= WhenModeChanged
            );
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Mode
        ///
        /// <summary>
        /// Gets the power mode.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static PowerModes Mode => _context.Mode;

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        ///
        /// <summary>
        /// Registers the callback to subscription.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static IDisposable Subscribe(Action callback) =>
            _subscription.Subscribe(callback);

        /* ----------------------------------------------------------------- */
        ///
        /// Configure
        ///
        /// <summary>
        /// Sets the PowerModeContext object.
        /// </summary>
        ///
        /// <remarks>
        /// プログラム上で独自に Power.Mode の状態を更新する必要がある
        /// 場合などに利用します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static void Configure(PowerModeContext context)
        {
            Debug.Assert(context != null);
            Interlocked.Exchange(ref _context, context).PropertyChanged -= WhenPropertyChanged;
            context.PropertyChanged -= WhenPropertyChanged;
            context.PropertyChanged += WhenPropertyChanged;
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// WhenPropertyChanged
        ///
        /// <summary>
        /// Occurs when the PropertyChanged event is fired.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void WhenPropertyChanged(object s, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(_context.Mode)) return;
            foreach (var callback in _subscription) callback();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenModeChanged
        ///
        /// <summary>
        /// Occurs when the PowerModeChanged event is fired.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void WhenModeChanged(object s, PowerModeChangedEventArgs e) =>
            _context.Mode = (PowerModes)(int)e.Mode;

        #endregion

        #region Fields
        private static readonly OnceInitializer _initializer;
        private static readonly Subscription<Action> _subscription = new Subscription<Action>();
        private static PowerModeContext _context;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// PowerModeContext
    ///
    /// <summary>
    /// Represents the power mode.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class PowerModeContext : ObservableBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// PowerModeContext
        ///
        /// <summary>
        /// Initializes a new instance of the PowerModeContext class
        /// with the specified value.
        /// </summary>
        ///
        /// <param name="mode">Initial value for power mode.</param>
        ///
        /* ----------------------------------------------------------------- */
        public PowerModeContext(PowerModes mode)
        {
            Mode = mode;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Mode
        ///
        /// <summary>
        /// Gets or sets the power mode.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public PowerModes Mode
        {
            get => _mode;
            set
            {
                if (IgnoreStatusChanged && value == PowerModes.StatusChange) return;
                SetProperty(ref _mode, value);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IgnoreStatusChanged
        ///
        /// <summary>
        /// Gets or sets the value indicating whether ignoring the
        /// StatusChanged value.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IgnoreStatusChanged
        {
            get => _ignore;
            set => SetProperty(ref _ignore, value);
        }

        #endregion

        #region Fields
        private PowerModes _mode;
        private bool _ignore = true;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// PowerModes
    ///
    /// <summary>
    /// Specifies the power mode.
    /// </summary>
    ///
    /// <remarks>
    /// .NET Standard の場合とソースコードを共有するために
    /// Microsoft.Win32.PowerModes と同様のものを定義します。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public enum PowerModes
    {
        /// <summary>Resume</summary>
        Resume = Microsoft.Win32.PowerModes.Resume,
        /// <summary>Status is changed</summary>
        StatusChange = Microsoft.Win32.PowerModes.StatusChange,
        /// <summary>Suspend</summary>
        Suspend = Microsoft.Win32.PowerModes.Suspend,
    }
}
