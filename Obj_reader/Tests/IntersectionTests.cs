using System;
using NUnit.Framework;
using Obj_reader.Structures;

namespace Obj_reader.Tests
{
    public class IntersectionTests
    {
        public void TestIntersectionInitialization()
        {
            var intersection = new Intersection(true, new Vector(1, 0, 0), 2.5f);

            Assert.IsTrue(intersection.hit);
            Assert.AreEqual(intersection.normal, new Vector(1, 0, 0));
            Assert.AreEqual(intersection.distance, 2.5f);
        }

        public void TestDefaultNormalValue()
        {
            var intersection = new Intersection(true, null, 2.5f);

            Assert.IsTrue(intersection.hit);
            Assert.IsNull(intersection.normal);
            Assert.AreEqual(intersection.distance, 2.5f);
        }

        public void TestIntersectionFieldAssignment()
        {
            var intersection = new Intersection(false, null, 0);

            intersection.hit = true;
            intersection.normal = new Vector(0, 1, 0);
            intersection.distance = 1.5f;

            Assert.IsTrue(intersection.hit);
            Assert.AreEqual(intersection.normal, new Vector(0, 1, 0));
            Assert.AreEqual(intersection.distance, 1.5f);
        }

        public void TestDefaultHitValue()
        {
            var intersection = new Intersection();

            Assert.IsFalse(intersection.hit);
            Assert.IsNull(intersection.normal);
            Assert.AreEqual(intersection.distance, 0f);
        }
    }
}
