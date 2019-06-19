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
namespace Cube.Forms
{
    #region IBindable

    /* --------------------------------------------------------------------- */
    ///
    /// IBinder
    ///
    /// <summary>
    /// Represents the interface that a window can be bindable with a
    /// presentable object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public interface IBinder
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Bind
        ///
        /// <summary>
        /// Binds the specified object.
        /// </summary>
        ///
        /// <param name="src">Object to bind.</param>
        ///
        /* ----------------------------------------------------------------- */
        void Bind(IPresentable src);
    }


    #endregion

    #region IDpiAwarable

    /* --------------------------------------------------------------------- */
    ///
    /// IDpiAwarable
    ///
    /// <summary>
    /// Represents the interface that a window or control can be aware of
    /// DPI changing.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public interface IDpiAwarable
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Dpi
        ///
        /// <summary>
        /// Gets or sets the current DPI.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        double Dpi { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// DpiChanged
        ///
        /// <summary>
        /// Occurs when the current DPI is changed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        event ValueChangedEventHandler<double> DpiChanged;
    }

    #endregion
}
