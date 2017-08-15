/* ------------------------------------------------------------------------- */
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
    /// CommonSequence
    ///
    /// <summary>
    /// シーケンスを保持するためのクラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    internal class CommonSequence<T>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// CommonSequence
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public CommonSequence(int older, int newer, int count, CommonSequence<T> next)
        {
            OlderStart = older;
            NewerStart = newer;
            Count      = count;
            Next       = next;
        }

        #endregion

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
        public CommonSequence<T> Next { get; set; }

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
        public CommonSequence<T> Reverse()
        {
            CommonSequence<T> top     = null;
            CommonSequence<T> current = this;

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
        public IEnumerable<Result<T>> ToResult(T[] older, T[] newer, bool diffonly, bool swap)
        {
            var dest   = new List<Result<T>>();
            var seq    = this;
            var array0 = swap ? newer : older;
            var array1 = swap ? older : newer;
            var prev0  = 0;
            var prev1  = 0;

            while (seq != null)
            {
                var start0 = swap ? seq.NewerStart : seq.OlderStart;
                var start1 = swap ? seq.OlderStart : seq.NewerStart;

                if (prev0 < start0 || prev1 < start1)
                {
                    var ocount = start0 - prev0;
                    var ncount = start1 - prev1;
                    dest.Add(Create(array0, prev0, ocount, array1, prev1, ncount));
                }

                if (seq.Count == 0) break; // End of contents

                prev0 = start0;
                prev1 = start1;

                if (!diffonly) dest.Add(Create(Condition.None, array0, prev0, seq.Count, array1, prev1, seq.Count));

                prev0 += seq.Count;
                prev1 += seq.Count;

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
            var n = Math.Min(count, src?.Length - start ?? 0);
            if (n <= 0) return null;

            var dest = new T[n];
            Array.Copy(src, start, dest, 0, n);
            return dest;
        }

        #endregion
    }
}
