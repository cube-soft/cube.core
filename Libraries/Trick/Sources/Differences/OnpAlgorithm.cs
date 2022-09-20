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
using System.Diagnostics;
using System.Linq;

/* ------------------------------------------------------------------------- */
///
/// OnpAlgorithm
///
/// <summary>
/// Provides functionality to detect the diff.
/// </summary>
///
/// <remarks>
/// Sun Wu, Udi Manber, and Gene Myers, "An O(NP) Sequence Comparison
/// Algorithm", Information Processing Letters Volume 35, Issue 6,
/// pp. 317-323, September 1990.
/// </remarks>
///
/* ------------------------------------------------------------------------- */
public class OnpAlgorithm<T>
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// OnpAlgorithm
    ///
    /// <summary>
    /// Initializes a new instance of the OnpAlgorithm class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public OnpAlgorithm() : this(EqualityComparer<T>.Default) { }

    /* --------------------------------------------------------------------- */
    ///
    /// OnpAlgorithm
    ///
    /// <summary>
    /// Initializes a new instance of the OnpAlgorithm class with the
    /// specified comparer.
    /// </summary>
    ///
    /// <param name="comparer">Object to compare.</param>
    ///
    /* --------------------------------------------------------------------- */
    public OnpAlgorithm(IEqualityComparer<T> comparer) { _cmp = comparer; }

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Compare
    ///
    /// <summary>
    /// Compares the specified sequence and detects the diff of them.
    /// </summary>
    ///
    /// <param name="older">Older sequence.</param>
    /// <param name="newer">Newer sequence.</param>
    /// <param name="mask">Mask of the results.</param>
    ///
    /// <returns>Diff results.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public IEnumerable<Result<T>> Compare(IEnumerable<T> older,
        IEnumerable<T> newer, Condition mask) =>
        Compare(older?.ToArray(), newer?.ToArray(), mask);

    /* --------------------------------------------------------------------- */
    ///
    /// Compare
    ///
    /// <summary>
    /// Compares the specified sequence and detects the diff of them.
    /// </summary>
    ///
    /// <param name="older">Older sequence.</param>
    /// <param name="newer">Newer sequence.</param>
    /// <param name="mask">Mask of the results.</param>
    ///
    /// <returns>Diff results.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public IEnumerable<Result<T>> Compare(T[] older, T[] newer, Condition mask)
    {
        if (older is null || older.Length == 0 || newer is null || newer.Length == 0)
        {
            return CompareEmpty(older, newer, mask);
        }

        _swap  = older.Length > newer.Length;
        _older = _swap ? newer : older;
        _newer = _swap ? older : newer;

        return Compare(mask);
    }

    #endregion

    #region Implementations for O(np) Sequence Comparison Algorithm

    /* --------------------------------------------------------------------- */
    ///
    /// CompareEmpty
    ///
    /// <summary>
    /// Compares the sequence.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private IEnumerable<Result<T>> CompareEmpty(T[] older, T[] newer,
        Condition mask) => new CommonSequence<T>(
        older?.Length ?? 0,
        newer?.Length ?? 0,
        0, null
    ).ToResult(older, newer, mask, false);

    /* --------------------------------------------------------------------- */
    ///
    /// Compare
    ///
    /// <summary>
    /// Compares the sequence.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private IEnumerable<Result<T>> Compare(Condition mask)
    {
        Debug.Assert(_older is not null && _newer is not null && _older.Length <= _newer.Length);

        _fp = new Snake[_older.Length + _newer.Length + 3];

        var d = _newer.Length - _older.Length;
        var p = 0;
        do
        {
            for (var k = -p; k < d; ++k) SearchSnake(k);
            for (var k = d + p; k >= d; --k) SearchSnake(k);
            ++p;
        }
        while (_fp[_newer.Length + 1].Position != _newer.Length + 1);

        var tail = new CommonSequence<T>(_older.Length, _newer.Length, 0, _fp[_newer.Length + 1].Sequence);
        return tail.Reverse().ToResult(_older, _newer, mask, _swap);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// SearchSnake
    ///
    /// <summary>
    /// Detects the snake.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private void SearchSnake(int k)
    {
        var kk = _older.Length + 1 + k;

        var lk = kk - 1;
        var lb = _fp[lk].Position;

        var rk = kk + 1;
        var rb = _fp[rk].Position - 1;

        var cs = (lb > rb) ? _fp[lk].Sequence : _fp[rk].Sequence;

        var p1 = Math.Max(lb, rb);
        var p0 = p1 - k;

        var start0 = p0;
        var start1 = p1;

        while (p0 < _older.Length && p1 < _newer.Length && _cmp.Equals(_older[p0], _newer[p1]))
        {
            ++p0;
            ++p1;
        }

        _fp[kk].Sequence = (start0 != p0) ? new(start0, start1, p0 - start0, cs) : cs;
        _fp[kk].Position = p1 + 1;
    }

    #endregion

    #region Fields

    private struct Snake : IEquatable<Snake>
    {
        public int Position { get; set; }
        public CommonSequence<T> Sequence { get; set; }
        public bool Equals(Snake e) => Position == e.Position && Sequence.Equals(e.Sequence);
    }

    private readonly IEqualityComparer<T> _cmp;
    private bool _swap;
    private T[] _older;
    private T[] _newer;
    private Snake[] _fp;

    #endregion
}
