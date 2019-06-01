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
using Cube.Mixin.String;
using Microsoft.Win32;
using System;

namespace Cube.FileSystem
{
    /* --------------------------------------------------------------------- */
    ///
    /// Startup
    ///
    /// <summary>
    /// Provides functionality to register and delete startup settings.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Startup : ObservableBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Startup
        ///
        /// <summary>
        /// Initializes a new instance of the Startup class with the
        /// specified name.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Startup(string name)
        {
            if (!name.HasValue()) throw new ArgumentException(nameof(name));
            Name = name;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Name
        ///
        /// <summary>
        /// Gets the name registered in startup programs.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Name { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Command
        ///
        /// <summary>
        /// Gets or sets the command corresponding to the name.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Command
        {
            get => _command;
            set => SetProperty(ref _command, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Enabled
        ///
        /// <summary>
        /// Gets or sets the value indicating whether the startup
        /// settings is enabled.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Enabled
        {
            get => _enabled;
            set => SetProperty(ref _enabled, value);
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// Loads settings from the registry.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Load()
        {
            using (var sk = OpenSubkey(false))
            {
                Command = sk.GetValue(Name, string.Empty) as string;
                Enabled = Command.HasValue();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// Saves settings to the registry.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Save()
        {
            using (var sk = OpenSubkey(true))
            {
                if (Enabled)
                {
                    if (Command.HasValue()) sk.SetValue(Name, Command);
                }
                else sk.DeleteValue(Name, false);
            }
        }

        /* ----------------------------------------------------------------- */
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
        /* ----------------------------------------------------------------- */
        protected override void Dispose(bool disposing) { }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// OpenSubkey
        ///
        /// <summary>
        /// Gets the RegistryKey object for startup programs.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private RegistryKey OpenSubkey(bool writable) => Registry.CurrentUser.OpenSubKey(
            @"Software\Microsoft\Windows\CurrentVersion\Run", writable
        );

        #endregion

        #region Fields
        private string _command = string.Empty;
        private bool _enabled = false;
        #endregion
    }
}
