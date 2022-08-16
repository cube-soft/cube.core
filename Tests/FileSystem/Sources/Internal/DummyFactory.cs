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

/* ------------------------------------------------------------------------- */
///
/// DummyFactory
///
/// <summary>
/// Provides functionality to create dummy data.
/// </summary>
///
/* ------------------------------------------------------------------------- */
static class DummyFactory
{
    /* --------------------------------------------------------------------- */
    ///
    /// Create
    ///
    /// <summary>
    /// Creates a new instance of the Dummy class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static Dummy Create() => new()
    {
        Number   = 123,
        Name     = "山田太郎",
        Sex      = Sex.Male,
        Age      = 15,
        Creation = new DateTime(2014, 12, 31, 23, 25, 30, DateTimeKind.Utc).ToLocalTime(),
        Contact  = new() { Type = "Phone", Value = "080-9876-5432" },
        Reserved = true,
        Secret   = "dummy data",
        Others   = new List<Address>
        {
            new() { Type = "PC",     Value = "pc@example.com" },
            new() { Type = "Mobile", Value = "mobile@example.com" }
        },
        Messages = new[]
        {
            "1st message",
            "2nd message",
            "3rd message",
        },
    };
}