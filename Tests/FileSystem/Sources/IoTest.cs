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
namespace Cube.FileSystem.Tests;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Cube.Tests;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// IoTest
///
/// <summary>
/// Provides a test fixture for the Io class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class IoTest : FileFixture
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Get
    ///
    /// <summary>
    /// Tests the Get method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCaseSource(nameof(TestCases))]
    public void Get(int id, IoController controller)
    {
        Io.Configure(controller);

        var src = GetSource("Sample.txt");
        Assert.That(Io.Exists(src), Is.True);
        Assert.That(Io.IsDirectory(src), Is.False);

        var dest = new Entity(GetSource("Sample.txt"));
        Assert.That(dest, Is.Not.Null, $"{id}");

        var cmp = new DateTime(2017, 6, 5);
        Assert.That(dest.RawName,        Is.EqualTo(src));
        Assert.That(dest.FullName,       Is.EqualTo(dest.RawName));
        Assert.That(dest.Name,           Is.EqualTo("Sample.txt"));
        Assert.That(dest.BaseName,       Is.EqualTo("Sample"));
        Assert.That(dest.Extension,      Is.EqualTo(".txt"));
        Assert.That(dest.DirectoryName,  Is.EqualTo(Examples));
        Assert.That(dest.CreationTime,   Is.GreaterThan(cmp));
        Assert.That(dest.LastWriteTime,  Is.GreaterThan(cmp));
        Assert.That(dest.LastAccessTime, Is.GreaterThan(cmp));

        var dir = new Entity(dest.DirectoryName);
        Assert.That(dir.FullName,        Is.EqualTo(Examples));
        Assert.That(dir.Name,            Is.EqualTo("Examples"));
        Assert.That(dir.BaseName,        Is.EqualTo("Examples"));
        Assert.That(dir.Extension,       Is.Empty);
        Assert.That(dir.DirectoryName,   Is.Not.Null.And.Not.Empty);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Serialize
    ///
    /// <summary>
    /// Tests to serialize and deserialize an Entity object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCaseSource(nameof(TestCases))]
    public void Serialize(int id, IoController controller)
    {
        Io.Configure(controller);

        var src = new Entity(GetSource("Sample.txt"));
        var bin = Get($"{nameof(Serialize)}-{id}.bin");
        var fmt = new BinaryFormatter();

        using (var fs = Io.Create(bin)) fmt.Serialize(fs, src);
        using (var fs = Io.Open(bin))
        {
            var dest = (Entity)fmt.Deserialize(fs);
            var cmp  = new DateTime(2017, 6, 5);

            Assert.That(dest.RawName,        Is.EqualTo(GetSource("Sample.txt")));
            Assert.That(dest.FullName,       Is.EqualTo(dest.RawName));
            Assert.That(dest.Name,           Is.EqualTo("Sample.txt"));
            Assert.That(dest.BaseName,       Is.EqualTo("Sample"));
            Assert.That(dest.Extension,      Is.EqualTo(".txt"));
            Assert.That(dest.DirectoryName,  Is.EqualTo(Examples));
            Assert.That(dest.CreationTime,   Is.GreaterThan(cmp));
            Assert.That(dest.LastWriteTime,  Is.GreaterThan(cmp));
            Assert.That(dest.LastAccessTime, Is.GreaterThan(cmp));
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Get_Throws
    ///
    /// <summary>
    /// Confirms the exception when the Get method is failed.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCaseSource(nameof(TestCases))]
    public void Get_Throws(int id, IoController controller)
    {
        Io.Configure(controller);
        Assert.That(() => new Entity(string.Empty), Throws.ArgumentException, $"{id}");
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetFiles
    ///
    /// <summary>
    /// Tests the GetFiles method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCaseSource(nameof(TestCases))]
    public void GetFiles(int id, IoController controller)
    {
        Io.Configure(controller);
        Assert.That(Io.GetFiles(Examples).Count(), Is.EqualTo(7), $"{id}");
        Assert.That(Io.GetFiles(GetSource("Sample.txt")).Count(), Is.EqualTo(0));

        var empty = Get("Empty");
        Io.CreateDirectory(empty);
        var dest = Io.GetFiles(empty).ToArray();
        Assert.That(dest, Is.Not.Null);
        Assert.That(dest.Count, Is.EqualTo(0));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetDirectories
    ///
    /// <summary>
    /// Tests the GetDirectories method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCaseSource(nameof(TestCases))]
    public void GetDirectories(int id, IoController controller)
    {
        Io.Configure(controller);
        Assert.That(Io.GetDirectories(Examples).Count(), Is.EqualTo(1), $"{id}");
        Assert.That(Io.GetDirectories(GetSource("Sample.txt")).Count(), Is.EqualTo(0));

        var empty = Get("Empty");
        Io.CreateDirectory(empty);
        var dest = Io.GetDirectories(empty).ToArray();
        Assert.That(dest, Is.Not.Null);
        Assert.That(dest.Count, Is.EqualTo(0));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Create
    ///
    /// <summary>
    /// Tests the Create method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCaseSource(nameof(TestCases))]
    public void Create(int id, IoController controller)
    {
        Io.Configure(controller);
        var dest = Get("Directory", $"{nameof(Create)}.txt");
        using (var stream = Io.Create(dest)) stream.WriteByte((byte)'A');
        Assert.That(new Entity(dest).Length, Is.EqualTo(1), $"{id}");
    }

    /* --------------------------------------------------------------------- */
    ///
    /// SetTime
    ///
    /// <summary>
    /// Tests the SetCreationTime, SetLastWriteTime, and SetLastAccessTime
    /// methods.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCaseSource(nameof(TestCases))]
    public void SetTime(int id, IoController controller)
    {
        Io.Configure(controller);

        var dest = Get($"{nameof(SetTime)}-{id}");
        Io.Copy(GetSource("SampleDirectory"), dest, true);

        var ts = new DateTime(2021, 6, 29, 12, 0, 0, DateTimeKind.Local);
        foreach (var f in Io.GetFiles(dest))
        {
            Io.SetCreationTime(f, ts);
            Io.SetLastWriteTime(f, ts);
            Io.SetLastAccessTime(f, ts);
            Io.SetTime(f, ts, ts, ts);
        }

        Io.SetCreationTime(dest, ts);
        Io.SetLastWriteTime(dest, ts);
        Io.SetLastAccessTime(dest, ts);
        Io.SetTime(dest, ts, ts, ts);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Delete
    ///
    /// <summary>
    /// Tests the Delete method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
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

    /* --------------------------------------------------------------------- */
    ///
    /// DeleteRecursive
    ///
    /// <summary>
    /// Tests the Delete method recursively.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCaseSource(nameof(TestCases))]
    public void Delete_Recursive(int id, IoController controller)
    {
        Io.Configure(controller);

        var dest = Get($"{nameof(Delete_Recursive)}_{id}");
        Io.Copy(GetSource("SampleDirectory"), dest, true);
        foreach (var f in Io.GetFiles(dest)) Io.SetAttributes(f, System.IO.FileAttributes.ReadOnly);
        Io.SetAttributes(dest, System.IO.FileAttributes.ReadOnly);
        Io.Delete(dest);

        Assert.That(Io.Exists(dest), Is.False, $"{id}");
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Delete_NotFound
    ///
    /// <summary>
    /// Confirms the behavior when deleting a non-existent file.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCaseSource(nameof(TestCases))]
    public void Delete_NotFound(int id, IoController controller)
    {
        Io.Configure(controller);
        var src = Get($"{nameof(Delete_NotFound)}-{id}");
        Io.Delete(src); // Assert.DoesNotThrow
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Move
    ///
    /// <summary>
    /// Tests the Move method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCaseSource(nameof(TestCases))]
    public void Move(int id, IoController controller)
    {
        Io.Configure(controller);

        var e0   = new Entity(GetSource("Sample.txt"));
        var src  = Io.Combine(Results, e0.Name);
        var dest = Io.Combine(Results, $"{nameof(Move)}-{id}.txt");

        // Step 0
        Io.Delete(src);
        Io.Delete(dest);
        Assert.That(Io.Exists(src), Is.False);
        Assert.That(Io.Exists(dest), Is.False);

        // Step 1
        Io.Copy(e0.FullName, src, false);
        Io.SetAttributes(src, System.IO.FileAttributes.ReadOnly);
        var e1 = new Entity(src);
        Assert.That(e1.Exists, Is.True);
        Assert.That(e1.CreationTime, Is.EqualTo(e0.CreationTime));
        Assert.That(e1.LastWriteTime, Is.EqualTo(e0.LastWriteTime));
        Assert.That(e1.LastAccessTime, Is.Not.EqualTo(e0.LastAccessTime));

        // Step 2
        Io.Copy(src, dest, false);
        Io.SetAttributes(dest, System.IO.FileAttributes.ReadOnly);
        var e2 = new Entity(dest);
        Assert.That(e2.Exists, Is.True);
        Assert.That(e2.CreationTime, Is.EqualTo(e0.CreationTime));
        Assert.That(e2.LastWriteTime, Is.EqualTo(e0.LastWriteTime));
        Assert.That(e2.LastAccessTime, Is.Not.EqualTo(e0.LastAccessTime));

        // Step 3
        Io.Move(src, dest, true);
        Assert.That(Io.Exists(src), Is.False);
        Assert.That(Io.Exists(dest), Is.True);
        var e3 = new Entity(dest);
        Assert.That(e3.Exists, Is.True);
        Assert.That(e3.CreationTime, Is.EqualTo(e0.CreationTime));
        Assert.That(e3.LastWriteTime, Is.EqualTo(e0.LastWriteTime));
        Assert.That(e3.LastAccessTime, Is.Not.EqualTo(e0.LastAccessTime));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Move_Recursive
    ///
    /// <summary>
    /// Tests the Move method recursively.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCaseSource(nameof(TestCases))]
    public void Move_Recursive(int id, IoController controller)
    {
        Io.Configure(controller);

        var src  = Io.Combine(Results, $"{nameof(Move_Recursive)}_{id}_Source");
        var dest = Io.Combine(Results, $"{nameof(Move_Recursive)}_{id}");

        Io.Copy(GetSource("SampleDirectory"), src, false);
        Assert.That(Io.Exists(src),  Is.True);

        Io.Copy(src, dest, false);
        Io.Move(src, dest, true);
        Assert.That(Io.Exists(src),  Is.False);
        Assert.That(Io.Exists(dest), Is.True);

        Io.Move(dest, src, false);
        Assert.That(Io.Exists(src),  Is.True);
        Assert.That(Io.Exists(dest), Is.False);

        Io.Delete(src);
        Assert.That(Io.Exists(src),  Is.False);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Move_IOException
    ///
    /// <summary>
    /// Confirms the exception when moving files.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCaseSource(nameof(TestCases))]
    public void Move_IOException(int id, IoController controller)
    {
        Io.Configure(controller);

        var src  = GetSource("Sample.txt");
        var dest = Get($"{nameof(Move_IOException)}-{id}.txt");

        Io.Copy(src, dest, true);
        Assert.That(Io.Exists(dest), Is.True);

        using (Io.Open(src))
        {
            Assert.That(() => Io.Move(src, dest, true), Throws.TypeOf<System.IO.IOException>());
        }
        Assert.That(Io.Exists(src),  Is.True, src);
        Assert.That(Io.Exists(dest), Is.True, dest);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Move_FileNotFoundException
    ///
    /// <summary>
    /// Tests when a FileNotFoundException exception occurs.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCaseSource(nameof(TestCases))]
    public void Move_FileNotFoundException(int id, IoController controller)
    {
        Io.Configure(controller);

        var src  = GetSource("FileNotFound.txt");
        var dest = Get($"{nameof(Move_FileNotFoundException)}-{id}.txt");
        Assert.That(() => Io.Move(src, dest, true), Throws.TypeOf<System.IO.FileNotFoundException>());
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Copy_NameTooLong
    ///
    /// <summary>
    /// Tests when too long file name is specified.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCaseSource(nameof(TestCases))]
    public void Copy_NameTooLong(int id, IoController controller)
    {
        Io.Configure(controller);

        var ss = new StringBuilder();
        ss.Append($"{nameof(Copy_NameTooLong)}-{id}-");
        for (var i = 0; i < 250; ++i) ss.Append(0);
        ss.Append(".txt");

        var dest = Get(ss.ToString());
        var src  = GetSource("Sample.txt");
        Assert.That(() => Io.Copy(src, dest, true), Throws.TypeOf<PathTooLongException>());
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Copy_PathTooLong
    ///
    /// <summary>
    /// Tests when too long file name is specified.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCaseSource(nameof(TestCases))]
    public void Copy_PathTooLong(int id, IoController controller)
    {
        Io.Configure(controller);

        try
        {
            var root = $"{nameof(Copy_PathTooLong)}-{id}";
            var ss = new StringBuilder();
            ss.Append($@"{root}\");
            for (var i = 0; i < 9; ++i) ss.Append($@"{i}000000000000000000000000000000\");
            ss.Append("Sample.txt");

            var dest = Get(ss.ToString());
            var src  = GetSource("Sample.txt");
            Io.Copy(src, dest, true);
            Io.Delete(Get(root));
            Assert.That(id, Is.EqualTo(2)); // AlphaFS
        }
        catch (Exception e) {
            Assert.That(id, Is.EqualTo(1)); // System.IO
            Assert.That(e,  Is.TypeOf<DirectoryNotFoundException>());
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Open_FileNotFoundException
    ///
    /// <summary>
    /// Confirms the exception when opening a file.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCaseSource(nameof(TestCases))]
    public void Open_FileNotFoundException(int id, IoController controller)
    {
        Io.Configure(controller);
        var src = Get($"{nameof(Open_FileNotFoundException)}-{id}.txt");
        Assert.That(() => Io.Open(src), Throws.TypeOf<System.IO.FileNotFoundException>());
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Exists_NullOrEmpty
    ///
    /// <summary>
    /// Tests the Exists method with null or empty values.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
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

    /* --------------------------------------------------------------------- */
    ///
    /// TestCases
    ///
    /// <summary>
    /// Gets test cases.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static IEnumerable<TestCaseData> TestCases
    {
        get
        {
            var n = 0;
            yield return new TestCaseData(++n, new IoController());
            yield return new TestCaseData(++n, new AlphaFS.IoController());
        }
    }

    #endregion
}
