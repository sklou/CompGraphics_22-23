using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        /*  public bool Intersects(Ray ray)
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
       */
        public (Vector? intersection, Vector? normal) Intersects(Ray ray)
        {
            Vector rayOriginToSphere = position - ray.origin;
            float tca = Vector.Dot(rayOriginToSphere, ray.direction);
            if (tca < 0)
            {
                return (null, null);
            }
            float d2 = Vector.Dot(rayOriginToSphere, rayOriginToSphere) - tca * tca;
            if (d2 > radius * radius)
            {
                return (null, null);
            }
            float thc = (float)Math.Sqrt(radius * radius - d2);
            float t0 = tca - thc;
            float t1 = tca + thc;
            if (t0 < 0)
            {
                t0 = t1;
            }
            if (t0 < 0)
            {
                return (null, null);
            }
            Vector intersectionPoint = ray.origin + t0 * ray.direction;
            Vector d = intersectionPoint - position;
            d.Normalize();
            Vector normal = d;
            return (intersectionPoint, normal);
        }

    }
}

