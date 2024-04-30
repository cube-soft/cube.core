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
using System.Runtime.CompilerServices;
using System.Threading;

/* ------------------------------------------------------------------------- */
///
/// LocalizableText
///
/// <summary>
/// Provides the functionality to manage the localizable text group.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public abstract class LocalizableText : ILocalizable
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// LocalizableText
    ///
    /// <summary>
    /// Initializes a new instance of the LocalizableText class with the
    /// specified arguments.
    /// </summary>
    ///
    /// <param name="factory">
    /// Function to get a text group of the specified language.
    /// </param>
    ///
    /// <param name="fallback">
    /// Text group to be used if text in the specified language is not found.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    protected LocalizableText(Func<Language, TextGroup> factory, TextGroup fallback)
    {
        _factory = factory;
        Fallback = fallback;
        Exchange(Locale.GetDefaultLanguage());
    }

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Fallback
    ///
    /// <summary>
    /// Text group to be used if text in the specified language is not found.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected TextGroup Fallback { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// Current
    ///
    /// <summary>
    /// Text group corresponding to the currently specified language.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected TextGroup Current =>_current;

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Reset
    ///
    /// <summary>
    /// Resets the resource with the specified language value.
    /// </summary>
    ///
    /// <param name="src">Language value.</param>
    ///
    /* --------------------------------------------------------------------- */
    public void Reset(Language src) => OnReset(src);

    /* --------------------------------------------------------------------- */
    ///
    /// OnReset
    ///
    /// <summary>
    /// Occurs when the Reset method is invoked.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected virtual void OnReset(Language src) => Exchange(src);

    /* --------------------------------------------------------------------- */
    ///
    /// Get
    ///
    /// <summary>
    /// Gets the text corresponding to the specified name.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected string Get([CallerMemberName] string name = null)
    {
        if (name is null) return null;
        if (Current is not null && Current.TryGetValue(name, out var s0)) return s0;
        if (Fallback is not null && Fallback.TryGetValue(name, out var s1)) return s1;
        return name;
    }

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// Exchange
    ///
    /// <summary>
    /// Exchanges the new value with the specified language.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private void Exchange(Language src) => Interlocked.Exchange(ref _current, _factory(src));

    #endregion

    #region Fields
    private readonly Func<Language, TextGroup> _factory;
    private TextGroup _current;
    #endregion
}
