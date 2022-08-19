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

using System;
/* ------------------------------------------------------------------------- */
///
/// MessageBase
///
/// <summary>
/// Represents the message to notify a URL.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class UriMessage : CancelMessage<Uri>
{
    /* --------------------------------------------------------------------- */
    ///
    /// UriMessage
    ///
    /// <summary>
    /// Initializes a new instance of the UriMessage class.
    /// </summary>
    ///
    /// <param name="src">Source URL.</param>
    ///
    /* --------------------------------------------------------------------- */
    public UriMessage(Uri src) : this(src, nameof(UriMessage)) { }

    /* --------------------------------------------------------------------- */
    ///
    /// UriMessage
    ///
    /// <summary>
    /// Initializes a new instance of the UriMessage class with the
    /// specified URL.
    /// </summary>
    ///
    /// <param name="src">Source URL.</param>
    /// <param name="text">Message text.</param>
    ///
    /* --------------------------------------------------------------------- */
    public UriMessage(Uri src, string text) : base(text) => Value = src;
}
