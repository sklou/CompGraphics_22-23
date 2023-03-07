using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_RayCaster.Structures
{
    struct Plane
    {
        public Vector position;
        public Vector normal;

        public Plane(Vector position, Vector normal)
        {
            this.position = position;
            this.normal = normal;
        }

        public bool Intersects(Ray ray)
        {
            float denominator = Vector.Dot(normal, ray.direction);
            if (Math.Abs(denominator) > 1e-6f)
            {
                Vector pointOnPlane = position - ray.origin;
                float t = Vector.Dot(pointOnPlane, normal) / denominator;
                if (t > 0)
                {
                    Vector intersectionPoint = ray.origin + t * ray.direction;
                    Vector relativePoint = intersectionPoint - position;
                    if (Math.Abs(relativePoint.x) <= 5f && Math.Abs(relativePoint.y) <= 5f)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
