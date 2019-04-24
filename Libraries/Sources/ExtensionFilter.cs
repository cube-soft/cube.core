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
using Cube.Collections;
using Cube.Mixin.String;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cube.FileSystem
{
    /* --------------------------------------------------------------------- */
    ///
    /// ExtensionFilter
    ///
    /// <summary>
    /// Provides functionality to create a string value that is used
    /// as a filter of either the OpenFileDialog or SaveFileDialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ExtensionFilter
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ExtensionFilter
        ///
        /// <summary>
        /// Initializes a new instance of the ExtensionFilter class
        /// with the specfied parameters
        /// </summary>
        ///
        /// <param name="description">Description for the filter.</param>
        /// <param name="extensions">
        /// List of target extensions (e.g., ".txt").
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public ExtensionFilter(string description, params string[] extensions)
        {
            Text = description;
            Targets  = extensions;
            IgnoreCase  = true;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ExtensionFilter
        ///
        /// <summary>
        /// Initializes a new instance of the ExtensionFilter class
        /// with the specfied parameters
        /// </summary>
        ///
        /// <param name="text">Description for the filter.</param>
        /// <param name="ignoreCase">Ignores case or not.</param>
        /// <param name="targets">
        /// List of target extensions (e.g., ".txt").
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public ExtensionFilter(string text, bool ignoreCase, params string[] targets)
        {
            Text       = text;
            Targets    = targets;
            IgnoreCase = ignoreCase;
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
            var e0 = Targets.Select(e => $"*{e}");
            var s0 = string.Join(", ", e0.ToArray());
            var e1 = e0.Select(e => Format(e));
            var s1 = string.Join(";", e1.ToArray());

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

    /* --------------------------------------------------------------------- */
    ///
    /// ExtensionFilterConverter
    ///
    /// <summary>
    /// Provides functionality to convert the ExtensionFilter objects
    /// to system required arguments.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class ExtensionFilterConverter
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
        public static string GetFilter(this IEnumerable<ExtensionFilter> src) =>
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
        public static int GetFilterIndex(this IEnumerable<ExtensionFilter> src, string path) =>
            src.GetFilterIndex(path, new IO());

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
        /// <param name="io">I/O handler.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static int GetFilterIndex(this IEnumerable<ExtensionFilter> src, string path, IO io)
        {
            if (!path.HasValue()) return 0;

            var ext = io.Get(path).Extension;
            var opt = StringComparison.InvariantCultureIgnoreCase;

            return src.Select((e, i) => KeyValuePair.Create(i + 1, e))
                      .FirstOrDefault(e => e.Value.Targets.Any(e2 => e2.Equals(ext, opt)))
                      .Key;
        }

        #endregion
    }
}
