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
namespace Cube.Tests.Extensions;

using Cube.Registries.Extensions;
using Microsoft.Win32;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// RegistryTest
///
/// <summary>
/// Represents tests of extended methods for the RegistryKey class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class RegistryTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// GetValue
    ///
    /// <summary>
    /// Executes the test to get the value of the registry.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void GetValue() => Assert.That(
        Registry.LocalMachine.GetValue<string>(
            @"SOFTWARE\Microsoft\Windows NT\CurrentVersion",
            "ProductName"
        ),
        Does.StartWith("Windows")
    );

    /* --------------------------------------------------------------------- */
    ///
    /// GetValue_Unmatched
    ///
    /// <summary>
    /// Confirms the behavior when the specified type is not matched.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void GetValue_Unmatched() => Assert.That(
        Registry.LocalMachine.GetValue<int>(
            @"SOFTWARE\Microsoft\Windows NT\CurrentVersion",
            "ProductName",
            100
        ),
        Is.EqualTo(100)
    );

    /* --------------------------------------------------------------------- */
    ///
    /// GetValue_NotFound_Subkey
    ///
    /// <summary>
    /// Confirms the behavior when the specified subkey does not
    /// exist.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void GetValue_NotFound_Subkey() => Assert.That(
        Registry.LocalMachine.GetValue<string>(
            @"Dummy\NotFound\SubKey",
            "Dummy"
        ),
        Is.Null
    );

    /* --------------------------------------------------------------------- */
    ///
    /// GetValue_NotFound_Number
    ///
    /// <summary>
    /// Confirms the behavior when the specified name does not
    /// exist.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void GetValue_NotFound_Number() => Assert.That(
        Registry.LocalMachine.GetValue<int>(
            @"SOFTWARE\Microsoft\Windows NT\CurrentVersion",
            "DummyValue"
        ),
        Is.EqualTo(0)
    );

    /* --------------------------------------------------------------------- */
    ///
    /// GetValue_NotFound_String
    ///
    /// <summary>
    /// Confirms the behavior when the specified name does not
    /// exist.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void GetValue_NotFound_String() => Assert.That(
        Registry.LocalMachine.GetValue<string>(
            @"SOFTWARE\Microsoft\Windows NT\CurrentVersion",
            "DummyValue"
        ),
        Is.Null
    );

    #endregion
}
