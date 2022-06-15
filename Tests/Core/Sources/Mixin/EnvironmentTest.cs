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
namespace Cube.Tests.Mixin;

using System;
using Cube.Mixin.Environment;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// EnvironmentTest
///
/// <summary>
/// Tests extended methods of the Environment class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class EnvironmentTest
{
    /* --------------------------------------------------------------------- */
    ///
    /// GetName
    ///
    /// <summary>
    /// Tests the GetName extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(Environment.SpecialFolder.System, @"Windows\System32")]
    public void GetName(Environment.SpecialFolder src, string value)
    {
        var dest = src.GetName().ToLowerInvariant();
        Assert.That(dest, Does.EndWith(value.ToLowerInvariant()));
    }
}
