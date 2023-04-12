using System;
using NUnit.Framework;
using Obj_reader.Structures;

namespace Obj_reader.Tests
{
	public class PointTests
    {
        public void TestPointConstructor()
        {
            double x = 1.0;
            double y = 2.0;
            double z = 3.0;

            Point point = new Point(x, y, z);

            Assert.AreEqual(x, point.x);
            Assert.AreEqual(y, point.y);
            Assert.AreEqual(z, point.z);
        }

        public void TestSetX()
        {
            double x = 1.0;
            double y = 2.0;
            double z = 3.0;

            Point point = new Point(x, y, z);

            double newX = 4.0;
            point.x = newX;

            Assert.AreEqual(newX, point.x);
        }

        public void TestSetY()
        {
            double x = 1.0;
            double y = 2.0;
            double z = 3.0;

            Point point = new Point(x, y, z);

            double newY = 5.0;
            point.y = newY;

            Assert.AreEqual(newY, point.y);
        }

        public void TestSetZ()
        {
            double x = 1.0;
            double y = 2.0;
            double z = 3.0;

            Point point = new Point(x, y, z);

            double newZ = 6.0;
            point.z = newZ;

            Assert.AreEqual(newZ, point.z);
        }

        public void TestPointEquality()
        {
            Point point1 = new Point(1.0, 2.0, 3.0);
            Point point2 = new Point(1.0, 2.0, 3.0);
            Point point3 = new Point(4.0, 5.0, 6.0);

            Assert.AreEqual(point1, point2);
            Assert.AreNotEqual(point1, point3);
        }
    }
}