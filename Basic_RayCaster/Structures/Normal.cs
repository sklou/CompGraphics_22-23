using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_RayCaster.Structures
{
    public struct Normal
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public Normal(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Normal operator -(Normal a)
        {
            return new Normal(-a.X, -a.Y, -a.Z);
        }

        public static Normal operator +(Normal a, Normal b)
        {
            return new Normal(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Normal operator -(Normal a, Normal b)
        {
            return new Normal(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Normal operator *(Normal a, double scalar)
        {
            return new Normal(a.X * scalar, a.Y * scalar, a.Z * scalar);
        }

        public static double operator *(Normal a, Vector b)
        {
            return a.X * b.x + a.Y * b.y + a.Z * b.z;
        }

        public double Magnitude()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }
        public double DotProduct(Vector vector)
        {
            return X * vector.x + Y * vector.y + Z * vector.z;
        }

        public Normal Normalize()
        {
            double magnitude = Magnitude();
            return new Normal(X / magnitude, Y / magnitude, Z / magnitude);
        }
    }

}

