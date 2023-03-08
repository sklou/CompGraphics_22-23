using System;
using Basic_RayCaster.Structures;
using NUnit.Framework;

namespace Basic_RayCaster.Tests
{
    public class DiscIntersectionTests
    {
        private Disc disc;
        
        public void SetUp()
        {
            Vector position = new Vector(0, 0, 0);
            float radius_big = 2f;
            float radius_small = 1f;
            disc = new Disc(position, radius_big, radius_small);
        }
        public void DiscIntersectionTrue()
        {
            Vector origin = new Vector(0, 0, -10);
            Vector direction = new Vector(0, 0, 1);
            Ray ray = new Ray(origin, direction);

            bool result = disc.Intersects(ray);

            Assert.IsTrue(result);
        }

        public void DiscIntersectionFalse()
        {
            Vector origin = new Vector(0, 0, -10);
            Vector direction = new Vector(1, 1, 0);
            Ray ray = new Ray(origin, direction);

            bool result = disc.Intersects(ray);

            Assert.IsFalse(result);
        }
    }
}
