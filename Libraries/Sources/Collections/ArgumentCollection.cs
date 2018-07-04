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
using Cube.Generics;
using System.Collections;
using System.Collections.Generic;

namespace Cube.Collections
{
    /* --------------------------------------------------------------------- */
    ///
    /// ArgumentCollection
    ///
    /// <summary>
    /// コマンドライン等の引数を解析するクラスです。
    /// </summary>
    ///
    /// <remarks>
    /// このクラスでは、各オプションは最大 1 つの引数しか指定できないと言う
    /// 制約を設けています。それ以外の引数は全て自身のシーケンスに格納され
    /// ます。また、同じオプションが複数回指定された場合、後に指定された
    /// 内容で上書きされます。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class ArgumentCollection : IReadOnlyList<string>
    {
        #region Constructors

        /* --------------------------------------------------------------------- */
        ///
        /// ArgumentCollection
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="src">引数一覧</param>
        ///
        /* --------------------------------------------------------------------- */
        public ArgumentCollection(IEnumerable<string> src) : this(src, '-') { }

        /* --------------------------------------------------------------------- */
        ///
        /// ArgumentCollection
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="src">引数一覧</param>
        /// <param name="prefix">オプションを示す接頭辞</param>
        ///
        /* --------------------------------------------------------------------- */
        public ArgumentCollection(IEnumerable<string> src, char prefix)
        {
            Prefix = prefix;
            Parse(src);
        }

        #endregion

        #region Properties

        /* --------------------------------------------------------------------- */
        ///
        /// Prefix
        ///
        /// <summary>
        /// オプションを示す接頭辞を取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public char Prefix { get; set; }

        /* --------------------------------------------------------------------- */
        ///
        /// Item(int)
        ///
        /// <summary>
        /// 非オプション要素を取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public string this[int index] => _primary[index];

        /* --------------------------------------------------------------------- */
        ///
        /// Count
        ///
        /// <summary>
        /// 非オプション要素の数を取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public int Count => _primary.Count;

        /* --------------------------------------------------------------------- */
        ///
        /// Options
        ///
        /// <summary>
        /// オプション引数一覧を取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public IReadOnlyDictionary<string, string> Options => _options;

        #endregion

        #region Methods

        /* --------------------------------------------------------------------- */
        ///
        /// GetEnumerator
        ///
        /// <summary>
        /// オプション引数以外の要素一覧を取得するための反復用オブジェクトを
        /// 取得します。
        /// </summary>
        ///
        /// <returns>反復用オブジェクト</returns>
        ///
        /* --------------------------------------------------------------------- */
        public IEnumerator<string> GetEnumerator() => _primary.GetEnumerator();

        /* --------------------------------------------------------------------- */
        ///
        /// GetEnumerator
        ///
        /// <summary>
        /// 非オプション要素を取得するための反復用オブジェクトを取得します。
        /// </summary>
        ///
        /// <returns>反復用オブジェクト</returns>
        ///
        /* --------------------------------------------------------------------- */
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region Implementations

        /* --------------------------------------------------------------------- */
        ///
        /// Parse
        ///
        /// <summary>
        /// コマンドライン等の引数を解析します。
        /// </summary>
        ///
        /// <param name="src">引数一覧</param>
        ///
        /* --------------------------------------------------------------------- */
        private void Parse(IEnumerable<string> src)
        {
            var option = string.Empty;

            foreach (var s in src)
            {
                if (!s.HasValue()) continue;

                if (s[0] == Prefix)
                {
                    if (option.HasValue()) UpdateOption(option, null);
                    option = s.TrimStart(Prefix);
                }
                else if (option.HasValue())
                {
                    UpdateOption(option, s);
                    option = string.Empty;
                }
                else _primary.Add(s);
            }

            if (option.HasValue()) UpdateOption(option, null);
        }

        /* --------------------------------------------------------------------- */
        ///
        /// UpdateOption
        ///
        /// <summary>
        /// オプションの内容を更新します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void UpdateOption(string key, string value)
        {
            if (_options.ContainsKey(key)) _options[key] = value;
            else _options.Add(key, value);
        }

        #endregion

        #region Fields
        private readonly List<string> _primary = new List<string>();
        private readonly Dictionary<string, string> _options = new Dictionary<string, string>();
        #endregion
    }
}
