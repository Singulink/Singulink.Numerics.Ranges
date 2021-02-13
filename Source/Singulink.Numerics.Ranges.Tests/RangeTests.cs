using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Singulink.Numerics.Ranges.Tests
{
    [TestClass]
    public class RangeTests
    {
        [TestMethod]
        public void Empty()
        {
            var x = Range32.Empty;
            Assert.IsTrue(x.IsEmpty);

            x = new Range32(1, 0);
            Assert.IsTrue(x.IsEmpty);
        }

        [TestMethod]
        public void Count()
        {
            Assert.AreEqual(100, new Range8(1, 100).Count);
            Assert.AreEqual(110, new Range16(-9, 100).Count);
            Assert.AreEqual(110, new Range32(-9, 100).Count);
            Assert.AreEqual(110ul, new Range64(-9, 100).Count);
            Assert.AreEqual(110, new SRange8(-9, 100).Count);
            Assert.AreEqual(100, new URange16(1, 100).Count);
            Assert.AreEqual(100, new URange32(1, 100).Count);
            Assert.AreEqual(100ul, new URange64(1, 100).Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotUnionSplitRanges()
        {
            var x = new Range32(5);
            var y = new Range32(7);

            x.UnionWith(y);
        }

        [TestMethod]
        public void UnionContained()
        {
            var x = new Range32(0, 10);
            var y = new Range32(5, 8);

            Assert.IsTrue(x.UnionWith(y) == new Range32(0, 10));
            Assert.IsTrue(y.UnionWith(x) == new Range32(0, 10));
        }

        [TestMethod]
        public void UnionOffset1()
        {
            var x = new Range16(0, 10);
            var y = new Range16(5, 14);

            Assert.IsTrue(x.UnionWith(y) == new Range16(0, 14));
            Assert.IsTrue(y.UnionWith(x) == new Range16(0, 14));
        }

        [TestMethod]
        public void UnionOffset2()
        {
            var x = new SRange8(8, 22);
            var y = new SRange8(5, 14);

            Assert.IsTrue(x.UnionWith(y) == new SRange8(5, 22));
            Assert.IsTrue(y.UnionWith(x) == new SRange8(5, 22));
        }

        [TestMethod]
        public void UnionEdge()
        {
            var x = new URange32(8, 22);
            var y = new URange32(23, 24);

            Assert.IsTrue(x.UnionWith(y) == new URange32(8, 24));
            Assert.IsTrue(y.UnionWith(x) == new URange32(8, 24));
        }

        [TestMethod]
        public void EmptyIntersectedSplitRanges()
        {
            var x = new Range16(5);
            var y = new Range16(6);

            Assert.IsTrue(x.IntersectWith(y).IsEmpty);
            Assert.IsTrue(y.IntersectWith(x).IsEmpty);
        }

        [TestMethod]
        public void IntersectContained()
        {
            var x = new Range16(0, 10);
            var y = new Range16(5, 8);

            Assert.IsTrue(x.IntersectWith(y) == new Range16(5, 8));
            Assert.IsTrue(y.IntersectWith(x) == new Range16(5, 8));
        }

        [TestMethod]
        public void IntersectOffset()
        {
            var x = new Range32(0, 10);
            var y = new Range32(5, 13);

            Assert.IsTrue(x.IntersectWith(y) == new Range32(5, 10));
            Assert.IsTrue(y.IntersectWith(x) == new Range32(5, 10));
        }

        [TestMethod]
        public void IntersectEdge1()
        {
            var x = new Range32(-10, 0);
            var y = new Range32(-10);

            Assert.IsTrue(x.IntersectWith(y) == new Range32(-10, -10));
            Assert.IsTrue(y.IntersectWith(x) == new Range32(-10, -10));
        }

        [TestMethod]
        public void IntersectEdge2()
        {
            var x = new Range32(10, 20);
            var y = new Range32(5, 10);

            Assert.IsTrue(x.IntersectWith(y) == new Range32(10, 10));
            Assert.IsTrue(y.IntersectWith(x) == new Range32(10, 10));
        }

        [TestMethod]
        public void Coalesce()
        {
            var a = new Range64(8, 22);
            var b = new Range64(5, 10);
            var c = new Range64(5, 10);
            var d = new Range64(1, 2);
            var e = new Range64(-1, 0);
            var f = new Range64(20, 24);
            var g = new Range64(48, 1000);
            var h = Range64.Empty;

            var set1 = new[] { a, b, c, d, e, f, g, h };
            var set2 = new[] { g, g, f, a, b, h, d, e, f, c };

            var result1 = Range64.Coalesce(set1);
            var result2 = Range64.Coalesce(set2);

            Assert.IsTrue(result1.SequenceEqual(new[] { new Range64(-1, 2), new Range64(5, 24), new Range64(48, 1000) }));
            Assert.IsTrue(result2.SequenceEqual(new[] { new Range64(-1, 2), new Range64(5, 24), new Range64(48, 1000) }));
        }
    }
}
