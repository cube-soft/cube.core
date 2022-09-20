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
namespace Cube.Xui.Logging.Extensions;

using System;
using System.Windows;
using System.Windows.Threading;

/* ------------------------------------------------------------------------- */
///
/// Methods
///
/// <summary>
/// Provides extended methods of the Logger class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class Methods
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// ObserveUiException
    ///
    /// <summary>
    /// Observes some exceptions and logs them.
    /// </summary>
    ///
    /// <param name="src">Target object.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void ObserveUiException(this Application src)
    {
        static void f0(object s, DispatcherUnhandledExceptionEventArgs e) => Logger.Error(e.Exception);
        if (src is not null)
        {
            src.DispatcherUnhandledException += f0;
            _disposable.Add(Disposable.Create(() => {
                if (src is not null) src.DispatcherUnhandledException -= f0;
            }));
        }

        static void f1(object s, UnhandledExceptionEventArgs e) => Logger.Error(e.ExceptionObject as Exception);
        if (AppDomain.CurrentDomain is not null) {
            AppDomain.CurrentDomain.UnhandledException += f1;
            _disposable.Add(Disposable.Create(() => {
                if (AppDomain.CurrentDomain is not null) AppDomain.CurrentDomain.UnhandledException -= f1;
            }));
        }
    }

    #endregion

    #region Fields
    private static readonly DisposableContainer _disposable = new();
    #endregion
}
