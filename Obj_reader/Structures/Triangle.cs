using Obj_reader.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Obj_reader.Structures
{
    struct Triangle : IShape
    {
        public Vector a;
        public Vector b;
        public Vector c;

        public Triangle(Vector a, Vector b, Vector c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
        public (Vector? intersection, Vector? normal) Intersects(Ray ray)
        {
            Vector ab = b - a;
            Vector ac = c - a;
            Vector h = Vector.Cross(ray.direction, ac);
            float det = Vector.Dot(ab, h);
            if (det > -float.Epsilon && det < float.Epsilon)
            {
                return (null, null);
            }
            float invDet = 1.0f / det;
            Vector s = ray.origin - a;
            float u = Vector.Dot(s, h) * invDet;
            if (u < 0 || u > 1)
            {
                return (null, null);
            }
            Vector q = Vector.Cross(s, ab);
            float v = Vector.Dot(ray.direction, q) * invDet;
            if (v < 0 || u + v > 1)
            {
                return (null, null);
            }
            float t = Vector.Dot(ac, q) * invDet;
            if (t < 0)
            {
                return (null, null);
            }
            Vector intersectionPoint = ray.origin + t * ray.direction;
            Vector normal = Vector.Cross(ab, ac);
            normal.Normalize();
            return (intersectionPoint, normal);
        }


    }
}

   
