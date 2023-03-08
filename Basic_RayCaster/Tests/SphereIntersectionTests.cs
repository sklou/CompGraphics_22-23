using System;
using Basic_RayCaster.Structures;
using NUnit.Framework;

namespace Basic_RayCaster.Tests
{
	public class SphereIntersectionTests
    {
        /* public void SphereIntersectionTrue()
         {
             Ray ray = new Ray(new Vector(0, 0, -5), new Vector(0, 0, 1));
             Sphere sphere = new Sphere(new Vector(0, 0, 0), 1);

             bool intersects = sphere.Intersects(ray);

             Assert.IsTrue(intersects);
         }

         public void SphereIntersectionFalse()
         {
             Ray ray = new Ray(new Vector(0, 0, -5), new Vector(0, 1, 0));
             Sphere sphere = new Sphere(new Vector(0, 0, 0), 1);

             bool intersects = sphere.Intersects(ray);

             Assert.IsFalse(intersects);
         }*/
        /* //Not realy sure about this one
        public void SphereIntersectionTrue()
        {
             Sphere sphere = new Sphere(new Vector(0, 0, 5), 1);
             Ray ray = new Ray(new Vector(0, 0, 0), new Vector(0, 0, 1));

             var (intersection, normal) = sphere.Intersects(ray);

             Assert.IsNotNull(intersection);
        }

         public void SphereIntersectionFalse()
         {
             Sphere sphere = new Sphere(new Vector(0, 0, 5), 1);
             Ray ray = new Ray(new Vector(0, 0, 0), new Vector(1, 1, 0));

             var (intersection, normal) = sphere.Intersects(ray);

             Assert.IsNull(intersection);
         }
        */
        public void SphereIntersectionTrue()
        {
            Vector spherePosition = new Vector(0, 0, -10);
            float sphereRadius = 2;
            Sphere sphere = new Sphere(spherePosition, sphereRadius);
            Vector rayOrigin = new Vector(0, 0, 0);
            Vector rayDirection = new Vector(0, 0, -1);
            Ray ray = new Ray(rayOrigin, rayDirection);
            Vector expectedIntersectionPoint = new Vector(0, 0, -8);
            Vector expectedNormal = new Vector(0, 0, 1);

            (Vector? intersection, Vector? normal) = sphere.Intersects(ray);

            Assert.IsNotNull(intersection);
            Assert.IsNotNull(normal);
            Assert.AreEqual(expectedIntersectionPoint.x, intersection.Value.x, 0.0001);
            Assert.AreEqual(expectedIntersectionPoint.y, intersection.Value.y, 0.0001);
            Assert.AreEqual(expectedIntersectionPoint.z, intersection.Value.z, 0.0001);
            Assert.AreEqual(expectedNormal.x, normal.Value.x, 0.0001);
            Assert.AreEqual(expectedNormal.y, normal.Value.y, 0.0001);
            Assert.AreEqual(expectedNormal.z, normal.Value.z, 0.0001);
        }

        public void SphereIntersectionFalse()
        {
            Vector spherePosition = new Vector(0, 0, -10);
            float sphereRadius = 2;
            Sphere sphere = new Sphere(spherePosition, sphereRadius);
            Vector rayOrigin = new Vector(0, 0, 0);
            Vector rayDirection = new Vector(0, 1, 0);
            Ray ray = new Ray(rayOrigin, rayDirection);

            (Vector? intersection, Vector? normal) = sphere.Intersects(ray);

            Assert.IsNull(intersection);
            Assert.IsNull(normal);
        }
    }
}