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
/// SimplexConverter
///
/// <summary>
/// Provides functionality to convert from the provided arguments.
/// The class throws the NotSupportedException if the ConvertBack
/// method is invoked.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public abstract class SimplexConverter : MarkupExtension, IValueConverter
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// SimplexConverter
    ///
    /// <summary>
    /// Initializes a new instance of the SimplexConverter class with
    /// the specified function.
    /// </summary>
    ///
    /// <param name="func">Function to convert.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected SimplexConverter(Func<object, object> func) : this((e, _, _, _) => func(e)) { }

    /* --------------------------------------------------------------------- */
    ///
    /// SimplexConverter
    ///
    /// <summary>
    /// Initializes a new instance of the SimplexConverter with the
    /// specified function.
    /// </summary>
    ///
    /// <param name="func">Function to convert.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected SimplexConverter(Func<object, Type, object, CultureInfo, object> func) => _func = func;

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
        _func(value, target, parameter, culture);

    /* --------------------------------------------------------------------- */
    ///
    /// ConvertBack
    ///
    /// <summary>
    /// The class does not support the method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public object ConvertBack(object value, Type target, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();

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
    private readonly Func<object, Type, object, CultureInfo, object> _func;
    #endregion
}
