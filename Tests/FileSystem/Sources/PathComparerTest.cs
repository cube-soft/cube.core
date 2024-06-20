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

using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// PathComparerTest
///
/// <summary>
/// Represents a list of tests for the PathComparer class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
internal class PathComparerTest
{
    /* --------------------------------------------------------------------- */
    ///
    /// Compare
    ///
    /// <summary>
    /// Tests the Compare method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(@"foo\bar\bas.txt",   @"hello\world.txt",     ExpectedResult = -1)]
    [TestCase(@"foo\bar\bas.txt",   @"foo\bar\bose.txt",    ExpectedResult = -1)]
    [TestCase(@"foo\bar\bas.txt",   @"foo\bar\bas.dat",     ExpectedResult =  1)]
    [TestCase(@"foo\bar\bas.txt",   @"foo\bar\bas-abc.txt", ExpectedResult = -1)]
    [TestCase(@"foo\bar\bas.txt",   @"foo\bar\bas-abc.dat", ExpectedResult = -1)]
    [TestCase(@"foo\bar\bas.txt",   @"foo\bar",             ExpectedResult =  1)]
    [TestCase(@"foo\bar\bas.txt",   @"FOO\BAR\BAS.TXT",     ExpectedResult =  1)]
    [TestCase(@"foo\bar\bas.txt",   @"foo/bar/bas.txt",     ExpectedResult =  0)]
    [TestCase(@"foo\bar\bas.txt",   @"foo\\bar\\bas.txt",   ExpectedResult =  0)]
    [TestCase(@"foo\bar\bas.txt",   @"",                    ExpectedResult =  1)]
    [TestCase(@"num\p2.txt",        @"num\p13.txt",         ExpectedResult = -1)]
    [TestCase(@"num\p5.txt",        @"num\p5-abc.txt",      ExpectedResult = -1)]
    [TestCase(@"num\p3\foo.txt",    @"num\p17\foo.txt",     ExpectedResult = -1)]
    [TestCase(@"\\h0\foo\bar.txt",  @"\\h1\foo\bar.txt",    ExpectedResult = -1)]
    [TestCase(@"\\?\C:\foo\bar",    @"\\?\D:\foo\bar",      ExpectedResult = -1)]
    [TestCase(@"",                  @"",                    ExpectedResult =  0)]
    public int Compare(string src, string cmp) => new PathComparer().Compare(src, cmp) switch
    {
        < 0 => -1,
        > 0 =>  1,
        _   =>  0,
    };
}
