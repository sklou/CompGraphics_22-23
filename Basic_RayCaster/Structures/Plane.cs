using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_RayCaster.Structures
{
    
    public struct Plane
    {
        public Point point_on_plane { get; }
        public Normal Normal { get; }

        public Plane(Point pointOnPlane, Normal normal)
        {
            point_on_plane = pointOnPlane;
            Normal = normal;
        }

      /*  public double Intersect(Ray ray)
        {
         
        }
      */
    }

}
