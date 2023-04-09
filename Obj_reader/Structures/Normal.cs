using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obj_reader.Structures
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

        public static Normal operator +(Normal a, Normal b)
        {
            return new Normal(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Normal operator -(Normal a, Normal b)
        {
            return new Normal(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
    }

}

