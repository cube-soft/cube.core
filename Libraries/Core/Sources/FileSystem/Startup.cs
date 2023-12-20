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
namespace Cube.FileSystem;

using System;
using System.Collections.Generic;
using System.Linq;
using Cube.Collections.Extensions;
using Cube.Text.Extensions;
using Microsoft.Win32;

/* ------------------------------------------------------------------------- */
///
/// Startup
///
/// <summary>
/// Provides functionality to register and delete startup settings.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class Startup : ObservableBase
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// Startup
    ///
    /// <summary>
    /// Initializes a new instance of the Startup class with the
    /// specified name.
    /// </summary>
    ///
    /// <param name="name">Name to register.</param>
    ///
    /* --------------------------------------------------------------------- */
    public Startup(string name)
    {
        bool exists(string s)
        {
            using var k = Open(false);
            return k?.GetValue(s) is not null;
        }

        if (!name.HasValue()) throw new ArgumentException(nameof(name));
        Name    = name;
        Enabled = exists(name);
    }

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Name
    ///
    /// <summary>
    /// Gets the name registered in startup programs.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Name { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// Enabled
    ///
    /// <summary>
    /// Gets or sets the value indicating whether the startup
    /// settings is enabled.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public bool Enabled
    {
        get => Get<bool>();
        set => Set(value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Source
    ///
    /// <summary>
    /// Gets or sets the source (filename, etc) that executes when
    /// startup.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Source
    {
        get => Get(() => string.Empty);
        set => Set(value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Arguments
    ///
    /// <summary>
    /// Gets or sets the arguments of the Source property.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public ICollection<string> Arguments { get; } = new List<string>();

    /* --------------------------------------------------------------------- */
    ///
    /// Command
    ///
    /// <summary>
    /// Gets the registered command.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Command =>
        Source.HasValue() ?
        new[] { Source }.Concat(Arguments).Join(" ", e => e.Quote()) :
        string.Empty;

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Save
    ///
    /// <summary>
    /// Saves settings to the registry.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public void Save() => Save(false);

    /* --------------------------------------------------------------------- */
    ///
    /// Save
    ///
    /// <summary>
    /// Saves settings to the registry with the specified settings.
    /// </summary>
    ///
    /// <param name="checkExists">
    /// Value indicating whether to check for the existence of the
    /// provided source. If the value is set to true and the provided
    /// source does not exist, the provided name will be removed from
    /// the registry regardless of the Enabled property.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    public void Save(bool checkExists)
    {
        bool isset()
        {
            if (!Enabled) return false;
            if (!checkExists) return true;
            return Source.HasValue() && Io.Exists(Source);
        }

        using var sk = Open(true);
        if (sk is null) return;
        if (isset()) sk.SetValue(Name, Command);
        else sk.DeleteValue(Name, false);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Dispose
    ///
    /// <summary>
    /// Releases the unmanaged resources used by the object and
    /// optionally releases the managed resources.
    /// </summary>
    ///
    /// <param name="disposing">
    /// true to release both managed and unmanaged resources;
    /// false to release only unmanaged resources.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    protected override void Dispose(bool disposing) { }

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// Open
    ///
    /// <summary>
    /// Gets the RegistryKey object for startup programs.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private RegistryKey Open(bool writable) => Registry.CurrentUser.OpenSubKey(
        @"Software\Microsoft\Windows\CurrentVersion\Run", writable
    );

    #endregion
}
