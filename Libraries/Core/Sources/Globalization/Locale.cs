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
namespace Cube.Globalization;

using System;
using System.Globalization;
using System.Threading;
using Cube.Collections;

/* ------------------------------------------------------------------------- */
///
/// Locale
///
/// <summary>
/// Provides the event trigger to change the locale.
/// </summary>
///
/// <seealso href="https://msdn.microsoft.com/ja-jp/library/cc392381.aspx" />
///
/* ------------------------------------------------------------------------- */
public static class Locale
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// Locale
    ///
    /// <summary>
    /// Initializes static fields.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    static Locale()
    {
        _default  = new(Language.Auto);
        _accessor = _default;
        _culture  = CultureInfo.CurrentCulture;
    }

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// GetCurrentLanguage
    ///
    /// <summary>
    /// Gets the current Language value.
    /// </summary>
    ///
    /// <returns>Language value.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static Language GetCurrentLanguage() => _accessor.Get();

    /* --------------------------------------------------------------------- */
    ///
    /// GetDefaultLanguage
    ///
    /// <summary>
    /// Gets the default Language value.
    /// </summary>
    ///
    /// <returns>Language value.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static Language GetDefaultLanguage() => GetDefaultCultureInfo().ToLanguage();

    /* --------------------------------------------------------------------- */
    ///
    /// GetDefaultCultureInfo
    ///
    /// <summary>
    /// Gets the default CultureInfo object.
    /// </summary>
    ///
    /// <returns>CultureInfo object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static CultureInfo GetDefaultCultureInfo() => _culture;

    /* --------------------------------------------------------------------- */
    ///
    /// Reset
    ///
    /// <summary>
    /// Resets the current locale with the specified Language value.
    /// </summary>
    ///
    /// <param name="value">Language value.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Reset(Language value)
    {
        if (!_accessor.Set(value)) return;
        foreach (var e in _resource) e.Reset(value);
        foreach (var e in _receiver) e(value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Subscribe
    ///
    /// <summary>
    /// Adds the specified localizable resource to the subscription.
    /// </summary>
    ///
    /// <param name="src">Localizable resource.</param>
    ///
    /// <returns>Object to remove the registered resource.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static IDisposable Subscribe(ILocalizable src) => _resource.Subscribe(src);

    /* --------------------------------------------------------------------- */
    ///
    /// Subscribe
    ///
    /// <summary>
    /// Adds the specified callback action to the subscription.
    /// </summary>
    ///
    /// <param name="action">Action when the locale changes.</param>
    ///
    /// <returns>Object to remove the registered callback.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static IDisposable Subscribe(Action<Language> action) => _receiver.Subscribe(action);

    /* --------------------------------------------------------------------- */
    ///
    /// Configure
    ///
    /// <summary>
    /// Resets the setter function.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static void Configure() => Configure(_default);

    /* --------------------------------------------------------------------- */
    ///
    /// Configure
    ///
    /// <summary>
    /// Updates the accessor of the language.
    /// </summary>
    ///
    /// <param name="accessor">Accessor object.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Configure(Accessor<Language> accessor) =>
        Interlocked.Exchange(ref _accessor, accessor);

    #endregion

    #region Fields
    private static Accessor<Language> _accessor;
    private static readonly CultureInfo _culture;
    private static readonly Accessor<Language> _default;
    private static readonly Subscription<ILocalizable> _resource = new();
    private static readonly Subscription<Action<Language>> _receiver = new();
    #endregion
}
