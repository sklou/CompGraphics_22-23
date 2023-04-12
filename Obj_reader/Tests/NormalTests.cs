using System;
using NUnit.Framework;
using Obj_reader.Structures;

namespace Obj_reader.Tests
{
	public class NormalTests
    {
        public void TestNormalCreation()
        {
            double x = 1.0;
            double y = 2.0;
            double z = 3.0;

            Normal normal = new Normal(x, y, z);

            Assert.AreEqual(x, normal.X);
            Assert.AreEqual(y, normal.Y);
            Assert.AreEqual(z, normal.Z);
        }

        public void TestNormalAddition()
        {
            Normal a = new Normal(1.0, 2.0, 3.0);
            Normal b = new Normal(4.0, 5.0, 6.0);

            Normal result = a + b;

            Assert.AreEqual(5.0, result.X);
            Assert.AreEqual(7.0, result.Y);
            Assert.AreEqual(9.0, result.Z);
        }

        public void TestNormalSubtraction()
        {
            Normal a = new Normal(4.0, 5.0, 6.0);
            Normal b = new Normal(1.0, 2.0, 3.0);

            Normal result = a - b;

            Assert.AreEqual(3.0, result.X);
            Assert.AreEqual(3.0, result.Y);
            Assert.AreEqual(3.0, result.Z);
        }
    }
}
