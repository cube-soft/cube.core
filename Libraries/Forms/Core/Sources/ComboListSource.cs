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
namespace Cube.Forms;

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

/* ------------------------------------------------------------------------- */
///
/// ComboListSource
///
/// <summary>
/// Represents the data source to be bound to the ComboBox object.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class ComboListSource<T> : IEnumerable<KeyValuePair<string, T>>, IListSource
{
    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// ContainsListCollection
    ///
    /// <summary>
    /// Gets a value indicating whether the collection is a collection of
    /// IList objects.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    bool IListSource.ContainsListCollection => false;

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// GetList
    ///
    /// <summary>
    /// Returns an IList that can be bound to a data source from an object
    /// that does not implement an IList itself.
    /// </summary>
    ///
    /// <returns>
    /// An IList that can be bound to a data source from the object.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public IList GetList() => _inner;

    /* --------------------------------------------------------------------- */
    ///
    /// GetEnumerator
    ///
    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    ///
    /// <returns>
    /// Enumerator that can be used to iterate through the collection.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public IEnumerator<KeyValuePair<string, T>> GetEnumerator() => _inner.GetEnumerator();

    /* --------------------------------------------------------------------- */
    ///
    /// GetEnumerator
    ///
    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    ///
    /// <returns>
    /// IEnumerator object that can be used to iterate through the
    /// collection.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /* --------------------------------------------------------------------- */
    ///
    /// Add
    ///
    /// <summary>
    /// Adds the specified key and value pair to the collection.
    /// </summary>
    ///
    /// <param name="key">Display contents in the ComboBox object.</param>
    /// <param name="value">
    /// Value associated with the displayed content of the ComboBox object.
    /// </param>
    ///
    /// <remarks>
    /// The method is mainly used with the collection initializer.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public void Add(string key, T value) => _inner.Add(new(key, value));

    #endregion

    #region Fields
    private readonly List<KeyValuePair<string, T>> _inner = new();
    #endregion
}
