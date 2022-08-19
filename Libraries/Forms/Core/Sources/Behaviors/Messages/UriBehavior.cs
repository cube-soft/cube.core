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
namespace Cube.Forms.Behaviors;

using System;
using System.Diagnostics;

/* ------------------------------------------------------------------------- */
///
/// UriBehavior
///
/// <summary>
/// Represents the behavior when an Uri message is received.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class UriBehavior : MessageBehavior<UriMessage>
{
    /* --------------------------------------------------------------------- */
    ///
    /// UriBehavior
    ///
    /// <summary>
    /// Initializes a new instance of the UriBehavior class
    /// with the specified arguments.
    /// </summary>
    ///
    /// <param name="aggregator">Message aggregator.</param>
    ///
    /* --------------------------------------------------------------------- */
    public UriBehavior(IAggregator aggregator) : base(aggregator, e =>
    {
        try
        {
            var proc = Process.Start(e.ToString());
            e.Cancel = proc is null;
        }
        catch (Exception err)
        {
            typeof(UriBehavior).LogDebug(err.Message);
            e.Cancel = true;
        }
    }) { }
}
