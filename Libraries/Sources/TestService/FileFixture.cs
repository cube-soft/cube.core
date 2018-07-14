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
using System.Reflection;

namespace Cube.FileSystem.TestService
{
    /* --------------------------------------------------------------------- */
    ///
    /// FileFixture
    ///
    /// <summary>
    /// ユニットテストでファイルを使用する際の補助クラスです。
    /// </summary>
    ///
    /// <remarks>
    /// このクラスは、主にユニットテスト用クラスの内部処理で利用されます。
    /// このクラスを直接オブジェクト化する事はできません。必要なクラスに
    /// 継承する形で利用して下さい。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class FileFixture
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// FileFixture
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected FileFixture() : this(new IO()) { }

        /* ----------------------------------------------------------------- */
        ///
        /// FileFixture
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="io">ファイル操作用オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        protected FileFixture(IO io)
        {
            IO       = io;
            Root     = IO.Get(Assembly.GetExecutingAssembly().Location).DirectoryName;
            Name     = GetType().FullName;
            Examples = IO.Combine(Root, nameof(Examples));
            Results  = IO.Combine(Root, nameof(Results), Name);

            if (!IO.Exists(Results)) IO.CreateDirectory(Results);
            Delete(Results);
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// IO
        ///
        /// <summary>
        /// ファイル操作用オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected IO IO { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Root
        ///
        /// <summary>
        /// テスト用リソースの存在するルートディレクトリへのパスを
        /// 取得、または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected string Root { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Examples
        ///
        /// <summary>
        /// テスト用ファイルの存在するフォルダへのパスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected string Examples { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Results
        ///
        /// <summary>
        /// テスト結果を格納するためのフォルダへのパスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected string Results { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Name
        ///
        /// <summary>
        /// クラス名を取得します。
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
        /// GetExamplesWith
        ///
        /// <summary>
        /// 指定されたパス一覧の先頭に Examples ディレクトリのパスを結合
        /// した結果を取得します。
        /// </summary>
        ///
        /// <param name="paths">結合パス一覧</param>
        ///
        /// <returns>結合結果</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected string GetExamplesWith(params string[] paths) =>
            IO.Combine(Examples, IO.Combine(paths));

        /* ----------------------------------------------------------------- */
        ///
        /// GetResultsWith
        ///
        /// <summary>
        /// 指定されたパス一覧の先頭に Results ディレクトリのパスを結合
        /// した結果を取得します。
        /// </summary>
        ///
        /// <param name="paths">結合パス一覧</param>
        ///
        /// <returns>結合結果</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected string GetResultsWith(params string[] paths) =>
            IO.Combine(Results, IO.Combine(paths));

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Delete
        ///
        /// <summary>
        /// 指定されたフォルダ内に存在する全てのファイルおよびフォルダを
        /// 削除します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Delete(string directory)
        {
            foreach (string f in IO.GetFiles(directory)) IO.Delete(f);
            foreach (string d in IO.GetDirectories(directory))
            {
                Delete(d);
                IO.Delete(d);
            }
        }

        #endregion
    }
}
