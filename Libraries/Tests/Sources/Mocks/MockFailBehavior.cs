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
namespace Cube.Tests.Mocks;

using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// MockFailBehavior
///
/// <summary>
/// Provides functionality to notify failure when receiving the provided
/// type object.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class MockFailBehavior<T> : MockMessageBehavior<T>
{
    /* --------------------------------------------------------------------- */
    ///
    /// MockFailBehavior
    ///
    /// <summary>
    /// Initializes a new instance of the MockFailBehavior class
    /// with the specified aggregator object.
    /// </summary>
    ///
    /// <param name="aggregator">Message aggregator.</param>
    ///
    /* --------------------------------------------------------------------- */
    public MockFailBehavior(IAggregator aggregator) : base(aggregator, e => Assert.Fail(e.ToString())) { }
}
