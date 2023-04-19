using System;
using NUnit.Framework;
using Obj_reader.Structures;

namespace Obj_reader.Tests
{
    public class CameraTests
    {
        [Test]
        public void GetRayThroughPixel_CorrectDirection()
        {
            var camera = new Camera(new Vector(0, 0, 0));
            int x = 100;
            int y = 50;
            int width = 200;
            int height = 100;

            var ray = camera.GetRayThroughPixel(x, y, width, height);

            Assert.That(ray.direction.x, Is.EqualTo(x * 0.011f - 0.25625f).Within(0.001f));
            Assert.That(ray.direction.y, Is.EqualTo(y * 0.022f - 0.12125f).Within(0.001f));
            Assert.That(ray.direction.z, Is.EqualTo(1f).Within(0.001f));
        }

        [Test]
        public void GetRayThroughPixel_CorrectOrigin()
        {
            var camera = new Camera(new Vector(1, 2, 3));
            int x = 100;
            int y = 50;
            int width = 200;
            int height = 100;

            var ray = camera.GetRayThroughPixel(x, y, width, height);

            Assert.That(ray.origin.x, Is.EqualTo(1f).Within(0.001f));
            Assert.That(ray.origin.y, Is.EqualTo(2f).Within(0.001f));
            Assert.That(ray.origin.z, Is.EqualTo(3f).Within(0.001f));
        }

        [Test]
        public void GetRayThroughPixel_NormalizedDirection()
        {
            var camera = new Camera(new Vector(0, 0, 0));
            int x = 100;
            int y = 50;
            int width = 200;
            int height = 100;

            var ray = camera.GetRayThroughPixel(x, y, width, height);

            Assert.That(ray.direction.Magnitude(), Is.EqualTo(1f).Within(0.001f));
        }
    }
}
