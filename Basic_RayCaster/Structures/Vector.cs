using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_RayCaster.Structures
{
    public struct Vector
    {
        public double x, y, z;

        public Vector(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public double Length()
        {
            return Math.Sqrt(x * x + y * y + z * z); // Довжина вектора
        }

        public Vector Normalize()
        {
            double length = Length();
            return new Vector(x / length, y / length, z / length); // Нормалізований вектор
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z); // Сума векторів
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z); // Різниця векторів
        }

        public static Vector operator -(Vector v1, Point v2)
        {
            return new Vector(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z); // Різниця векторів
        }

        public static Vector operator *(Vector v, double scalar)
        {
            return new Vector(v.x * scalar, v.y * scalar, v.z * scalar); // Скалярний добуток
        }

        public static Vector operator *(double scalar, Vector v)
        {
            return v * scalar;
        }

        public static double Dot(Vector v1, Vector v2)
        {
            return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z; // Скалярний добуток векторів
        }

        public static Vector Cross(Vector v1, Vector v2)
        {
            return new Vector(v1.y * v2.z - v1.z * v2.y, v1.z * v2.x - v1.x * v2.z, v1.x * v2.y - v1.y * v2.x); // Векторний добуток векторів
        }

       
    }


}
