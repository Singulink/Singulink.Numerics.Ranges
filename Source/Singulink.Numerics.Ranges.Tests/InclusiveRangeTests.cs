using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Singulink.Numerics.Ranges.Tests
{
    [TestClass]
    public class InclusiveRangeTests
    {
        [TestMethod]
        public void Empty()
        {
            var x = InclusiveRange32.Empty;
            Assert.IsTrue(x.IsEmpty);

            x = new InclusiveRange32(1, 0);
            Assert.IsTrue(x.IsEmpty);
        }

        [TestMethod]
        public void Count()
        {
            Assert.AreEqual(100, new InclusiveRange8(1, 100).Count);
            Assert.AreEqual(110, new InclusiveRange16(-9, 100).Count);
            Assert.AreEqual(110, new InclusiveRange32(-9, 100).Count);
            Assert.AreEqual(110, new InclusiveSRange8(-9, 100).Count);
            Assert.AreEqual(100, new InclusiveURange16(1, 100).Count);
            Assert.AreEqual(100, new InclusiveURange32(1, 100).Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotUnionSplitInclusiveRanges()
        {
            var x = InclusiveRange32.SingleValue(5);
            var y = InclusiveRange32.SingleValue(7);

            x.UnionWith(y);
        }

        [TestMethod]
        public void UnionContained()
        {
            var x = new InclusiveRange32(0, 10);
            var y = new InclusiveRange32(5, 8);

            Assert.IsTrue(x.UnionWith(y) == new InclusiveRange32(0, 10));
            Assert.IsTrue(y.UnionWith(x) == new InclusiveRange32(0, 10));
        }

        [TestMethod]
        public void UnionOffset1()
        {
            var x = new InclusiveRange16(0, 10);
            var y = new InclusiveRange16(5, 14);

            Assert.IsTrue(x.UnionWith(y) == new InclusiveRange16(0, 14));
            Assert.IsTrue(y.UnionWith(x) == new InclusiveRange16(0, 14));
        }

        [TestMethod]
        public void UnionOffset2()
        {
            var x = new InclusiveSRange8(8, 22);
            var y = new InclusiveSRange8(5, 14);

            Assert.IsTrue(x.UnionWith(y) == new InclusiveSRange8(5, 22));
            Assert.IsTrue(y.UnionWith(x) == new InclusiveSRange8(5, 22));
        }

        [TestMethod]
        public void UnionEdge()
        {
            var x = new InclusiveURange32(8, 22);
            var y = new InclusiveURange32(23, 24);

            Assert.IsTrue(x.UnionWith(y) == new InclusiveURange32(8, 24));
            Assert.IsTrue(y.UnionWith(x) == new InclusiveURange32(8, 24));
        }

        [TestMethod]
        public void EmptyIntersectedSplitInclusiveRanges()
        {
            var x = InclusiveRange16.SingleValue(5);
            var y = InclusiveRange16.SingleValue(6);

            Assert.IsTrue(x.IntersectWith(y).IsEmpty);
            Assert.IsTrue(y.IntersectWith(x).IsEmpty);
        }

        [TestMethod]
        public void IntersectContained()
        {
            var x = new InclusiveRange16(0, 10);
            var y = new InclusiveRange16(5, 8);

            Assert.IsTrue(x.IntersectWith(y) == new InclusiveRange16(5, 8));
            Assert.IsTrue(y.IntersectWith(x) == new InclusiveRange16(5, 8));
        }

        [TestMethod]
        public void IntersectOffset()
        {
            var x = new InclusiveRange32(0, 10);
            var y = new InclusiveRange32(5, 13);

            Assert.IsTrue(x.IntersectWith(y) == new InclusiveRange32(5, 10));
            Assert.IsTrue(y.IntersectWith(x) == new InclusiveRange32(5, 10));
        }

        [TestMethod]
        public void IntersectEdge1()
        {
            var x = new InclusiveRange32(-10, 0);
            var y = InclusiveRange32.SingleValue(-10);

            Assert.IsTrue(x.IntersectWith(y) == new InclusiveRange32(-10, -10));
            Assert.IsTrue(y.IntersectWith(x) == new InclusiveRange32(-10, -10));
        }

        [TestMethod]
        public void IntersectEdge2()
        {
            var x = new InclusiveRange32(10, 20);
            var y = new InclusiveRange32(5, 10);

            Assert.IsTrue(x.IntersectWith(y) == new InclusiveRange32(10, 10));
            Assert.IsTrue(y.IntersectWith(x) == new InclusiveRange32(10, 10));
        }

        [TestMethod]
        public void Coalesce()
        {
            var a = new InclusiveRange32(8, 22);
            var b = new InclusiveRange32(5, 10);
            var c = new InclusiveRange32(5, 10);
            var d = new InclusiveRange32(1, 2);
            var e = new InclusiveRange32(-1, 0);
            var f = new InclusiveRange32(20, 24);
            var g = new InclusiveRange32(48, 1000);
            var h = InclusiveRange32.Empty;

            var set1 = new[] { a, b, c, d, e, f, g, h };
            var set2 = new[] { g, g, f, a, b, h, d, e, f, c };

            var result1 = InclusiveRange32.Coalesce(set1);
            var result2 = InclusiveRange32.Coalesce(set2);

            Assert.IsTrue(result1.SequenceEqual(new[] { new InclusiveRange32(-1, 2), new InclusiveRange32(5, 24), new InclusiveRange32(48, 1000) }));
            Assert.IsTrue(result2.SequenceEqual(new[] { new InclusiveRange32(-1, 2), new InclusiveRange32(5, 24), new InclusiveRange32(48, 1000) }));
        }
    }
}
