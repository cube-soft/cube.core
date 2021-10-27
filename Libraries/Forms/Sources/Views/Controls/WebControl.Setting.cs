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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;

namespace Cube.Forms.Controls
{
    /* --------------------------------------------------------------------- */
    ///
    /// WebControl
    ///
    /// <summary>
    /// Provides functionality to get or set the WebControl system settings.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    partial class WebControl
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// EmulateVersion
        ///
        /// <summary>
        /// Gets or sets the IE version.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static WebControlVersion EmulateVersion
        {
            get => GetEmulateVersion();
            set => SetEmulateVersion(value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GpuRendering
        ///
        /// <summary>
        /// Gets or sets a value indicating whether GPU rendering mode is
        /// enabled.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static bool GpuRendering
        {
            get => GetGpuRendering();
            set => SetGpuRendering(value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MaxConnections
        ///
        /// <summary>
        /// Gets or sets the maximum number of simultaneous connections.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static int MaxConnections
        {
            get => GetMaxConnections();
            set => SetMaxConnections(value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// NavigationSounds
        ///
        /// <summary>
        /// Gets or sets a value indicating whether the navigation sounds
        /// is enabled.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static bool NavigationSounds
        {
            get => GetNavigationSounds();
            set => SetNavigationSounds(value);
        }

        #endregion

        #region Implementations

        #region EmulateVersion

        /* ----------------------------------------------------------------- */
        ///
        /// GetEmulateVersion
        ///
        /// <summary>
        /// Gets the IE version.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static WebControlVersion GetEmulateVersion()
        {
            using var root = OpenFeatureControl();
            using var sk   = root?.OpenSubKey(_RegEmulation, false);
            if (sk == null) return WebControlVersion.IE7;

            var module   = Process.GetCurrentProcess().MainModule;
            var filename = Path.GetFileName(module.FileName);
            var version  = sk.GetValue(filename);
            return version != null ? (WebControlVersion)version : WebControlVersion.IE7;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetEmulateVersion
        ///
        /// <summary>
        /// Sets the IE version.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void SetEmulateVersion(WebControlVersion version)
        {
            using var root = OpenFeatureControl(true);
            using var sk   = root?.CreateSubKey(_RegEmulation);
            if (sk == null) return;

            var module   = Process.GetCurrentProcess().MainModule;
            var filename = Path.GetFileName(module.FileName);
            var value    = (version == WebControlVersion.Latest) ? GetLatestVersion() : version;
            sk.SetValue(filename, (int)value);
        }

        #endregion

        #region GpuRendering

        /* ----------------------------------------------------------------- */
        ///
        /// GetGpuRendering
        ///
        /// <summary>
        /// Gets a value indicating whether GPU rendering mode is enabled.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static bool GetGpuRendering()
        {
            using var root = OpenFeatureControl();
            using var sk   = root?.OpenSubKey(_RegRendering, false);
            if (sk == null) return false;

            var module   = Process.GetCurrentProcess().MainModule;
            var filename = Path.GetFileName(module.FileName);
            var value    = sk.GetValue(filename);
            return value != null && (int)value == 1;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetGpuRendering
        ///
        /// <summary>
        /// Sets a value indicating whether GPU rendering mode is enabled.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void SetGpuRendering(bool enabled)
        {
            using var root = OpenFeatureControl(true);
            using var sk   = root?.CreateSubKey(_RegRendering);
            if (sk == null) return;

            var module   = Process.GetCurrentProcess().MainModule;
            var filename = Path.GetFileName(module.FileName);
            var value    = enabled ? 1 : 0;
            sk.SetValue(filename, value);
        }

        #endregion

        #region MaxConnections

        /* ----------------------------------------------------------------- */
        ///
        /// GetMaxConnections
        ///
        /// <summary>
        /// Gets the maximum number of simultaneous connections.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static int GetMaxConnections()
        {
            const int default_max_connection = 6;

            using var root = OpenFeatureControl();
            using var sk = root?.OpenSubKey(_RegMaxConnections, false);
            if (sk == null) return default_max_connection;

            var module   = Process.GetCurrentProcess().MainModule;
            var filename = Path.GetFileName(module.FileName);
            var value    = sk.GetValue(filename);
            return value != null ? (int)value : default_max_connection;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetMaxConnections
        ///
        /// <summary>
        /// Sets the maximum number of simultaneous connections.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void SetMaxConnections(int number)
        {
            if (number < 2 || number > 128) return;

            using var root = OpenFeatureControl(true);
            using var sk   = root?.CreateSubKey(_RegMaxConnections);
            using var sk10 = root?.CreateSubKey(_RegMaxConnections10);

            var module   = Process.GetCurrentProcess().MainModule;
            var filename = Path.GetFileName(module.FileName);
            if (sk != null) sk.SetValue(filename, number);
            if (sk10 != null) sk10.SetValue(filename, number);
        }

        #endregion

        #region NavigationSounds

        /* ----------------------------------------------------------------- */
        ///
        /// GetNavigationSounds
        ///
        /// <summary>
        /// Gets a value indicating whether the navigation sounds is enabled.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static bool GetNavigationSounds() =>
            UrlMon.NativeMethods.CoInternetIsFeatureEnabled(
                21,     // FEATURE_DISABLE_NAVIGATION_SOUNDS
                0x02    // SET_FEATURE_ON_PROCESS
            ) != 0;

        /* ----------------------------------------------------------------- */
        ///
        /// SetNavigationSounds
        ///
        /// <summary>
        /// Sets a value indicating whether the navigation sounds is enabled.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void SetNavigationSounds(bool enabled)=>
            UrlMon.NativeMethods.CoInternetSetFeatureEnabled(
                21,     // FEATURE_DISABLE_NAVIGATION_SOUNDS
                0x02,   // SET_FEATURE_ON_PROCESS
                !enabled
            );

        #endregion

        /* ----------------------------------------------------------------- */
        ///
        /// OpenFeatureControl
        ///
        /// <summary>
        /// Gets the FeatureControl subkey object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static RegistryKey OpenFeatureControl(bool writable = false)
        {
            var name = Path.Combine(_RegRoot, @"Main\FeatureControl");
            return writable ?
                   Registry.CurrentUser.CreateSubKey(name) :
                   Registry.CurrentUser.OpenSubKey(name, false);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetLatestVersion
        ///
        /// <summary>
        /// Gets the latest installed IE version.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static WebControlVersion GetLatestVersion()
        {
            using var sk = Registry.LocalMachine.OpenSubKey(_RegRoot, false);

            var value = sk?.GetValue("svcVersion") as string ??
                        sk?.GetValue("Version")    as string;
            if (value == null) return WebControlVersion.IE7;

            var src = int.Parse(value.Substring(0, value.IndexOf('.')));
            return GetVersionMap().TryGetValue(src, out var dest) ?
                   dest :
                   WebControlVersion.IE7;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetVersionMap
        ///
        /// <summary>
        /// Gets the IE version collection.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static Dictionary<int, WebControlVersion> GetVersionMap() => _vmap ??= new()
        {
            {  7, WebControlVersion.IE7  },
            {  8, WebControlVersion.IE8  },
            {  9, WebControlVersion.IE9  },
            { 10, WebControlVersion.IE10 },
            { 11, WebControlVersion.IE11 },
        };

        #endregion

        #region Fields
        private static readonly string _RegRoot = @"Software\Microsoft\Internet Explorer";
        private static readonly string _RegEmulation = "FEATURE_BROWSER_EMULATION";
        private static readonly string _RegRendering = "FEATURE_GPU_RENDERING ";
        private static readonly string _RegMaxConnections = "FEATURE_MAXCONNECTIONSPERSERVER";
        private static readonly string _RegMaxConnections10 = "FEATURE_MAXCONNECTIONSPER1_0SERVER";
        private static Dictionary<int, WebControlVersion> _vmap;
        #endregion
    }
}
