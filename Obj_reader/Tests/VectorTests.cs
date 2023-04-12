using System;
using NUnit.Framework;
using Obj_reader.Structures;

namespace Obj_reader.Tests
{
    public class VectorTests
    {
        public void VectorConstructorTest()
        {
            Vector v = new Vector(1.0f, 2.0f, 3.0f);
            Assert.AreEqual(1.0f, v.x);
            Assert.AreEqual(2.0f, v.y);
            Assert.AreEqual(3.0f, v.z);

            Vector zeroVector = Vector.Zero;
            Assert.AreEqual(0.0f, zeroVector.x);
            Assert.AreEqual(0.0f, zeroVector.y);
            Assert.AreEqual(0.0f, zeroVector.z);
        }

        public void VectorNormalizeTest()
        {
            Vector v = new Vector(3.0f, 0.0f, 4.0f);
            v.Normalize();
            Assert.AreEqual(0.6f, v.x, 0.01f);
            Assert.AreEqual(0.0f, v.y, 0.01f);
            Assert.AreEqual(0.8f, v.z, 0.01f);

            Vector zeroVector = Vector.Zero;
            zeroVector.Normalize();
            Assert.AreEqual(0.0f, zeroVector.x);
            Assert.AreEqual(0.0f, zeroVector.y);
            Assert.AreEqual(0.0f, zeroVector.z);
        }

        public void VectorMagnitudeTest()
        {
            Vector v = new Vector(3.0f, 0.0f, 4.0f);
            float mag = v.Magnitude();
            Assert.AreEqual(5.0f, mag, 0.01f);

            Vector zeroVector = Vector.Zero;
            mag = zeroVector.Magnitude();
            Assert.AreEqual(0.0f, mag);
        }

        public void VectorToVectorTest()
        {
            Vector? nullableVector = new Vector(3.0f, 0.0f, 4.0f);
            Vector v = Vector.ToVector(nullableVector);
            Assert.AreEqual(3.0f, v.x);
            Assert.AreEqual(0.0f, v.y);
            Assert.AreEqual(4.0f, v.z);

            nullableVector = null;
            v = Vector.ToVector(nullableVector);
            Assert.AreEqual(0.0f, v.x);
            Assert.AreEqual(0.0f, v.y);
            Assert.AreEqual(0.0f, v.z);
        }

        public void VectorDotTest()
        {
            Vector v1 = new Vector(1.0f, 2.0f, 3.0f);
            Vector v2 = new Vector(4.0f, 5.0f, 6.0f);
            float dot = Vector.Dot(v1, v2);
            Assert.AreEqual(32.0f, dot, 0.01f);

            Vector? nullableVector = new Vector(1.0f, 2.0f, 3.0f);
            dot = Vector.Dot2(nullableVector, v2);
            Assert.AreEqual(32.0f, dot, 0.01f);
        }
    }
}