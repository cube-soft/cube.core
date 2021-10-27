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
using System.Windows.Forms;

namespace Cube.Mixin.Forms.Controls
{
    /* --------------------------------------------------------------------- */
    ///
    /// ComboBoxExtension
    ///
    /// <summary>
    /// Provides the extended methods of the ComboBox class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class ComboBoxExtension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Bind
        ///
        /// <summary>
        /// Binds the specified ComboBox object with the specified data.
        /// </summary>
        ///
        /// <param name="src">ComboBox object.</param>
        /// <param name="data">UserData</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Bind<T>(this ComboBox src, IEnumerable<KeyValuePair<string, T>> data)
        {
            var selected = src.SelectedValue;

            src.DataSource    = data;
            src.DisplayMember = "Key";
            src.ValueMember   = "Value";

            if (selected is T) src.SelectedValue = selected;
        }

        #endregion
    }
}
