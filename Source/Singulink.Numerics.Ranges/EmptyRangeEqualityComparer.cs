using System;
using System.Collections.Generic;

namespace Singulink.Numerics;

internal class EmptyRangeEqualityComparer<TRange> : EqualityComparer<TRange>
    where TRange : struct, IAnyRange, IEquatable<TRange>
{
    public override bool Equals(TRange x, TRange y)
    {
        if (x.IsEmpty)
            return y.IsEmpty;

        return y.IsEmpty ? false : x.Equals(y);
    }

    public override int GetHashCode(TRange obj)
    {
        return obj.IsEmpty ? 0 : obj.GetHashCode();
    }
}
