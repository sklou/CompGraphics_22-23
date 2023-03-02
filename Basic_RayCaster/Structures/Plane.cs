using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_RayCaster.Structures
{
    
    public struct Plane
    {
        public Point PointOnPlane { get; }
        public Normal Normal { get; }

        public Plane(Point pointOnPlane, Normal normal)
        {
            PointOnPlane = pointOnPlane;
            Normal = normal;
        }

        public double? Intersect(Ray ray)
        {
            double denominator = Normal.DotProduct(ray.direction);
            if (denominator == 0)
            {
                return null;
            }

            double t = Normal.DotProduct(PointOnPlane - ray.origin) / denominator;
            return t >= 0 ? t : null;
        }

        public Normal NormalAtPoint(Point point)
        {
            return Normal;
        }
    }

}
