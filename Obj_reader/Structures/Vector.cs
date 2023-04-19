using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Obj_reader.Structures
{ 
    struct Vector
    {
        public float x;
        public float y;
        public float z;
        public Vector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public static Vector Zero
        {
            get { return new Vector(0.0f, 0.0f, 0.0f); }
        }

        public static Vector ToVector(Vector? nullableVector)
        {
            if (nullableVector == null)
            {
                return Vector.Zero;
            }
            else
            {
                return nullableVector.Value;
            }
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector operator *(float a, Vector b)
        {
            return new Vector(a * b.x, a * b.y, a * b.z);
        }
        public static float Dot(Vector a, Vector b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static float Dot2(Vector? a, Vector b)
        {
            a.Value.Normalize();
            return a.Value.x * b.x + a.Value.y * b.y + a.Value.z * b.z;
        }
        public static Vector Cross(Vector a, Vector b)
        {
            return new Vector(
                a.y * b.z - a.z * b.y,
                a.z * b.x - a.x * b.z,
                a.x * b.y - a.y * b.x
            );
        }
        public static float Distance(Vector a, Vector b)
        {
            return (float)Math.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z));
        }
        public float Magnitude()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }
        public void Normalize()
        {
            float magn = Magnitude();
            x /= magn;
            y /= magn;
            z /= magn;
        }

        public static float Dot(Vector? normal, Vector l)
        {
            normal.Value.Normalize();
            Vector value = normal.Value;
            value.x = -1 * normal.Value.x;
            value.y = -1 * normal.Value.y;
            value.z = -1 * normal.Value.z;


            Vector n = (Vector)value;
            // Vector n = (Vector)normal;
            l.Normalize();
            Vector v = l;

            float dotProduct = n.x * v.x + n.y * v.y + n.z * v.z;

            return dotProduct;
        }


        public static Vector Transform(Vector vector, Matrix matrix)
        {
            if (matrix.Values.GetLength(0) != 4 || matrix.Values.GetLength(1) != 4)
            {
                throw new ArgumentException("Matrix must be a 4x4 matrix.");
            }

            float x = vector.x * matrix.Values[0, 0] + vector.y * matrix.Values[1, 0] + vector.z * matrix.Values[2, 0] + matrix.Values[3, 0];
            float y = vector.x * matrix.Values[0, 1] + vector.y * matrix.Values[1, 1] + vector.z * matrix.Values[2, 1] + matrix.Values[3, 1];
            float z = vector.x * matrix.Values[0, 2] + vector.y * matrix.Values[1, 2] + vector.z * matrix.Values[2, 2] + matrix.Values[3, 2];

            return new Vector(x, y, z);
        }

       

    }
}
