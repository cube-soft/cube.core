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
using System.Collections.Generic;
using System.Linq;

namespace Cube.FileSystem
{
    /* --------------------------------------------------------------------- */
    ///
    /// DisplayFilter
    ///
    /// <summary>
    /// OpenFileDialog などの Filter に指定する文字列を生成するための
    /// クラスです。
    /// </summary>
    ///
    /// <remarks>
    /// 例えば、"テキストファイル (*.txt)|*.txt;*.TXT" のような文字列が
    /// 生成されます。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class DisplayFilter
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// DisplayFilter
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="description">ファイルの種類に関する説明</param>
        /// <param name="extensions">拡張子一覧</param>
        ///
        /// <remarks>
        /// 拡張子は ".txt" のような形で指定して下さい。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public DisplayFilter(string description, params string[] extensions)
        {
            Description = description;
            Extensions  = extensions;
            IgnoreCase  = true;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DisplayFilter
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="description">ファイルの種類に関する説明</param>
        /// <param name="extensions">拡張子一覧</param>
        /// <param name="ignoreCase">
        /// 大文字・小文字の区別を無視するかどうか
        /// </param>
        ///
        /// <remarks>
        /// 拡張子は ".txt" のような形で指定して下さい。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public DisplayFilter(string description, bool ignoreCase, params string[] extensions)
        {
            Description = description;
            Extensions  = extensions;
            IgnoreCase  = ignoreCase;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Description
        ///
        /// <summary>
        /// ファイルの種類を表す文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Description { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Extensions
        ///
        /// <summary>
        /// 表示対象となる拡張子一覧を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IEnumerable<string> Extensions { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// IgnoreCase
        ///
        /// <summary>
        /// 大文字・小文字の区別を無視するかどうかを示す値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IgnoreCase { get; }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// ToString
        ///
        /// <summary>
        /// 文字列に変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public override string ToString()
        {
            var e0 = Extensions.Select(e => $"*{e}");
            var s0 = string.Join(", ", e0.ToArray());
            var e1 = e0.Select(e => Format(e));
            var s1 = string.Join(";", e1.ToArray());

            return $"{Description} ({s0})|{s1}";
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Format
        ///
        /// <summary>
        /// IgnoreCase の設定に従って拡張子表記を変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private string Format(string src)
        {
            if (!IgnoreCase) return src;

            var x = src.ToLower();
            var y = src.ToUpper();

            return x.Equals(y) ? x : $"{x};{y}";
        }

        #endregion
    }
}
