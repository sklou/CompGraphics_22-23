using System;
using NUnit.Framework;
using Obj_reader.Structures;

namespace Obj_reader.Tests
{
	public class RayTests
    {
        public void TestRayInitialization()
        {
            Vector origin = new Vector(1, 2, 3);
            Vector direction = new Vector(4, 5, 6);
            Ray ray = new Ray(origin, direction);
            Assert.AreEqual(origin, ray.origin);
            Assert.AreEqual(direction, ray.direction);
        }

        public void TestRayPointAt()
        {
            Vector origin = new Vector(1, 2, 3);
            Vector direction = new Vector(1, 0, 0);
            Ray ray = new Ray(origin, direction);
            Vector point = ray.PointAt(2);
            Assert.AreEqual(new Vector(3, 2, 3), point);
        }

        public void TestRayEquality()
        {
            Vector origin1 = new Vector(1, 2, 3);
            Vector direction1 = new Vector(4, 5, 6);
            Ray ray1 = new Ray(origin1, direction1);

            Vector origin2 = new Vector(1, 2, 3);
            Vector direction2 = new Vector(4, 5, 6);
            Ray ray2 = new Ray(origin2, direction2);

            Assert.AreEqual(ray1, ray2);
        }
    }
}
