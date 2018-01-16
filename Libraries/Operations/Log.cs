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
using System.Reflection;
using log4net;

namespace Cube.Log
{
    /* --------------------------------------------------------------------- */
    ///
    /// Log.Operations
    /// 
    /// <summary>
    /// Log オブジェクトに対する操作を定義するための拡張メソッド用クラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public static class Operations
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Configure
        ///
        /// <summary>
        /// 初期設定を行います。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static void Configure()
            => log4net.Config.XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetCallingAssembly()));

        /* ----------------------------------------------------------------- */
        ///
        /// ObserveTaskException
        ///
        /// <summary>
        /// UnobservedTaskException を監視し、取得した例外をログに
        /// 出力します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static void ObserveTaskException()
        {
            System.Threading.Tasks.TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                var type = typeof(System.Threading.Tasks.TaskScheduler);
                Error(type, e.Exception.ToString());
            };
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Debug
        ///
        /// <summary>
        /// デバッグ情報をログに出力します。
        /// </summary>
        /// 
        /// <param name="type">対象となるオブジェクトの型情報</param>
        /// <param name="message">メッセージ</param>
        /// 
        /* ----------------------------------------------------------------- */
        public static void Debug(Type type, string message)
            => Logger(type).Debug(message);

        /* ----------------------------------------------------------------- */
        ///
        /// Debug
        ///
        /// <summary>
        /// デバッグ情報をログに出力します。
        /// </summary>
        /// 
        /// <param name="type">対象となるオブジェクトの型情報</param>
        /// <param name="message">メッセージ</param>
        /// <param name="err">例外情報</param>
        /// 
        /* ----------------------------------------------------------------- */
        public static void Debug(Type type, string message, Exception err)
            => Logger(type).Debug(message, err);

        /* ----------------------------------------------------------------- */
        ///
        /// Info
        ///
        /// <summary>
        /// 情報をログに出力します。
        /// </summary>
        /// 
        /// <param name="type">対象となるオブジェクトの型情報</param>
        /// <param name="message">メッセージ</param>
        /// 
        /* ----------------------------------------------------------------- */
        public static void Info(Type type, string message)
            => Logger(type).Info(message);

        /* ----------------------------------------------------------------- */
        ///
        /// Info
        ///
        /// <summary>
        /// 情報をログに出力します。
        /// </summary>
        /// 
        /// <param name="type">対象となるオブジェクトの型情報</param>
        /// <param name="message">メッセージ</param>
        /// <param name="err">例外情報</param>
        /// 
        /* ----------------------------------------------------------------- */
        public static void Info(Type type, string message, Exception err)
            => Logger(type).Info(message, err);

        /* ----------------------------------------------------------------- */
        ///
        /// Info
        ///
        /// <summary>
        /// システム情報をログに出力します。
        /// </summary>
        /// 
        /// <param name="type">対象となるオブジェクトの型情報</param>
        /// <param name="assembly">アセンブリ情報</param>
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
        /// Warn
        ///
        /// <summary>
        /// 警告をログに出力します。
        /// </summary>
        /// 
        /// <param name="type">対象となるオブジェクトの型情報</param>
        /// <param name="message">メッセージ</param>
        /// 
        /* ----------------------------------------------------------------- */
        public static void Warn(Type type, string message)
            => Logger(type).Warn(message);

        /* ----------------------------------------------------------------- */
        ///
        /// Warn
        ///
        /// <summary>
        /// 警告をログに出力します。
        /// </summary>
        /// 
        /// <param name="type">対象となるオブジェクトの型情報</param>
        /// <param name="message">メッセージ</param>
        /// <param name="err">例外情報</param>
        /// 
        /* ----------------------------------------------------------------- */
        public static void Warn(Type type, string message, Exception err)
            => Logger(type).Warn(message, err);

        /* ----------------------------------------------------------------- */
        ///
        /// Error
        ///
        /// <summary>
        /// エラーをログに出力します。
        /// </summary>
        /// 
        /// <param name="type">対象となるオブジェクトの型情報</param>
        /// <param name="message">メッセージ</param>
        /// 
        /* ----------------------------------------------------------------- */
        public static void Error(Type type, string message)
            => Logger(type).Error(message);

        /* ----------------------------------------------------------------- */
        ///
        /// Error
        ///
        /// <summary>
        /// エラーをログに出力します。
        /// </summary>
        /// 
        /// <param name="type">対象となるオブジェクトの型情報</param>
        /// <param name="message">メッセージ</param>
        /// <param name="err">例外情報</param>
        /// 
        /* ----------------------------------------------------------------- */
        public static void Error(Type type, string message, Exception err)
            => Logger(type).Error(message, err);

        /* ----------------------------------------------------------------- */
        ///
        /// Fatal
        ///
        /// <summary>
        /// 致命的なエラーをログに出力します。
        /// </summary>
        /// 
        /// <param name="type">対象となるオブジェクトの型情報</param>
        /// <param name="message">メッセージ</param>
        /// 
        /* ----------------------------------------------------------------- */
        public static void Fatal(Type type, string message)
            => Logger(type).Fatal(message);

        /* ----------------------------------------------------------------- */
        ///
        /// Fatal
        ///
        /// <summary>
        /// 致命的なエラーをログに出力します。
        /// </summary>
        /// 
        /// <param name="type">対象となるオブジェクトの型情報</param>
        /// <param name="message">メッセージ</param>
        /// <param name="err">例外情報</param>
        /// 
        /* ----------------------------------------------------------------- */
        public static void Fatal(Type type, string message, Exception err)
            => Logger(type).Fatal(message, err);

        #endregion

        #region Extended methods

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
        public static void LogDebug<T>(this T src, string message)
            => src.Logger().Debug(message);

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
        public static void LogDebug<T>(this T src, string message, Exception err)
            => src.Logger().Debug(message, err);

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
        public static void LogInfo<T>(this T src, string message)
            => src.Logger().Info(message);

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
        public static void LogInfo<T>(this T src, string message, Exception err)
            => src.Logger().Info(message, err);

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
        public static void LogInfo<T>(this T src, Assembly assembly)
            => Info(src.GetType(), assembly);

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
        public static void LogWarn<T>(this T src, string message)
            => src.Logger().Warn(message);

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
        public static void LogWarn<T>(this T src, string message, Exception err)
            => src.Logger().Warn(message, err);

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
        public static void LogError<T>(this T src, string message)
            => src.Logger().Error(message);

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
        public static void LogError<T>(this T src, string message, Exception err)
            => src.Logger().Error(message, err);

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
        public static void LogFatal<T>(this T src, string message)
            => src.Logger().Fatal(message);

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
        public static void LogFatal<T>(this T src, string message, Exception err)
            => src.Logger().Fatal(message, err);

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// Logger
        ///
        /// <summary>
        /// ログ出力用オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static ILog Logger(Type type)
            => LogManager.GetLogger(type);

        /* ----------------------------------------------------------------- */
        ///
        /// Logger
        ///
        /// <summary>
        /// ログ出力用オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static ILog Logger<T>(this T src)
            => Logger(src.GetType());

        #endregion
    }
}
