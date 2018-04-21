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
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cube.FileSystem.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// OperatorTest
    ///
    /// <summary>
    /// Cube.FileSystem.Operator のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class OperatorTest : FileHelper
    {
        #region Tests

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
        public void GetFiles(Operator io)
        {
            Assert.That(io.GetFiles(Examples).Count(), Is.EqualTo(2));
            Assert.That(io.GetFiles(Example("Sample.txt")), Is.Null);

            var empty = Result("Empty");
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
        public void GetDirectories(Operator io)
        {
            Assert.That(io.GetDirectories(Examples).Count(), Is.EqualTo(1));
            Assert.That(io.GetDirectories(Example("Sample.txt")), Is.Null);

            var empty = Result("Empty");
            io.CreateDirectory(empty);
            var result = io.GetDirectories(empty);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Get_Properties
        ///
        /// <summary>
        /// Get で取得できるオブジェクトのプロパティを確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Get_Properties(Operator io)
        {
            var file = io.Get(Example("Sample.txt"));
            var dir  = io.Get(file.DirectoryName);
            var cmp  = new DateTime(2017, 6, 5);

            Assert.That(file.FullName,             Is.EqualTo(Example("Sample.txt")));
            Assert.That(file.Name,                 Is.EqualTo("Sample.txt"));
            Assert.That(file.NameWithoutExtension, Is.EqualTo("Sample"));
            Assert.That(file.Extension,            Is.EqualTo(".txt"));
            Assert.That(file.DirectoryName,        Is.EqualTo(Examples));
            Assert.That(file.CreationTime,         Is.GreaterThan(cmp));
            Assert.That(file.LastWriteTime,        Is.GreaterThan(cmp));
            Assert.That(file.LastAccessTime,       Is.GreaterThan(cmp));

            Assert.That(dir.FullName,              Is.EqualTo(Examples));
            Assert.That(dir.Name,                  Is.EqualTo("Examples"));
            Assert.That(dir.NameWithoutExtension,  Is.EqualTo("Examples"));
            Assert.That(dir.Extension,             Is.Empty);
            Assert.That(dir.DirectoryName,         Is.Not.Null.And.Not.Empty);

            Assert.DoesNotThrow(() =>
            {
                file.Refresh();
                dir.Refresh();
            });
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
        public void Get_Throws(Operator io) =>
            Assert.That(() => io.Get(string.Empty), Throws.TypeOf<ArgumentException>());

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
        public void Create(Operator io)
        {
            var dest = Result(@"Directory\Create.txt");
            using (var stream = io.Create(dest)) stream.WriteByte((byte)'A');
            Assert.That(io.Get(dest).Length, Is.EqualTo(1));
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
        public void OpenWrite(Operator io)
        {
            var src  = io.Get(Example("Sample.txt"));
            var dest = io.Get(Result("OpenWrite.txt"));

            io.Copy(src.FullName, dest.FullName, true);
            io.SetAttributes(dest.FullName, src.Attributes);
            io.SetCreationTime(dest.FullName, src.CreationTime);
            io.SetLastWriteTime(dest.FullName, DateTime.Now);
            io.SetLastAccessTime(dest.FullName, DateTime.Now);

            var count = dest.Length;
            using (var stream = io.OpenWrite(dest.FullName)) stream.WriteByte((byte)'A');
            Assert.That(dest.Length, Is.EqualTo(count));

            var newfile = Result(@"Directory\OpenWrite.txt");
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
        public void Delete(Operator io)
        {
            var dest = Result("Delete.txt");

            io.Copy(Example("Sample.txt"), dest);
            io.SetAttributes(dest, System.IO.FileAttributes.ReadOnly);
            io.Delete(dest);

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
        public void DeleteRecursive(Operator io)
        {
            var name = "SampleDirectory";
            var dest = Result(name);

            io.Copy(Example(name), dest, true);
            foreach (var f in io.GetFiles(dest)) io.SetAttributes(f, System.IO.FileAttributes.ReadOnly);
            io.Delete(dest);

            Assert.That(io.Exists(dest), Is.False);
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
        public void Delete_NotFound(Operator io) =>
            Assert.DoesNotThrow(() => io.Delete(Result("NotFound")));

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
        public void Move(Operator io)
        {
            io.Failed += (s, e) => Assert.Fail($"{e.Name}: {e.Exception}");

            var name = "SampleDirectory";
            var src  = io.Get(io.Combine(Results, name));
            var dest = io.Get(io.Combine(Results, $"{name}-Move"));

            io.Copy(Example(name), src.FullName, false);
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
        public void Move_Failed(Operator io)
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
            var dest = io.Combine(Results, "Moved.txt");
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
        public void Move_Throws(Operator io) => Assert.That(
            () => io.Move(
                io.Combine(Results, "FileNotFound.txt"),
                io.Combine(Results, "Moved.txt")
            ),
            Throws.TypeOf<System.IO.FileNotFoundException>()
        );

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
        public void Open_Failed(Operator io)
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

            var src    = io.Combine(Results, "FileNotFound.txt");
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
        public void Open_Throws(Operator io) => Assert.That(
            () => io.OpenRead(io.Combine(Results, "FileNotFound.txt")),
            Throws.TypeOf<System.IO.FileNotFoundException>()
        );

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
                yield return new TestCaseData(new Operator());
                yield return new TestCaseData(new AfsOperator());
            }
        }

        #endregion
    }
}
