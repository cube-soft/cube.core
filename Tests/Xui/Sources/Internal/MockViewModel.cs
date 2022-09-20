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
namespace Cube.Xui.Tests;

using System.Threading;
using System.Windows.Input;

/* ------------------------------------------------------------------------- */
///
/// MockViewModel
///
/// <summary>
/// Represents the ViewModel of the MockWindow class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
class MockViewModel : PresentableBase<Person>
{
    /* --------------------------------------------------------------------- */
    ///
    /// MockViewModel
    ///
    /// <summary>
    /// Initializes a new instance of the MockViewModel class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public MockViewModel() : base(new Person(), new Aggregator(), new SynchronizationContext()) { }

    /* --------------------------------------------------------------------- */
    ///
    /// Value
    ///
    /// <summary>
    /// Gets the bindable value.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public Person Value => Facade;

    /* --------------------------------------------------------------------- */
    ///
    /// Detect
    ///
    /// <summary>
    /// Gets the Detect command.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public ICommand Detect { get; } = new DelegateCommand(
        () => Logger.Debug("Event is fired"),
        () => true
    );

    /* --------------------------------------------------------------------- */
    ///
    /// Test
    ///
    /// <summary>
    /// Sends the specified message for testing.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public void Test<T>(T msg) => Send(msg);
}
