Cube.Logging.NLog
====

[![Package](https://img.shields.io/nuget/v/cube.logging.nlog)](https://www.nuget.org/packages/cube.logging.nlog/)
[![AppVeyor](https://img.shields.io/appveyor/build/clown/cube-core)](https://ci.appveyor.com/project/clown/cube-core)
[![Codecov](https://img.shields.io/codecov/c/github/cube-soft/cube.core)](https://codecov.io/gh/cube-soft/cube.core)

The Cube.Logging.NLog package uses the [NLog](https://nlog-project.org/) library to implement Cube.ILoggerSource interface. When you use the NLog for logging operations, describe the following statement at first.

```cs
// using Cube;
Logger.Configure(new Cube.Logging.NLog.LoggerSource());
```

The package is available for .NET Framework 3.5, 4.6, .NET Standard 2.0, or later. 

## Installation

You can install the library through the NuGet package. Add dependencies in your project file or select it from the NuGet packages UI on Visual Studio. For more information, see the [NuGet](https://www.nuget.org/packages/cube.logging.nlog/) page.

## Contributing

1. Fork [Cube.Core](https://github.com/cube-soft/cube.core/fork) repository.
2. Create a feature branch from the [master](https://github.com/cube-soft/cube.core/tree/master) branch (git checkout -b my-new-feature origin/master).
3. Commit your changes.
4. Rebase your local changes against the master branch.
5. Run the dotnet test command or the Visual Studio (NUnit 3 test adapter) and confirm that it passes.
6. Create new Pull Request.

## License

Copyright © 2010 [CubeSoft, Inc.](https://www.cube-soft.com/)
These packages are licensed under the [Apache 2.0](https://github.com/cube-soft/cube.core/blob/master/License.txt).