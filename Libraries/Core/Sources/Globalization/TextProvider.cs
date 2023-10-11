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
using Cube.Collections;

/* ------------------------------------------------------------------------- */
///
/// TextGroup
///
/// <summary>
/// Provides the functionality to get the text corresponding to a specific
/// language.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public abstract class TextProvider
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// TextProvider
    ///
    /// <summary>
    /// Initializes a new instance of the TextProvider class with the
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
    protected TextProvider(Func<Language, TextGroup> factory, TextGroup fallback) :
        this(factory, fallback, true) { }

    /* --------------------------------------------------------------------- */
    ///
    /// TextProvider
    ///
    /// <summary>
    /// Initializes a new instance of the TextProvider class with the
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
    /// <param name="subscribe">
    /// Value indicating whether or not to register the Reset method to the
    /// Subscribe static method of the Locale class.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    protected TextProvider(Func<Language, TextGroup> factory, TextGroup fallback, bool subscribe)
    {
        _factory = factory;
        Fallback = fallback;
        if (subscribe) Locale.Subscribe(Reset);
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
    protected TextGroup Current { get => _current; }

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Subscribe
    ///
    /// <summary>
    /// Adds the specified callback to the subscription.
    /// </summary>
    ///
    /// <param name="callback">
    /// Callback action when the text group changes.
    /// </param>
    ///
    /// <returns>
    /// Object to remove the registered callback.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public IDisposable Subscribe(Action callback) => _subscription.Subscribe(callback);

    /* --------------------------------------------------------------------- */
    ///
    /// Reset
    ///
    /// <summary>
    /// Resets the language settings.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public void Reset(Language src)
    {
        var dest = _factory(src);
        _ = Interlocked.Exchange(ref _current, dest);
        foreach (var callback in _subscription) callback();
    }

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
        if (Current is not null && Current.TryGetValue(name, out var s0)) return s0;
        if (Fallback is not null && Fallback.TryGetValue(name, out var s1)) return s1;
        return name;
    }

    #endregion

    #region Fields
    private readonly Func<Language, TextGroup> _factory;
    private readonly Subscription<Action> _subscription = new();
    private TextGroup _current;
    #endregion
}
