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
/// Condition
///
/// <summary>
/// Specifies the diff condition.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[Flags]
public enum Condition
{
    /// <summary>No diff.</summary>
    None = 0x01,
    /// <summary>Older results are empty.</summary>
    Inserted = 0x02,
    /// <summary>Newer results are empty.</summary>
    Deleted = 0x04,
    /// <summary>Changed content.</summary>
    Changed = 0x08,

    /// <summary>Mask that indicates that there has been some change.</summary>
    DiffOnly = Inserted | Deleted | Changed,
    /// <summary>Mask of all defined elements.</summary>
    Any = None | DiffOnly,
}

/* ------------------------------------------------------------------------- */
///
/// Result
///
/// <summary>
/// Represents a part of the diff results.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class Result<T>
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// Result
    ///
    /// <summary>
    /// Initializes a new instance of the Result class with the
    /// specified arguments.
    /// </summary>
    ///
    /// <param name="condition">Diff condition.</param>
    /// <param name="older">Target part of the older sequence.</param>
    /// <param name="newer">Target part of the newer sequence.</param>
    ///
    /* --------------------------------------------------------------------- */
    public Result(Condition condition, IEnumerable<T> older, IEnumerable<T> newer)
    {
        Condition = condition;
        Older     = older;
        Newer     = newer;
    }

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Condition
    ///
    /// <summary>
    /// Gets the diff condition.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public Condition Condition { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// Older
    ///
    /// <summary>
    /// Gets the target part of the older sequence.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public IEnumerable<T> Older { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// Newer
    ///
    /// <summary>
    /// Gets the target part of the newer sequence.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public IEnumerable<T> Newer { get; }

    #endregion
}
