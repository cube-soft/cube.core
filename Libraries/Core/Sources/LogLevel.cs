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
namespace Cube;

/* ------------------------------------------------------------------------- */
///
/// LogLevel
///
/// <summary>
/// Defines logging severity levels.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public enum LogLevel
{
    /// <summary>Note that Cube.Logger does not use the level.</summary>
    Trace = 0,
    /// <summary>Logs that are used for interactive investigation during development.</summary>
    Debug = 1,
    /// <summary>Logs that track the general flow of the application.</summary>
    Information = 2,
    /// <summary>Logs that highlight an abnormal or unexpected event in the application flow, but do not otherwise cause the application execution to stop.</summary>
    Warning = 3,
    /// <summary>Logs that highlight when the current flow of execution is stopped due to a failure.</summary>
    Error = 4,
}
