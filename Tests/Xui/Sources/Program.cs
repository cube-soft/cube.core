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

using System;
using System.Windows;
using Cube.Xui.Logging.Extensions;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// Program
///
/// <summary>
/// Represetns the main program.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[SetUpFixture]
static class Program
{
    /* --------------------------------------------------------------------- */
    ///
    /// OneTimeSetup
    ///
    /// <summary>
    /// Invokes the setup only once.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [OneTimeSetUp]
    public static void OneTimeSetup()
    {
        Logger.Configure(new Cube.Logging.NLog.LoggerSource());
        BindingLogger.Setup();
        Logger.ObserveTaskException();
        Application.Current.ObserveUiException();
        Logger.Info(typeof(Program).Assembly);
    }
}
