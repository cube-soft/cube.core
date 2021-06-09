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
using System.Threading.Tasks;
using Cube.Mixin.Logging;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// Logger
    ///
    /// <summary>
    /// Provides settings and methods for logging.
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

        #endregion

        #region Implementations

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
            typeof(TaskScheduler).LogError(e.Exception);

        #endregion
    }
}
