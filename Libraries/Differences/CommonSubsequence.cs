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
        public IEnumerable<Result<T>> ToResult(IList<T> older, IList<T> newer,
            bool swap, bool all = true) => swap ?
            ToResultSwap(older, newer, all) :
            ToResultNormal(older, newer, all);

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// ToResultNormal
        /// 
        /// <summary>
        /// Result オブジェクトに変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private IEnumerable<Result<T>> ToResultNormal(IList<T> older, IList<T> newer, bool all)
        {
            var dest = new List<Result<T>>();
            var cs   = this;
            var iold = 0;
            var inew = 0;

            while (cs != null)
            {
                if (iold < cs.OlderStart || inew < cs.NewerStart)
                {
                    dest.Add(Create(
                        older, iold, cs.OlderStart - iold,
                        newer, inew, cs.NewerStart - inew
                    ));
                }

                if (cs.Count == 0) break; // End of contents

                iold = cs.OlderStart;
                inew = cs.NewerStart;

                if (all) dest.Add(Create(Condition.None, older, iold, cs.Count, newer, inew, cs.Count));

                iold += cs.Count;
                inew += cs.Count;

                cs = cs.Next;
            }

            return dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToResultSwap
        /// 
        /// <summary>
        /// Result オブジェクトに変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private IEnumerable<Result<T>> ToResultSwap(IList<T> older, IList<T> newer, bool all)
        {
            var dest = new List<Result<T>>();
            var cs   = this;
            var iold = 0;
            var inew = 0;

            while (cs != null)
            {
                if (iold < cs.NewerStart || inew < cs.OlderStart)
                {
                    dest.Add(Create(
                        older, iold, cs.NewerStart - iold,
                        newer, inew, cs.OlderStart - inew
                    ));
                }

                if (cs.Count == 0) break; // End of contents

                iold = cs.NewerStart;
                inew = cs.OlderStart;

                if (all) dest.Add(Create(Condition.None, older, iold, cs.Count, newer, inew, cs.Count));

                iold += cs.Count;
                inew += cs.Count;

                cs = cs.Next;
            }

            return dest;
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
        private Result<T> Create(IList<T> older, int ostart, int ocount,
            IList<T> newer, int nstart, int ncount)
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
            IList<T> older, int ostart, int ocount,
            IList<T> newer, int nstart, int ncount)
            => new Result<T>(
                condition,
                Range(older, ostart, ocount),
                Range(newer, nstart, ncount)
            );

        /* ----------------------------------------------------------------- */
        ///
        /// Range
        /// 
        /// <summary>
        /// IList(T) の一部を表すオブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private IList<T> Range(IList<T> src, int start, int count)
        {
            var dest = new List<T>(count);
            for (var i = start; i < count; ++i) dest.Add(src[i]);
            return dest;
        }

        #endregion
    }
}
