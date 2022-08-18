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
/// CancelMessage
///
/// <summary>
/// Represents the common message with a value and cancel status.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class CancelMessage<TValue> : Message<TValue>
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// CancelMessage
    ///
    /// <summary>
    /// Initializes a new instance of the CancelMessage class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public CancelMessage() : this($"{typeof(TValue).Name} CancelMessage") { }

    /* --------------------------------------------------------------------- */
    ///
    /// CancelMessage
    ///
    /// <summary>
    /// Initializes a new instance of the CancelMessage class with the
    /// specified text.
    /// </summary>
    ///
    /// <param name="text">Message text.</param>
    ///
    /* --------------------------------------------------------------------- */
    public CancelMessage(string text) : base(text) { }

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Cancel
    ///
    /// <summary>
    /// Gets or sets a value indicating whether to cancel the operation.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public bool Cancel { get; set; }

    #endregion
}
