﻿/* ------------------------------------------------------------------------- */
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
namespace Cube.Tests;

using System;
using System.IO;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// StreamProxyTest
///
/// <summary>
/// Provides functionality to test the StreamProxy class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class StreamProxyTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Create
    ///
    /// <summary>
    /// Tests the constructor and confirms properties.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Create()
    {
        var src  = new MemoryStream(new byte[] { 0, 1, 2, 3, 4, 5 });
        var dest = new StreamProxy(src);

        Assert.That(dest.CanRead,  Is.EqualTo(src.CanRead).And.True);
        Assert.That(dest.CanSeek,  Is.EqualTo(src.CanSeek).And.True);
        Assert.That(dest.CanWrite, Is.EqualTo(src.CanWrite).And.True);
        Assert.That(dest.Length,   Is.EqualTo(src.Length).And.EqualTo(6));
        Assert.That(dest.Position, Is.EqualTo(src.Position).And.EqualTo(0));

        dest.Position = 3;
        Assert.That(dest.Position, Is.EqualTo(src.Position).And.EqualTo(3));

        dest.Dispose();
        dest.Dispose(); // ignore
        Assert.That(dest.CanRead,  Is.False);
        Assert.That(dest.CanSeek,  Is.False);
        Assert.That(dest.CanWrite, Is.False);
        Assert.That(dest.Length,   Is.EqualTo(0));
        Assert.That(dest.Position, Is.EqualTo(0));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Create_Throws
    ///
    /// <summary>
    /// Tests the constructor with null arguments.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Create_Throws()
    {
        Assert.That(() => { using (new StreamProxy(null)) { } }, Throws.ArgumentNullException);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Read
    ///
    /// <summary>
    /// Tests to read bytes from the StreamProxy.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Read()
    {
        var src  = new MemoryStream(new byte[] { 0, 1, 2, 3, 4, 5 });
        var dest = new StreamProxy(src);

        var bytes = new byte[3];
        Assert.That(dest.Read(bytes, 0, 3), Is.EqualTo(3));

        dest.Dispose();
        Assert.That(() =>dest.Read(bytes, 0, 3), Throws.TypeOf<NullReferenceException>());
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Write
    ///
    /// <summary>
    /// Tests to write bytes to the StreamProxy.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Write()
    {
        var bytes = new byte[] { 0, 1, 2, 3, 4, 5, };
        var src   = new MemoryStream(bytes);
        var dest  = new StreamProxy(src);

        Assert.That(dest.Seek(1, SeekOrigin.Begin), Is.EqualTo(1L));
        dest.SetLength(4);
        dest.Write(new byte[] { 9, 9, 9 }, 0, 3);
        dest.Flush();
        Assert.That(bytes[0], Is.EqualTo((byte)0));
        Assert.That(bytes[1], Is.EqualTo((byte)9));
        Assert.That(bytes[2], Is.EqualTo((byte)9));
        Assert.That(bytes[3], Is.EqualTo((byte)9));
        Assert.That(bytes[4], Is.EqualTo((byte)4));

        dest.Dispose();
        Assert.That(() => dest.Write(new byte[] { 1 }, 0, 1), Throws.TypeOf<NullReferenceException>());
    }

    /* --------------------------------------------------------------------- */
    ///
    /// LeaveOpen
    ///
    /// <summary>
    /// Tests to create a StreamProxy instance with leave open mode.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void LeaveOpen()
    {
        var src  = new MemoryStream(new byte[] { 0, 1, 2, 3, 4, 5 });
        var dest = new StreamProxy(src, true);

        dest.Dispose();
        var bytes = new byte[3];
        Assert.That(src.Read(bytes, 0, 3), Is.EqualTo(3));
    }

    #endregion
}
