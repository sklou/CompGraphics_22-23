using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_RayCaster.Structures
{
    public struct Normal
    {
        public double x;
        public double y;
        public double z;

        public Normal(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Normal(Vector v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }

        public static Normal operator +(Normal n1, Normal n2)
        {
            return new Normal(n1.x + n2.x, n1.y + n2.y, n1.z + n2.z);
        }

        public static Normal operator -(Normal n1, Normal n2)
        {
            return new Normal(n1.x - n2.x, n1.y - n2.y, n1.z - n2.z);
        }

        public static Normal operator *(double d, Normal n)
        {
            return new Normal(d * n.x, d * n.y, d * n.z);
        }

        public static double operator *(Normal n1, Normal n2)
        {
            return n1.x * n2.x + n1.y * n2.y + n1.z * n2.z;
        }

        public static double operator *(Normal n1, Vector v2)
        {
            return n1.x * v2.x + n1.y * v2.y + n1.z * v2.z;
        }

        public static Normal operator /(Normal n, double d)
        {
            return new Normal(n.x / d, n.y / d, n.z / d);
        }

        public double LengthSquared
        {
            get { return x * x + y * y + z * z; }
        }

        public double Length
        {
            get { return Math.Sqrt(LengthSquared); }
        }

        public Normal Normalized
        {
            get { return this / Length; }
        }
    }

}

