using System;
using NUnit.Framework;
using Obj_reader.Structures;

namespace Obj_reader.Tests
{
    public class CameraTests
    {
        public void GetRayThroughPixel_CorrectDirection()
        {
            var camera = new Camera(new Vector(0, 0, 0));
            int x = 100;
            int y = 50;
            int width = 200;
            int height = 100;

            var ray = camera.GetRayThroughPixel(x, y, width, height);

            Assert.That(ray.Direction.X, Is.EqualTo(x * 0.011f - 0.25625f).Within(0.001f));
            Assert.That(ray.Direction.Y, Is.EqualTo(y * 0.022f - 0.12125f).Within(0.001f));
            Assert.That(ray.Direction.Z, Is.EqualTo(1f).Within(0.001f));
        }

        public void GetRayThroughPixel_CorrectOrigin()
        {
            var camera = new Camera(new Vector(1, 2, 3));
            int x = 100;
            int y = 50;
            int width = 200;
            int height = 100;

            var ray = camera.GetRayThroughPixel(x, y, width, height);

            Assert.That(ray.Origin.X, Is.EqualTo(1f).Within(0.001f));
            Assert.That(ray.Origin.Y, Is.EqualTo(2f).Within(0.001f));
            Assert.That(ray.Origin.Z, Is.EqualTo(3f).Within(0.001f));
        }

        public void GetRayThroughPixel_NormalizedDirection()
        {
            var camera = new Camera(new Vector(0, 0, 0));
            int x = 100;
            int y = 50;
            int width = 200;
            int height = 100;

            var ray = camera.GetRayThroughPixel(x, y, width, height);

            Assert.That(ray.Direction.Length(), Is.EqualTo(1f).Within(0.001f));
        }
    }
}

