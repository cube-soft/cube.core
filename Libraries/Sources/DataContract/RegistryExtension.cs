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
using Cube.Mixin.Generics;
using Microsoft.Win32;

namespace Cube.DataContract.Mixin
{
    /* --------------------------------------------------------------------- */
    ///
    /// RegistryExtension
    ///
    /// <summary>
    /// Provides extended methods to access values of the registry.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class RegistryExtension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// GetValue
        ///
        /// <summary>
        /// Gets a value of type T from the specified arguments.
        /// </summary>
        ///
        /// <param name="src">Root key of the target registry.</param>
        /// <param name="subkey">Name of the registry subkey.</param>
        /// <param name="name">Name of the getting value.</param>
        ///
        /// <returns>Value of type T.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T GetValue<T>(this RegistryKey src, string subkey, string name) =>
            src.GetValue(subkey, name, default(T));

        /* ----------------------------------------------------------------- */
        ///
        /// GetValue
        ///
        /// <summary>
        /// Gets a value of type T from the specified arguments.
        /// </summary>
        ///
        /// <param name="src">Root key of the target registry.</param>
        /// <param name="subkey">Name of the registry subkey.</param>
        /// <param name="name">Name of the getting value.</param>
        /// <param name="defaultValue">
        /// Default value to be used when it fails.
        /// </param>
        ///
        /// <returns>Value of type T.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T GetValue<T>(this RegistryKey src, string subkey, string name, T defaultValue)
        {
            using (var sk = src.OpenSubKey(subkey, false))
            {
                if (sk == null) return defaultValue;
                return sk.GetValue(name, defaultValue).TryCast(defaultValue);
            }
        }

        #endregion
    }
}
