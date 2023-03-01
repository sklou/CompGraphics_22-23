using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_RayCaster.Structures
{
    public struct Sphere
    {
        public Point center;
        public double radius;

        public Sphere(Point center, double radius)
        {
            this.center = center;
            this.radius = radius;
        }

        /*public double Intersect(Ray ray)
        {
            return
        }
        */
    }

}
