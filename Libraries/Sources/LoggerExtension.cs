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
using System.Windows;
using System.Windows.Threading;

namespace Cube.Xui
{
    /* --------------------------------------------------------------------- */
    ///
    /// LoggerExtension
    ///
    /// <summary>
    /// Logger オブジェクトに対する操作を定義するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class LoggerExtension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// ObserveUiException
        ///
        /// <summary>
        /// UnhandledException を監視し、取得した例外をログに出力します。
        /// </summary>
        ///
        /// <param name="src">監視対象となるオブジェクト</param>
        ///
        /// <returns>監視を解除するためのオブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IDisposable ObserveUiException(this Application src)
        {
            if (src != null) src.DispatcherUnhandledException += WhenDispatcherError;
            AppDomain.CurrentDomain.UnhandledException += WhenDomainError;

            return Disposable.Create(() =>
            {
                if (src != null) src.DispatcherUnhandledException -= WhenDispatcherError;
                AppDomain.CurrentDomain.UnhandledException -= WhenDomainError;
            });
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// WhenDispatcherError
        ///
        /// <summary>
        /// DispatcherUnhandledException 発生時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void WhenDispatcherError(object s, DispatcherUnhandledExceptionEventArgs e) =>
            s.LogError(e.Exception.ToString(), e.Exception);

        /* ----------------------------------------------------------------- */
        ///
        /// WhenDomainError
        ///
        /// <summary>
        /// UnhandledException 発生時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void WhenDomainError(object s, UnhandledExceptionEventArgs e) =>
            Logger.Error(typeof(AppDomain), e.ExceptionObject.ToString(), e.ExceptionObject as Exception);

        #endregion
    }
}
