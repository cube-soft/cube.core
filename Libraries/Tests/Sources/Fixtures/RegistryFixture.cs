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
namespace Cube.Tests;

using Cube.DataContract;
using Cube.FileSystem;
using Microsoft.Win32;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// RegistryFixture
///
/// <summary>
/// Provides functionality for files and registry testing.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public abstract class RegistryFixture : FileFixture
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// RegistryFixture
    ///
    /// <summary>
    /// Initializes a new instance of the RegistryFixture class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected RegistryFixture() => RootKeyName = Io.Combine("CubeSoft", GetType().FullName);

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Root
    ///
    /// <summary>
    /// Gets the name of the root subkey. Note that the registry subkey
    /// with the name is assumed to be under the RootRegistryKey value.
    /// </summary>
    ///
    /// <see cref="Proxy" />
    ///
    /* --------------------------------------------------------------------- */
    protected string RootKeyName { get; }

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// GetKeyName
    ///
    /// <summary>
    /// Gets the combined subkey name of the RootKeyName and specified names.
    /// </summary>
    ///
    /// <param name="names">List of subkey names to be combined.</param>
    ///
    /// <returns>Combined subkey name.</returns>
    ///
    /* --------------------------------------------------------------------- */
    protected string GetKeyName(params string[] names) => Io.Combine(RootKeyName, Io.Combine(names));

    /* --------------------------------------------------------------------- */
    ///
    /// CreateKey
    ///
    /// <summary>
    /// Creates a new subkey of the registry with the specified names.
    /// </summary>
    ///
    /// <param name="names">List of subkey names.</param>
    ///
    /// <returns>RegistryKey object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    protected RegistryKey CreateKey(params string[] names) =>
        Proxy.RootRegistryKey.CreateSubKey(GetKeyName(names));

    /* --------------------------------------------------------------------- */
    ///
    /// OpenKey
    ///
    /// <summary>
    /// Opens the specified subkey as read-only.
    /// </summary>
    ///
    /// <param name="names">List of subkey names.</param>
    ///
    /// <returns>RegistryKey object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    protected RegistryKey OpenKey(params string[] names) =>
        Proxy.RootRegistryKey.OpenSubKey(GetKeyName(names), false);

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
    protected override void Teardown()
    {
        try { Format.Registry.Delete(RootKeyName); }
        finally { base.Teardown(); }
    }

    #endregion
}
