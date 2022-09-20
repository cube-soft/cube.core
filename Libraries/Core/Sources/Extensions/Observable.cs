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
namespace Cube.Observable.Extensions;

using System;
using System.ComponentModel;

/* ------------------------------------------------------------------------- */
///
/// Methods
///
/// <summary>
/// Provides extended methods of INotifyPropertyChanged,
/// IObservePropertyChanged, and their related classes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class Methods
{
    /* --------------------------------------------------------------------- */
    ///
    /// Subscribe
    ///
    /// <summary>
    /// Associates the specified callback to the PropertyChanged event.
    /// </summary>
    ///
    /// <param name="src">Observable source.</param>
    /// <param name="handler">
    /// Action to invoked when the PropertyChanged event is fired.
    /// </param>
    ///
    /// <returns>
    /// Object to remove the callback from the PropertyChanged event
    /// handler.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public static IDisposable Subscribe(this INotifyPropertyChanged src, ObservableHandler handler)
    {
        void f(object s, PropertyChangedEventArgs e) => handler(e.PropertyName);
        src.PropertyChanged += f;
        return Disposable.Create(() => src.PropertyChanged -= f);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Subscribe
    ///
    /// <summary>
    /// Associates the specified callback to the PropertyChanged event.
    /// </summary>
    ///
    /// <param name="src">Observable source.</param>
    ///
    /// <param name="map">
    /// Object to map PropertyChanged events to actions.
    /// </param>
    ///
    /// <param name="others">
    /// Handler called when PropertyChanged event does not match any of the
    /// map items.
    /// </param>
    ///
    /// <returns>
    /// Object to remove the callback from the PropertyChanged event
    /// handler.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public static IDisposable Subscribe(this INotifyPropertyChanged src,
        ObservableHandlerDictionary map, ObservableHandler others)
    {
        void f(object s, PropertyChangedEventArgs e)
        {
            if (map.TryGetValue(e.PropertyName, out var h)) h(e.PropertyName);
            else others?.Invoke(e.PropertyName);
        }
        src.PropertyChanged += f;
        return Disposable.Create(() => src.PropertyChanged -= f);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Forward
    ///
    /// <summary>
    /// Forwards the PropertyChanged events to the specified ObservableBase
    /// object.
    /// </summary>
    ///
    /// <param name="src">Observable source.</param>
    /// <param name="dest">Observable target.</param>
    ///
    /// <returns>
    /// Object to remove the callback from the PropertyChanged event
    /// handler.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public static IDisposable Forward(this INotifyPropertyChanged src, ObservableBase dest) =>
        src.Subscribe(dest.Refresh);

    /* --------------------------------------------------------------------- */
    ///
    /// Hook
    ///
    /// <summary>
    /// Observes the specified observer and the specified object.
    /// </summary>
    ///
    /// <param name="src">Source observer.</param>
    /// <param name="obj">Object to be observed.</param>
    /// <param name="names">Target property names.</param>
    ///
    /// <returns>Source observer.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static T Hook<T>(this T src, INotifyPropertyChanged obj, params string[] names)
        where T : IObservePropertyChanged
    {
        src.Observe(obj, names);
        return src;
    }
}
