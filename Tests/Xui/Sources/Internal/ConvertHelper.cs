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
namespace Cube.Xui.Tests;

using System.Globalization;
using System.Windows.Data;

/* ------------------------------------------------------------------------- */
///
/// ConvertHelper
///
/// <summary>
/// Provides support functions to test converters.
/// </summary>
///
/* ------------------------------------------------------------------------- */
class ConvertHelper
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Convert
    ///
    /// <summary>
    /// Invokes the IValueConverter.Convert method.
    /// </summary>
    ///
    /// <param name="src">Object to invoke the Convert method.</param>
    /// <param name="value">Source value.</param>
    ///
    /// <returns>Result value.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public T Convert<T>(IValueConverter src, object value) =>
        (T)src.Convert(value, typeof(T), null, CultureInfo.CurrentCulture);

    /* --------------------------------------------------------------------- */
    ///
    /// ConvertBack
    ///
    /// <summary>
    /// Invokes the IValueConverter.ConvertBack method.
    /// </summary>
    ///
    /// <param name="src">Object to invoke the ConvertBack method.</param>
    /// <param name="value">Source value.</param>
    ///
    /// <returns>Result value.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public T ConvertBack<T>(IValueConverter src, object value) =>
        (T)src.ConvertBack(value, typeof(T), null, CultureInfo.CurrentCulture);

    #endregion
}
