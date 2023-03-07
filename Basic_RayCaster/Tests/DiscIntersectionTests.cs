using System;
using Basic_RayCaster.Structures;
using NUnit.Framework;

namespace Basic_RayCaster.Tests
{
    public class DiscIntersectionTests
    {
        public void DiscIntersectionTrue()
        {
            Vector discPosition = new Vector(0, 0, 0);
            float bigRadius = 5.0f;
            float smallRadius = 2.0f;
            Disc disc = new Disc(discPosition, bigRadius, smallRadius);

            Vector rayOrigin = new Vector(0, 0, -10);
            Vector rayDirection = new Vector(0, 0, 1);
            Ray ray = new Ray(rayOrigin, rayDirection);

            bool intersects = disc.Intersects(ray);

            Assert.IsTrue(intersects);
        }

        public void DiscIntersectionFalse()
        {
            Vector discPosition = new Vector(0, 0, 0);
            float bigRadius = 5.0f;
            float smallRadius = 2.0f;
            Disc disc = new Disc(discPosition, bigRadius, smallRadius);

            Vector rayOrigin = new Vector(0, 0, -10);
            Vector rayDirection = new Vector(1, 1, 0);
            Ray ray = new Ray(rayOrigin, rayDirection);

            bool intersects = disc.Intersects(ray);

            Assert.IsFalse(intersects);
        }
    }
}
