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
/// Message(TValue)
///
/// <summary>
/// Represents the common message with a value.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class Message<TValue> : MessageBase
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// Message
    ///
    /// <summary>
    /// Initializes a new instance of the Message class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public Message() : this($"{typeof(TValue).Name} Message") { }

    /* --------------------------------------------------------------------- */
    ///
    /// Message
    ///
    /// <summary>
    /// Initializes a new instance of the Message class with the specified
    /// text.
    /// </summary>
    ///
    /// <param name="text">Message text.</param>
    ///
    /* --------------------------------------------------------------------- */
    public Message(string text) : base(text) { }

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Value
    ///
    /// <summary>
    /// Gets or sets the user defined value.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public TValue Value { get; set; }

    #endregion
}
