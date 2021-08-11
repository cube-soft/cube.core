Cube.Core
====

[![Core](https://badgen.net/nuget/v/cube.core?label=core)](https://www.nuget.org/packages/cube.core/)
[![FileSystem](https://badgen.net/nuget/v/cube.filesystem?label=filesystem)](https://www.nuget.org/packages/cube.filesystem/)
[![AlphaFS](https://badgen.net/nuget/v/cube.filesystem.alphafs?label=alphafs)](https://www.nuget.org/packages/cube.filesystem.alphafs/)
[![AppVeyor](https://badgen.net/appveyor/ci/clown/cube-core)](https://ci.appveyor.com/project/clown/cube-core)
[![Codecov](https://badgen.net/codecov/c/github/cube-soft/cube.core)](https://codecov.io/gh/cube-soft/cube.core)

The Cube.Core package provides support the MVVM pattern in WPF or WinForms applications,
and the repository has some more utility packages. These packages are basically available for .NET Framework 3.5, 4.5, .NET Standard 2.0, or later. Note that Cube.FileSystem.AlphaFS package supports only .NET Framework.

## Installation

You can install the library through the NuGet package.
Add dependencies, as you need, in your project file using the following syntax:

    <PackageReference Include="Cube.Core" Version="4.0.1" />
    <PackageReference Include="Cube.FileSystem" Version="4.0.1" />
    <PackageReference Include="Cube.FileSystem.AlphaFS" Version="4.0.1" />

Or select it from the NuGet packages UI on Visual Studio.

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