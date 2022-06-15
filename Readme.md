Cube.Core
====

[![Core](https://badgen.net/nuget/v/cube.core?label=core)](https://www.nuget.org/packages/cube.core/)
[![Forms](https://badgen.net/nuget/v/cube.forms?label=forms)](https://www.nuget.org/packages/cube.forms/)
[![Xui](https://badgen.net/nuget/v/cube.xui?label=xui)](https://www.nuget.org/packages/cube.xui/)
[![AlphaFS](https://badgen.net/nuget/v/cube.filesystem.alphafs?label=alphafs)](https://www.nuget.org/packages/cube.filesystem.alphafs/)
[![AppVeyor](https://badgen.net/appveyor/ci/clown/cube-core)](https://ci.appveyor.com/project/clown/cube-core)
[![Codecov](https://badgen.net/codecov/c/github/cube-soft/cube.core)](https://codecov.io/gh/cube-soft/cube.core)

The project provides support the MVVM pattern in WinForms or WPF applications. This project can be roughly divided into the three packages as follows:

* [Cube.Core](https://www.nuget.org/packages/cube.core/)
* [Cube.Forms](https://www.nuget.org/packages/cube.forms/) (WinForms)
* [Cube.Xui](https://www.nuget.org/packages/cube.xui/) (WPF)

The Cube.Core package provides some additional utility functions. One of them is Cube.FileSystem.Io class, which provides functionality to switch between the use of .NET standard methods or other third party libraries for I/O operations such as move, copy, delete, etc. The [Cube.FileSystem.AlphaFS](https://www.nuget.org/packages/cube.filesystem.alphafs/) package is the implementations of those I/O operations by using the [AlphaFS](https://alphafs.alphaleonis.com/) library.

These packages are basically available for .NET Framework 3.5, 4.7, .NET Standard 2.0, .NET 6, or later. Note that the Cube.FileSystem.AlphaFS package supports only .NET Framework.

The Cube.Core package depends on the [NLog](https://www.nuget.org/packages/NLog/) package. Other packages may depend on more packages. For more information, see the readme of each package.

## Installation

You can install the library through the NuGet package. Add dependencies in your project file or select it from the NuGet packages UI on Visual Studio. For more information, see the [NuGet](https://www.nuget.org/packages/cube.core/) page.

## Contributing

1. Fork [Cube.Core](https://github.com/cube-soft/cube.core/fork) repository.
2. Create a feature branch from the [master](https://github.com/cube-soft/cube.core/tree/master) branch (git checkout -b my-new-feature origin/master).
3. Commit your changes.
4. Rebase your local changes against the master branch.
5. Run the dotnet test command or the Visual Studio (NUnit 3 test adapter) and confirm that it passes.
6. Create new Pull Request.

## License

Copyright © 2010 [CubeSoft, Inc.](https://www.cube-soft.jp/)
These packages are licensed under the [Apache 2.0](https://github.com/cube-soft/cube.core/blob/master/License.txt).