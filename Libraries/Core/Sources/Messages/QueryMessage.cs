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
namespace Cube;

/* ------------------------------------------------------------------------- */
///
/// QueryMessage(TSource, TValue)
///
/// <summary>
/// Represents the common message with a source, value, and cancel status.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class QueryMessage<TSource, TValue> : CancelMessage<TValue>
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// QueryMessage
    ///
    /// <summary>
    /// Initializes a new instance of the QueryMessage class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public QueryMessage(TSource src) : this(src, $"{typeof(TSource).Name} and {typeof(TValue).Name} QueryMessage") { }

    /* --------------------------------------------------------------------- */
    ///
    /// QueryMessage
    ///
    /// <summary>
    /// Initializes a new instance of the QueryMessage class with the
    /// specified text.
    /// </summary>
    ///
    /// <param name="src">Source information at query time.</param>
    /// <param name="text">Message text.</param>
    ///
    /* --------------------------------------------------------------------- */
    public QueryMessage(TSource src, string text) : base(text) => Source = src;

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Source
    ///
    /// <summary>
    /// Get or set the source information at query time.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public TSource Source { get; }

    #endregion
}
