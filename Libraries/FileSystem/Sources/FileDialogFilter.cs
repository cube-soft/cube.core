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
using Cube.Mixin.Collections;
using Cube.Mixin.String;

namespace Cube.FileSystem
{
    #region FileDialogFilter

    /* --------------------------------------------------------------------- */
    ///
    /// FileDialogFilter
    ///
    /// <summary>
    /// Provides functionality to create a filter description for the
    /// OpenFileDialog or SaveFileDialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class FileDialogFilter
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// FileDialogFilter
        ///
        /// <summary>
        /// Initializes a new instance of the FileDialogFilter class
        /// with the specified parameters
        /// </summary>
        ///
        /// <param name="description">Description for the filter.</param>
        /// <param name="extensions">
        /// List of target extensions (e.g., ".txt").
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public FileDialogFilter(string description, params string[] extensions) :
            this(description, true, extensions) { }

        /* ----------------------------------------------------------------- */
        ///
        /// FileDialogFilter
        ///
        /// <summary>
        /// Initializes a new instance of the FileDialogFilter class
        /// with the specified parameters
        /// </summary>
        ///
        /// <param name="text">Description for the filter.</param>
        /// <param name="ignoreCase">Ignores case or not.</param>
        /// <param name="extensions">
        /// List of target extensions (e.g., ".txt").
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public FileDialogFilter(string text, bool ignoreCase, params string[] extensions)
        {
            Text       = text;
            IgnoreCase = ignoreCase;
            Targets    = extensions;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Text
        ///
        /// <summary>
        /// Gets a description for the filter.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Text { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Targets
        ///
        /// <summary>
        /// Gets a list of target extensions.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IEnumerable<string> Targets { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// IgnoreCase
        ///
        /// <summary>
        /// Gets a value indicating whether letter cases of the specified
        /// extensions are ignored.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IgnoreCase { get; }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// ToString
        ///
        /// <summary>
        /// Converts to a string representing the filter.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public override string ToString()
        {
            var src = Targets.Select(e => $"*{e}");
            var s0  = src.Join(", ");
            var s1  = src.Join(";", e => Format(e));

            return $"{Text} ({s0})|{s1}";
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Format
        ///
        /// <summary>
        /// Converts an extension to a filter string according to the user
        /// settings.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private string Format(string src)
        {
            if (!IgnoreCase) return src;

            var x = src.ToLower();
            var y = src.ToUpper();

            return x.Equals(y) ? x : $"{x};{y}";
        }

        #endregion
    }

    #endregion

    #region FileDialogFilterExtension

    /* --------------------------------------------------------------------- */
    ///
    /// FileDialogFilterExtension
    ///
    /// <summary>
    /// Provides functionality to convert the FileDialogFilter objects
    /// to system required arguments.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class FileDialogFilterExtension
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

    #endregion
}
