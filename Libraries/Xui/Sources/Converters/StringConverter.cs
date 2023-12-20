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
namespace Cube.Xui.Converters;


#region UpperCase

/* ------------------------------------------------------------------------- */
///
/// UpperCase
///
/// <summary>
/// Provides functionality to convert to upper case.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class UpperCase : SimplexConverter
{
    /* --------------------------------------------------------------------- */
    ///
    /// UpperCase
    ///
    /// <summary>
    /// Initializes a new instance of the UpperCase class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public UpperCase() : base((e, _, _, c) => e?.ToString().ToUpper(c) ?? string.Empty) { }
}

#endregion

#region LowerCase

/* ------------------------------------------------------------------------- */
///
/// LowerCase
///
/// <summary>
/// Provides functionality to convert to lower case.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class LowerCase : SimplexConverter
{
    /* --------------------------------------------------------------------- */
    ///
    /// LowerCase
    ///
    /// <summary>
    /// Initializes a new instance of the LowerCase class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public LowerCase() : base((e, _, _, c) => e?.ToString().ToLower(c) ?? string.Empty) { }
}

#endregion
