using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_RayCaster.Structures
{
    public struct Plane
    {
        public Point point;
        public Normal normal;

        public Plane(Point point, Normal normal)
        {
            this.point = point;
            this.normal = normal.Normalized;
        }

        public double Intersect(Ray ray)
        {
            double denom = normal * ray.direction;      //denom = sqrt(A^2 + B^2 + C^2)
            if (denom > 0)
            {
                return double.PositiveInfinity;
            }
            else
            {
                Vector v = point - ray.origin;
                double t = normal * v / denom;
                if (t < 0)
                {
                    return double.PositiveInfinity;
                }
                else
                {
                    return t;
                }
            }
        }
    }

}
