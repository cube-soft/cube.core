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
namespace Cube.Tests.Forms;

using Cube.Forms.Behaviors;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// MockDialogBehavior
///
/// <summary>
/// Provides functionality to process a DialogMessage object for testing.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class MockDialogBehavior : MessageBehavior<DialogMessage>
{
    /* --------------------------------------------------------------------- */
    ///
    /// MockDialogBehavior
    ///
    /// <summary>
    /// Initializes a new instance of the MockDialogBehavior class with
    /// the specified aggregator object.
    /// </summary>
    ///
    /// <param name="aggregator">Message aggregator.</param>
    ///
    /* --------------------------------------------------------------------- */
    public MockDialogBehavior(IAggregator aggregator) : base(aggregator, e =>
    {
        e.Value  = e.Buttons == DialogButtons.YesNo ? DialogStatus.Yes : DialogStatus.Ok;
        e.Cancel = false;

        if (e.Icon == DialogIcon.Error) Assert.Fail(e.Text);
        else Logger.Debug($"{e.Icon} {e.Text}");
    }) { }
}
