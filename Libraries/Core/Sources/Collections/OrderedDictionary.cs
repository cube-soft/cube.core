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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

/* ------------------------------------------------------------------------- */
///
/// OrderedDictionary
///
/// <summary>
/// Represents a dictionary that preserves the insertion order.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class OrderedDictionary<TKey, TValue> :
    EnumerableBase<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// OrderedDictionary
    ///
    /// <summary>
    /// Initializes a new instance of the OrderedDictionary(TKey, TValue)
    /// class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public OrderedDictionary() : this(null) { }

    /* --------------------------------------------------------------------- */
    ///
    /// OrderedDictionary
    ///
    /// <summary>
    /// Initializes a new instance of the OrderedDictionary(TKey, TValue)
    /// class with the specified collection.
    /// </summary>
    ///
    /// <param name="cp">Collection to be copied.</param>
    ///
    /* --------------------------------------------------------------------- */
    public OrderedDictionary(IDictionary<TKey, TValue> cp)
    {
        if (cp is null) return;
        foreach (var kv in cp) _core.Add(kv.Key, kv.Value);
    }

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Count
    ///
    /// <summary>
    /// Gets the number of key/values pairs.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public int Count => _core.Count;

    /* --------------------------------------------------------------------- */
    ///
    /// IsReadOnly
    ///
    /// <summary>
    /// Gets a value indicating whether the OrderedDictionary(TKey, TValue)
    /// collection is read-only.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public bool IsReadOnly => false;

    /* --------------------------------------------------------------------- */
    ///
    /// Item(TKey)
    ///
    /// <summary>
    /// Gets or sets the value with the specified key.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public TValue this[TKey key]
    {
        get
        {
            if (Equals(key, default(TKey))) throw new ArgumentNullException(nameof(key));
            if (ContainsKey(key)) return (TValue)_core[key];
            else throw new KeyNotFoundException(key.ToString());
        }

        set
        {
            if (Equals(key, default(TKey))) throw new ArgumentNullException(nameof(key));
            if (ContainsKey(key)) _core[key] = value;
            else throw new KeyNotFoundException(key.ToString());
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Item(int)
    ///
    /// <summary>
    /// Gets or sets the value at the specified index.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public TValue this[int index]
    {
        get
        {
            if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException();
            return (TValue)_core[index];
        }

        set
        {
            if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException();
            _core[index] = value;
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Keys
    ///
    /// <summary>
    /// Gets an ICollection(TKey) object containing the keys in the
    /// OrderedDictionary(TKey, TValue) collection.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public ICollection<TKey> Keys => _core.Keys.Cast<TKey>().ToList().AsReadOnly();

    /* --------------------------------------------------------------------- */
    ///
    /// Values
    ///
    /// <summary>
    /// Gets an ICollection(TValue) object containing the values in the
    /// OrderedDictionary(TKey, TValue) collection.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public ICollection<TValue> Values => _core.Values.Cast<TValue>().ToList().AsReadOnly();

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Contains
    ///
    /// <summary>
    /// Determines whether the OrderedDictionary(TKey, TValue) collection
    /// contains a specific item.
    /// </summary>
    ///
    /// <param name="item">
    /// Item to locate in the OrderedDictionary(TKey, TValue) collection.
    /// </param>
    ///
    /// <returns>
    /// true if the OrderedDictionary(TKey, TValue) collection contains an
    /// item; otherwise, false.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public bool Contains(KeyValuePair<TKey, TValue> item) =>
        item.Key is not null && ContainsKey(item.Key) && _core[item.Key].Equals(item.Value);

    /* --------------------------------------------------------------------- */
    ///
    /// ContainsKey
    ///
    /// <summary>
    /// Determines whether the OrderedDictionary(TKey, TValue) collection
    /// contains a specific key.
    /// </summary>
    ///
    /// <param name="key">
    /// Key to locate in the OrderedDictionary(TKey, TValue) collection.
    /// </param>
    ///
    /// <returns>
    /// true if the OrderedDictionary(TKey, TValue) collection contains an
    /// element with the specified key; otherwise, false.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public bool ContainsKey(TKey key) => _core.Contains(key);

    /* --------------------------------------------------------------------- */
    ///
    /// Add
    ///
    /// <summary>
    /// Adds an entry with the specified KeyValuePair(TKey, TValue) object
    /// into the OrderedDictionary(TKey, TValue) collection with the lowest
    /// available index.
    /// </summary>
    ///
    /// <param name="item">Key/value pair of the entry to add.</param>
    ///
    /* --------------------------------------------------------------------- */
    public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

    /* --------------------------------------------------------------------- */
    ///
    /// Add
    ///
    /// <summary>
    /// Adds an entry with the specified key and value into the
    /// OrderedDictionary(TKey, TValue) collection with the lowest available
    /// index.
    /// </summary>
    ///
    /// <param name="key">Key of the entry to add.</param>
    /// <param name="value">Value of the entry to add. </param>
    ///
    /* --------------------------------------------------------------------- */
    public void Add(TKey key, TValue value)
    {
        if (Equals(key, default(TKey))) throw new ArgumentNullException(nameof(key));
        _core.Add(key, value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Remove
    ///
    /// <summary>
    /// Removes the entry with the specified key/value pair from the
    /// OrderedDictionary(TKey, TValue) collection.
    /// </summary>
    ///
    /// <param name="item">Key/value pair of the entry to remove.</param>
    ///
    /// <returns>
    /// true for success; otherwise, false.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        if (!Contains(item)) return false;
        _core.Remove(item.Key);
        return true;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Remove
    ///
    /// <summary>
    /// Removes the entry with the specified key from the
    /// OrderedDictionary(TKey, TValue) collection.
    /// </summary>
    ///
    /// <param name="key">The key of the entry to remove.</param>
    ///
    /// <returns>
    /// true for success; otherwise, false.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public bool Remove(TKey key)
    {
        if (!ContainsKey(key)) return false;
        _core.Remove(key);
        return true;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Clear
    ///
    /// <summary>
    /// Removes all elements from the OrderedDictionary(TKey, TValue)
    /// collection.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public void Clear() => _core.Clear();

    /* --------------------------------------------------------------------- */
    ///
    /// CopyTo
    ///
    /// <summary>
    /// Copies the OrderedDictionary(TKey, TValue) elements to a
    /// one-dimensional array object at the specified index.
    /// </summary>
    ///
    /// <param name="dest">One-dimensional array object to copy to.</param>
    /// <param name="offset">Index in array at which copying begins.</param>
    ///
    /* --------------------------------------------------------------------- */
    public void CopyTo(KeyValuePair<TKey, TValue>[] dest, int offset)
    {
        if (dest is null) throw new ArgumentNullException(nameof(dest));
        if (offset < 0 || offset >= dest.Length) throw new ArgumentOutOfRangeException();
        if (dest.Length - offset < _core.Count) throw new ArgumentException("too small array");

        var index = offset;
        foreach (var item in this) dest[index++] = item;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// TryGetValue
    ///
    /// <summary>
    /// Gets the value associated with the specified key.
    /// </summary>
    ///
    /// <param name="key">Key of the value to get.</param>
    /// <param name="dest">
    /// When this method returns, contains the value associated with the
    /// specified key, if the key is found; otherwise, the default value
    /// for the type of the value parameter.
    /// </param>
    ///
    /// <returns>
    /// true if the OrderedDictionary(TKey, TValue) contains an element
    /// with the specified key; otherwise, false.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public bool TryGetValue(TKey key, out TValue dest)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        var result = key is not null && ContainsKey(key);
        dest = result ? this[key] : default;
        return result;
    }

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
    public override IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() =>
        _core.Cast<DictionaryEntry>()
             .Select(e => new KeyValuePair<TKey, TValue>((TKey)e.Key, (TValue)e.Value))
             .GetEnumerator();

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// Dispose
    ///
    /// <summary>
    /// Releases the unmanaged resources used by the object and
    /// optionally releases the managed resources.
    /// </summary>
    ///
    /// <param name="disposing">
    /// true to release both managed and unmanaged resources;
    /// false to release only unmanaged resources.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    protected override void Dispose(bool disposing)
    {
        if (disposing) _core.Clear();
    }

    #endregion

    #region Fields
    private readonly OrderedDictionary _core = new();
    #endregion
}
