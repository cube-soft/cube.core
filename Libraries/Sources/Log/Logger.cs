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
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Cube
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
        /// Separator
        ///
        /// <summary>
        /// Gets or sets values to separate words.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string Separator { get; set; } = "\t";

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
            return Disposable.Create(() => TaskScheduler.UnobservedTaskException -= WhenTaskError);
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
        /// <param name="values">User messages.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Debug(Type type, params string[] values) =>
            GetCore(type).Debug(GetMessage(values));

        /* ----------------------------------------------------------------- */
        ///
        /// Debug
        ///
        /// <summary>
        /// Outputs log as DEBUG level.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="error">Exception object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Debug(Type type, Exception error) =>
            GetCore(type).Debug(GetErrorMessage(error));

        /* ----------------------------------------------------------------- */
        ///
        /// Debug
        ///
        /// <summary>
        /// Monitors the running time and outputs it as DEBUG level.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="func">Function to monitor.</param>
        /// <param name="message">Method name or message.</param>
        ///
        /// <returns>Function result.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T Debug<T>(Type type, Func<T> func, [CallerMemberName] string message = null)
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
        public static void Debug(Type type, Action action, [CallerMemberName] string message = null)
        {
            var sw = Stopwatch.StartNew();
            action();
            Debug(type, $"{message} ({sw.Elapsed})");
        }

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
        /// <param name="values">User messages.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Info(Type type, params string[] values) =>
            GetCore(type).Info(GetMessage(values));

        /* ----------------------------------------------------------------- */
        ///
        /// Info
        ///
        /// <summary>
        /// Outputs log as INFO level.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="error">Exception object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Info(Type type, Exception error) =>
            GetCore(type).Info(GetErrorMessage(error));

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
            var ver = new SoftwareVersion(assembly);

            Info(type, $"{asm.Product} {ver.ToString(true)}");
            Info(type, $"{Environment.OSVersion}");
            Info(type, $".NET Framework {Environment.Version}");
            Info(type, $"{Environment.UserName}@{Environment.MachineName}");
        }

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
        /// <param name="values">User messages.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Warn(Type type, params string[] values) =>
            GetCore(type).Warn(GetMessage(values));

        /* ----------------------------------------------------------------- */
        ///
        /// Warn
        ///
        /// <summary>
        /// Outputs log as WARN level.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="error">Exception object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Warn(Type type, Exception error) =>
            GetCore(type).Warn(GetErrorMessage(error));

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
        /// <param name="alternative">
        /// Value that returns when an exception occurs.
        /// </param>
        ///
        /// <returns>Function result.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T Warn<T>(Type type, Func<T> func, T alternative) =>
            Invoke(func, e => Warn(type, e), alternative);

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
        public static void Warn(Type type, Action action) =>
            Invoke(action, e => Warn(type, e));

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
        /// <param name="values">User messages.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Error(Type type, params string[] values) =>
            GetCore(type).Error(GetMessage(values));

        /* ----------------------------------------------------------------- */
        ///
        /// Error
        ///
        /// <summary>
        /// Outputs log as ERROR level.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="error">Exception object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Error(Type type, Exception error) =>
            GetCore(type).Error(GetErrorMessage(error));

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
        /// <param name="alternative">
        /// Value that returns when an exception occurs.
        /// </param>
        ///
        /// <returns>Function result.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T Error<T>(Type type, Func<T> func, T alternative) =>
            Invoke(func, e => Error(type, e), alternative);

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
        public static void Error(Type type, Action action) =>
            Invoke(action, e => Error(type, e));

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
        /// <param name="values">User messages.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Fatal(Type type, params string[] values) =>
            GetCore(type).Fatal(GetMessage(values));

        /* ----------------------------------------------------------------- */
        ///
        /// Fatal
        ///
        /// <summary>
        /// Outputs log as FATAL level.
        /// </summary>
        ///
        /// <param name="type">Targe type information.</param>
        /// <param name="error">Exception object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Fatal(Type type, Exception error) =>
            GetCore(type).Fatal(GetErrorMessage(error));

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
        /// <param name="alternative">
        /// Value that returns when an exception occurs.
        /// </param>
        ///
        /// <returns>Function result.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T Fatal<T>(Type type, Func<T> func, T alternative) =>
            Invoke(func, e => Fatal(type, e), alternative);

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
        public static void Fatal(Type type, Action action) =>
            Invoke(action, e => Fatal(type, e));

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
        private static NLog.Logger GetCore(Type type) => NLog.LogManager.GetLogger(type.FullName);

        /* ----------------------------------------------------------------- */
        ///
        /// GetMessage
        ///
        /// <summary>
        /// Gets the message from the specified arguments.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static string GetMessage(string[] values) =>
            values.Length == 1 ? values[0] :
            values.Length >  1 ? string.Join(Separator, values) : string.Empty;

        /* ----------------------------------------------------------------- */
        ///
        /// GetErrorMessage
        ///
        /// <summary>
        /// Gets the error message from the specified exception.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static string GetErrorMessage(Exception src) =>
            src is Win32Exception we ?
            $"{we.Message} (0x{we.NativeErrorCode:X8}){Environment.NewLine}{we.StackTrace}" :
            src.ToString();

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the specified function.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static T Invoke<T>(Func<T> func, Action<Exception> error, T alternative)
        {
            try { return func(); }
            catch (Exception err) { error(err); }
            return alternative;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the specified action.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void Invoke(Action action, Action<Exception> error)
        {
            try { action(); }
            catch (Exception err) { error(err); }
        }

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
            Error(typeof(TaskScheduler), e.Exception);

        #endregion
    }
}
