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

namespace Cube
{
    #region ProgressEventArgs

    /* --------------------------------------------------------------------- */
    ///
    /// ProgressEventArgs
    ///
    /// <summary>
    /// Provides methods to create a new instance of the
    /// ProgressEventArgs(T) class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class ProgressEventArgs
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// Creates a new instance of the ProgressEventArgs(T) class with
        /// the specified arguments.
        /// </summary>
        ///
        /// <param name="ratio">Current progress ratio.</param>
        /// <param name="value">Value to use for the event.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static ProgressEventArgs<T> Create<T>(double ratio, T value) =>
            new ProgressEventArgs<T>(ratio, value);

        #endregion
    }

    #endregion

    #region ProgressEventArgs<T>

    /* --------------------------------------------------------------------- */
    ///
    /// ProgressEventArgs(T)
    ///
    /// <summary>
    /// Provides progress ratio and a value of type T to use for events.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ProgressEventArgs<T> : ValueEventArgs<T>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ProgressEventArgs
        ///
        /// <summary>
        /// Creates a new instance of the ProgressEventArgs(T) class with
        /// the specified arguments.
        /// </summary>
        ///
        /// <param name="ratio">Current progress ratio.</param>
        /// <param name="value">Value to use for the event.</param>
        ///
        /* ----------------------------------------------------------------- */
        public ProgressEventArgs(double ratio, T value) : base(value)
        {
            Ratio = ratio;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Ratio
        ///
        /// <summary>
        /// Gets a current progress ratio.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public double Ratio { get; }

        #endregion
    }

    #endregion

    #region ProgressEventHandlers

    /* --------------------------------------------------------------------- */
    ///
    /// ProgressEventHandler(T)
    ///
    /// <summary>
    /// Represents the method to invoke an event.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public delegate void ProgressEventHandler<T>(object sender, ProgressEventArgs<T> e);

    #endregion
}
