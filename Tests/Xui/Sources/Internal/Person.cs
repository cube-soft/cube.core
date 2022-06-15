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
namespace Cube.Xui.Tests;

using Cube.DataContract;

/* ------------------------------------------------------------------------- */
///
/// Person
///
/// <summary>
/// Represents the dummy data for testing.
/// </summary>
///
/* ------------------------------------------------------------------------- */
class Person : SerializableBase
{
    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Name
    ///
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Name
    {
        get => Get<string>();
        set => Set(value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Age
    ///
    /// <summary>
    /// Gets or sets the age.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public int Age
    {
        get => Get<int>();
        set => Set(value);
    }

    #endregion
}
