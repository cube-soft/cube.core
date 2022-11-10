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

using System.Collections.Generic;

/* ------------------------------------------------------------------------- */
///
/// MockOpenFileBehavior
///
/// <summary>
/// Provides functionality to process an OpenFileMessage object for testing.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class MockOpenFileBehavior : MockMessageBehavior<OpenFileMessage>
{
    /* --------------------------------------------------------------------- */
    ///
    /// MockOpenFileBehavior
    ///
    /// <summary>
    /// Initializes a new instance of the MockOpenFileBehavior class with
    /// the specified arguments.
    /// </summary>
    ///
    /// <param name="value">Value of the received message.</param>
    /// <param name="aggregator">Message aggregator.</param>
    ///
    /* --------------------------------------------------------------------- */
    public MockOpenFileBehavior(string value, IAggregator aggregator) :
        this(new[] { value }, aggregator) { }

    /* --------------------------------------------------------------------- */
    ///
    /// MockOpenFileBehavior
    ///
    /// <summary>
    /// Initializes a new instance of the MockOpenFileBehavior class
    /// with the specified arguments.
    /// </summary>
    ///
    /// <param name="value">Value of the received message.</param>
    /// <param name="aggregator">Message aggregator.</param>
    ///
    /* --------------------------------------------------------------------- */
    public MockOpenFileBehavior(IEnumerable<string> value, IAggregator aggregator) : base(aggregator, e =>
    {
        e.Value  = value;
        e.Cancel = false;

        Logger.Debug(e.Text);
        foreach (var s in value) Logger.Debug(s);
        Logger.Debug($"{e.GetFilterIndex()} ({e.GetFilterText()})");
    }) { }
}
