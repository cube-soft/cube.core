Cube.Forms
====

[![NuGet](https://img.shields.io/nuget/v/Cube.Forms.svg)](https://www.nuget.org/packages/Cube.Forms/)
[![AppVeyor](https://ci.appveyor.com/api/projects/status/k5a3hpx8q788dpq2?svg=true)](https://ci.appveyor.com/project/clown/cube-forms)

The Cube.Forms packages provides GUI components available for .NET Framework 3.5, 4.5, .NET 5.0, or later.
The package also provides the functionality to support the MVVM pattern in WinForms applications.
Note that basic functionality for the MVVM pattern is implemented in the [Cube.Core](https://github.com/cube-soft/Cube.Core), and the WPF part is in the [Cube.Xui](https://github.com/cube-soft/Cube.Xui).


## Installation

You can install the library through the NuGet package.
Add a dependency in your project file using the following syntax:

    <ItemGroup>
        <PackageReference Include="Cube.Forms" Version="4.0.0" />
    </ItemGroup>

Or select it from the NuGet packages UI on Visual Studio.

## Contributing

1. Fork [Cube.Forms](https://github.com/cube-soft/Cube.Forms/fork) repository.
2. Create a feature branch from the master or stable branch (e.g. git checkout -b my-new-feature origin/master). Note that the master branch may refer to some pre-release NuGet packages. Try the [rake clobber](https://github.com/cube-soft/Cube.Forms/blob/master/Rakefile) command when build errors occur.
3. Commit your changes.
4. Rebase your local changes against the master or stable branch.
5. Run the dotnet test command or the Visual Studio (NUnit 3 test adapter) and confirm that it passes.
6. Create a new Pull Request.

## License

Copyright © 2010 [CubeSoft, Inc.](https://www.cube-soft.jp/)
The project is licensed under the [Apache 2.0](https://github.com/cube-soft/Cube.Forms/blob/master/License.txt).