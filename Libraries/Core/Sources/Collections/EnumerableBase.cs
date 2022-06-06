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
namespace Cube.Collections;

using System.Collections;
using System.Collections.Generic;

/* ------------------------------------------------------------------------- */
///
/// EnumerableBase
///
/// <summary>
/// Represents the simplest implementation of the IEnumerable(T)
/// interface.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public abstract class EnumerableBase<T> : DisposableBase, IEnumerable<T>
{
    #region Methods

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
    public abstract IEnumerator<T> GetEnumerator();

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

    #endregion
}
