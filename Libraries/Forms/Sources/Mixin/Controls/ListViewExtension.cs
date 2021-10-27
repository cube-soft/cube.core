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
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Cube.Mixin.Forms.Controls
{
    /* --------------------------------------------------------------------- */
    ///
    /// ListViewExtension
    ///
    /// <summary>
    /// Provides extended methods of the ListView class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class ListViewExtension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Ascend
        ///
        /// <summary>
        /// Sorts the specified SelectedIndexCollection object in ascending
        /// order.
        /// </summary>
        ///
        /// <param name="src">Source collection.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<int> Ascend(this ListView.SelectedIndexCollection src) =>
            src.Cast<int>().OrderBy(i => i);

        /* ----------------------------------------------------------------- */
        ///
        /// Descend
        ///
        /// <summary>
        /// Sorts the specified SelectedIndexCollection object in
        /// descending order.
        /// </summary>
        ///
        /// <param name="src">Source collection.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<int> Descend(ListView.SelectedIndexCollection src) =>
            src.Cast<int>().OrderByDescending(i => i);

        #endregion
    }
}
