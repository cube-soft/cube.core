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
namespace Cube.Mixin.Forms.Controls;

using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

/* ------------------------------------------------------------------------- */
///
/// ComboBoxExtension
///
/// <summary>
/// Provides the extended methods of the ComboBox class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class ComboBoxExtension
{
    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static void Bind<T>(this ComboBox src, IList<KeyValuePair<string, T>> data)
    {
        var obj = src.SelectedValue;
        var cmp = EqualityComparer<T>.Default;

        src.DataSource    = data;
        src.DisplayMember = "Key";
        src.ValueMember   = "Value";

        if (obj is T v && data.Any(e => cmp.Equals(e.Value, v))) src.SelectedValue = v;
        else if (data.Count > 0) src.SelectedValue = data[0].Value;
    }
}
