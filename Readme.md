Cube.Core
====

[![Core](https://img.shields.io/nuget/v/cube.core?label=core)](https://www.nuget.org/packages/cube.core/)
[![Forms](https://img.shields.io/nuget/v/cube.forms?label=forms)](https://www.nuget.org/packages/cube.forms/)
[![Forms.Controls](https://img.shields.io/nuget/v/cube.forms.controls?label=forms.controls)](https://www.nuget.org/packages/cube.forms.controls/)
[![Xui](https://img.shields.io/nuget/v/cube.xui?label=xui)](https://www.nuget.org/packages/cube.xui/)
[![AlphaFS](https://img.shields.io/nuget/v/cube.filesystem.alphafs?label=alphafs)](https://www.nuget.org/packages/cube.filesystem.alphafs/)
[![NLog](https://img.shields.io/nuget/v/cube.logging.nlog?label=nlog)](https://www.nuget.org/packages/cube.logging.nlog/)
[![AppVeyor](https://img.shields.io/appveyor/build/clown/cube-core)](https://ci.appveyor.com/project/clown/cube-core)
[![Codecov](https://img.shields.io/codecov/c/github/cube-soft/cube.core)](https://codecov.io/gh/cube-soft/cube.core)

The project provides support the MVVM pattern in WinForms or WPF (or possibly other frameworks) applications. This project can be roughly divided into the three packages as follows:

* [Cube.Core](https://www.nuget.org/packages/cube.core/)
* [Cube.Forms](https://www.nuget.org/packages/cube.forms/) (WinForms)
* [Cube.Xui](https://www.nuget.org/packages/cube.xui/) (WPF)

The Cube.Core package provides some additional utility functions. One of them is Cube.FileSystem.Io class, which provides functionality to switch between the use of .NET standard methods or other third party libraries for I/O operations such as move, copy, delete, etc. The [Cube.FileSystem.AlphaFS](https://www.nuget.org/packages/cube.filesystem.alphafs/) package is the implementations of those I/O operations by using the [AlphaFS](https://alphafs.alphaleonis.com/) library.

Note that the libraries may output debug logs internally, but by default, these logs are not actually output anywhere. To check the contents of the debug logs, add [Cube.Logging.NLog](https://www.nuget.org/packages/cube.logging.nlog/) package and add the following code.

```cs
Cube.Logger.Configure(new Cube.Logging.NLog.LoggerSource());
```

These packages are basically available for .NET Framework 3.5, 4.6, .NET Standard 2.0, .NET 6, or later. Note that the Cube.FileSystem.AlphaFS package supports only .NET Framework.

## Nullable reference types

The v10 product projects enable nullable reference types. Tests and the private
test support library keep their existing nullable settings.

Annotations describe the values an API can actually accept or return. They do
not add runtime null checks or change CLR signatures. Parameter order, optional
defaults and generic constraints are retained. Consumers with nullable analysis
enabled may receive new warnings, including in overrides and interface
implementations.

### Using the contracts

- Check optional results such as `IoEx.GetOrDefault`, `TryCast<T>`, icon lookup
  and `NoticeQueue.Dequeue` before dereferencing them.
- `Logger.TryGet` and `OrderedDictionary.TryGetValue` describe the failed result
  with `MaybeNullWhen(false)`. A successful result still follows the nullability
  of the supplied generic type.
- `HasValue` establishes that its argument is non-null when it returns true.
  URI `With` methods preserve a non-null input URI in their return contract.
- `Message<T>.Value`, a default-constructed `Accessor<T>` and uninitialized
  bindable values may contain `default(T)`. Reference-type results therefore
  require a null check. Use a nullable generic argument when a setter or a
  supplied delegate should accept null.
- `ObservableBase.Get<T>` and `SerializableBase.Get<T>` may return `default(T)`,
  including when an explicit null property name is supplied. Their result is
  annotated conservatively even for the factory overload. A derived property
  may assert non-null only when its factory, setter and property name establish
  that invariant.
- Unconfigured notice styles, shortcut properties, images and event handlers
  remain null. Nullable migration does not replace these defaults with empty
  strings or new objects.

Projects targeting legacy frameworks keep identical internal attribute definitions
in their own `Sources/Internal/NullableHack.cs`. Projects whose target framework
already provides these attributes do not include the helper. Target-specific
files are maintained on each framework branch, without conditional compilation.
The helpers add no public API or runtime package dependency.

`OrderedDictionary.TryGetValue` keeps its nullable flow contract. An explicit
`IDictionary` implementation delegates to it, with a null-forgiving expression
at the legacy interface boundary. Interface callers use their target framework's
annotations; concrete callers receive Cube's contract. Both paths retain the same
runtime results, including `default(TValue)` on failure and stored null values
when `TValue` is nullable. No nullable warnings are disabled.

Null-forgiving expressions are limited to compatibility boundaries and local
invariants, such as an initialized lifecycle field, a populated dialog result,
or a non-null property factory. They are not used to advertise a non-null result
when a normal execution path returns null. Bindings handle absent message
payloads, and string converters retain their empty-string fallback when an
object's `ToString()` returns null.

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

Copyright © 2010 [CubeSoft, Inc.](https://www.cube-soft.com/)
These packages are licensed under the [Apache 2.0](https://github.com/cube-soft/cube.core/blob/master/License.txt).