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
/// ProcessMessage
///
/// <summary>
/// Represents the message to notify a command line.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class ProcessMessage : CancelMessage<string>
{
    /* --------------------------------------------------------------------- */
    ///
    /// ProcessMessage
    ///
    /// <summary>
    /// Initializes a new instance of the ProcessMessage class with the
    /// specified command line.
    /// </summary>
    ///
    /// <param name="src">Command line.</param>
    ///
    /* --------------------------------------------------------------------- */
    public ProcessMessage(string src) : this(src, nameof(ProcessMessage)) { }

    /* --------------------------------------------------------------------- */
    ///
    /// ProcessMessage
    ///
    /// <summary>
    /// Initializes a new instance of the ProcessMessage class with the
    /// specified arguments.
    /// </summary>
    ///
    /// <param name="src">Command line.</param>
    /// <param name="text">Message text.</param>
    ///
    /* --------------------------------------------------------------------- */
    public ProcessMessage(string src, string text) : base(text) => Value = src;
}
