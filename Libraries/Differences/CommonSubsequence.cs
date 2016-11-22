/* ------------------------------------------------------------------------- */
///
/// CommonSubsequence.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///  http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
///
/* ------------------------------------------------------------------------- */
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Cube.Differences
{
    /* --------------------------------------------------------------------- */
    ///
    /// CommonSubsequence
    ///
    /// <summary>
    /// シーケンスを保持するためのクラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    internal class CommonSubsequence<T>
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// OlderStart
        /// 
        /// <summary>
        /// 変更前コンテンツの開始インデックスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int OlderStart { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// NewerStart
        /// 
        /// <summary>
        /// 変更後コンテンツの開始インデックスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int NewerStart { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Count
        /// 
        /// <summary>
        /// 対象範囲を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int Count { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Next
        /// 
        /// <summary>
        /// 次のシーケンスを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public CommonSubsequence<T> Next { get; set; }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Reverse
        /// 
        /// <summary>
        /// リンクリストを反転させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public CommonSubsequence<T> Reverse()
        {
            CommonSubsequence<T> top     = null;
            CommonSubsequence<T> current = this;

            while (current != null)
            {
                var next = current.Next;
                current.Next = top;
                top = current;
                current = next;
            }
            return top;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToResult
        /// 
        /// <summary>
        /// Result オブジェクトに変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IEnumerable<Result<T>> ToResult(T[] older, T[] newer, bool swap, bool all = true)
        {
            var dest  = new List<Result<T>>();
            var seq   = this;
            var oprev = 0;
            var nprev = 0;

            while (seq != null)
            {
                var ostart = swap ? seq.NewerStart : seq.OlderStart;
                var nstart = swap ? seq.OlderStart : seq.NewerStart;

                if (oprev < ostart || nprev < nstart)
                {
                    var ocount = ostart - oprev;
                    var ncount = nstart - nprev;
                    dest.Add(Create(older, oprev, ocount, newer, nprev, ncount));
                }

                if (seq.Count == 0) break; // End of contents

                oprev = ostart;
                nprev = nstart;

                if (all) dest.Add(Create(Condition.None, older, oprev, seq.Count, newer, nprev, seq.Count));

                oprev += seq.Count;
                nprev += seq.Count;

                seq = seq.Next;
            }

            return dest;
        }

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        /// 
        /// <summary>
        /// Result(T) オブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Result<T> Create(T[] older, int ostart, int ocount,
            T[] newer, int nstart, int ncount)
        {
            Debug.Assert(ocount > 0 || ncount > 0);

            var cond = ocount > 0 && ncount > 0 ? Condition.Changed :
                       ocount > 0               ? Condition.Deleted :
                                                  Condition.Inserted;
            return Create(cond,
                older, ostart, ocount,
                newer, nstart, ncount
            );
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        /// 
        /// <summary>
        /// Result(T) オブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Result<T> Create(Condition condition,
            T[] older, int ostart, int ocount,
            T[] newer, int nstart, int ncount)
            => new Result<T>(
                condition,
                Slice(older, ostart, ocount),
                Slice(newer, nstart, ncount)
            );

        /* ----------------------------------------------------------------- */
        ///
        /// Slice
        /// 
        /// <summary>
        /// 配列の一部を表すオブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private IEnumerable<T> Slice(T[] src, int start, int count)
        {
            Debug.Assert(src != null);
            var n = Math.Min(src.Length - start, count);
            if (n <= 0) return null;

            var dest = new T[n];
            Array.Copy(src, start, dest, 0, n);
            return dest;
        }

        #endregion
    }
}
