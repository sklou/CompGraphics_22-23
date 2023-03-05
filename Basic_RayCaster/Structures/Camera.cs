using Basic_RayCaster.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_RayCaster.Structures
{
    struct Camera
    {
        public Vector position;

        public Camera(Vector position)
        {
            this.position = position;
        }

       /* public Ray GetRay_For_Pixel(int x, int y, int width, int height)
        {
           
        }*/
    }
}
