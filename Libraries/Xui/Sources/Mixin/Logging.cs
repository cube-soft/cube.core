﻿/* ------------------------------------------------------------------------- */
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
using System.Windows;
using System.Windows.Threading;

namespace Cube.Logging
{
    /* --------------------------------------------------------------------- */
    ///
    /// XuiExtension
    ///
    /// <summary>
    /// Provides extended methods of the Logger class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class XuiExtension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// ObserveUiException
        ///
        /// <summary>
        /// Monitors UnhandledException and outputs to the log.
        /// </summary>
        ///
        /// <param name="src">Target object.</param>
        ///
        /// <returns>Object to dispose.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IDisposable ObserveUiException(this Application src)
        {
            if (src != null) src.DispatcherUnhandledException += WhenDispatcherError;
            if (AppDomain.CurrentDomain != null) AppDomain.CurrentDomain.UnhandledException += WhenDomainError;

            return Disposable.Create(() =>
            {
                if (src != null) src.DispatcherUnhandledException -= WhenDispatcherError;
                if (AppDomain.CurrentDomain != null) AppDomain.CurrentDomain.UnhandledException -= WhenDomainError;
            });
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// WhenDispatcherError
        ///
        /// <summary>
        /// Executes when a DispatcherUnhandledException occurs.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void WhenDispatcherError(object s, DispatcherUnhandledExceptionEventArgs e) =>
            s.GetType().LogError(e.Exception);

        /* ----------------------------------------------------------------- */
        ///
        /// WhenDomainError
        ///
        /// <summary>
        /// Executes when an UnhandledException occurs.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void WhenDomainError(object s, UnhandledExceptionEventArgs e) =>
            typeof(AppDomain).LogError(e.ExceptionObject as Exception);

        #endregion
    }
}
