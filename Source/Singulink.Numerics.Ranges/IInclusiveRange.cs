using System;
using System.Collections.Generic;
using System.Text;

namespace Singulink.Numerics;

/// <summary>
/// Represents a range of values with an inclusive start and end.
/// </summary>
public interface IInclusiveRange<T> : IEnumerable<T>
{
    /// <summary>
    /// Gets the start value of the range.
    /// </summary>
    public T Start { get; }

#pragma warning disable CA1716 // Identifiers should not match keywords

    /// <summary>
    /// Gets the end value of the range.
    /// </summary>
    public T End { get; }

#pragma warning restore CA1716 // Identifiers should not match keywords

    /// <summary>
    /// Gets a value indicating whether the range is empty.
    /// </summary>
    public bool IsEmpty { get; }
}