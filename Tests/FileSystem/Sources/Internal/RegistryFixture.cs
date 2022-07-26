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
namespace Cube.FileSystem.Tests;

using System.Reflection;
using Cube.DataContract;
using Cube.Mixin.Registry;
using Cube.Tests;
using Microsoft.Win32;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// RegistryFixture
///
/// <summary>
/// Provides functionality to assist the test with the registry.
/// </summary>
///
/* ------------------------------------------------------------------------- */
class RegistryFixture : FileFixture
{
    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Assembly
    ///
    /// <summary>
    /// Gets the Assembly object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected Assembly Assembly { get; } = Assembly.GetExecutingAssembly();

    /* --------------------------------------------------------------------- */
    ///
    /// Shared
    ///
    /// <summary>
    /// Gets the subkey name that is shared in the tests.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected string Shared => $@"CubeSoft\{GetType().Name}";

    /* --------------------------------------------------------------------- */
    ///
    /// Default
    ///
    /// <summary>
    /// Gets the default subkey name.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected string Default => nameof(Default);

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// GetKeyName
    ///
    /// <summary>
    /// Gets the registry key name with the specified value.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected string GetKeyName(string subkey) => $@"{Shared}\{subkey}";

    /* --------------------------------------------------------------------- */
    ///
    /// CreateSubKey
    ///
    /// <summary>
    /// Creates a new subkey of the registry with the specified value.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected RegistryKey CreateSubKey(string subkey) =>
        Proxy.RootRegistryKey.CreateSubKey(GetKeyName(subkey));

    /* --------------------------------------------------------------------- */
    ///
    /// OpenSubKey
    ///
    /// <summary>
    /// Opens the specified subkey as read-only.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected RegistryKey OpenSubKey(string subkey) =>
        Proxy.RootRegistryKey.OpenSubKey(GetKeyName(subkey), false);

    /* --------------------------------------------------------------------- */
    ///
    /// Setup
    ///
    /// <summary>
    /// Invokes the setup operation.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [SetUp]
    protected virtual void Setup()
    {
        using var k = CreateSubKey(Default);
        k.SetValue("ID", 1357);
        k.SetValue(nameof(Person.Name), "山田太郎");
        k.SetValue(nameof(Person.Sex), 0);
        k.SetValue(nameof(Person.Age), 52);
        k.SetValue(nameof(Person.Creation), "2015/03/16 02:32:26");
        k.SetValue(nameof(Person.Reserved), 1);

        using (var sk = k.CreateSubKey(nameof(Person.Contact)))
        {
            sk.SetValue(nameof(Address.Type), "Phone");
            sk.SetValue(nameof(Address.Value), "090-1234-5678");
        }

        using (var sk = k.CreateSubKey(nameof(Person.Others)))
        {
            sk.SetValue("0", nameof(Address.Type),  "PC");
            sk.SetValue("0", nameof(Address.Value), "pc@example.com");
            sk.SetValue("1", nameof(Address.Type),  "Mobile");
            sk.SetValue("1", nameof(Address.Value), "mobile@example.com");
        }

        using (var sk = k.CreateSubKey(nameof(Person.Messages)))
        {
            sk.SetValue("0", "", "1st message");
            sk.SetValue("1", "", "2nd message");
            sk.SetValue("2", "", "3rd message");
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Teardown
    ///
    /// <summary>
    /// Invokes the tear-down operation.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TearDown]
    protected override void Teardown() => Proxy.RootRegistryKey.DeleteSubKeyTree(Shared, false);

    #endregion
}
