using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Singulink.Numerics;

/// <summary>
/// Represents a range of values with an inclusive start and exclusive end.
/// </summary>
public interface IRange<T> : IEnumerable<T>
{
    /// <summary>
    /// Gets the inclusive start value of the range.
    /// </summary>
    public T Start { get; }

#pragma warning disable CA1716 // Identifiers should not match keywords

    /// <summary>
    /// Gets the exclusive end value of the range.
    /// </summary>
    public T End { get; }

#pragma warning restore CA1716 // Identifiers should not match keywords

    /// <summary>
    /// Gets a value indicating whether the range is empty.
    /// </summary>
    public bool IsEmpty { get; }
}