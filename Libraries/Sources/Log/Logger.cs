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
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace Cube.Log
{
    /* --------------------------------------------------------------------- */
    ///
    /// Logger
    ///
    /// <summary>
    /// Provides methods for logging.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Logger
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Configure
        ///
        /// <summary>
        /// Initializes settings of logging.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static void Configure() => log4net.Config.XmlConfigurator.Configure();

        /* ----------------------------------------------------------------- */
        ///
        /// ObserveTaskException
        ///
        /// <summary>
        /// Observes UnobservedTaskException exceptions and outputs to the
        /// log file.
        /// </summary>
        ///
        /// <returns>Disposable object to stop to monitor.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IDisposable ObserveTaskException()
        {
            TaskScheduler.UnobservedTaskException -= WhenTaskError;
            TaskScheduler.UnobservedTaskException += WhenTaskError;
            return Disposable.Create(
                () => TaskScheduler.UnobservedTaskException -= WhenTaskError
            );
        }

        #region Debug

        /* ----------------------------------------------------------------- */
        ///
        /// Debug
        ///
        /// <summary>
        /// Outputs log as DEBUG level.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="message">Message string.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Debug(Type type, string message) =>
            GetCore(type).Debug(message);

        /* ----------------------------------------------------------------- */
        ///
        /// Debug
        ///
        /// <summary>
        /// Outputs log as DEBUG level.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="message">Message string.</param>
        /// <param name="err">Exception object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Debug(Type type, string message, Exception err) =>
            GetCore(type).Debug(message, err);

        /* ----------------------------------------------------------------- */
        ///
        /// Debug
        ///
        /// <summary>
        /// Monitors the running time and outputs it as DEBUG level.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="message">Message string.</param>
        /// <param name="func">Function to monitor.</param>
        ///
        /// <returns>Function result.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T Debug<T>(Type type, string message, Func<T> func)
        {
            var sw   = Stopwatch.StartNew();
            var dest = func();
            Debug(type, $"{message} ({sw.Elapsed})");
            return dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Debug
        ///
        /// <summary>
        /// Monitors the running time and outputs it as DEBUG level.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="message">Message string.</param>
        /// <param name="action">Action to monitor.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Debug(Type type, string message, Action action) =>
            Debug(type, message, () =>
        {
            action();
            return true;
        });

        #endregion

        #region Info

        /* ----------------------------------------------------------------- */
        ///
        /// Info
        ///
        /// <summary>
        /// Outputs log as INFO level.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="message">Message string.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Info(Type type, string message) =>
            GetCore(type).Info(message);

        /* ----------------------------------------------------------------- */
        ///
        /// Info
        ///
        /// <summary>
        /// Outputs log as INFO level.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="message">Message string.</param>
        /// <param name="err">Exception object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Info(Type type, string message, Exception err) =>
            GetCore(type).Info(message, err);

        /* ----------------------------------------------------------------- */
        ///
        /// Info
        ///
        /// <summary>
        /// Outputs system information as INFO level.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="assembly">Assembly object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Info(Type type, Assembly assembly)
        {
            var asm = new AssemblyReader(assembly);
            var sv  = new SoftwareVersion(assembly);

            Info(type, $"{asm.Product} {sv.ToString(true)}");
            Info(type, $"{Environment.OSVersion}");
            Info(type, $"Microsoft .NET Framework {Environment.Version}");
            Info(type, $"{Environment.UserName}@{Environment.MachineName}");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Info
        ///
        /// <summary>
        /// Monitors the running time and outputs it as INFO level.
        /// </summary>
        ///
        /// <param name="type">Target type information.</param>
        /// <param name="message">Message string.</param>
        /// <param name="func">Function to monitor.</param>
        ///
        /// <returns>Function result.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T Info<T>(Type type, string message, Func<T> func)
        {
            var sw = Stopwatch.StartNew();
            var dest = func();
            Info(type, $"{message} ({sw.Elapsed})");
            return dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Info
        ///
        /// <summary>
        /// Monitors the running time and outputs it as INFO level.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="message">Message string.</param>
        /// <param name="action">Action to monitor.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Info(Type type, string message, Action action) =>
            Info(type, message, () =>
        {
            action();
            return true;
        });

        #endregion

        #region Warn

        /* ----------------------------------------------------------------- */
        ///
        /// Warn
        ///
        /// <summary>
        /// Outputs log as WARN level.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="message">Message string.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Warn(Type type, string message) =>
            GetCore(type).Warn(message);

        /* ----------------------------------------------------------------- */
        ///
        /// Warn
        ///
        /// <summary>
        /// Outputs log as WARN level.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="message">Message string.</param>
        /// <param name="err">Exception object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Warn(Type type, string message, Exception err) =>
            GetCore(type).Warn(message, err);

        /* ----------------------------------------------------------------- */
        ///
        /// Warn
        ///
        /// <summary>
        /// Outputs log as WARN level when an exception occurs.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="func">Function to monitor.</param>
        /// <param name="err">
        /// Value that returns when an exception occurs.
        /// </param>
        ///
        /// <returns>Function result.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T Warn<T>(Type type, Func<T> func, T err)
        {
            try { return func(); }
            catch (Exception e) { Warn(type, e.ToString(), e); }
            return err;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Warn
        ///
        /// <summary>
        /// Outputs log as WARN level when an exception occurs.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="action">Function to monitor.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Warn(Type type, Action action) => Warn(type, () =>
        {
            action();
            return true;
        }, false);

        #endregion

        #region Error

        /* ----------------------------------------------------------------- */
        ///
        /// Error
        ///
        /// <summary>
        /// Outputs log as ERROR level.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="message">Message string.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Error(Type type, string message) =>
            GetCore(type).Error(message);

        /* ----------------------------------------------------------------- */
        ///
        /// Error
        ///
        /// <summary>
        /// Outputs log as ERROR level.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="message">Message string.</param>
        /// <param name="err">Exception object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Error(Type type, string message, Exception err) =>
            GetCore(type).Error(message, err);

        /* ----------------------------------------------------------------- */
        ///
        /// Error
        ///
        /// <summary>
        /// Outputs log as ERROR level when an exception occurs.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="func">Function to monitor.</param>
        /// <param name="err">
        /// Value that returns when an exception occurs.
        /// </param>
        ///
        /// <returns>Function result.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T Error<T>(Type type, Func<T> func, T err)
        {
            try { return func(); }
            catch (Exception e) { Error(type, e.ToString(), e); }
            return err;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Error
        ///
        /// <summary>
        /// Outputs log as ERROR level when an exception occurs.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="action">Function to monitor.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Error(Type type, Action action) => Error(type, () =>
        {
            action();
            return true;
        }, false);

        #endregion

        #region Fatal

        /* ----------------------------------------------------------------- */
        ///
        /// Fatal
        ///
        /// <summary>
        /// Outputs log as FATAL level.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="message">Message string.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Fatal(Type type, string message) =>
            GetCore(type).Fatal(message);

        /* ----------------------------------------------------------------- */
        ///
        /// Fatal
        ///
        /// <summary>
        /// Outputs log as FATAL level.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="message">Message string.</param>
        /// <param name="err">Exception object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Fatal(Type type, string message, Exception err) =>
            GetCore(type).Fatal(message, err);

        /* ----------------------------------------------------------------- */
        ///
        /// Fatal
        ///
        /// <summary>
        /// Outputs log as FATAL level when an exception occurs.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="func">Function to monitor.</param>
        /// <param name="err">
        /// Value that returns when an exception occurs.
        /// </param>
        ///
        /// <returns>Function result.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T Fatal<T>(Type type, Func<T> func, T err)
        {
            try { return func(); }
            catch (Exception e) { Fatal(type, e.ToString(), e); }
            return err;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Fatal
        ///
        /// <summary>
        /// Outputs log as FATAL level when an exception occurs.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="action">Function to monitor.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Fatal(Type type, Action action) => Fatal(type, () =>
        {
            action();
            return true;
        }, false);

        #endregion

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// GetCore
        ///
        /// <summary>
        /// Gets the logger object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static log4net.ILog GetCore(Type type) => log4net.LogManager.GetLogger(type);

        /* ----------------------------------------------------------------- */
        ///
        /// WhenTaskError
        ///
        /// <summary>
        /// Occurs when the UnobservedTaskException is raised.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void WhenTaskError(object s, UnobservedTaskExceptionEventArgs e) =>
            Error(typeof(TaskScheduler), e.Exception.ToString(), e.Exception);

        #endregion
    }
}
