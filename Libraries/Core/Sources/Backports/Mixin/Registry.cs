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
using Microsoft.Win32;

namespace Cube.Backports
{
    /* --------------------------------------------------------------------- */
    ///
    /// RegistryExtension
    ///
    /// <summary>
    /// Provides extended methods for the Registry class for compatibility
    /// with .NET Framework 3.5.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class RegistryExtension
    {
        /* ----------------------------------------------------------------- */
        ///
        /// DeleteSubKeyTree
        ///
        /// <summary>
        /// Deletes the specified subkey and any child subkeys recursively,
        /// and specifies whether an exception is raised if the subkey
        /// is not found.
        /// </summary>
        ///
        /// <param name="key">A source RegistryKey object.</param>
        ///
        /// <param name="subkey">
        /// The name of the subkey to delete. This string is not
        /// case-sensitive.
        /// </param>
        ///
        /// <param name="throwOnMissingSubKey">
        /// Indicates whether an exception should be raised if the specified
        /// subkey cannot be found. If this argument is true and the specified
        /// subkey does not exist, an exception is raised.
        /// If this argument is false and the specified subkey does not
        /// exist, no action is taken.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public static void DeleteSubKeyTree(this RegistryKey key, string subkey, bool throwOnMissingSubKey)
        {
            try { key.DeleteSubKeyTree(subkey); }
            catch { if (throwOnMissingSubKey) throw; }
        }
    }
}
