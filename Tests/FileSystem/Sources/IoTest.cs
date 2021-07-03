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
using Cube.Tests;
using NUnit.Framework;

namespace Cube.FileSystem.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// IoTest
    ///
    /// <summary>
    /// Provides a test fixture for the Io class.
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
        /// Tests the Get method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Get(int id, IoController controller)
        {
            Io.Configure(controller);

            var file = Io.Get(GetSource("Sample.txt"));
            Assert.That(file, Is.Not.Null, $"{id}");

            var cmp = new DateTime(2017, 6, 5);
            Assert.That(file.Source,         Is.EqualTo(GetSource("Sample.txt")));
            Assert.That(file.FullName,       Is.EqualTo(file.Source));
            Assert.That(file.Name,           Is.EqualTo("Sample.txt"));
            Assert.That(file.BaseName,       Is.EqualTo("Sample"));
            Assert.That(file.Extension,      Is.EqualTo(".txt"));
            Assert.That(file.DirectoryName,  Is.EqualTo(Examples));
            Assert.That(file.CreationTime,   Is.GreaterThan(cmp));
            Assert.That(file.LastWriteTime,  Is.GreaterThan(cmp));
            Assert.That(file.LastAccessTime, Is.GreaterThan(cmp));

            var dir = Io.Get(file.DirectoryName);
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
        /// Confirms the exception when the Get method is failed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Get_Throws(int id, IoController controller)
        {
            Io.Configure(controller);
            Assert.That(() => Io.Get(string.Empty), Throws.ArgumentException, $"{id}");
        }

       /* ----------------------------------------------------------------- */
        ///
        /// GetFiles
        ///
        /// <summary>
        /// Tests the GetFiles method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void GetFiles(int id, IoController controller)
        {
            Io.Configure(controller);
            Assert.That(Io.GetFiles(Examples).Count(), Is.EqualTo(7), $"{id}");
            Assert.That(Io.GetFiles(GetSource("Sample.txt")).Count(), Is.EqualTo(0));

            var empty = Get("Empty");
            Io.CreateDirectory(empty);
            var result = Io.GetFiles(empty);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetDirectories
        ///
        /// <summary>
        /// Tests the GetDirectories method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void GetDirectories(int id, IoController controller)
        {
            Io.Configure(controller);
            Assert.That(Io.GetDirectories(Examples).Count(), Is.EqualTo(1), $"{id}");
            Assert.That(Io.GetDirectories(GetSource("Sample.txt")).Count(), Is.EqualTo(0));

            var empty = Get("Empty");
            Io.CreateDirectory(empty);
            var result = Io.GetDirectories(empty);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// Tests the Create method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Create(int id, IoController controller)
        {
            Io.Configure(controller);
            var dest = Get("Directory", $"{nameof(Create)}.txt");
            using (var stream = Io.Create(dest)) stream.WriteByte((byte)'A');
            Assert.That(Io.Get(dest).Length, Is.EqualTo(1), $"{id}");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetTime
        ///
        /// <summary>
        /// Tests the SetCreationTime, SetLastWriteTime, and SetLastAccessTime
        /// methods.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void SetTime(int id, IoController controller)
        {
            Io.Configure(controller);

            var dest = Get(nameof(SetTime));
            Io.Copy(GetSource("SampleDirectory"), dest, true);

            var ts = new DateTime(2021, 6, 29, 12, 0, 0, DateTimeKind.Local);
            foreach (var f in Io.GetFiles(dest))
            {
                Io.SetCreationTime(f, ts);
                Io.SetLastWriteTime(f, ts);
                Io.SetLastAccessTime(f, ts);
            }

            Io.SetCreationTime(dest, ts);
            Io.SetLastWriteTime(dest, ts);
            Io.SetLastAccessTime(dest, ts);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetTime_Throws
        ///
        /// <summary>
        /// Tests the SetCreationTime, SetLastWriteTime, and SetLastAccessTime
        /// methods.
        /// </summary>
        ///
        /// <remarks>
        /// AlphaFS does not thorw.
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void SetTime_Throws()
        {
            Io.Configure(new IoController());

            var dest = Get(nameof(SetTime_Throws));
            Io.Delete(dest);
            Io.Copy(GetSource("SampleDirectory"), dest, false);

            var ts = new DateTime(2021, 6, 29, 13, 0, 0, DateTimeKind.Local);
            foreach (var f in Io.GetFiles(dest))
            {
                Io.SetAttributes(f, System.IO.FileAttributes.ReadOnly);
                Assert.That(() => Io.SetCreationTime(f, ts),
                    Throws.TypeOf<UnauthorizedAccessException>());
                Assert.That(() => Io.SetLastWriteTime(f, ts),
                    Throws.TypeOf<UnauthorizedAccessException>());
                Assert.That(() => Io.SetLastAccessTime(f, ts),
                    Throws.TypeOf<UnauthorizedAccessException>());
            }

            Io.SetAttributes(dest, System.IO.FileAttributes.ReadOnly);
            Io.SetCreationTime(dest, ts);
            Io.SetLastWriteTime(dest, ts);
            Io.SetLastAccessTime(dest, ts);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Delete
        ///
        /// <summary>
        /// Tests the Delete method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Delete(int id, IoController controller)
        {
            Io.Configure(controller);

            var dest = Get($"{nameof(Delete)}.txt");

            Io.Copy(GetSource("Sample.txt"), dest, true);
            Io.SetAttributes(dest, System.IO.FileAttributes.ReadOnly);
            Io.Delete(dest);

            Assert.That(Io.Exists(dest), Is.False, $"{id}");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DeleteRecursive
        ///
        /// <summary>
        /// Tests the Delete method recursively.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Delete_Recursive(int id, IoController controller)
        {
            Io.Configure(controller);

            var name = "SampleDirectory";
            var dest = Get(name);

            Io.Copy(GetSource(name), dest, true);
            foreach (var f in Io.GetFiles(dest)) Io.SetAttributes(f, System.IO.FileAttributes.ReadOnly);
            Io.SetAttributes(dest, System.IO.FileAttributes.ReadOnly);
            Io.Delete(dest);

            Assert.That(Io.Exists(dest), Is.False, $"{id}");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Delete_NotFound
        ///
        /// <summary>
        /// Confirms the behavior when deleting a non-existent file.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Delete_NotFound(int id, IoController controller)
        {
            Io.Configure(controller);
            var src = Get($"{nameof(Delete_NotFound)}-{id}");
            Io.Delete(src); // Assert.DoesNotThrow
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Move
        ///
        /// <summary>
        /// Tests the Move method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Move(int id, IoController controller)
        {
            Io.Configure(controller);

            var name = "SampleDirectory";
            var src  = Io.Get(Io.Combine(Results, name));
            var dest = Io.Get(Io.Combine(Results, $"{name}-{nameof(Move)}-{id}"));

            Io.Copy(GetSource(name), src.FullName, false);
            src.Refresh();
            Assert.That(src.Exists, Is.True);

            Io.Copy(src.FullName, dest.FullName, false);
            Io.Move(src.FullName, dest.FullName, true);
            src.Refresh();
            dest.Refresh();
            Assert.That(src.Exists, Is.False);
            Assert.That(dest.Exists, Is.True);

            Io.Move(dest.FullName, src.FullName, false);
            src.Refresh();
            dest.Refresh();
            Assert.That(src.Exists, Is.True);
            Assert.That(dest.Exists, Is.False);

            Io.Delete(src.FullName);
            src.Refresh();
            Assert.That(src.Exists, Is.False);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Move_Throws
        ///
        /// <summary>
        /// Confirms the exception when moving files.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Move_Throws(int id, IoController controller)
        {
            Io.Configure(controller);
            var src  = Get("FileNotFound.txt");
            var dest = Get($"{nameof(Move_Throws)}-{id}.txt");
            Assert.That(() => Io.Move(src, dest, true), Throws.TypeOf<System.IO.FileNotFoundException>());
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Open_Throws
        ///
        /// <summary>
        /// Confirms the exception when opening a file.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Open_Throws(int id, IoController controller)
        {
            Io.Configure(controller);
            var src = Get($"{nameof(Open_Throws)}-{id}.txt");
            Assert.That(() => Io.Open(src), Throws.TypeOf<System.IO.FileNotFoundException>());
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Exists_NullOrEmpty
        ///
        /// <summary>
        /// Tests the Exists method with null or empty values.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Exists_NullOrEmpty(int id, IoController controller)
        {
            Io.Configure(controller);
            Assert.That(Io.Exists(string.Empty), Is.False, $"{id}");
            Assert.That(Io.Exists(""),           Is.False, $"{id}");
            Assert.That(Io.Exists(null),         Is.False, $"{id}");
        }

        #endregion

        #region TestCases

        /* ----------------------------------------------------------------- */
        ///
        /// TestCases
        ///
        /// <summary>
        /// Gets test cases.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                var n = 0;
                yield return new TestCaseData(n++, new IoController());
            }
        }

        #endregion
    }
}
