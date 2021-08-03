Cube.Core
====

[![NuGet](https://img.shields.io/nuget/v/Cube.Core.svg?label=core)](https://www.nuget.org/packages/Cube.Core/)
[![NuGet](https://img.shields.io/nuget/v/Cube.FileSystem.svg?label=filesystem)](https://www.nuget.org/packages/Cube.FileSystem/)
[![NuGet](https://img.shields.io/nuget/v/Cube.FileSystem.AlphaFS.svg?label=alphafs)](https://www.nuget.org/packages/Cube.FileSystem.AlphaFS/)
[![AppVeyor](https://ci.appveyor.com/api/projects/status/romqhgh1ben6eedn?svg=true)](https://ci.appveyor.com/project/clown/cube-core)
[![Codecov](https://codecov.io/gh/cube-soft/Cube.Core/branch/master/graph/badge.svg)](https://codecov.io/gh/cube-soft/Cube.Core)

The Cube.Core package provides support the MVVM pattern in WPF or WinForms applications,
and the repository has some more utility packages.
These packages are basically available for .NET Framework 3.5, 4.5, .NET Standard 2.0, or later.
Note that Cube.FileSystem.AlphaFS package supports only .NET Framework.

## Installation

You can install the library through the NuGet package.
Add dependencies, as you need, in your project file using the following syntax:

    <ItemGroup>
        <PackageReference Include="Cube.Core" Version="4.0.1" />
        <PackageReference Include="Cube.FileSystem" Version="4.0.1" />
        <PackageReference Include="Cube.FileSystem.AlphaFS" Version="4.0.1" />
    </ItemGroup>

Or select it from the NuGet packages UI on Visual Studio.

## Contributing

1. Fork [Cube.Core](https://github.com/cube-soft/Cube.Core/fork) repository.
2. Create a feature branch from the [master](https://github.com/cube-soft/Cube.Core/tree/master) branch (git checkout -b my-new-feature origin/master).
3. Commit your changes.
4. Rebase your local changes against the master branch.
5. Run the dotnet test command or the Visual Studio (NUnit 3 test adapter) and confirm that it passes.
6. Create new Pull Request.

## License

Copyright © 2010 [CubeSoft, Inc.](https://www.cube-soft.jp/)
These packages are licensed under the [Apache 2.0](https://github.com/cube-soft/Cube.Core/blob/master/License.txt).