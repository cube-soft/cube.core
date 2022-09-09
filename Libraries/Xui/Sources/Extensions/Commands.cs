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
namespace Cube.Xui.Commands.Extensions;

using System.Windows.Input;

/* ------------------------------------------------------------------------- */
///
/// Methods
///
/// <summary>
/// Provides extended methods of the ICommand interface.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class Methods
{
    /* --------------------------------------------------------------------- */
    ///
    /// Execute
    ///
    /// <summary>
    /// Execute the command without any parameters.
    /// </summary>
    ///
    /// <param name="src">Command object.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Execute(this ICommand src) => src?.Execute(null);

    /* --------------------------------------------------------------------- */
    ///
    /// CanExecute
    ///
    /// <summary>
    /// Returns a value that determines the specified command
    /// can be executed with any parameters.
    /// </summary>
    ///
    /// <param name="src">Command object.</param>
    ///
    /// <returns>
    /// false for the specified object is null; otherwise, returns the
    /// result of ICommand.CanExecute(null).
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public static bool CanExecute(this ICommand src) => src?.CanExecute(null) ?? false;
}
