Cube.FileSystem.AlphaFS
====

[![Package](https://img.shields.io/nuget/v/cube.filesystem.alphafs)](https://www.nuget.org/packages/cube.filesystem.alphafs/)
[![AppVeyor](https://img.shields.io/appveyor/build/clown/cube-core)](https://ci.appveyor.com/project/clown/cube-core)
[![Codecov](https://img.shields.io/codecov/c/github/cube-soft/cube.core)](https://codecov.io/gh/cube-soft/cube.core)

The Cube.FileSystem.AlphaFS package uses the [AlphaFS](https://alphafs.alphaleonis.com/) library to provide I/O operations such as move, copy, and delete. These functions are available via the Cube.FileSystem.Io class. When you use the AlphaFS for I/O operations, describe the following statement at first.

```cs
// using Cube.FileSystem;
Io.Configure(new Cube.FileSystem.AlphaFS.IoController());
```

All I/O operations of the Cube.* projects also are performed via the Cube.FileSystem.Io class and therefore affected by its configuration.

The package is available for .NET Framework 3.5, 4.6, or later. 

## Installation

You can install the library through the NuGet package. Add dependencies in your project file or select it from the NuGet packages UI on Visual Studio. For more information, see the [NuGet](https://www.nuget.org/packages/cube.filesystem.alphafs/) page.

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