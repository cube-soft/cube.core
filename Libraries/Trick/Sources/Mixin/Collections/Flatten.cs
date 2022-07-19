namespace Cube.Mixin.Collections;

using System;
using System.Collections.Generic;
using System.Linq;

/* ------------------------------------------------------------------------- */
///
/// FlattenExtension
///
/// <summary>
/// Provides extended methods to convert a tree structure to a
/// one-dimensional sequence.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class FlattenExtension
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Flatten
    ///
    /// <summary>
    /// Convert a tree structure to a one-dimensional sequence with
    /// breadth first search.
    /// </summary>
    ///
    /// <param name="src">Source sequence.</param>
    /// <param name="func">Conversion function.</param>
    ///
    /// <returns>Converted sequence.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static IEnumerable<T> Flatten<T>(this IEnumerable<T> src, Func<T, IEnumerable<T>> func) =>
        src.Flatten((e, s) => func(e));

    /* --------------------------------------------------------------------- */
    ///
    /// Flatten
    ///
    /// <summary>
    /// Convert a tree structure to a one-dimensional sequence with
    /// breadth first search..
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static IEnumerable<T> Flatten<T>(this IEnumerable<T> src, Func<T, IEnumerable<T>, IEnumerable<T>> func) =>
        src.Concat(
            src.Where(e => func(e, src) != null)
               .SelectMany(e => func(e, src).Flatten(func))
        );

    #endregion
}
