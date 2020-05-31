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
using System.IO;
using Cube.Mixin.Assembly;
using Cube.Mixin.Syntax;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// FileFixture
    ///
    /// <summary>
    /// Provides functionality to load or save files for tests.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    class FileFixture
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// FileFixture
        ///
        /// <summary>
        /// Initializes a new instance of the FileFixture class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected FileFixture()
        {
            Root     = typeof(FileFixture).Assembly.GetDirectoryName();
            Name     = GetType().FullName;
            Examples = Path.Combine(Root, nameof(Examples));
            Results  = Path.Combine(Root, $@"{nameof(Results)}\{Name}");

            if (!Directory.Exists(Results)) _ = Directory.CreateDirectory(Results);
            Delete(Results);
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Root
        ///
        /// <summary>
        /// Gets the path of the root directory that has test resources.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected string Root { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Examples
        ///
        /// <summary>
        /// Gets the path of the directory that has example files for tests.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected string Examples { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Results
        ///
        /// <summary>
        /// Gets the path of the directory that test results is saved.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected string Results { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Name
        ///
        /// <summary>
        /// Gets the class name.
        /// </summary>
        ///
        /// <remarks>
        /// テスト結果を格納するディレクトリの生成時に使用します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected string Name { get; }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Get
        ///
        /// <summary>
        /// Gets the absolute path with the specified file or directory,
        /// assuming that it is in the Results directory.
        /// </summary>
        ///
        /// <param name="paths">
        /// List of file or directory names to be combined as a path.
        /// </param>
        ///
        /// <returns>Combined path.</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected string Get(params string[] paths) =>
            Path.Combine(Results, string.Join("\"", paths));

        /* ----------------------------------------------------------------- */
        ///
        /// GetSource
        ///
        /// <summary>
        /// Gets the absolute path with the specified file or directory,
        /// assuming that it is in the Examples directory.
        /// </summary>
        ///
        /// <param name="paths">
        /// List of file or directory names to be combined as a path.
        /// </param>
        ///
        /// <returns>Combined path.</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected string GetSource(params string[] paths) =>
            Path.Combine(Examples, string.Join("\"", paths));

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Delete
        ///
        /// <summary>
        /// Deletes all of files and directories in the specified path.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Delete(string directory)
        {
            Directory.GetFiles(directory).Each(f => {
                File.SetAttributes(f, FileAttributes.Normal);
                File.Delete(f);
            });

            Directory.GetDirectories(directory).Each(d => {
                Delete(d);
                Directory.Delete(d);
            });
        }

        #endregion
    }
}
