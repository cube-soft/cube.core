Cube.Core
====

[![NuGet](https://img.shields.io/nuget/v/Cube.Core.svg)](https://www.nuget.org/packages/Cube.Core/)
[![AppVeyor](https://ci.appveyor.com/api/projects/status/romqhgh1ben6eedn?svg=true)](https://ci.appveyor.com/project/clown/cube-core)
[![Codecov](https://codecov.io/gh/cube-soft/Cube.Core/branch/master/graph/badge.svg)](https://codecov.io/gh/cube-soft/Cube.Core)

Cube.Core supports the MVVM pattern in WPF or WinForms applications.
The library is available for .NET Framework 3.5, 4.5, .NET Standard 2.0, or later.

## Installation

You can install the library through the NuGet package.
Add a dependency in your project file using the following syntax:

    <ItemGroup>
        <PackageReference Include="Cube.Core" Version="3.1.0" />
    </ItemGroup>

Or select it from the NuGet packages UI on Visual Studio.

## Dependencies

* [NLog](https://nlog-project.org/)
* [Microsoft.Win32.Registry](https://www.nuget.org/packages/Microsoft.Win32.Registry/) ... .NET Standard 2.0
* [Microsoft.Win32.SystemEvents](https://www.nuget.org/packages/Microsoft.Win32.SystemEvents/) ... .NET Standard 2.0

## Contributing

1. Fork [Cube.Core](https://github.com/cube-soft/Cube.Core/fork) repository.
2. Create a feature branch from the [master](https://github.com/cube-soft/Cube.Core/tree/master) branch (git checkout -b my-new-feature origin/master).
3. Commit your changes.
4. Rebase your local changes against the master branch.
5. Run the dotnet test command or the Visual Studio (NUnit 3 test adapter) and confirm that it passes.
6. Create new Pull Request.

## License

Copyright © 2010 [CubeSoft, Inc.](https://www.cube-soft.jp/)
The project is licensed under the [Apache 2.0](https://github.com/cube-soft/Cube.Core/blob/master/License.txt).