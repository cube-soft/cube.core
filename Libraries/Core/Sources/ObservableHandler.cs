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

using System.Collections.Generic;

/* ------------------------------------------------------------------------- */
///
/// ObservableHandler
///
/// <summary>
/// Represents the delegate to invoke an action when a PropertyChanged
/// event occurs.
/// </summary>
///
/// <param name="name">Property name.</param>
///
/* ------------------------------------------------------------------------- */
public delegate void ObservableHandler(string name);

/* ------------------------------------------------------------------------- */
///
/// ObservableHandlerDictionary
///
/// <summary>
/// Represents the object to map PropertyChanged events to actions.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class ObservableHandlerDictionary : Dictionary<string, ObservableHandler> { }
