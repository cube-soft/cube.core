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
namespace Cube.Xui;

using System;
using System.Windows;

/* ------------------------------------------------------------------------- */
///
/// DependencyFactory
///
/// <summary>
/// Provides functionality to create DependencyProperty objects.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class DependencyFactory
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Create
    ///
    /// <summary>
    /// Creates a new instance of the DependencyProperty class
    /// with the specified types and arguments.
    /// </summary>
    ///
    /// <typeparam name="TOwner">Owner type.</typeparam>
    /// <typeparam name="TProperty">Property type.</typeparam>
    ///
    /// <param name="name">Property name.</param>
    /// <param name="callback">Action to set a new value.</param>
    ///
    /// <returns>DependencyProperty object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static DependencyProperty Create<TOwner, TProperty>(
        string name,
        Action<TOwner, TProperty> callback
    ) where TOwner : DependencyObject => Create(name, default, callback);

    /* --------------------------------------------------------------------- */
    ///
    /// Create
    ///
    /// <summary>
    /// Creates a new instance of the DependencyProperty class
    /// with the specified types and arguments.
    /// </summary>
    ///
    /// <typeparam name="TOwner">Owner type.</typeparam>
    /// <typeparam name="TProperty">Property type.</typeparam>
    ///
    /// <param name="name">Property name.</param>
    /// <param name="value">Default value of the property.</param>
    /// <param name="callback">Action to set a new value.</param>
    ///
    /// <returns>DependencyProperty object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static DependencyProperty Create<TOwner, TProperty>(
        string name,
        TProperty value,
        Action<TOwner, TProperty> callback
    ) where TOwner : DependencyObject => DependencyProperty.RegisterAttached(
        name,
        typeof(TProperty),
        typeof(TOwner),
        new PropertyMetadata(value, (s, e) => {
            if (s is TOwner owner && e.NewValue is TProperty v) callback(owner, v);
        })
    );

    #endregion
}
