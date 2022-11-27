# Singulink.Numerics.Ranges

[![Chat on Discord](https://img.shields.io/discord/906246067773923490)](https://discord.gg/EkQhJFsBu6)
[![View nuget packages](https://img.shields.io/nuget/v/Singulink.Numerics.Ranges.svg)](https://www.nuget.org/packages/Singulink.Numerics.Ranges/)
[![Build and Test](https://github.com/Singulink/Singulink.Numerics.Ranges/workflows/build%20and%20test/badge.svg)](https://github.com/Singulink/Singulink.Numerics.Ranges/actions?query=workflow%3A%22build+and+test%22)

**Ranges** provides a range type for each primitive integer type. Ranges can be enumerated over, intersected, unioned and coalesced. The range types correspond to integer types as follows:
- `Range8`: `Byte` / `byte`
- `SRange8`: `SByte` / `sbyte`
- `Range16`: `Int16` / `short`
- `URange16`: `UInt16` / `ushort`
- `Range32`: `Int32` / `int`
- `URange32`: `UInt32` / `uint`
- `Range64`: `Int64` / `long`
- `URange64`: `UInt64` / `ulong`

There is also a similar set of inclusive range types named `InclusiveRange8`, `InclusiveSRange8`, etc., for all but the 64 bit integer types.

### About Singulink

We are a small team of engineers and designers dedicated to building beautiful, functional and well-engineered software solutions. We offer very competitive rates as well as fixed-price contracts and welcome inquiries to discuss any custom development / project support needs you may have.

This package is part of our **Singulink Libraries** collection. Visit https://github.com/Singulink to see our full list of publicly available libraries and other open-source projects.

## Usage

Sticking to `Range` types instead of `InclusiveRange` types is recommended unless you are optimizing memory usage and you must be able to include the primitive type's max value as part of the range. `Range` types use exclusive end boundaries, so for example a `new Range8(0, 255)` excludes the value `255` and only goes up to `254`. You can still construct `Range` objects from an inclusive end value using methods like `Range8.Inclusive(0, 254)`, but an exception will be thrown if you try to include the primitive type's max value.

A "gotcha" to look out for with the `InclusiveRange` types is that the default value (i.e. `new InclusiveRange8()` or `default(InclusiveRange8)`) is a range containing the single value `0` instead of an empty range. If you are using inclusive ranges, make sure you always use the `Empty` property (i.e. `InclusiveRange8.Empty`) if you want an empty range instead of the default value. The default value of `Range` types is an empty range, which aligns more with the expected behavior.

### V2 Changes From V1

There are memory usage and performance improvements in V2, but V2 introduces many behavioral/API changes to faciliate these improvements, i.e. the `Range` types are no longer inclusive. Switching usages over to `InclusiveRange` types is not a 1:1 conversion, i.e. the default value is not an empty range (see above) like it was in V1 and some of the API surface has changed. You can continue to use V1 without any issues, or if you wish to update to V2 to benefit from the improvements then you should extensively review, revalidate and retest any code that utilizes ranges.

## Installation

The package is available on NuGet - simply install the `Singulink.Numerics.Ranges` package.

**Supported Runtimes**: Anywhere .NET Standard 2.1+ is supported, including:
- .NET Core 3.0+
- Mono 6.4+
- Xamarin.iOS 12.16+
- Xamarin.Android 10.0+

## API

You can view the API on [FuGet](https://www.fuget.org/packages/Singulink.Numerics.Ranges).
