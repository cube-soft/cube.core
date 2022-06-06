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
namespace Cube.Collections;

/* ------------------------------------------------------------------------- */
///
/// ArgumentToken
///
/// <summary>
/// Represents an item of arguments. The class is mainly used in the
/// IArgumentPreprocessor implemented classes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class ArgumentToken
{
    /* --------------------------------------------------------------------- */
    ///
    /// ArgumentToken
    ///
    /// <summary>
    /// Initializes a new instance of the ArgumentToken class with the
    /// specified value.
    /// </summary>
    ///
    /// <param name="value">Value of the argument.</param>
    ///
    /* --------------------------------------------------------------------- */
    public ArgumentToken(string value) : this(value, string.Empty) { }

    /* --------------------------------------------------------------------- */
    ///
    /// ArgumentToken
    ///
    /// <summary>
    /// Initializes a new instance of the ArgumentToken class with the
    /// specified arguments.
    /// </summary>
    ///
    /// <param name="value">Value of the argument.</param>
    /// <param name="prefix">Prefix of the argument.</param>
    ///
    /* --------------------------------------------------------------------- */
    public ArgumentToken(string value, string prefix)
    {
        Value  = value;
        Prefix = prefix;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Value
    ///
    /// <summary>
    /// Gets the value of the token.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Value { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// Prefix
    ///
    /// <summary>
    /// Gets the prefix value of the token.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Prefix { get; }
}
