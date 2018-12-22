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
    /// Provides extended methods for logging.
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
        /// <param name="type">Target type.</param>
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

        #region Extended methods

        #region LogDebug

        /* ----------------------------------------------------------------- */
        ///
        /// LogDebug
        ///
        /// <summary>
        /// デバッグ情報をログに出力します。
        /// </summary>
        ///
        /// <param name="src">対象となるオブジェクト</param>
        /// <param name="message">メッセージ</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogDebug<T>(this T src, string message) =>
            Debug(src.GetType(), message);

        /* ----------------------------------------------------------------- */
        ///
        /// LogDebug
        ///
        /// <summary>
        /// デバッグ情報をログに出力します。
        /// </summary>
        ///
        /// <param name="src">対象となるオブジェクト</param>
        /// <param name="message">メッセージ</param>
        /// <param name="err">例外情報</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogDebug<T>(this T src, string message, Exception err) =>
            Debug(src.GetType(), message, err);

        /* ----------------------------------------------------------------- */
        ///
        /// LogDebug
        ///
        /// <summary>
        /// 実行時間をデバッグ情報としてログに出力します。
        /// </summary>
        ///
        /// <param name="src">対象となるオブジェクト</param>
        /// <param name="message">メッセージ</param>
        /// <param name="func">実行内容</param>
        ///
        /// <returns>実行結果</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static U LogDebug<T, U>(this T src, string message, Func<U> func) =>
            Debug(src.GetType(), message, func);

        /* ----------------------------------------------------------------- */
        ///
        /// LogDebug
        ///
        /// <summary>
        /// 実行時間をデバッグ情報としてログに出力します。
        /// </summary>
        ///
        /// <param name="src">対象となるオブジェクト</param>
        /// <param name="message">メッセージ</param>
        /// <param name="action">実行内容</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogDebug<T>(this T src, string message, Action action) =>
            Debug(src.GetType(), message, action);

        #endregion

        #region LogInfo

        /* ----------------------------------------------------------------- */
        ///
        /// LogInfo
        ///
        /// <summary>
        /// 情報をログに出力します。
        /// </summary>
        ///
        /// <param name="src">対象となるオブジェクト</param>
        /// <param name="message">メッセージ</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogInfo<T>(this T src, string message) =>
            Info(src.GetType(), message);

        /* ----------------------------------------------------------------- */
        ///
        /// LogInfo
        ///
        /// <summary>
        /// 情報をログに出力します。
        /// </summary>
        ///
        /// <param name="src">対象となるオブジェクト</param>
        /// <param name="message">メッセージ</param>
        /// <param name="err">例外情報</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogInfo<T>(this T src, string message, Exception err) =>
            Info(src.GetType(), message, err);

        /* ----------------------------------------------------------------- */
        ///
        /// LogInfo
        ///
        /// <summary>
        /// 情報をログに出力します。
        /// </summary>
        ///
        /// <param name="src">対象となるオブジェクト</param>
        /// <param name="assembly">アセンブリ情報</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogInfo<T>(this T src, Assembly assembly) =>
            Info(src.GetType(), assembly);

        /* ----------------------------------------------------------------- */
        ///
        /// LogInfo
        ///
        /// <summary>
        /// 実行時間をログに出力します。
        /// </summary>
        ///
        /// <param name="src">対象となるオブジェクト</param>
        /// <param name="message">メッセージ</param>
        /// <param name="func">実行内容</param>
        ///
        /// <returns>実行結果</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static U LogInfo<T, U>(this T src, string message, Func<U> func) =>
            Info(src.GetType(), message, func);

        /* ----------------------------------------------------------------- */
        ///
        /// LogInfo
        ///
        /// <summary>
        /// 実行時間をログに出力します。
        /// </summary>
        ///
        /// <param name="src">対象となるオブジェクト</param>
        /// <param name="message">メッセージ</param>
        /// <param name="action">実行内容</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogInfo<T>(this T src, string message, Action action) =>
            Info(src.GetType(), message, action);

        #endregion

        #region LogWarn

        /* ----------------------------------------------------------------- */
        ///
        /// LogWarn
        ///
        /// <summary>
        /// 警告をログに出力します。
        /// </summary>
        ///
        /// <param name="src">対象となるオブジェクト</param>
        /// <param name="message">メッセージ</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogWarn<T>(this T src, string message) =>
            Warn(src.GetType(), message);

        /* ----------------------------------------------------------------- */
        ///
        /// LogWarn
        ///
        /// <summary>
        /// 警告をログに出力します。
        /// </summary>
        ///
        /// <param name="src">対象となるオブジェクト</param>
        /// <param name="message">メッセージ</param>
        /// <param name="err">例外情報</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogWarn<T>(this T src, string message, Exception err) =>
            Warn(src.GetType(), message, err);

        /* ----------------------------------------------------------------- */
        ///
        /// LogWarn
        ///
        /// <summary>
        /// 例外発生時に警告としてログに出力します。
        /// </summary>
        ///
        /// <param name="src">対象となるオブジェクト</param>
        /// <param name="func">実行内容</param>
        ///
        /// <returns>実行結果</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static U LogWarn<T, U>(this T src, Func<U> func) =>
            LogWarn(src, func, default(U));

        /* ----------------------------------------------------------------- */
        ///
        /// LogWarn
        ///
        /// <summary>
        /// 例外発生時に警告としてログに出力します。
        /// </summary>
        ///
        /// <param name="src">対象となるオブジェクト</param>
        /// <param name="func">実行内容</param>
        /// <param name="err">エラー時の値</param>
        ///
        /// <returns>実行結果</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static U LogWarn<T, U>(this T src, Func<U> func, U err) =>
            Warn(src.GetType(), func, err);

        /* ----------------------------------------------------------------- */
        ///
        /// LogWarn
        ///
        /// <summary>
        /// 例外発生時に警告としてログに出力します。
        /// </summary>
        ///
        /// <param name="src">対象となるオブジェクト</param>
        /// <param name="action">実行内容</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogWarn<T>(this T src, Action action) =>
            Warn(src.GetType(), action);

        #endregion

        #region LogError

        /* ----------------------------------------------------------------- */
        ///
        /// LogError
        ///
        /// <summary>
        /// エラーをログに出力します。
        /// </summary>
        ///
        /// <param name="src">対象となるオブジェクト</param>
        /// <param name="message">メッセージ</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogError<T>(this T src, string message) =>
            Error(src.GetType(), message);

        /* ----------------------------------------------------------------- */
        ///
        /// LogError
        ///
        /// <summary>
        /// エラーをログに出力します。
        /// </summary>
        ///
        /// <param name="src">対象となるオブジェクト</param>
        /// <param name="message">メッセージ</param>
        /// <param name="err">例外情報</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogError<T>(this T src, string message, Exception err) =>
            Error(src.GetType(), message, err);

        /* ----------------------------------------------------------------- */
        ///
        /// LogError
        ///
        /// <summary>
        /// 例外発生時にエラーとしてログに出力します。
        /// </summary>
        ///
        /// <param name="src">対象となるオブジェクト</param>
        /// <param name="func">実行内容</param>
        ///
        /// <returns>実行結果</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static U LogError<T, U>(this T src, Func<U> func) =>
            LogError(src, func, default(U));

        /* ----------------------------------------------------------------- */
        ///
        /// LogError
        ///
        /// <summary>
        /// 例外発生時にエラーとしてログに出力します。
        /// </summary>
        ///
        /// <param name="src">対象となるオブジェクト</param>
        /// <param name="func">実行内容</param>
        /// <param name="err">エラー時の値</param>
        ///
        /// <returns>実行結果</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static U LogError<T, U>(this T src, Func<U> func, U err) =>
            Error(src.GetType(), func, err);

        /* ----------------------------------------------------------------- */
        ///
        /// LogError
        ///
        /// <summary>
        /// 例外発生時にエラーとしてログに出力します。
        /// </summary>
        ///
        /// <param name="src">対象となるオブジェクト</param>
        /// <param name="action">実行内容</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogError<T>(this T src, Action action) =>
            Error(src.GetType(), action);

        #endregion

        #region LogFatal

        /* ----------------------------------------------------------------- */
        ///
        /// LogFatal
        ///
        /// <summary>
        /// 致命的なエラーをログに出力します。
        /// </summary>
        ///
        /// <param name="src">対象となるオブジェクト</param>
        /// <param name="message">メッセージ</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogFatal<T>(this T src, string message) =>
            Fatal(src.GetType(), message);

        /* ----------------------------------------------------------------- */
        ///
        /// LogFatal
        ///
        /// <summary>
        /// 致命的なエラーをログに出力します。
        /// </summary>
        ///
        /// <param name="src">対象となるオブジェクト</param>
        /// <param name="message">メッセージ</param>
        /// <param name="err">例外情報</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogFatal<T>(this T src, string message, Exception err) =>
            Fatal(src.GetType(), message, err);

        /* ----------------------------------------------------------------- */
        ///
        /// LogFatal
        ///
        /// <summary>
        /// 例外発生時に致命的なエラーとしてログに出力します。
        /// </summary>
        ///
        /// <param name="src">対象となるオブジェクト</param>
        /// <param name="func">実行内容</param>
        ///
        /// <returns>実行結果</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static U LogFatal<T, U>(this T src, Func<U> func) =>
            LogFatal(src, func, default(U));

        /* ----------------------------------------------------------------- */
        ///
        /// LogFatal
        ///
        /// <summary>
        /// 例外発生時に致命的なエラーとしてログに出力します。
        /// </summary>
        ///
        /// <param name="src">対象となるオブジェクト</param>
        /// <param name="func">実行内容</param>
        /// <param name="err">エラー時の値</param>
        ///
        /// <returns>実行結果</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static U LogFatal<T, U>(this T src, Func<U> func, U err) =>
            Fatal(src.GetType(), func, err);

        /* ----------------------------------------------------------------- */
        ///
        /// LogFatal
        ///
        /// <summary>
        /// 例外発生時にエラーとしてログに出力します。
        /// </summary>
        ///
        /// <param name="src">対象となるオブジェクト</param>
        /// <param name="action">実行内容</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void LogFatal<T>(this T src, Action action) =>
            Fatal(src.GetType(), action);

        #endregion

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// GetCore
        ///
        /// <summary>
        /// ログ出力用オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static log4net.ILog GetCore(Type type) => log4net.LogManager.GetLogger(type);

        /* ----------------------------------------------------------------- */
        ///
        /// WhenTaskError
        ///
        /// <summary>
        /// UnobservedTaskException 発生時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void WhenTaskError(object s, UnobservedTaskExceptionEventArgs e) =>
            Error(typeof(TaskScheduler), e.Exception.ToString(), e.Exception);

        #endregion
    }
}
