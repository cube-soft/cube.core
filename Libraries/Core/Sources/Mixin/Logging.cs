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
using System.Reflection;
using System.Threading.Tasks;
using Cube.Mixin.Assembly;
using Cube.Mixin.Collections;

namespace Cube.Mixin.Logging
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
    public static class Extension
    {
        #region Methods

        #region Debug

        /* ----------------------------------------------------------------- */
        ///
        /// LogDebug
        ///
        /// <summary>
        /// Outputs log as DEBUG level.
        /// </summary>
        ///
        /// <param name="type">Target type information.</param>
        /// <param name="values">User messages.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogDebug(this Type type, params string[] values) =>
            GetCore(type).Debug(GetMessage(values));

        #endregion

        #region Info

        /* ----------------------------------------------------------------- */
        ///
        /// LogInfo
        ///
        /// <summary>
        /// Outputs log as INFO level.
        /// </summary>
        ///
        /// <param name="type">Target type information.</param>
        /// <param name="values">User messages.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogInfo(this Type type, params string[] values) =>
            GetCore(type).Info(GetMessage(values));

        /* ----------------------------------------------------------------- */
        ///
        /// LogInfo
        ///
        /// <summary>
        /// Outputs system information as INFO level.
        /// </summary>
        ///
        /// <param name="type">Target type information.</param>
        /// <param name="assembly">Assembly object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogInfo(this Type type, System.Reflection.Assembly assembly)
        {
            LogInfo(type, $"{assembly.GetProduct()} {assembly.GetVersionString(4, true)}");
            LogInfo(type, $"CLR {System.Environment.Version} ({System.Environment.OSVersion})");
            LogInfo(type, $"{System.Environment.UserName}@{System.Environment.MachineName}");
        }

        #endregion

        #region Warn

        /* ----------------------------------------------------------------- */
        ///
        /// LogWarn
        ///
        /// <summary>
        /// Outputs log as WARN level.
        /// </summary>
        ///
        /// <param name="type">Target type information.</param>
        /// <param name="values">User messages.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogWarn(this Type type, params string[] values) =>
            GetCore(type).Warn(GetMessage(values));

        /* ----------------------------------------------------------------- */
        ///
        /// LogWarn
        ///
        /// <summary>
        /// Outputs log as WARN level.
        /// </summary>
        ///
        /// <param name="type">Target type information.</param>
        /// <param name="error">Exception object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogWarn(this Type type, Exception error) =>
            GetCore(type).Warn(GetErrorMessage(error));

        /* ----------------------------------------------------------------- */
        ///
        /// LogWarn
        ///
        /// <summary>
        /// Outputs log as WARN level when an exception occurs.
        /// </summary>
        ///
        /// <param name="type">Target type information.</param>
        /// <param name="action">Function to monitor.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogWarn(this Type type, Action action) =>
            Invoke(action, e => LogWarn(type, e));

        #endregion

        #region Error

        /* ----------------------------------------------------------------- */
        ///
        /// LogError
        ///
        /// <summary>
        /// Outputs log as ERROR level.
        /// </summary>
        ///
        /// <param name="type">Target type information.</param>
        /// <param name="values">User messages.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogError(this Type type, params string[] values) =>
            GetCore(type).Error(GetMessage(values));

        /* ----------------------------------------------------------------- */
        ///
        /// LogError
        ///
        /// <summary>
        /// Outputs log as ERROR level.
        /// </summary>
        ///
        /// <param name="type">Target type information.</param>
        /// <param name="error">Exception object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogError(this Type type, Exception error) =>
            GetCore(type).Error(GetErrorMessage(error));

        /* ----------------------------------------------------------------- */
        ///
        /// LogError
        ///
        /// <summary>
        /// Outputs log as ERROR level when an exception occurs.
        /// </summary>
        ///
        /// <param name="type">Target type information.</param>
        /// <param name="action">Function to monitor.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogError(this Type type, Action action) =>
            Invoke(action, e => LogError(type, e));

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
            values.Length >  1 ? values.Join(Logger.Separator) : string.Empty;

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
            $"{we.Message} (0x{we.NativeErrorCode:X8}){System.Environment.NewLine}{we.StackTrace}" :
            src.ToString();

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
            LogError(typeof(TaskScheduler), e.Exception);

        #endregion
    }
}
