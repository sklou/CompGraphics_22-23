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
                
                Vector intersectionPoint = ray.origin + t * ray.direction;
                Vector normal = Vector.Cross(ab, ac);
                normal.Normalize();
                return (intersectionPoint, -1 * normal);
            }
            else
            {
                
                Vector intersectionPoint = ray.origin + t * ray.direction;
                Vector normal = Vector.Cross(ab, ac);
                normal.Normalize();
                return (intersectionPoint, normal);
            }
        }

        public static Triangle[] CreateTriangles(List<Tuple<int, int, int>> triangleIndices, List<Vector> vertices)
        {
            Triangle[] triangles = new Triangle[triangleIndices.Count];

            for (int i = 0; i < triangleIndices.Count; i++)
            {
                int index1 = triangleIndices[i].Item1;
                int index2 = triangleIndices[i].Item2;
                int index3 = triangleIndices[i].Item3;

                Vector vertex1 = vertices[index1];
                Vector vertex2 = vertices[index2];
                Vector vertex3 = vertices[index3];

                triangles[i] = new Triangle(vertex1, vertex2, vertex3);
            }

            return triangles;
        }

    }
}

   
