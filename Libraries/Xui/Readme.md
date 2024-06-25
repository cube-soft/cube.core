Cube.Xui
====

[![Package](https://img.shields.io/nuget/v/cube.xui)](https://www.nuget.org/packages/cube.xui/)
[![AppVeyor](https://img.shields.io/appveyor/build/clown/cube-core)](https://ci.appveyor.com/project/clown/cube-core)
[![Codecov](https://img.shields.io/codecov/c/github/cube-soft/cube.core)](https://codecov.io/gh/cube-soft/cube.core)

The Cube.Xui package has bindings, converters, commands, behaviors, and other components to provide support the MVVM pattern in WPF applications. The package is available for .NET Framework 3.5, 4.6.2, .NET 6.0, or later. Note that basic components to practice the MVVM pattern is implemented in the [Cube.Core](https://www.nuget.org/packages/cube.core/) package, and the WinForms part is in the [Cube.Forms](https://www.nuget.org/packages/cube.forms/) package.

The Cube.Xui package depends on the [Microsoft.Xaml.Behaviors.Wpf](https://www.nuget.org/packages/Microsoft.Xaml.Behaviors.Wpf/) package. However, in the case of .NET Framework 3.5, it depends on the old [Microsoft Expression Blend SDK](https://www.microsoft.com/ja-jp/download/details.aspx?id=10801) instead of that. In addtion, the packages implements some features by referring to some of the code in the following projects.

* [MVVM Light Toolkit](https://github.com/lbugnion/mvvmlight)
* [Prism](https://github.com/PrismLibrary/Prism)

## Installation

You can install the library through the NuGet package. Add dependencies in your project file or select it from the NuGet packages UI on Visual Studio. For more information, see the [NuGet](https://www.nuget.org/packages/cube.xui/) page.

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