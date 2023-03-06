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

        public bool Intersects(Ray ray)
        {
            Vector rayOriginToSphere = position - ray.origin;
            float closest_point = Vector.Dot(rayOriginToSphere, ray.direction);
            if (closest_point < 0)
            {
                return false;
            }
            float d2 = Vector.Dot(rayOriginToSphere, rayOriginToSphere) - closest_point * closest_point;
            if (d2 > radius * radius)
            {
                return false;
            }
            return true;
        }
    }
}
