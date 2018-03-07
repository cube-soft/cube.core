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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Cube.Collections
{
    /* --------------------------------------------------------------------- */
    ///
    /// OrderedDictionary
    ///
    /// <summary>
    /// 挿入順序を維持する Dictionary(TKey, TValue) クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class OrderedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        #region Constructors

        /* --------------------------------------------------------------------- */
        ///
        /// OrderedDictionary
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public OrderedDictionary() { }

        /* --------------------------------------------------------------------- */
        ///
        /// OrderedDictionary
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="cp">コピー元オブジェクト</param>
        ///
        /* --------------------------------------------------------------------- */
        public OrderedDictionary(IDictionary<TKey, TValue> cp)
        {
            if (cp == null) return;
            foreach (KeyValuePair<TKey, TValue> kv in cp) _core.Add(kv.Key, kv.Value);
        }

        #endregion

        #region Properties

        /* --------------------------------------------------------------------- */
        ///
        /// Count
        ///
        /// <summary>
        /// 要素数を取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public int Count => _core.Count;

        /* --------------------------------------------------------------------- */
        ///
        /// IsReadOnly
        ///
        /// <summary>
        /// 読み取り専用かどうかを示す値を取得します。
        /// </summary>
        ///
        /// <remarks>
        /// このプロパティは常に false を返します。
        /// </remarks>
        ///
        /* --------------------------------------------------------------------- */
        public bool IsReadOnly => false;

        /* --------------------------------------------------------------------- */
        ///
        /// Item(TKey)
        ///
        /// <summary>
        /// キーに対応する値を取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public TValue this[TKey key]
        {
            get
            {
                if (key == null) throw new ArgumentNullException();
                if (ContainsKey(key)) return (TValue)_core[key];
                else throw new KeyNotFoundException(key.ToString());
            }

            set
            {
                if (key == null) throw new ArgumentNullException();
                if (ContainsKey(key)) _core[key] = value;
                else throw new KeyNotFoundException(key.ToString());
            }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// Item(int)
        ///
        /// <summary>
        /// インデックスに対応する値を取得または設定します。
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
        /// Key
        ///
        /// <summary>
        /// キー一覧を取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public ICollection<TKey> Keys => _core.Keys.Cast<TKey>().ToList().AsReadOnly();

        /* --------------------------------------------------------------------- */
        ///
        /// Values
        ///
        /// <summary>
        /// 値一覧を取得します。
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
        /// 要素が含まれているかどうかを判別します。
        /// </summary>
        ///
        /// <param name="item">要素</param>
        ///
        /// <returns>含まれているかどうか</returns>
        ///
        /* --------------------------------------------------------------------- */
        public bool Contains(KeyValuePair<TKey, TValue> item) =>
            ContainsKey(item.Key) ? _core[item.Key].Equals(item.Value) : false;

        /* --------------------------------------------------------------------- */
        ///
        /// ContainsKey
        ///
        /// <summary>
        /// 指定されたキーを持つ要素が含まれているかどうかを判別します。
        /// </summary>
        ///
        /// <param name="key">キー</param>
        ///
        /// <returns>含まれているかどうか</returns>
        ///
        /* --------------------------------------------------------------------- */
        public bool ContainsKey(TKey key) => key != null && _core.Contains(key);

        /* --------------------------------------------------------------------- */
        ///
        /// Add
        ///
        /// <summary>
        /// 要素を追加します。
        /// </summary>
        ///
        /// <param name="item">要素</param>
        ///
        /* --------------------------------------------------------------------- */
        public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

        /* --------------------------------------------------------------------- */
        ///
        /// Add
        ///
        /// <summary>
        /// 要素を追加します。
        /// </summary>
        ///
        /// <param name="key">キー</param>
        /// <param name="value">値</param>
        ///
        /* --------------------------------------------------------------------- */
        public void Add(TKey key, TValue value)
        {
            if (key == null) throw new ArgumentNullException();
            _core.Add(key, value);
        }

        /* --------------------------------------------------------------------- */
        ///
        /// Remove
        ///
        /// <summary>
        /// 要素を削除します。
        /// </summary>
        ///
        /// <param name="item">要素</param>
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
        /// キーを持つ要素を削除します。
        /// </summary>
        ///
        /// <param name="key">キー</param>
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
        /// 全ての要素を削除します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public void Clear() => _core.Clear();

        /* --------------------------------------------------------------------- */
        ///
        /// CopyTo
        ///
        /// <summary>
        /// 要素をコピーします。
        /// </summary>
        ///
        /// <param name="dest">コピー先</param>
        /// <param name="offset">コピー開始位置</param>
        ///
        /* --------------------------------------------------------------------- */
        public void CopyTo(KeyValuePair<TKey, TValue>[] dest, int offset)
        {
            if (dest == null) throw new ArgumentNullException();
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
        /// キーに対応する値の取得を試みます。
        /// </summary>
        ///
        /// <param name="key">キー</param>
        /// <param name="dest">値の格納用オブジェクト</param>
        ///
        /// <returns>取得に成功したかどうか</returns>
        ///
        /* --------------------------------------------------------------------- */
        public bool TryGetValue(TKey key, out TValue dest)
        {
            var result = ContainsKey(key);
            dest = result ? this[key] : default(TValue);
            return result;
        }

        /* --------------------------------------------------------------------- */
        ///
        /// GetEnumerator
        ///
        /// <summary>
        /// 反復用オブジェクトを取得します。
        /// </summary>
        ///
        /// <returns>反復用オブジェクト</returns>
        ///
        /* --------------------------------------------------------------------- */
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => ((IEnumerable)_core)
            .Cast<DictionaryEntry>()
            .Select(e => new KeyValuePair<TKey, TValue>((TKey)e.Key, (TValue)e.Value))
            .GetEnumerator();

        /* --------------------------------------------------------------------- */
        ///
        /// GetEnumerator
        ///
        /// <summary>
        /// 反復用オブジェクトを取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region Fields
        OrderedDictionary _core = new OrderedDictionary();
        #endregion
    }
}
