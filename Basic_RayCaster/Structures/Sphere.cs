using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_RayCaster.Structures
{
    struct Sphere
    {
        public Vector position;
        public float radius;

        public Sphere(Vector position, float radius)
        {
            this.position = position;
            this.radius = radius;
        }

        /*public bool Intersect(Ray ray)
        {
           
        }*/
    }
}
