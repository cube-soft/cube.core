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

namespace Cube.Forms.Controls
{
    /* --------------------------------------------------------------------- */
    ///
    /// ComboBoxExtension
    ///
    /// <summary>
    /// System.Windows.Forms.ComboBox の拡張用クラスです。
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
        /// ComboBox オブジェクトに対してデータ・バインディングを
        /// 実行します。
        /// </summary>
        ///
        /// <param name="src">ComboBox オブジェクト</param>
        /// <param name="data">データ</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Bind<T>(this System.Windows.Forms.ComboBox src,
            IEnumerable<KeyValuePair<string, T>> data)
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
