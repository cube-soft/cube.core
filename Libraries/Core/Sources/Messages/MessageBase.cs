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
/// MessageBase
///
/// <summary>
/// Represents the base class of messages.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public abstract class MessageBase
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// MessageBase
    ///
    /// <summary>
    /// Initializes a new instance of the MessageBase class with the
    /// specified text.
    /// </summary>
    ///
    /// <param name="text">Message text.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected MessageBase(string text) => Text = text;

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Text
    ///
    /// <summary>
    /// Gets the text for the message.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Text { get; }

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// ToString
    ///
    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    ///
    /// <returns>
    /// A string that represents the current object.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public override string ToString() => Text;

    #endregion
}
