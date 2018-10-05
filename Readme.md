Cube.Xui
====

[![NuGet](https://img.shields.io/nuget/v/Cube.Xui.svg)](https://www.nuget.org/packages/Cube.Xui/)
[![AppVeyor](https://ci.appveyor.com/api/projects/status/brama8ylsuk8xjer?svg=true)](https://ci.appveyor.com/project/clown/cube-xui)
[![Codecov](https://codecov.io/gh/cube-soft/Cube.Xui/branch/master/graph/badge.svg)](https://codecov.io/gh/cube-soft/Cube.Xui)

Cube.Xui is a WPF based GUI library for CubeSoft applications.

## Installation

You can install using NuGet like this:

    PM> Install-Package Cube.Xui

Or select it from the NuGet packages UI on Visual Studio.

## Dependencies

* [Cube.Core](https://github.com/cube-soft/Cube.Core)
* [MVVM Light Toolkit](http://www.mvvmlight.net/)

## Contributing

1. Fork [Cube.Xui](https://github.com/cube-soft/Cube.Xui/fork) repository.
2. Create a feature branch from the [stable](https://github.com/cube-soft/Cube.Xui/tree/stable) branch (git checkout -b my-new-feature origin/stable). The [master](https://github.com/cube-soft/Cube.Xui/tree/master) branch may refer some pre-released NuGet packages. See [AppVeyor.yml](https://github.com/cube-soft/Cube.Xui/blob/master/AppVeyor.yml) if you want to build and commit in the master branch.
3. Commit your changes.
4. Rebase your local changes against the stable (or master) branch.
5. Run test suite with the [NUnit](http://nunit.org/) console or the Visual Studio (NUnit 3 test adapter) and confirm that it passes.
6. Create new Pull Request.

## License

Copyright &copy; 2010 [CubeSoft, Inc.](http://www.cube-soft.jp/)
The project is licensed under the [Apache 2.0](https://github.com/cube-soft/Cube.Xui/blob/master/License.txt).