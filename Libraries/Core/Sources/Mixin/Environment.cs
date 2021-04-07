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
using Source = System.Environment;

namespace Cube.Mixin.Environment
{
    /* --------------------------------------------------------------------- */
    ///
    /// Extension
    ///
    /// <summary>
    /// Provides extended methods of the Environment class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Extension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// GetName
        ///
        /// <summary>
        /// Gets the directory name corresponding to the specified value.
        /// </summary>
        ///
        /// <param name="src">
        /// Value that represents the special folder.
        /// </param>
        ///
        /// <returns>Directory name.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetName(this Source.SpecialFolder src) => Source.GetFolderPath(src);

        #endregion
    }
}
