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
using System.Threading;
using System.Windows.Forms;
using Cube.Mixin.String;

namespace Cube.Mixin.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// FormExtension
    ///
    /// <summary>
    /// Provides the extended methods of the Form class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class FormExtension
    {
        #region Methods

        #region UpdateCulture

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateCulture
        ///
        /// <summary>
        /// Updates the culture information of the specified language.
        /// </summary>
        ///
        /// <param name="src">Form object.</param>
        /// <param name="value">Language to show.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void UpdateCulture<T>(this T src, Language value) where T : Form
        {
            var ci = value.ToCultureInfo();
            Thread.CurrentThread.CurrentCulture   = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            var crm = new ComponentResourceManager(typeof(T));
            crm.ApplyResources(src, "$this");
            UpdateCulture(src.Controls, crm);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateCulture
        ///
        /// <summary>
        /// Updates the culture information.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void UpdateCulture(Control.ControlCollection src, ComponentResourceManager crm)
        {
            foreach (Control e in src)
            {
                crm.ApplyResources(e, e.Name);
                UpdateCulture(e.Controls, crm);
            }
        }

        #endregion

        #region UpdateText

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateText
        ///
        /// <summary>
        /// Update the title of the form with the notation
        /// "message - ProductName".
        /// </summary>
        ///
        /// <param name="src">Form object.</param>
        /// <param name="text">Text to show at the title bar.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void UpdateText(this Form src, string text) =>
            src.UpdateText(text, src.ProductName);

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateText
        ///
        /// <summary>
        /// Update the title of the form with the notation
        /// "message - ProductName".
        /// </summary>
        ///
        /// <param name="src">Form object.</param>
        /// <param name="text">Text to show at the title bar.</param>
        /// <param name="product">Product name.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void UpdateText(this Form src, string text, string product)
        {
            var ss = new System.Text.StringBuilder();

            _ = ss.Append(text);
            if (text.HasValue() && product.HasValue()) _ = ss.Append(" - ");
            _ = ss.Append(product);

            src.Text = ss.ToString();
        }

        #endregion

        #region TopMost

        /* ----------------------------------------------------------------- */
        ///
        /// ResetTopMost
        ///
        /// <summary>
        /// Resets the value of the TopMost property.
        /// </summary>
        ///
        /// <param name="src">Form object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void ResetTopMost(this Form src)
        {
            var tmp = src.TopMost;
            src.TopMost = false;
            src.TopMost = true;
            src.TopMost = tmp;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetTopMost
        ///
        /// <summary>
        /// Shows the specified window at the top most.
        /// </summary>
        ///
        /// <param name="src">Form object.</param>
        /// <param name="active">
        /// Value indicating whether to make the specified window active.
        /// </param>
        ///
        /// <remarks>
        /// The method is mainly used to display at the top-most without
        /// taking the focus away. In this case, the TopMost property
        /// will be false even when it is displayed in the top-most
        /// position.
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static void SetTopMost(this Form src, bool active)
        {
            if (active)
            {
                src.Activate();
                src.TopMost = true;
            }
            else src.SetTopMostWithoutActivate();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetTopMostWithoutActivate
        ///
        /// <summary>
        /// Sets the TopMost flag without making the window active.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void SetTopMostWithoutActivate(this Form src)
        {
            var pos  = src.Location;
            var size = src.ClientSize;

            if (Cube.Forms.User32.NativeMethods.SetWindowPos(src.Handle,
                (IntPtr)(-1), // HWND_TOPMOST
                0, 0, 0, 0,
                0x0413 // SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSENDCHANGING | SWP_NOSIZE
            )) {
                src.Location   = pos;
                src.ClientSize = size;
            }
        }

        #endregion

        #endregion
    }
}
