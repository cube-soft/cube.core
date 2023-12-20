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
namespace Cube.Xui.Converters;

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

/* ------------------------------------------------------------------------- */
///
/// DuplexConverter
///
/// <summary>
/// Represents the base class that supports Convert and ConvertBack
/// methods.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public abstract class DuplexConverter : MarkupExtension, IValueConverter
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// DuplexConverter
    ///
    /// <summary>
    /// Initializes a new instance of the DuplexConverter class with
    /// the specified arguments.
    /// </summary>
    ///
    /// <param name="convert">Function to convert.</param>
    /// <param name="back">Function for reverse conversion.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected DuplexConverter(Func<object, object> convert, Func<object, object> back) :
        this((e, _, _, _) => convert(e), (e, _, _, _) => back(e)) { }

    /* --------------------------------------------------------------------- */
    ///
    /// DuplexConverter
    ///
    /// <summary>
    /// Initializes a new instance of the DuplexConverter class with
    /// the specified arguments.
    /// </summary>
    ///
    /// <param name="convert">Function to convert.</param>
    /// <param name="back">Function for reverse conversion.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected DuplexConverter(
        Func<object, Type, object, CultureInfo, object> convert,
        Func<object, Type, object, CultureInfo, object> back)
    {
        _convert = convert;
        _back    = back;
    }

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Convert
    ///
    /// <summary>
    /// Invokes the conversion.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public object Convert(object value, Type target, object parameter, CultureInfo culture) =>
        _convert(value, target, parameter, culture);

    /* --------------------------------------------------------------------- */
    ///
    /// ConvertBack
    ///
    /// <summary>
    /// Invokes the reverse conversion.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public object ConvertBack(object value, Type target, object parameter, CultureInfo culture) =>
        _back(value, target, parameter, culture);

    /* --------------------------------------------------------------------- */
    ///
    /// ProvideValue
    ///
    /// <summary>
    /// Returns the self object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public override object ProvideValue(IServiceProvider serviceProvider) => this;

    #endregion

    #region Fields
    private readonly Func<object, Type, object, CultureInfo, object> _convert;
    private readonly Func<object, Type, object, CultureInfo, object> _back;
    #endregion
}
