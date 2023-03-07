using System;
using Basic_RayCaster.Structures;
using NUnit.Framework;

namespace Basic_RayCaster.Tests
{
	public class PlaneIntersectionTests
	{
        public void PlaneIntersectionTrue()
        {
            Plane plane = new Plane(new Vector(0f, 0f, 0f), new Vector(0f, 0f, 1f));
            Ray ray = new Ray(new Vector(0f, 0f, -1f), new Vector(0f, 0f, 1f));

            bool result = plane.Intersects(ray);

            Assert.IsTrue(result);
        }

        public void PlaneIntersectionFalse()
        {
            Plane plane = new Plane(new Vector(0f, 0f, 0f), new Vector(0f, 0f, 1f));
            Ray ray = new Ray(new Vector(0f, 0f, -1f), new Vector(1f, 0f, 0f));

            bool result = plane.Intersects(ray);

            Assert.IsFalse(result);
        }
    }
}
