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
using Cube.Tests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cube.FileSystem.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// IoTest
    ///
    /// <summary>
    /// Cube.FileSystem.Operator のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class IoTest : FileFixture
    {
        #region Tests

         /* ----------------------------------------------------------------- */
        ///
        /// Get
        ///
        /// <summary>
        /// Get で取得できるオブジェクトのプロパティを確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Get(int id, IO io)
        {
            var file = io.Get(GetSource("Sample.txt"));
            Assert.That(file, Is.Not.Null, $"{id}");

            var cmp = new DateTime(2017, 6, 5);
            Assert.That(file.FullName,       Is.EqualTo(GetSource("Sample.txt")));
            Assert.That(file.Name,           Is.EqualTo("Sample.txt"));
            Assert.That(file.BaseName,       Is.EqualTo("Sample"));
            Assert.That(file.Extension,      Is.EqualTo(".txt"));
            Assert.That(file.DirectoryName,  Is.EqualTo(Examples));
            Assert.That(file.CreationTime,   Is.GreaterThan(cmp));
            Assert.That(file.LastWriteTime,  Is.GreaterThan(cmp));
            Assert.That(file.LastAccessTime, Is.GreaterThan(cmp));

            var dir = io.Get(file.DirectoryName);
            Assert.That(dir.FullName,        Is.EqualTo(Examples));
            Assert.That(dir.Name,            Is.EqualTo("Examples"));
            Assert.That(dir.BaseName,        Is.EqualTo("Examples"));
            Assert.That(dir.Extension,       Is.Empty);
            Assert.That(dir.DirectoryName,   Is.Not.Null.And.Not.Empty);

            file.Refresh(); // Assert.DoesNotThrow
            dir.Refresh();  // Assert.DoesNotThrow
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Get_Throws
        ///
        /// <summary>
        /// ファイル情報の取得に失敗するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Get_Throws(int id, IO io)
        {
            Assert.That(() => io.Get(string.Empty), Throws.ArgumentException, $"{id}");
        }

       /* ----------------------------------------------------------------- */
        ///
        /// GetFiles
        ///
        /// <summary>
        /// ファイル一覧を取得するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void GetFiles(int id, IO io)
        {
            Assert.That(io.GetFiles(Examples).Count(), Is.EqualTo(2), $"{id}");
            Assert.That(io.GetFiles(GetSource("Sample.txt")).Count(), Is.EqualTo(0));

            var empty = Get("Empty");
            io.CreateDirectory(empty);
            var result = io.GetFiles(empty);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetDirectories
        ///
        /// <summary>
        /// ディレクトリ一覧を取得するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void GetDirectories(int id, IO io)
        {
            Assert.That(io.GetDirectories(Examples).Count(), Is.EqualTo(1), $"{id}");
            Assert.That(io.GetDirectories(GetSource("Sample.txt")).Count(), Is.EqualTo(0));

            var empty = Get("Empty");
            io.CreateDirectory(empty);
            var result = io.GetDirectories(empty);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// 書き込み用ストリームを生成するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Create(int id, IO io)
        {
            var dest = Get("Directory", $"{nameof(Create)}.txt");
            using (var stream = io.Create(dest)) stream.WriteByte((byte)'A');
            Assert.That(io.Get(dest).Length, Is.EqualTo(1), $"{id}");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OpenWrite
        ///
        /// <summary>
        /// 書き込み用ストリームを生成するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void OpenWrite(int id, IO io)
        {
            var src  = io.Get(GetSource("Sample.txt"));
            var dest = io.Get(Get($"{nameof(OpenWrite)}.txt"));

            io.Copy(src.FullName, dest.FullName, true);
            io.SetAttributes(dest.FullName, src.Attributes);
            io.SetCreationTime(dest.FullName, src.CreationTime);
            io.SetLastWriteTime(dest.FullName, DateTime.Now);
            io.SetLastAccessTime(dest.FullName, DateTime.Now);

            var count = dest.Length;
            using (var stream = io.OpenWrite(dest.FullName)) stream.WriteByte((byte)'A');
            Assert.That(dest.Length, Is.EqualTo(count), $"{id}");

            var newfile = Get("Directory", $"{nameof(OpenWrite)}.txt");
            using (var stream = io.OpenWrite(newfile)) stream.WriteByte((byte)'A');
            Assert.That(io.Get(newfile).Length, Is.EqualTo(1));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Delete
        ///
        /// <summary>
        /// ファイルを削除するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Delete(int id, IO io)
        {
            var dest = Get($"{nameof(Delete)}.txt");

            io.Copy(GetSource("Sample.txt"), dest);
            io.SetAttributes(dest, System.IO.FileAttributes.ReadOnly);
            io.Delete(dest);

            Assert.That(io.Exists(dest), Is.False, $"{id}");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TryDelete
        ///
        /// <summary>
        /// ファイルを削除するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void TryDelete(int id, IO io)
        {
            var dest = Get($"{nameof(TryDelete)}-{id}.txt");

            io.Copy(GetSource("Sample.txt"), dest);
            io.SetAttributes(dest, System.IO.FileAttributes.ReadOnly);

            Assert.That(io.TryDelete(dest), Is.True);
            Assert.That(io.Exists(dest), Is.False);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DeleteRecursive
        ///
        /// <summary>
        /// ディレクトリを再帰的に削除するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void DeleteRecursive(int id, IO io)
        {
            var name = "SampleDirectory";
            var dest = Get(name);

            io.Copy(GetSource(name), dest, true);
            foreach (var f in io.GetFiles(dest)) io.SetAttributes(f, System.IO.FileAttributes.ReadOnly);
            io.Delete(dest);

            Assert.That(io.Exists(dest), Is.False, $"{id}");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Delete_NotFound
        ///
        /// <summary>
        /// 存在しないファイルの削除を試みた時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Delete_NotFound(int id, IO io)
        {
            var src = Get($"{nameof(Delete_NotFound)}-{id}");
            io.Delete(src); // Assert.DoesNotThrow
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TryDelete_NotFound
        ///
        /// <summary>
        /// 存在しないファイルの削除を試みた時の挙動を確認します。
        /// </summary>
        ///
        /// <remarks>
        /// 存在しないファイルを削除した時には例外が発生しないため、
        /// TryDelete の戻り値は true となります。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void TryDelete_NotFound(int id, IO io)
        {
            var src = Get($"{nameof(TryDelete_NotFound)}-{id}");
            Assert.That(io.TryDelete(src), Is.True);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TryDelete_AccessDenied
        ///
        /// <summary>
        /// 使用されているファイルを削除しようとした時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void TryDelete_AccessDenied(int id, IO io)
        {
            var src = Get($"{nameof(TryDelete_AccessDenied)}-{id}.txt");
            using (io.Create(src)) Assert.That(io.TryDelete(src), Is.False);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Move
        ///
        /// <summary>
        /// 移動のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Move(int id, IO io)
        {
            io.Failed += (s, e) => Assert.Fail($"{e.Name}: {e.Exception}");

            var name = "SampleDirectory";
            var src  = io.Get(io.Combine(Results, name));
            var dest = io.Get(io.Combine(Results, $"{name}-{nameof(Move)}-{id}"));

            io.Copy(GetSource(name), src.FullName, false);
            src.Refresh();
            Assert.That(src.Exists, Is.True);

            io.Copy(src.FullName, dest.FullName, false);
            io.Move(src.FullName, dest.FullName, true);
            src.Refresh();
            dest.Refresh();
            Assert.That(src.Exists, Is.False);
            Assert.That(dest.Exists, Is.True);

            io.Move(dest.FullName, src.FullName);
            src.Refresh();
            dest.Refresh();
            Assert.That(src.Exists, Is.True);
            Assert.That(dest.Exists, Is.False);

            io.Delete(src.FullName);
            src.Refresh();
            Assert.That(src.Exists, Is.False);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Move_Failed
        ///
        /// <summary>
        /// 移動操作に失敗するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Move_Failed(int id, IO io)
        {
            var failed = false;
            io.Failed += (s, e) =>
            {
                // try twice
                e.Cancel = failed;
                failed   = true;

                Assert.That(e.Name,          Is.EqualTo("Move"));
                Assert.That(e.Paths.Count(), Is.EqualTo(2));
                Assert.That(e.Exception,     Is.TypeOf<System.IO.FileNotFoundException>());
            };

            var src  = io.Combine(Results, "FileNotFound.txt");
            var dest = io.Combine(Results, $"{nameof(Move_Failed)}-{id}.txt");
            io.Move(src, dest);

            Assert.That(failed, Is.True);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Move_Throws
        ///
        /// <summary>
        /// 移動操作に失敗するテストを実行します。
        /// </summary>
        ///
        /// <remarks>
        /// Failed イベントにハンドラを登録していない場合、File.Move を
        /// 実行した時と同様の例外が発生します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Move_Throws(int id, IO io)
        {
            var src  = Get("FileNotFound.txt");
            var dest = Get($"{nameof(Move_Throws)}-{id}.txt");
            Assert.That(() => io.Move(src, dest), Throws.TypeOf<System.IO.FileNotFoundException>());
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Open_Failed
        ///
        /// <summary>
        /// ファイルを開く操作に失敗するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Open_Failed(int id, IO io)
        {
            var failed = false;
            io.Failed += (s, e) =>
            {
                // try twice
                e.Cancel = failed;
                failed   = true;

                Assert.That(e.Name,          Is.EqualTo("OpenRead"));
                Assert.That(e.Paths.Count(), Is.EqualTo(1));
                Assert.That(e.Exception,     Is.TypeOf<System.IO.FileNotFoundException>());
            };

            var src    = Get($"{nameof(Open_Failed)}-{id}.txt");
            var stream = io.OpenRead(src);

            Assert.That(failed, Is.True);
            Assert.That(stream, Is.Null);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Open_Throws
        ///
        /// <summary>
        /// ファイルを開く操作に失敗するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Open_Throws(int id, IO io)
        {
            var src = Get($"{nameof(Open_Throws)}-{id}.txt");
            Assert.That(() => io.OpenRead(src), Throws.TypeOf<System.IO.FileNotFoundException>());
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Exists_NullOrEmpty
        ///
        /// <summary>
        /// Executes the test of Exists method with null or empty values.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Exists_NullOrEmpty(int id, IO io)
        {
            Assert.That(io.Exists(string.Empty), Is.False, $"{id}");
            Assert.That(io.Exists(""),           Is.False, $"{id}");
            Assert.That(io.Exists(null),         Is.False, $"{id}");
        }

        #endregion

        #region TestCases

        /* ----------------------------------------------------------------- */
        ///
        /// TestCases
        ///
        /// <summary>
        /// 各種 Operator のテスト用データを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                var n = 0;
                yield return new TestCaseData(n++, new IO());
                yield return new TestCaseData(n++, new AfsIO());
            }
        }

        #endregion
    }
}
