using System;
using System.Collections.Generic;
using System.Text;

namespace Singulink.Numerics;

#pragma warning disable CA2208 // Instantiate argument exceptions correctly

internal static class Throw
{
    public static void End_OutOfRange()
    {
        throw new ArgumentOutOfRangeException("end", "Ranges cannot contain the data type's maximum value.");
    }

    public static void Value_OutOfRange()
    {
        throw new ArgumentOutOfRangeException("value", "Ranges cannot contain the data type's maximum value.");
    }

    public static void Range_UnionInvalid()
    {
        throw new ArgumentException("Cannot form a union between this range and the specified range.", "range");
    }
}
