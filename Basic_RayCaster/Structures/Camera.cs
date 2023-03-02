using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_RayCaster.Structures
{
   
    public struct Camera
    {
        public Point Position;
        public Vector Direction;
        public double FieldOfView;

        public Camera(Point position, Vector direction, double FoV)
        {
            Position = position;
            Direction = direction;
            FieldOfView = FoV;
        }
    }
    /*  public Ray GetRay(double x, double y)
     {
         Vector rayDirection = forward + ((x - 0.5) * width * right) + ((y - 0.5) * height * up);
         return new Ray(position, rayDirection.Normalize());
     }*/

}
