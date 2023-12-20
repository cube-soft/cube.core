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
namespace Cube.Collections.Differences;

using System;
using System.Collections.Generic;

/* ------------------------------------------------------------------------- */
///
/// CommonSequence
///
/// <summary>
/// Represents the sequence of diff results.
/// </summary>
///
/* ------------------------------------------------------------------------- */
internal class CommonSequence<T>
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// CommonSequence
    ///
    /// <summary>
    /// Initializes a new instance of the CommonSequence class with
    /// the specified arguments.
    /// </summary>
    ///
    /// <param name="older">Start position of the older content.</param>
    /// <param name="newer">Start position of the newer content.</param>
    /// <param name="count">Number of elements.</param>
    /// <param name="next">Next sequence.</param>
    ///
    /* --------------------------------------------------------------------- */
    public CommonSequence(int older, int newer, int count, CommonSequence<T> next)
    {
        OlderStart = older;
        NewerStart = newer;
        Count      = count;
        Next       = next;
    }

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// OlderStart
    ///
    /// <summary>
    /// Get the start position of the older content.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public int OlderStart { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// NewerStart
    ///
    /// <summary>
    /// Get the start position of the newer content.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public int NewerStart { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// Count
    ///
    /// <summary>
    /// Gets the number of elements in the content.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public int Count { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// Next
    ///
    /// <summary>
    /// Gets the next sequence.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public CommonSequence<T> Next { get; set; }

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Reverse
    ///
    /// <summary>
    /// Reverses the provided linked list.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public CommonSequence<T> Reverse()
    {
        var top     = default(CommonSequence<T>);
        var current = this;

        while (current is not null)
        {
            var next = current.Next;
            current.Next = top;
            top = current;
            current = next;
        }
        return top;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// ToResult
    ///
    /// <summary>
    /// Converts to the collection of Result(T) objects.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public IEnumerable<Result<T>> ToResult(T[] older, T[] newer, Condition mask, bool swap)
    {
        var none   = Condition.None;
        var dest   = new List<Result<T>>();
        var seq    = this;
        var array0 = swap ? newer : older;
        var array1 = swap ? older : newer;
        var prev0  = 0;
        var prev1  = 0;

        while (seq is not null)
        {
            var start0 = swap ? seq.NewerStart : seq.OlderStart;
            var start1 = swap ? seq.OlderStart : seq.NewerStart;

            if (prev0 < start0 || prev1 < start1)
            {
                var ocount = start0 - prev0;
                var ncount = start1 - prev1;
                var cond = ocount > 0 && ncount > 0 ? Condition.Changed :
                           ocount > 0               ? Condition.Deleted :
                                                      Condition.Inserted;

                if (mask.HasFlag(cond)) dest.Add(Create(cond, array0, prev0, ocount, array1, prev1, ncount));
            }

            if (seq.Count == 0) break; // End of contents

            prev0 = start0;
            prev1 = start1;

            if (mask.HasFlag(none)) dest.Add(Create(none, array0, prev0, seq.Count, array1, prev1, seq.Count));

            prev0 += seq.Count;
            prev1 += seq.Count;

            seq = seq.Next;
        }

        return dest;
    }

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// Create
    ///
    /// <summary>
    /// Creates a new instance of the Result(T) class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private Result<T> Create(Condition condition,
        T[] older, int ostart, int ocount,
        T[] newer, int nstart, int ncount) => new(
        condition,
        Slice(older, ostart, ocount),
        Slice(newer, nstart, ncount)
    );

    /* --------------------------------------------------------------------- */
    ///
    /// Slice
    ///
    /// <summary>
    /// Gets the part of the specified collection.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private IEnumerable<T> Slice(T[] src, int start, int count)
    {
        if (src is null) return null;
        var n = Math.Min(count, src.Length - start);
        if (n <= 0) return null;

        var dest = new T[n];
        Array.Copy(src, start, dest, 0, n);
        return dest;
    }

    #endregion
}
