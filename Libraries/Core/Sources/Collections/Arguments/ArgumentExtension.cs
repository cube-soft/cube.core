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
namespace Cube.Collections;

using System;

/* ------------------------------------------------------------------------- */
///
/// ArgumentExtension
///
/// <summary>
/// Provides extended methods of the Argument enumeration.
/// </summary>
///
/* ------------------------------------------------------------------------- */
internal static class ArgumentExtension
{
    /* --------------------------------------------------------------------- */
    ///
    /// Get
    ///
    /// <summary>
    /// Gets the preprocessor from the specified kind.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static IArgumentPreprocessor Get(this Argument src) => src switch
    {
        Argument.Posix   => new PosixArgumentPreprocessor(),
        Argument.Gnu     => new GnuArgumentPreprocessor(),
        Argument.Dos     => new DosArgumentPreprocessor(),
        Argument.Windows => new WindowsArgumentPreprocessor(),
        _                => throw new ArgumentException(),
    };
}
