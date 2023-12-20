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
/// Query
///
/// <summary>
/// Provides functionality to create a new instance of the Query(T, U)
/// or related classes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class Query
{
    #region NewMessage

    /* --------------------------------------------------------------------- */
    ///
    /// NewMessage
    ///
    /// <summary>
    /// Creates a new instance of the QueryMessage(TValue, TValue) class
    /// with the specified query.
    /// </summary>
    ///
    /// <typeparam name="TValue">type of source and result.</typeparam>
    ///
    /// <param name="src">Source information.</param>
    ///
    /// <returns>QueryMessage(TValue, TValue) object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static QueryMessage<TValue, TValue> NewMessage<TValue>(TValue src) =>
        NewMessage<TValue, TValue>(src);

    /* --------------------------------------------------------------------- */
    ///
    /// NewMessage
    ///
    /// <summary>
    /// Creates a new instance of the QueryMessage(TSource, TValue) class
    /// with the specified query.
    /// </summary>
    ///
    /// <typeparam name="TSource">type of query source.</typeparam>
    /// <typeparam name="TValue">type of result value.</typeparam>
    ///
    /// <param name="src">Source information.</param>
    ///
    /// <returns>QueryMessage(TSource, TValue) object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static QueryMessage<TSource, TValue> NewMessage<TSource, TValue>(TSource src) =>
        NewMessage(src, default(TValue));

    /* --------------------------------------------------------------------- */
    ///
    /// NewMessage
    ///
    /// <summary>
    /// Creates a new instance of the QueryMessage(T, U) class with
    /// the specified query and default value.
    /// </summary>
    ///
    /// <typeparam name="TSource">type of query source.</typeparam>
    /// <typeparam name="TValue">type of result value.</typeparam>
    ///
    /// <param name="src">Source information.</param>
    /// <param name="value">Default value of the message.</param>
    ///
    /// <returns>QueryMessage(T, U) object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static QueryMessage<TSource, TValue> NewMessage<TSource, TValue>(TSource src, TValue value) =>
        new(src) { Value = value };

    #endregion
}
