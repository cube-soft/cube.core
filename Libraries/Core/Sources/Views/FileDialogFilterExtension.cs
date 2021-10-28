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
using System;
using System.Collections.Generic;
using System.Linq;
using Cube.Collections;
using Cube.FileSystem;
using Cube.Mixin.String;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// FileDialogFilterConverter
    ///
    /// <summary>
    /// Provides functionality to convert the FileDialogFilter objects
    /// to system required arguments.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class FileDialogFilterConverter
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// GetFilter
        ///
        /// <summary>
        /// Gets a string value that represents the filter of either the
        /// OpenFileDialog or SaveFileDialog.
        /// </summary>
        ///
        /// <param name="src">DisplayFilter collection.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetFilter(this IEnumerable<FileDialogFilter> src) =>
            src.Select(e => e.ToString()).Aggregate((x, y) => $"{x}|{y}");

        /* ----------------------------------------------------------------- */
        ///
        /// GetFilterIndex
        ///
        /// <summary>
        /// Gets the index of the first occurrence of the specified path
        /// in the current DisplayFilter collection.
        /// </summary>
        ///
        /// <param name="src">DisplayFilter collection.</param>
        /// <param name="path">File path.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static int GetFilterIndex(this IEnumerable<FileDialogFilter> src, string path)
        {
            if (!path.HasValue()) return 0;

            var ext = Io.Get(path).Extension;
            var opt = StringComparison.InvariantCultureIgnoreCase;

            return src.Select((e, i) => KeyValuePair.Create(i + 1, e))
                      .FirstOrDefault(e => e.Value.Targets.Any(e2 => e2.Equals(ext, opt)))
                      .Key;
        }

        #endregion
    }
}
