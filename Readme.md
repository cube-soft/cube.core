Cube.FileSystem
====

[![NuGet](https://img.shields.io/nuget/v/Cube.FileSystem.svg)](https://www.nuget.org/packages/Cube.FileSystem/)
[![AppVeyor](https://ci.appveyor.com/api/projects/status/6exuqpkn7ct3a790?svg=true)](https://ci.appveyor.com/project/clown/cube-filesystem)
[![Codecov](https://codecov.io/gh/cube-soft/Cube.FileSystem/branch/master/graph/badge.svg)](https://codecov.io/gh/cube-soft/Cube.FileSystem)

Cube.FileSystem is an I/O library for CubeSoft applications.

## Installation

You can install using NuGet like this:

    PM> Install-Package Cube.FileSystem

Or select it from the NuGet packages UI on Visual Studio.

## Dependencies

* [Cube.Core](https://github.com/cube-soft/Cube.Core)
* [AlphaFS](http://alphafs.alphaleonis.com/)

## Contributing

1. Fork [Cube.FileSystem](https://github.com/cube-soft/Cube.FileSystem/fork) repository.
2. Create a feature branch from the [stable](https://github.com/cube-soft/Cube.FileSystem/tree/stable) branch (git checkout -b my-new-feature origin/stable). The [master](https://github.com/cube-soft/Cube.FileSystem/tree/master) branch may refer some pre-released NuGet packages. See [AppVeyor.yml](https://github.com/cube-soft/Cube.FileSystem/blob/master/AppVeyor.yml) if you want to build and commit in the master branch.
3. Commit your changes.
4. Rebase your local changes against the stable (or master) branch.
5. Run test suite with the [NUnit](http://nunit.org/) console or the Visual Studio (NUnit 3 test adapter) and confirm that it passes.
6. Create new Pull Request.

## License

Copyright (c) 2010 [CubeSoft, Inc.](http://www.cube-soft.jp/)
The project is licensed under the [Apache 2.0](https://github.com/cube-soft/Cube.FileSystem/blob/master/License.txt).